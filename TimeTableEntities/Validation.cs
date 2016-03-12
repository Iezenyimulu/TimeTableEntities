using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTableEntities {
    public class Validation {

        static List<Schedule> schedules = new List<Schedule>();
        static ValidationModel validation = new ValidationModel();


        public static Tuple<bool, ValidationModel> ValidateSchedule(TimeTableDb db, ScheduleModel schedule) {

            schedules = (from s in db.Schedules
                         select s).ToList();

            if (schedule.PeriodPerWeek > 0) {
                bool maxPeriodValSuccess = ValidateMaxPeriod(db, schedule);
                if (!maxPeriodValSuccess)
                    return Tuple.Create(false, validation);
            }
            validation.Id = "Success"; validation.Message = "validation successfully";
            return Tuple.Create(true, validation);

        }


        private static bool ValidateMaxPeriod(TimeTableDb db, ScheduleModel schedule) {

            //check that subject does not exceed number of period per week           

            if (schedule.PeriodPerWeek > 0) {
                var level = schedule.Class.Substring(0, 3);

                switch (level.ToUpper()) {
                    case "SS2":
                    case "SS3":
                        level = "SS"; break;
                }

                var category = (from c in db.Classes
                               where c.Name == schedule.Class
                               select c.Category).FirstOrDefault().ToLower();

                string subject = "";
                ValidateSubject(db, schedule, out subject);

                if (String.IsNullOrEmpty(subject)) {
                    validation.Id = "Subject"; validation.Message = "Does not exist. Check that you entered the right name";
                    return false;
                }


                var maxPeriodPerWeek = (from c in db.ClassSubjects
                                        where c.Subject.ToLower() == subject.ToLower() &&
                                           c.Level == level &&
                                           c.Category.ToLower() == category
                                        select c.PeriodPerWeek).FirstOrDefault();

                var bookedPeriod = 0;
                if(schedules.Where( s => s.Subject.ToLower()== schedule.Subject.ToLower() &&
                                      s.Class == schedule.Class).Count() > 0)
                   bookedPeriod = (from s in db.Schedules
                                   where s.Subject.ToLower() == schedule.Subject.ToLower() &&
                                      s.Class == schedule.Class
                                    select s.Period??0).Sum();

                var unbookedPeriod = maxPeriodPerWeek - bookedPeriod;

                if (  bookedPeriod == maxPeriodPerWeek) {
                    validation.Id = "PeriodPerWeek"; validation.Message = "Subject has reached max period per week";
                    return false;
                }
                else
                    if (schedule.PeriodPerWeek > 1 && unbookedPeriod < schedule.PeriodPerWeek) {
                        validation.Id = "PeriodPerWeek"; validation.Message = String.Format("{0} cannot be booked for this Subject. Number period left to be booked {1}",
                            schedule.PeriodPerWeek, unbookedPeriod);
                        return false;
                    }
            }

            return true;
        }


        public static bool ValidatePeriod(TimeTableDb db, ScheduleModel schedule) {
            //specific period by user        
            var periodBooked = schedules.Exists(s => s.Class.ToLower() == schedule.Class.ToLower() &&
                  s.Period == schedule.Period &&
                   s.Day.ToLower() == schedule.Day.ToLower());

            if (periodBooked) 
                //validation.Id = "Period"; validation.Message = "Specified period had been booked for this class";
                return false;
            

            return true;
        }

        public static string ValidateSubject(TimeTableDb db, ScheduleModel schedule, out string subject) {

            subject = (from s in db.Subjects
                       where s.Alias.ToLower() == schedule.Subject.ToLower()
                       select s.Name).FirstOrDefault();

            if (String.IsNullOrEmpty(subject)) {

                var subjectExist = db.Subjects.ToList().Exists(s => s.Name.ToLower() == schedule.Subject.ToLower());
                if (subjectExist) {
                    schedule.Subject = (from s in db.Subjects
                                        where s.Name.ToLower() == schedule.Subject.ToLower()
                                        select s.Alias).FirstOrDefault();
                    subject = schedule.Subject;
                }
            }
                return subject;

        }


        public static bool ValidateTeacher(TimeTableDb db, ScheduleModel schedule) {
            //check teacher booked for the same period in  diff class
            var teacherBooked = schedules.Exists(s => s.Teacher.ToLower() == schedule.Teacher.ToLower() &&
                                       s.Period == schedule.Period &&
                                       s.Day.ToLower() == schedule.Day.ToLower() &&
                                      (s.Class.ToLower() != schedule.Class .ToLower()&&s.Class.ToLower() == schedule.Class.ToLower()));
            if (teacherBooked) {
                //validation.Id = "Teacher"; validation.Message = "Has been scheduled for this period";
                return false;
            }
            return true;
        }

        public static bool ValidateSubject(TimeTableDb db, ScheduleModel schedule) {
            //check teacher booked for the same period in  diff class
            var subjectBooked = (from s in schedules
                                 where  s.Teacher.ToLower() == schedule.Teacher.ToLower() &&
                                       s.Day == schedule.Day &&
                                       s.Subject.ToLower() == schedule.Subject.ToLower() &&
                                       s.Class.ToLower() == schedule.Class.ToLower()
                                 select s).ToList().Count();
            if(subjectBooked <= 0)
            subjectBooked = (from s in Scheduler.ScheduleList
                             where s.Teacher.ToLower() == schedule.Teacher.ToLower() &&
                                   s.Day.ToLower() == schedule.Day.ToLower() &&
                                   s.Subject.ToLower() == schedule.Subject.ToLower() &&
                                   s.Class.ToLower() == schedule.Class.ToLower()
                             select s).ToList().Count();

            
            if (subjectBooked >= 2) {
                //validation.Id = "Subject"; validation.Message = String.Format("Has been scheduled for this class on specified {0}", schedule.Day);
                return false;
            }
            return true;
        }

    }
}
