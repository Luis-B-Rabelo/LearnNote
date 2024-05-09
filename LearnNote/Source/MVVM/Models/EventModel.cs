namespace LearnNote.Model
{
    public class EventModel
    {
        #region Properties

        private uint _eventId;
        private string _name;
        private string _description;
        private DateOnly _date;
        private TimeOnly _time;

        #endregion

        #region Getters & Setters

        public uint EventId 
        { 
            get { return _eventId; } 
            set { _eventId = value; } 
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public DateOnly Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public TimeOnly Time
        {
            get { return _time; }
            set { _time = value; }
        }
        #endregion
    }
}
