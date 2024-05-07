using LearnNote.Source.MVVM.ViewModels;


namespace LearnNote.Source.MVVM.Views;

public partial class StartPage : ContentPage
{
    public StartPage(StartViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}