using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTableEntities {
   public class ScheduleModel {
        public int Id { get; set; }
        public string Class { get; set; }
        public string Day { get; set; }
        public string Elective { get; set; }
        public string Teacher { get; set; }
        public int Period { get; set; }
        public int PeriodPerWeek { get; set; }
        public string Subject { get; set; }
    }
}
