using System;
using System.Collections.Generic;
using LearnNote.Source.Core;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LearnNote.Source.MVVM.ViewModels
{
    public class AlarmViewModel : NavBar, INotifyPropertyChanged
    {
        private int _remainingTime;
        private string _timerType;
        private bool _isRunning;
        private bool _isPaused;

        public int RemainingTime //Armazena o tempo restante do timer
        {
            get => _remainingTime;
            set
            {
                _remainingTime = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TimeDisplay));
            }
        }

        public string TimerType //Onde armazena o tipo do timer (Foco ou pausa)

        {
            get => _timerType;
            set
            {
                _timerType = value;
                OnPropertyChanged();
            }
        }

        public string TimeDisplay => $"{RemainingTime / 60:D2}:{RemainingTime % 60:D2}"; //Exibe o tempo em "mm:ss"

        public async void StartTimer() //Método para iniciar o timer
        {
            _isRunning = true;
            _isPaused = false;

            while (_isRunning)
            {
                TimerType = "Foco";
                await RunTimer(10); //10 segundos para testes, só trocar pra 25 * 60
                if (!_isRunning) break;

                TimerType = "Pausa";
                await RunTimer(10); //10 segundos para testes, só trocar pra 5 * 60
            }

            _isRunning = false;
        }

        private async Task RunTimer(int duration) //Método do contador do timer, fica atualizando o reimaingTime, e para ele caso esteja pausado
        {
            for (RemainingTime = duration; RemainingTime >= 0; RemainingTime--)
            {
                while (_isPaused)
                {
                    await Task.Delay(100); //Aviso: Não sei explicar o pq é tão importante, mas não tira se não vai crashar
                }
                if (!_isRunning)
                    break;
                await Task.Delay(1000); //Ritmo em que os segundos são decrementados
            }
        }

        public void PauseTimer() //Método para pausar o timer
        {
            _isPaused = !_isPaused;
        }

        public void ResetTimer() //Método para resetar o timer
        {
            _isRunning = false;
            _isPaused = false;
            RemainingTime = 0;
            TimerType = "Foco";
        }

        //Evento para notificar mudanças nas propriedades
        public event PropertyChangedEventHandler PropertyChanged;

        //Método para notificar mudanças nas propriedades
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
