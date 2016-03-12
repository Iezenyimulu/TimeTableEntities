using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTableEntities {
    public class Booking {

        static readonly string[] DAYS = { "Mon", "Tues", "Wed", "Thur", "Fri" };
        static readonly int[] PERIODS = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        static Dictionary<string, List<int>> singlePeriod;
        static Dictionary<string, List<int>> doublePeriods;
        static List<Schedule> schedules = new List<Schedule>();

        public static void FindAvailableTime(TimeTableDb db, ScheduleModel schedule) {
           
                var onePeriod = schedule.PeriodPerWeek % 2;
                Dictionary<string, List<int>> vacancy = new Dictionary<string, List<int>>();

                var temp = new List<int>();

                foreach (var day in DAYS) {
                    foreach (var period in PERIODS) {
                        var booked = schedules.Exists(s => s.Class == schedule.Class &&
                                       s.Day == day &&
                                       s.Period == period);
                        if (!booked)
                            temp.Add(period);
                    }
                    if (temp.Count > 0) {
                        temp.Sort();
                        vacancy.Add(day, new List<int>(temp));
                        temp.Clear();
                    }
                }
                GroupPeriodsToSchedule(vacancy, out singlePeriod, out doublePeriods);
                FindOptimumTime(db,schedule);
            //}
        }

        private static void GroupPeriodsToSchedule(Dictionary<string, List<int>> vacancy, out Dictionary<string, List<int>> singlePeriod, out Dictionary<string, List<int>> doublePeriods) {
            doublePeriods = new Dictionary<string, List<int>>();
            singlePeriod = new Dictionary<string, List<int>>();
            var tempD = new List<int>();
            var tempS = new List<int>();



            foreach (var day in vacancy) {
                foreach (var period in day.Value) {
                    var first = period;
                    //subsequent period (+1)found means a second  is available. first period is added to double period
                    if (day.Value.Contains(first + 1))
                        tempD.Add(first);
                    //any period is added as/to single period
                    tempS.Add(first);
                }
                if (tempD.Count > 0) {
                    tempD.Sort();
                    doublePeriods.Add(day.Key, new List<int>(tempD));
                }
                if (tempS.Count > 0) {
                    tempS.Sort();
                    singlePeriod.Add(day.Key, new List<int>(tempS));
                }

                tempS.Clear();
                tempD.Clear();
            }
        }


        private static void FindOptimumTime(TimeTableDb db, ScheduleModel schedule) {
            var scheduled = false;
            
            var numberofDoublePeriods = 0;
            int onePeriod = schedule.PeriodPerWeek % 2;
            var periodPerWeek = schedule.PeriodPerWeek;
            int k = schedule.PeriodPerWeek  ;
            while (k  > 1) {
                numberofDoublePeriods++;
                k /= 2;
            }
            bool isScience = ((from s in db.Subjects
                               where s.Alias == schedule.Subject
                               select s.IsScience).FirstOrDefault()) ?? false;

            if (numberofDoublePeriods > 0) {
                for (var i = numberofDoublePeriods; i > 0; i--) {
                    scheduled = false;
                    foreach (var item in doublePeriods) {
                        if (scheduled)
                            break;
                        var periodList = (item.Value);
                        if (!isScience)
                            periodList.Reverse();
                        foreach (var period in periodList) {
                            schedule.Day = item.Key;
                           
                            scheduled  = BookDoublePeriods(db, schedule, period);
                            if (scheduled) {
                                item.Value.Remove(period);
                                break;
                            }
                        }
                    }
                }
            }
            if (onePeriod > 0 || schedule.PeriodPerWeek == 1) {
                scheduled = false;
                foreach (var item in singlePeriod) {
                    var periodList = (item.Value);
                    if (!isScience)
                        periodList.Reverse();
                    foreach (var period in periodList) {                       
                        schedule.Day = item.Key;

                        scheduled = BookSinglePeriod(db, schedule, period);
                        if (scheduled) {
                            break;
                        }
                    }
                    if (scheduled)
                        break;
                }
            }


        }

        private static bool BookDoublePeriods(TimeTableDb db, ScheduleModel schedule, int period) {
            var scheduled = false;

                    var first = period;
                    var second = period + 1;
                    schedule.Period = first;
            //validate subject. validate first and second periods and teacher for those  period
                    if (Validation.ValidateSubject(db, schedule)) {
                        if (Validation.ValidatePeriod(db, schedule)) {
                            if (Validation.ValidateTeacher(db, schedule)) {

                                schedule.Period = second;
                                if (Validation.ValidatePeriod(db, schedule)) {
                                    if (Validation.ValidateTeacher(db, schedule)) {
                                        schedule.Period = first;
                                        AddScheduleToList(
                                             new ScheduleModel {
                                                 Class = schedule.Class,
                                                 Teacher = schedule.Teacher,
                                                 Day = schedule.Day,
                                                 Elective = schedule.Elective,
                                                 Subject = schedule.Subject,
                                                 Period = first
                                             });
                                        //second period
                                        AddScheduleToList(
                                            new ScheduleModel {
                                                Class = schedule.Class,
                                                Teacher = schedule.Teacher,
                                                Day = schedule.Day,
                                                Elective = schedule.Elective,
                                                Subject = schedule.Subject,
                                                Period = second
                                            });
                                        scheduled = true;
                                    }
                                }
                            }
                        }
                    }
          
            return scheduled;
        }


        private static bool BookSinglePeriod(TimeTableDb db, ScheduleModel schedule, int period) {
            var scheduled = false;

                    schedule.Period = period;
                    if (Validation.ValidateSubject(db, schedule)) {
                        if (Validation.ValidatePeriod(db, schedule)) {
                            if (Validation.ValidateTeacher(db, schedule)) {
                                AddScheduleToList(new ScheduleModel {
                                    Class = schedule.Class,
                                    Teacher = schedule.Teacher,
                                    Day = schedule.Day,
                                    Elective = schedule.Elective,
                                    Subject = schedule.Subject,
                                    Period = period
                                });
                                scheduled = true;
                            }
                        }
                    }
                    return scheduled;
        }


        private static void AddScheduleToList(ScheduleModel schedule) {
            Scheduler.ScheduleList.Add(schedule);
        }




    }
}
