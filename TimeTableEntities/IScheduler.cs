using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTableEntities {
    public interface IScheduler {

        IEnumerable<Schedule> GetSchedule();
        IEnumerable<Schedule> GetSubjectSchedule(string subject);
        IEnumerable<Schedule> GetTeacherSchedule(string teacher);
        
        ValidationModel AddSchedule(ScheduleModel schedule);
        ValidationModel EditSchedule(ScheduleModel schedule);
 
        void DeleteSchedule(Schedule schedule);
               
    }
}
