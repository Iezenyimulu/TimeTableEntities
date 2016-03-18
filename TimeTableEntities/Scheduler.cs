using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTableEntities {
    public class Scheduler : IScheduler, IDisposable {

        TimeTableDb db = new TimeTableDb();
        public static List<ScheduleModel> ScheduleList= new List<ScheduleModel>() ;

       public IEnumerable<Schedule> GetSchedule() {
            var schedules = (from s in db.Schedules
                             select s).ToList();
            return schedules;
        }

        public IEnumerable<Schedule> GetSubjectSchedule(string subject) {
            var subjectSchedule = (from s in db.Schedules
                                   where s.Subject == subject
                                   select s).ToList();
            return subjectSchedule;
        }

        public IEnumerable<Schedule> GetTeacherSchedule(string name) {
            var teacherSchedule = (from s in db.Schedules
                                   where s.Teacher == name
                                   select s).ToList();
            return teacherSchedule;
        }


        public ScheduleModel GetSchedule(int id) {
            if (id > 0) {
                var schedule = (from s in db.Schedules
                                where s.Id == id
                                select s).FirstOrDefault();

                var level = "";
                if (schedule.Class.Substring(0, 1).ToUpper() == "S") {
                    level = schedule.Class.Substring(0, 3);

                    if (level.ToUpper() == "SS2" || level.ToUpper() == "SS3")
                        level = "SS";
                }
                else if (schedule.Class.Substring(0, 1).ToUpper() == "J")
                    level = "JS";

                var category = (from c in db.Classes
                                where c.Name.ToLower() == schedule.Class.ToLower()
                                select c.Category).FirstOrDefault();

                string subject = (from s in db.Subjects
                                  where s.Alias == schedule.Subject
                                  select s.Name).FirstOrDefault();


                var periodPerWeek = (from c in db.ClassSubjects
                                     where c.Subject.ToLower() == subject.ToLower() &&
                                        c.Level == level &&
                                        c.Category.ToLower() == category.ToLower()
                                     select c.PeriodPerWeek).FirstOrDefault();

                return new ScheduleModel {
                    Class = schedule.Class,
                    Teacher = schedule.Teacher,
                    Elective = schedule.Elective,
                    Day = schedule.Day,
                    Subject = schedule.Subject,
                    PeriodPerWeek = periodPerWeek ?? 0
                };
            }

            return new ScheduleModel();

        }


        public ValidationModel AddSchedule(ScheduleModel scheduleModel) {

            Tuple<bool, ValidationModel> validated = Validation.ValidateSchedule(db, scheduleModel);
            if (validated.Item1) {
                Booking.FindAvailableTime(db, scheduleModel);
                foreach (var s in ScheduleList) {
                    UpdateSchedule(CloneSchedule(s));
                }

                db.SaveChanges();
                ScheduleList.Clear();
            }
            return validated.Item2;
        }

      
       public ValidationModel EditSchedule(ScheduleModel scheduleModel) {

                Tuple<bool, ValidationModel> validated = Validation.ValidateSchedule(db, scheduleModel);
            if (validated.Item1) {
                Booking.FindAvailableTime(db, scheduleModel);
                UpdateSchedule(CloneSchedule(scheduleModel), true);

                db.SaveChanges();
            }
            return validated.Item2;
        }


       private Schedule CloneSchedule(ScheduleModel s) {
           var schedule = new Schedule();
          
               schedule.Id = s.Id;
               schedule.Class = s.Class;
               schedule.Day = s.Day;
               schedule.Elective = s.Elective;
               schedule.Period = s.Period;
               schedule.Subject = s.Subject;
               schedule.Teacher = s.Teacher;
           

           return schedule;
       }

       private void UpdateSchedule(Schedule schedule, bool isEdit = false) {
           // modify existing schedule
           if (isEdit) {
               var existing = db.Schedules.Where(s => s.Id == schedule.Id).First();
               existing.Class = schedule.Class;
               existing.Day = schedule.Day;
               existing.Period = schedule.Period;
               existing.Subject = schedule.Subject;
               existing.Teacher = schedule.Teacher;

               if (!String.IsNullOrWhiteSpace(schedule.Elective)) {
                   var electives = (from e in db.Electives
                                    where e.Code == schedule.Elective
                                    select e).ToList();
                   foreach (var e in electives) {
                       //existing.Elective = schedule.Elective;
                       //existing.Subject = e.Subject;
                       existing.Teacher = e.Teacher;
                   }
               }
           }
           // add new schedule
           else {
               if (!String.IsNullOrWhiteSpace(schedule.Elective)) {
                   var electives = (from e in db.Electives
                                    where e.Code == schedule.Elective
                                    select e).ToList();
                   foreach (var e in electives) {
                       Schedule electiveSubject = new Schedule();
                       electiveSubject.Class = schedule.Class;
                       electiveSubject.Day = schedule.Day;
                       electiveSubject.Elective = schedule.Elective;
                       electiveSubject.Period = schedule.Period;
                       electiveSubject.Subject = schedule.Subject; //e.Subject;
                       electiveSubject.Teacher = e.Teacher;
                       db.Schedules.AddObject(electiveSubject);
                   }
               }
               else
                   db.Schedules.AddObject(schedule);               
           }
       }


        public void DeleteSchedule(Schedule schedule) {
            var existing = db.Schedules.Where(s => s.Id == schedule.Id).First();
            db.Schedules.DeleteObject(existing);
        }

        public void Dispose() {
            db.Dispose();
            // Dispose of unmanaged resources.
            //Dispose(true);
            //// Suppress finalization.
            //GC.SuppressFinalize(this);
        }
    }
}
