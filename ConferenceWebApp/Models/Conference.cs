using System.Text.RegularExpressions;

namespace ConferenceWebApp.Models
{
    public class Conference
    {
        public int id { get; set; }
        public string name{ get; set; }
        public string description{ get; set; }
        public string begin_date { get; set; }
        public string end_date { get; set; }
        public string link { get; set; }

        public bool FindKeyWordInNameOrDescription(string keyWord)
        {
            if (Regex.IsMatch(name, keyWord, RegexOptions.IgnoreCase)
                        || Regex.IsMatch(description, keyWord, RegexOptions.IgnoreCase))
            {
                return true;
            }

            return false;
        }

        public bool IsBetweenDates(string _beginDate, string _endDate)
        {
            DateTime userBeginDate = DateTime.Parse(_beginDate);
            DateTime userEndDate = DateTime.Parse(_endDate).AddDays(1);

            DateTime conferenceBeginDate = DateTime.Parse(this.begin_date);
            DateTime conferenceEndDate = DateTime.Parse(this.end_date);
            if ((conferenceBeginDate - userBeginDate).TotalDays >= 0
                && (userEndDate - conferenceEndDate).TotalDays >= 0)
            {
                return true;
            }

            return false;
        }
    }
}
