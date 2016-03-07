using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTableEntities {
    public class Scheduler : IScheduler, IDisposable {

        TimeTableEntities db = new TimeTableEntities();
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

        public ValidationModel AddSchedule(ScheduleModel scheduleModel) {

            Tuple<bool, ValidationModel> validated = Validation.ValidateSchedule(db, scheduleModel);
            if (validated.Item1) {
                Booking.FindAvailableTime(db, scheduleModel);
                UpdateSchedule(CloneSchedule());
            }
            return validated.Item2;
        }

      
       public ValidationModel EditSchedule(ScheduleModel scheduleModel) {

                Tuple<bool, ValidationModel> validated = Validation.ValidateSchedule(db, scheduleModel);
            if (validated.Item1) {
                Booking.FindAvailableTime(db, scheduleModel);
                UpdateSchedule(CloneSchedule(), true);
            }
            return validated.Item2;
        }


       private Schedule CloneSchedule() {
           var schedule = new Schedule();
           foreach (var s in ScheduleList) {
               schedule.Id = s.Id;
               schedule.Class = s.Class;
               schedule.Day = s.Day;
               schedule.Elective = s.Elective;
               schedule.Period = s.Period;
               schedule.Subject = s.Subject;
           }

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
