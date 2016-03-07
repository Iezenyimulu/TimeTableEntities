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

        public static void FindAvailableTime(TimeTableEntities db, ScheduleModel schedule) {
            //if (schedule.PeriodPerWeek > 1) {
                var onePeriod = schedule.PeriodPerWeek % 2;
                //Dictionary<int, Tuple<string, string, string, string, string>> vacancy = new Dictionary<int, Tuple<string, string, string, string, string>>();
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
                        vacancy.Add(day, temp);
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
                    //subsequent period found means the first period is added to double period
                    if (day.Value.Contains(first + 1))
                        tempD.Add(first);
                    //if (onePeriod) 
                    tempS.Add(first);
                }
                if (tempD.Count > 0) {
                    tempD.Sort();
                    doublePeriods.Add(day.Key, tempD);
                }
                if (tempS.Count > 0) {
                    tempS.Sort();
                    singlePeriod.Add(day.Key, tempS);
                }

                tempS.Clear();
                tempD.Clear();
            }
        }


        private static void FindOptimumTime(TimeTableEntities db, ScheduleModel schedule) {
            var numberofDoublePeriods = 0;
            var onePeriod = schedule.PeriodPerWeek % 2;
            var periodPerWeek = schedule.PeriodPerWeek;
            while (schedule.PeriodPerWeek / 2 > 1) {
                numberofDoublePeriods++;
            }
            //onePeriod > 0 || schedule.PeriodPerWeek == 1
            bool isScience = ((from s in db.Subjects
                               where s.Alias == schedule.Subject
                               select s.IsScience).FirstOrDefault()) ?? false;

            if (numberofDoublePeriods > 0) {
                for (var i = 0; i < numberofDoublePeriods; i++) {
                    foreach (var item in doublePeriods) {
                        var periodList = (item.Value);
                        if (!isScience)
                            periodList.Reverse();
                        foreach (var period in periodList) {
                            schedule.Day = item.Key;

                            BookDoublePeriods(db, schedule, period);
                        }
                    }
                }
            }
            if (onePeriod > 0 || schedule.PeriodPerWeek == 1) {
                foreach (var item in doublePeriods) {
                    var periodList = (item.Value);
                    if (!isScience)
                        periodList.Reverse();
                    foreach (var period in periodList) {
                        schedule.Day = item.Key;

                        BookSinglePeriod(db, schedule, period);
                    }
                }
            }


        }

        private static void BookDoublePeriods(TimeTableEntities db, ScheduleModel schedule, int period) {

            switch (period) {
                case 1:
                    schedule.Period = period;
                    if (Validation.ValidateTeacher(db, schedule)) {
                        schedule.Period = period + 1;
                        if (Validation.ValidateTeacher(db, schedule)) {
                            schedule.Period = period;
                            AddScheduleToList(schedule);
                            schedule.Period = period + 1;
                            AddScheduleToList(schedule);
                        }
                    }
                    break;
                case 2:
                    schedule.Period = period;
                    if (Validation.ValidateTeacher(db, schedule)) {
                        schedule.Period = period + 1;
                        if (Validation.ValidateTeacher(db, schedule)) {
                            schedule.Period = period;
                            AddScheduleToList(schedule);
                            schedule.Period = period + 1;
                            AddScheduleToList(schedule);
                        }
                    }
                    break;

                case 3:
                    schedule.Period = period;
                    if (Validation.ValidateTeacher(db, schedule)) {
                        schedule.Period = period + 1;
                        if (Validation.ValidateTeacher(db, schedule)) {
                            schedule.Period = period;
                            AddScheduleToList(schedule);
                            schedule.Period = period + 1;
                            AddScheduleToList(schedule);
                        }
                    }
                    break;
                case 4:
                    schedule.Period = period;
                    if (Validation.ValidateTeacher(db, schedule)) {
                        schedule.Period = period + 1;
                        if (Validation.ValidateTeacher(db, schedule)) {
                            schedule.Period = period;
                            AddScheduleToList(schedule);
                            schedule.Period = period + 1;
                            AddScheduleToList(schedule);
                        }
                    }
                    break;
                case 5:
                    schedule.Period = period;
                    if (Validation.ValidateTeacher(db, schedule)) {
                        schedule.Period = period + 1;
                        if (Validation.ValidateTeacher(db, schedule)) {
                            schedule.Period = period;
                            AddScheduleToList(schedule);
                            schedule.Period = period + 1;
                            AddScheduleToList(schedule);
                        }
                    }
                    break;
                case 7:
                    schedule.Period = period;
                    if (Validation.ValidateTeacher(db, schedule)) {
                        schedule.Period = period + 1;
                        if (Validation.ValidateTeacher(db, schedule)) {
                            schedule.Period = period;
                            AddScheduleToList(schedule);
                            schedule.Period = period + 1;
                            AddScheduleToList(schedule);
                        }
                    }
                    break;

                case 8:
                    schedule.Period = period;
                    if (Validation.ValidateTeacher(db, schedule)) {
                        schedule.Period = period + 1;
                        if (Validation.ValidateTeacher(db, schedule)) {
                            schedule.Period = period;
                            AddScheduleToList(schedule);
                            schedule.Period = period + 1;
                            AddScheduleToList(schedule);
                        }
                    }
                    break;
                case 9:
                    schedule.Period = period;
                    if (Validation.ValidateTeacher(db, schedule)) {
                        schedule.Period = period + 1;
                        if (Validation.ValidateTeacher(db, schedule)) {
                            schedule.Period = period;
                            AddScheduleToList(schedule);
                            schedule.Period = period + 1;
                            AddScheduleToList(schedule);
                        }
                    }
                    break;

            }
        }


        private static void BookSinglePeriod(TimeTableEntities db, ScheduleModel schedule, int period) {

            switch (period) {
                case 1:
                    schedule.Period = period;
                    if (Validation.ValidateTeacher(db, schedule))
                        AddScheduleToList(schedule);
                    break;
                case 2:
                    schedule.Period = period;
                    if (Validation.ValidateTeacher(db, schedule))
                        AddScheduleToList(schedule);
                    break;

                case 3:
                    schedule.Period = period;
                    if (Validation.ValidateTeacher(db, schedule))
                        AddScheduleToList(schedule);
                    break;
                case 4:
                    schedule.Period = period;
                    if (Validation.ValidateTeacher(db, schedule))
                        AddScheduleToList(schedule);
                    break;
                case 5:
                    schedule.Period = period;
                    if (Validation.ValidateTeacher(db, schedule))
                        AddScheduleToList(schedule);
                    break;
                case 7:
                    schedule.Period = period;
                    if (Validation.ValidateTeacher(db, schedule))
                        AddScheduleToList(schedule);
                    break;

                case 8:
                    schedule.Period = period;
                    if (Validation.ValidateTeacher(db, schedule))
                        AddScheduleToList(schedule);
                    break;
                case 9:
                    schedule.Period = period;
                    if (Validation.ValidateTeacher(db, schedule))
                        AddScheduleToList(schedule);
                    break;
            }
        }


        private static void AddScheduleToList(ScheduleModel schedule) {
            Scheduler.ScheduleList.Add(schedule);
        }




    }
}
