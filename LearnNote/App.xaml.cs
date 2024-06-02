﻿namespace LearnNote
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            window.MinimumWidth = 1280;
            window.MinimumHeight = 720;

            return window;
        }
    }
}
