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
        private uint activityId;
        private string name;
        private string description;
        private byte dayWeek;
        private TimeOnly startTime;
        private TimeOnly endTime;
        private bool alarm;
        private bool studyTime;
    }
}
