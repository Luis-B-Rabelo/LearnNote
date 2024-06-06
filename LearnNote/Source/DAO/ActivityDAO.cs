using LearnNote.Model;
using System.Collections.ObjectModel;

namespace LearnNote.Source.DAO
{
    public class ActivityDAO : BaseDAO
    {
        public static bool CheckIfActivityExist(uint activityId)
        {
            Dictionary<string, object> activitySearch = new Dictionary<string, object>
            {
                { "activityId", activityId }
            };

            string[] specific = { "activityId" };

            if(SelectSpecificsByProperties("activitytable", specific, activitySearch) != null)
            {
                return true;
            }
            else 
            {
                return false; 
            }
        }

        public static bool CreateActivity(ActivityModel activity)
        {
            try 
            {
                Dictionary<string, object> activityInsert = new Dictionary<string, object>
                {
                    { "activityId", activity.ActivityId },
                    { "activityName", activity.Name },
                    { "activityDescription", activity.Description },
                    { "activityDayWeek", activity.DayWeek },
                    { "activityNum", activity.ActivityNum },
                    { "userIdFk", activity.UserIdFk }
                };

                if(InsertInto("activitytable", activityInsert))
                {
                    return true;
                }
                else 
                { 
                    return false; 
                }
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

        public static bool UpdateActivity(ActivityModel activity)
        {
            try
            {
                Dictionary<string, object> activityUpdate = new Dictionary<string, object>
                {
                    { "activityName", activity.Name },
                    { "activityDescription", activity.Description },
                    { "activityDayWeek", activity.DayWeek },
                    { "activityNum", activity.ActivityNum },
                };

                Dictionary<string, object> where = new Dictionary<string, object>
                {
                    { "activityId", activity.ActivityId }
                };

                if (UpdateByProperties("activitytable", activityUpdate, where))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static ObservableCollection<ActivityModel>? UserActivities(uint userId)
        {
            ObservableCollection<ActivityModel> activities;

            Dictionary<string, object> search = new Dictionary<string, object>
            {
                { "userIdFk", userId }
            };

            List<Dictionary<string, object>> elements = SelectWholeByProperties("activitytable", search);

            if (elements != null)
            {
                activities = new ObservableCollection<ActivityModel>();

                foreach (Dictionary<string, object> element in elements)
                {
                    activities.Add(new ActivityModel
                    {
                        ActivityId = (uint)element["activityId"],
                        Name = (string)element["activityName"],
                        Description = (string)element["activityDescription"],
                        DayWeek = (byte)element["activityDayWeek"],
                        ActivityNum = (byte)element["activityNum"],
                        UserIdFk = userId
                    });
                }
            }
            else
            {
                activities = null;
            }

            return activities;
        }
    }
}
