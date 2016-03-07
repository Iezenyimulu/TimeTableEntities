using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTableEntities {
    public class Validation {

        //const string[] DAYS ={ "Mon", "Tues", "Wed", "Thur", "Fri"};
        //const int[] PERIODS = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        // Dictionary<string, List<int>> singlePeriod ;
        // Dictionary<string, List<int>> doublePeriods;
        static List<Schedule> schedules = new List<Schedule>();
        static ValidationModel validation = new ValidationModel();


        public static Tuple<bool, ValidationModel> ValidateSchedule(TimeTableEntities db, ScheduleModel schedule) {

            schedules = (from s in db.Schedules
                         select s).ToList();

            if (schedule.PeriodPerWeek > 0) {
                bool maxPeriodValSuccess = ValidateMaxPeriod(db, schedule);
                if (!maxPeriodValSuccess)
                    return Tuple.Create(false, validation);
            }
            validation.Id = "Schedule"; validation.Message = "Booked successfully";
            return Tuple.Create(true, validation);

        }


        private static bool ValidateMaxPeriod(TimeTableEntities db, ScheduleModel schedule) {

            //check that subject does not exceed number of period per week           

            if (schedule.PeriodPerWeek > 0) {

                var maxPeriodPerWeek = (from s in db.Subjects
                                        where s.Alias == schedule.Subject
                                        select s.PeriodPerWeek).FirstOrDefault();

                var bookedPeriod = (from s in db.Schedules
                                    where s.Subject == schedule.Subject &&
                                      s.Class == schedule.Class
                                    select s.Period).Sum();

                var unbookedPeriod = maxPeriodPerWeek - bookedPeriod;

                if (bookedPeriod == null || bookedPeriod <= maxPeriodPerWeek) {
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


        private bool ValidateSpecifiedPeriod(TimeTableEntities db, ScheduleModel schedule) {
            //specific period by user        
            var periodBooked = schedules.Exists(s => s.Class == schedule.Class &&
                  s.Period == schedule.Period &&
                   s.Day == schedule.Day);

            if (periodBooked) {
                validation.Id = "Period"; validation.Message = "Specified period had been booked for this class";
                return false;
            }

            return true;
        }





        public static bool ValidateTeacher(TimeTableEntities db, ScheduleModel schedule) {
            //check teacher booked for the same period in  diff class
            var teacherBooked = schedules.Exists(s => s.Teacher == schedule.Teacher &&
                                       s.Period == schedule.Period &&
                                       s.Day == schedule.Day &&
                                      (s.Class != schedule.Class && s.Class == schedule.Class));
            if (teacherBooked) {
                validation.Id = "Teacher"; validation.Message = "Has been scheduled for this period";
                return false;
            }
            return true;
        }

    }
}
