namespace LearnNote.Model
{
    public class UserModel
    {
        #region Properties

        private uint _userId;
        private string _email;
        private string _userName;

        #endregion

        #region Getters & Setters

        public uint UserId
        {
            get => _userId;
            set => _userId = value;
        }

        public string Email
        { 
            get => _email;
            set => _email = value; 
        }

        public string UserName
        {
            get => _userName;
            set => _userName = value;
        }

        #endregion
    }
}
