using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using FeedMeHash.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LinqToTwitter;

namespace FeedMeHash.Controllers
{
    public class HomeController : Controller
    {
        private IOAuthCredentials _oAuthCredentials = new SessionStateCredentials();
        private MvcAuthorizer _mvcAuthorizer;
        private TwitterContext _twitterTool;

        public ActionResult Index()
        {
            ViewBag.Message = "Feed Me Hash";
                 
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult Search(string hashText)
        {
            try
            {
                if (_oAuthCredentials.ConsumerKey == null || _oAuthCredentials.ConsumerSecret == null)
                {
                    _oAuthCredentials.ConsumerKey = WebConfigurationManager.AppSettings["TwitterConsumerKey"].ToString();
                    _oAuthCredentials.ConsumerSecret = WebConfigurationManager.AppSettings["TwitterConsumerSecret"].ToString();
                    _oAuthCredentials.AccessToken = WebConfigurationManager.AppSettings["TwitterAccessToken"].ToString();
                    _oAuthCredentials.OAuthToken = WebConfigurationManager.AppSettings["TwitterOAuthToken"].ToString();

                }

                _mvcAuthorizer = new MvcAuthorizer
                {
                    Credentials = _oAuthCredentials
                };

                _twitterTool = new TwitterContext(_mvcAuthorizer);

                var hashtagQueryResults = (from search in _twitterTool.Search
                                                where search.Type == SearchType.Search &&
                                                search.Query == "#" + hashText
                                                select search).FirstOrDefault();

                var tweetDataList = new List<TweetData>();

                foreach (var status in hashtagQueryResults.Statuses)
                {
                    tweetDataList.Add(new TweetData
                    {
                        created_at = status.CreatedAt.ToString(),
                        text = status.Text,
                        profile_image_url = status.User.ProfileImageUrl,
                        profile_background_image_url = status.User.ProfileBannerUrl,
                        profile_text_color = status.User.ProfileTextColor,
                        name = status.User.Name,
                        screen_name = status.ScreenName,
                        location = status.User.Location,
                        url = status.User.Url                        
                    });
                }

                return new JsonResult { Data = JsonConvert.SerializeObject(tweetDataList), ContentType = "application/json" };
            }
            catch (System.Exception e)
            {
                return new JsonResult { Data = e.Message, ContentType = "application/json" };
            }

            
        }
    }
}
