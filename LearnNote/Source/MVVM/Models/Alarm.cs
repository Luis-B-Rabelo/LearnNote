using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnNote.Model
{
    public class Alarm
    {
        #region "Propriedades"

        protected uint _alarmId;
        protected TimeOnly _startTime;
        protected TimeOnly _endTime;

        #endregion

        #region "getters e setters"

        public uint AlarmId
        {
            get { return _alarmId; }
            set { _alarmId = value; }
        }

        public TimeOnly StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        public TimeOnly EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        #endregion
    }
}
