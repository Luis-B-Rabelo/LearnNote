using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnNote.Source.MVVM.Views;

namespace LearnNote.Source.Core
{
    public partial class NavBar : ObservableObject
    {

        protected uint _userId;

        protected uint UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged("NavBar UserId");
            }
        }

        [RelayCommand]
        protected async Task NavigateToConfigs()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(ConfigsPage));
            }
            catch (Exception ex)
            {

            }
        }

        [RelayCommand]
        protected async Task NavigateToMyNotebooks()
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(MyNotebooksPage)}?PassUserId={UserId}");
            }
            catch (Exception ex)
            {

            }
        }

        [RelayCommand]
        protected async Task NavigateToPlanner()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(PlannerPage));
            }
            catch (Exception ex)
            {

            }
        }

        [RelayCommand]
        protected async Task NavigateToCalendar()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(CalendarPage));
            }
            catch (Exception ex)
            {

            }
        }

        [RelayCommand]
        protected async Task NavigateToHome()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(HomePage));
            }
            catch (Exception ex)
            {

            }
        }

        [RelayCommand]
        protected async Task NavigateToAlarmPage()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(AlarmPage));
            }
            catch (Exception ex)
            {

            }
        }

    }
}
