using System.Configuration;
using System.Net.Security;

using TweetSharp;


namespace ShitterTwitter.MessagePublisher
{
    public class TwitterManeger : ITwitterManeger
    {
        private string _consumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
        private string _consumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];
        private string _accessToken = ConfigurationManager.AppSettings["AccessToken"];
        private string _accessTokenSecret = ConfigurationManager.AppSettings["AccessTokenSecret"];

        private TwitterAccount _account;
        private TwitterService _service;
        public TwitterManeger()
        {
          _service = new TwitterService(_consumerKey,_consumerSecret);
            _service.AuthenticateWith(_accessToken,_accessTokenSecret);
        }
        //TODO auto like all mentions 


        public void sendTweet(string tweet)
        {
            _service.SendTweet(new SendTweetOptions {Status = tweet});



        }
    }
}