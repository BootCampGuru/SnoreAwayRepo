using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnoreAway.Models
{
    public class PostSleep
    {
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int Id { get; set; }
        public int ProfileId { get; set; }

        public int SessionId { get; set; }
        public bool SleepWell { get; set; }

        public bool OnTime { get; set; }
        public int WakeNumber { get; set; }
        public string CreationDate { get; set; }

        public PostSleep()
        {
            //empty constructor  
        }
        public PostSleep(int profileid, bool sleepwell, int wakenumber, string filename, bool ontime)
        {
            ProfileId = profileid;
            SleepWell = sleepwell;
            WakeNumber = wakenumber;
            OnTime = ontime;
            CreationDate = DateTime.Now.ToString();
        }

    }
}
