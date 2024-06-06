namespace LearnNote.Model
{
    public class ActivityModel
    {
        #region Properties

        private uint _activityId;
        private string _name;
        private string _description;
        private byte _dayWeek;
        private byte _activityNum;
        private uint _userIdFk;

        #endregion

        #region Getters & Setters

        public ActivityModel Activity
        {
            get => this; 
        }

        public uint ActivityId
        {
            get => _activityId;
            set => _activityId = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public byte DayWeek
        {
            get => _dayWeek;
            set => _dayWeek = value;
        }

        public byte ActivityNum
        {
            get => _activityNum;
            set => _activityNum = value;
        }

        public uint UserIdFk
        {
            get => _userIdFk;
            set => _userIdFk = value;
        }



        #endregion

        #region Methods
        public ActivityModel NewActivity(uint activityId, uint userIdFk, byte dayWeek, byte activityNum)
        {
            _activityId = activityId;
            _name = string.Empty;
            _description = string.Empty;
            _dayWeek = dayWeek;
            _activityNum = activityNum;
            _userIdFk = userIdFk;

            return this;
        }
        #endregion
    }
}
