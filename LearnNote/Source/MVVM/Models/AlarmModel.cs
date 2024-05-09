namespace LearnNote.Model
{
    public class AlarmModel
    {
        #region Properties

        protected uint _alarmId;
        protected TimeOnly _startTime;
        protected TimeOnly _endTime;

        #endregion

        #region Getters & Setters

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
