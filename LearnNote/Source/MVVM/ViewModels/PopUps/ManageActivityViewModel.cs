using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnNote.Model;
using LearnNote.Source.DAO;
using LearnNote.Source.MVVM.Views;
using System.ComponentModel;

namespace LearnNote.Source.MVVM.ViewModels.PopUps
{
    public partial class ManageActivityViewModel : ObservableObject, INotifyPropertyChanged
    {
        #region Properties
        public event PropertyChangedEventHandler? PropertyChanged;

        private ActivityModel _activity;

        private string _name;

        private string _description;

        #endregion

        #region Getters & Setters
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                _activity.Name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                _activity.Description = value;
                OnPropertyChanged();
            }
        }

        public ActivityModel Activity
        {
            get => _activity;
            set
            {
                _activity = value;
                Name = _activity.Name;
                Description = _activity.Description;
                GlobalFunctionalities.Logger.Debug("Atividade setada");
                OnPropertyChanged();
            }
        }

        public uint UserIdFk { get; set; }

        #endregion

        public ManageActivityViewModel(ActivityModel activity, uint userId)
        {
            Activity = activity;
            UserIdFk = userId;
        }

        [RelayCommand]
        public void SaveActivity(Popup popup)
        {
            if(ActivityDAO.CheckIfActivityExist(_activity.ActivityId))
            {
                ActivityDAO.UpdateActivity(Activity);
                Shell.Current.GoToAsync($"{nameof(PlannerPage)}?PassUserId={UserIdFk}");
                popup.Close();
            }
            else
            {
                ActivityDAO.CreateActivity(Activity);
                Shell.Current.GoToAsync($"{nameof(PlannerPage)}?PassUserId={UserIdFk}");
                popup.Close();
            }
        }
    }
}
