using Android.Text.Format;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnNote.Model
{
    public class Activity
    {
        #region "Propriedades"

        private uint _activityId;
        private string _name;
        private string _description;
        private byte _dayWeek;
        private TimeOnly _startTime;
        private TimeOnly _endTime;
        private bool _alarm;
        private bool _studyTime;

        #endregion

        #region "getters e setters"

        public uint ActivityId 
        {  
            get { return _activityId; } 
            set {  _activityId = value; } 
        }

        public string Name
        { 
            get { return _name; } 
            set {  _name = value; } 
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public byte DayWeek
        { 
            get { return _dayWeek; } 
            set { _dayWeek = value; } 
        }

        public TimeOnly StartTime
        { 
            get { return _startTime; } 
            set {  _startTime = value; } 
        }

        public TimeOnly EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        public bool Alarm
        {
            get { return _alarm; }
            set { _alarm = value; }
        }

        public bool StudyTime
        {
            get { return _studyTime; }
            set { _studyTime = value; }
        }

        #endregion
    }
}
