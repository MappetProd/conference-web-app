global using OptionsValues = System.Collections.Generic.Dictionary<string, string>;

using ConferenceWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace ConferenceWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly SQliteDatabase db;
        public HomeController(SQliteDatabase _db)
        {
            db = _db;
        }
        public IActionResult OutputSortedConferences(string jsonConferences)
        {
            List<Conference>? deserializedConferences = JsonConvert.DeserializeObject<List<Conference>>(jsonConferences);
            if (deserializedConferences == null) deserializedConferences = new List<Conference>();

            List<Conference> sortedConferences = SortConferencesByDate(deserializedConferences);
            ViewBag.Conferences = sortedConferences;
            return View("Home");
        }

        public IActionResult FilterConferences(string? keyWord, string? beginDate, string? endDate)
        {
            List<Conference> allConferences = db.RetrieveAll<Conference>();
            List<Filter> filters = new List<Filter>();
            if (keyWord != null)
            {
                Filter keywordFilter = new Filter("keyword", new OptionsValues() { { "keyword", keyWord } });
                filters.Add(keywordFilter);
            }

            if ((beginDate != null) && (endDate != null))
            {
                OptionsValues values = new OptionsValues();
                values.Add("userBeginDate", beginDate);
                values.Add("userEndDate", endDate);

                Filter dateFilter = new Filter("date", values);
                filters.Add(dateFilter);
            }

            List<Conference> filteredConferences = HandleFilter(allConferences, filters);
            return RedirectToAction("OutputSortedConferences", "Home", new { jsonConferences = JsonConvert.SerializeObject(filteredConferences) });
        }

        public List<Conference> SortConferencesByDate(List<Conference> conferences) 
        {
            List<Conference> sortedByDate = conferences;
            for (int i = 0; i < conferences.Count; i++)
            {
                bool swapped = false;
                for (int j = 0; j < conferences.Count - i - 1; j++)
                {
                    Conference conference = sortedByDate[j];
                    Conference nextConference = sortedByDate[j + 1];
                    if (DateTime.Parse(conference.begin_date) > DateTime.Parse(nextConference.begin_date))
                    {
                        swapped = true;
                        sortedByDate[j + 1] = conference;
                        sortedByDate[j] = nextConference;
                    }
                }

                if (!swapped)
                {
                    break;
                }
            }
            return sortedByDate;
        }

        public List<Conference> HandleFilter(List<Conference> all, List<Filter> filters)
        {
            List<Conference> filtered = new List<Conference>();
            foreach (Conference conference in all)
            {
                int filtersPassed = 0;
                foreach (Filter filter in filters)
                {
                    if (filter != null && filter.ExecuteFilterFunction(conference))
                    {
                        filtersPassed++;
                    }
                }

                if (filtersPassed == filters.Count)
                {
                    filtered.Add(conference);
                }
            }
            return filtered;
        }
    }
}
