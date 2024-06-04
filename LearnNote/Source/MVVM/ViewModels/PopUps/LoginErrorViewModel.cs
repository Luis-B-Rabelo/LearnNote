
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearnNote.Source.DAO;
using LearnNote.Source.MVVM.Views;
using System.Collections.Specialized;
using System.ComponentModel;

namespace LearnNote.Source.MVVM.ViewModels.PopUps
{
    public partial class LoginErrorViewModel : ObservableObject, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        #region Properties
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _error;

        #endregion

        #region Getters & Setters
        public string Error
        {
            get => _error;
            set
            {
                _error = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
