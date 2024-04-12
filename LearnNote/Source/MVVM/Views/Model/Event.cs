using Java.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnNote.Model
{
    public class Event
    {
        private uint eventId;
        private string name;
        private string description;
        private DateOnly date;
        private TimeOnly time;
    }
}
