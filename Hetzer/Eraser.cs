using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Parameters;

namespace Hetzer
{
    class Eraser
    {
        int counter = 0;
        int safe_index = 0;
        long safe_id = 0;

        private Eraser()
        {

        }

        public static Eraser CreateEraser(int leftTweet)
        {
            if (Auth.Credentials != null)
            {
                var o = new Eraser();
                o.safe_index = leftTweet;
                return o;
            }
            else return null;
        }

        public void StartErase()
        {
            while (Delete(GetTimeline())) { }
            Message.PublishMessage(string.Format("Pulse : removed {0} tweet(s).", (counter - safe_index + 1)), User.GetAuthenticatedUser().Id);
        }

        public IEnumerable<Tweetinvi.Models.ITweet> GetTimeline()
        {
            var param = CreateParaeater(safe_id);
            var timeline = User.GetAuthenticatedUser().GetUserTimeline(param);
            return timeline;
        }

        public bool Delete(IEnumerable<Tweetinvi.Models.ITweet> timeline)
        {
            if (timeline == null || timeline.Count() == 1) return false;
            foreach (var t in timeline)
            {
                if (++counter > safe_index && t.Id != safe_id)
                {
                    Console.WriteLine("Distroy: (" + t.CreatedAt.ToString("yyyy-MM-dd hh:mm") + ")" + t.Text);
                    t.Destroy();
                }
                else
                {
                    safe_id = t.Id;
                }
            }
            counter--;
            return true;
        }

        private UserTimelineParameters CreateParaeater(long safeTweetId)
        {
            var p = new UserTimelineParameters
            {
                MaximumNumberOfTweetsToRetrieve = 100
            };
            if (safeTweetId != 0) p.MaxId = safeTweetId;
            return p;
        }

        private void Print(Tweetinvi.Models.ITweet t)
        {
            Console.WriteLine(t.CreatedAt.ToString("yyyy-MM-dd hh:mm") + "\t: " + t.Text);
        }
    }
}
