using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnNote.Model
{
    public class User
    {
        #region "Propriedades"

        private uint _userId;
        private string _email;
        private string _name;

        #endregion

        #region "getters e setters"

        public uint UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string Email
        { 
            get { return _email; } 
            set {  _email = value; } 
        }

        public string Name
        { 
            get { return _name; } 
            set { _name = value; } 
        }

        #endregion
    }
}
