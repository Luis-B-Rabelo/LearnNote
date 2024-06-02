using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using LearnNote.Model;
using LearnNote.Source.Core;
using LearnNote.Source.DAO;
using LearnNote.Source.MVVM.ViewModels.PopUps;
using LearnNote.Source.MVVM.Views;
using NLog;
using System.Collections.ObjectModel;

namespace LearnNote.Source.MVVM.ViewModels
{
    public partial class MyNotebooksViewModel : NavBar, IQueryAttributable
    {
        private readonly IPopupService _popupService;

        private ObservableCollection<NotebookModel> _notebooks;

        public ObservableCollection<NotebookModel> Notebooks
        {
            get => _notebooks;
            set
            {
                _notebooks = value;
                OnPropertyChanged("Notebooks");
            }
        }

        public uint NotebookId { get; private set; }
        public string Title { get; private set; }

        public ushort QuantityNotes { get; private set; }

        public MyNotebooksViewModel(IPopupService popupService)
        {
            _popupService = popupService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("PassUserId"))
            {
                UserId = uint.Parse(query["PassUserId"].ToString());
                Notebooks = NotebookDAO.SelectUserNotebooks(UserId);

#if DEBUG
                GlobalFunctionalities.Logger.ForDebugEvent()
                    .Message("Parametro passado e puxando cadernos")
                    .Property("PassUserId", UserId)
                    .Property("Cadernos", _notebooks)
                    .Log();
#endif

            }
        }

        [RelayCommand]
        public async Task OpenNotebook(uint notebookId)
        {
            await Shell.Current.GoToAsync($"{nameof(NotebookPage)}?PassNotebookId={notebookId}");
        }
        

        [RelayCommand]
        public async Task DisplayAddNotebookPopUp()
        {
            _popupService.ShowPopup<AddNotebookViewModel>(onPresenting: viewModel => viewModel.UserIdFk = UserId);
        }
    }
}
