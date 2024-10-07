using ConferenceWebApp.Models;

namespace ConferenceWebApp.Controllers
{
    public class Filter
    {
        public string name;

        public OptionsValues values;

        public Filter(string name, OptionsValues options)
        {
            this.name = name;
            this.values = options;
        }

        public bool ExecuteFilterFunction(Conference conference)
        {
            if (name == "keyword" && values.TryGetValue("keyword", out string keyWord))
            {
                return conference.FindKeyWordInNameOrDescription(keyWord);
            }
            else if (name == "date" && values.TryGetValue("userBeginDate", out string userBeginDate)
                        && values.TryGetValue("userEndDate", out string userEndDate))
            {
                return conference.IsBetweenDates(userBeginDate, userEndDate);
            }

            return false;
        }
    }
}
