using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnoreAway.Models
{
    public class History
    {
        //The Id property is marked as the Primary Key  
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int Id { get; set; }
        public int ProfileId { get; set; }

        public double SleepDuration { get; set; }

        public int SleepScore { get; set; }

        public string FileName { get; set; }
        public string CreationDate { get; set; }

        public History()
        {
            //empty constructor  
        }
        public History(int profileid, double sleepduration, int sleepscore, string filename)
        {
            ProfileId = profileid;
            SleepDuration = sleepduration;
            SleepScore = sleepscore;
            FileName = filename;
            CreationDate = DateTime.Now.ToString();
        }

    }
}
