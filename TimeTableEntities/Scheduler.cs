using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTableEntities {
    public class Scheduler : IScheduler, IDisposable {

        TimeTableEntities db = new TimeTableEntities();

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

        public ValidationModel AddSchedule(Schedule schedule) {
            ValidationModel validation;
            var success = ValidateSchedule(schedule, out validation);
            if (success)
                UpdateSchedule(schedule);
               
            return validation;
        }                     

       public ValidationModel EditSchedule(Schedule schedule) {
             ValidationModel validation;
            var success = ValidateSchedule(schedule, out validation);
            if (success)
                UpdateSchedule(schedule, true);
               
            return validation;
        }

        private bool ValidateSchedule(Schedule schedule, out ValidationModel validation) {

            validation = new ValidationModel();
            //check that subject does not exceed number of period per week
            var schedules = (from s in db.Schedules
                             select s).ToList();

            var periodPerWeek = (from s in db.Subjects
                                 where s.Alias == schedule.Subject
                                 select s.PeriodPerWeek).FirstOrDefault();

            var reachedLimit = (from s in db.Schedules
                                where s.Subject == schedule.Subject &&
                                  s.Class == schedule.Class
                                select s.Period).Sum();

            if (reachedLimit == null || reachedLimit <= periodPerWeek) {
                validation.Id = "PeriodPerWeek"; validation.Message = "Subject has reached max period per week";
            }
            else {
                 var periodBooked = schedules.Exists(s => s.Class == schedule.Class &&
                       s.Period == schedule.Period &&
                        s.Day == schedule.Day);

                if (periodBooked) {
                    validation.Id = "Schedule"; validation.Message = "Period had been booked for this class";
                }
                else {
                    //check teacher booked for the same period in  diff class
                    var teacherBooked = schedules.Exists(s => s.Teacher == schedule.Teacher &&
                                               s.Period == schedule.Period &&
                                               s.Day == schedule.Day &&
                                               s.Class != schedule.Class);
                    if (teacherBooked) {
                        validation.Id = "Teacher"; validation.Message = "Has been scheduled for another class at this period";
                    }
                    else {
                        validation.Id = "Time table"; validation.Message = "Update successfull";
                        return true;
                    }
                }                
            }
            return false;
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
                        existing.Elective = schedule.Elective;
                        existing.Subject = e.Subject;
                        existing.Teacher = e.Teacher;
                        db.SaveChanges();
                    }
                }

                db.SaveChanges();
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
                        electiveSubject.Subject = e.Subject;
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
