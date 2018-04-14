using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnoreAway.Models
{
    public class Session
    {
        //The Id property is marked as the Primary Key  
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int Id { get; set; }

        public int ProfileId { get; set; }

        public string FileLocation { get; set; }
        
        public string StartTime { get; set;}

        public string EndTime { get; set; }

        public string CreationDate { get; set; }
        public Session()
        {
            //empty constructor  
        }
        public Session(string profildid, string filelocation, string starttime, string endtime)
        {
            ProfileId = ProfileId;
            FileLocation = filelocation;
            StartTime = starttime;
            EndTime = endtime;
            CreationDate = DateTime.Now.ToString();
        }
    }
}
