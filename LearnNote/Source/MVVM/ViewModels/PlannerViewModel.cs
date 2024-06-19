
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using LearnNote.Model;
using LearnNote.Source.Core;
using LearnNote.Source.DAO;
using LearnNote.Source.MVVM.ViewModels.PopUps;
using LearnNote.Source.MVVM.Views.PopUps;
using NLog;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace LearnNote.Source.MVVM.ViewModels
{
    public partial class PlannerViewModel : NavBar, IQueryAttributable, INotifyPropertyChanged
    {
        private ObservableCollection<ActivityModel> _activities;

        private ObservableCollection<ActivityModel> _createdActivities;

        public ObservableCollection<ActivityModel> Activities
        {
            get => _activities;
            set
            {
                _activities = value;
                OnPropertyChanged();
            }
        }

        public uint ActivityId { get; private set; }

        public string Name { get; private set; }

        public uint DayWeek { get; private set; }

        public uint ActivityNum { get; private set; }

        public ActivityModel Activity { get; private set; } 

        public PlannerViewModel()
        {

        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("PassUserId"))
            {
#if DEBUG
                GlobalFunctionalities.Logger.Debug("Parâmetros passados com sucesso");
#endif

                UserId = uint.Parse(query["PassUserId"].ToString());

                CreatePlanner();

#if DEBUG
                GlobalFunctionalities.Logger.ForDebugEvent()
                    .Message("Atividades criadas para o planner")
                    .Property("QntAtividades", Activities.Count)
                    .Property("Atividades", Activities)
                    .Log();
#endif
            }
        }

        private void CreatePlanner()
        {
            Activities = new ObservableCollection<ActivityModel>();

            _createdActivities = ActivityDAO.UserActivities(UserId);

            ActivityModel defaulter = new ActivityModel();
            defaulter.NewActivity(ActivityId, 0, 0, 0);

            for(byte dw = 1; dw <= 5;  dw++) 
            { 
                for(byte act = 1; act <= 11; act++)
                {
                    uint composedUint = ((UserId + 1) + dw) * ((UserId + 1) + dw + 1) / 2 + dw;
                    uint activityId = (composedUint + act) * (composedUint + act + 1) / 2 + act;

                    if(_createdActivities != null)
                    {
                        ActivityModel activity = _createdActivities.SingleOrDefault(activity => activity.ActivityId == activityId, defaulter);

                        if (activity != defaulter)
                        {
                            Activities.Add(activity);
                        }
                        else
                        {
                            ActivityModel activityModel = new ActivityModel();
                            Activities.Add(activityModel.NewActivity(activityId, UserId, dw, act));
                        }
                    }
                    else
                    {
                        ActivityModel activityModel = new ActivityModel();
                        Activities.Add(activityModel.NewActivity(activityId, UserId, dw, act));
                    }
                }
            }

            _createdActivities = null;
        }

        [RelayCommand]
        public void OpenActivity(ActivityModel activity)
        {
#if DEBUG
            GlobalFunctionalities.Logger.ForDebugEvent()
                .Message("Atividade aberta")
                .Property("Atividade", activity)
                .Log();
#endif
            Shell.Current.ShowPopup( new ManageActivityPopUp(activity, UserId));
        }
    }
}
