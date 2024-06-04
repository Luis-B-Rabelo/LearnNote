using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace LearnNote.Source.MVVM.ViewModels.PopUps
{
    public partial class DeleteNoteViewModel : ObservableObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
