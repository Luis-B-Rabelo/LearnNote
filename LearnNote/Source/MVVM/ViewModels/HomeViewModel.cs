using CommunityToolkit.Mvvm.ComponentModel;

namespace LearnNote.Source.MVVM.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private string _pageName;

        public string PageName
        {
            get 
            { 
                return _pageName; 
            }
        }

        public HomeViewModel() 
        {
            _pageName = "HomePage";
        }
    }
}
