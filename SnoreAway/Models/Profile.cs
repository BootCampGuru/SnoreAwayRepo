using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnoreAway.Models
{
    public class Profile
    {
        //The Id property is marked as the Primary Key  
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool SmokeFlag { get; set; }

        public bool DrinkFlag { get; set; }
        public bool MedicationFlag { get; set; }

        public string DinnerTime { get; set; }
        public string CreationDate { get; set; }
        public Profile()
        {
            //empty constructor  
        }
        public Profile(string firstname, string lastname,bool smokeflag,bool drinkflag,bool medicationflag,string dinnertime)
        {
            FirstName = firstname;
            LastName = lastname;
            SmokeFlag = smokeflag;
            DrinkFlag = drinkflag;
            MedicationFlag = medicationflag;
            DinnerTime = dinnertime;
            CreationDate = DateTime.Now.ToString();
        }
    }
}
