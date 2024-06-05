using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnNote.Source.DAO;
using LearnNote.Source.MVVM.Views;
using NLog;
using System.ComponentModel;

namespace LearnNote.Source.MVVM.ViewModels.PopUps
{
    public partial class AddNotebookViewModel : ObservableObject, INotifyPropertyChanged
    {
        #region Properties
        public event PropertyChangedEventHandler? PropertyChanged;

        private string? _title;

        private uint _userIdFk;

        #endregion

        #region Getters & Setters
        public string? Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public uint UserIdFk
        {
            get => _userIdFk;
            set
            {
                _userIdFk = value;
                OnPropertyChanged();
            }
        }

        #endregion

        [RelayCommand]
        public async Task AddNotebook(Popup popup)
        {
            uint notebookId;
            notebookId = NotebookDAO.CreateNotebook(Title, UserIdFk);

#if DEBUG
            GlobalFunctionalities.Logger.ForDebugEvent()
                .Message("Adicionando caderno")
                .Property("NotebookId", notebookId)
                .Log();
#endif

            if (notebookId != 0)
            {
                await Shell.Current.GoToAsync($"{nameof(NotebookPage)}?PassNotebookId={notebookId}");
                popup.Close();
            }
            else
            {
                await Shell.Current.GoToAsync(nameof(MyNotebooksPage));
                popup.Close();
            }
        }
    }
}
