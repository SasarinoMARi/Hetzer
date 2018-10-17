using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Exceptions;

namespace Hetzer
{
    class AchiveClean
    {
        public static void clean()
        {
            string[] path = {
                "D:\\Downloads\\4071252374_028e8b5a458ed48f9065e709a99e456446bb9c5f\\data\\js\\tweets\\2015_11.js",
                "D:\\Downloads\\4071252374_028e8b5a458ed48f9065e709a99e456446bb9c5f\\data\\js\\tweets\\2015_12.js",
                "D:\\Downloads\\4071252374_028e8b5a458ed48f9065e709a99e456446bb9c5f\\data\\js\\tweets\\2016_01.js",
                "D:\\Downloads\\4071252374_028e8b5a458ed48f9065e709a99e456446bb9c5f\\data\\js\\tweets\\2016_02.js",
                "D:\\Downloads\\4071252374_028e8b5a458ed48f9065e709a99e456446bb9c5f\\data\\js\\tweets\\2016_03.js",
                "D:\\Downloads\\4071252374_028e8b5a458ed48f9065e709a99e456446bb9c5f\\data\\js\\tweets\\2016_04.js",
                "D:\\Downloads\\4071252374_028e8b5a458ed48f9065e709a99e456446bb9c5f\\data\\js\\tweets\\2016_05.js",
                "D:\\Downloads\\4071252374_028e8b5a458ed48f9065e709a99e456446bb9c5f\\data\\js\\tweets\\2016_06.js",
                "D:\\Downloads\\4071252374_028e8b5a458ed48f9065e709a99e456446bb9c5f\\data\\js\\tweets\\2016_10.js",
                };
            int c = 0;
            List<long> ids = new List<long>();
            foreach (var item in path)
            {
                TweetEntity[] json = loadFile(item);
                foreach (var tweet in json)
                {
                    c++;
                    //if(tweet.retweeted_status == null || tweet.retweeted_status.id == 0)
                    //{
                    //    Console.WriteLine("destroy : " + tweet.id);
                    //}
                    //else
                    //{
                    //    Console.WriteLine("unretweet : " + tweet.retweeted_status.id);
                    //    Tweet.UnRetweet(tweet.retweeted_status.id);
                    //}
                    //Tweet.DestroyTweet(tweet.id);
                    ids.Add(tweet.id);
                }
            }
            IEnumerable<Tweetinvi.Models.ITweet> tt = Tweet.GetTweets(ids.ToArray());
            foreach (var t in tt)
            {
                if (t.IsTweetDestroyed)
                {
                    Console.WriteLine("Aleady destroyed");
                }
                else
                {
                    t.Destroy();
                }
            }
            Console.WriteLine("\ncount: " + c);
        }

        private static TweetEntity[] loadFile(string path)
        {
            var str = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<TweetEntity[]>(str);
        }
        
    }

    class TweetEntity
    {
        public long id;
        public TweetEntity retweeted_status;
    }
}
