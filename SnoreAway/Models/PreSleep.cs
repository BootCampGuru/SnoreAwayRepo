﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnoreAway.Models
{
    public class PreSleep
    {
        //The Id property is marked as the Primary Key  
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int Id { get; set; }

        public int ProfileId { get; set; }
        public bool SmokeFlag { get; set; }
        public bool DrinkFlag { get; set; }

        public bool WorkoutFlag { get; set; }

        public bool CoffeeFlag { get; set; }

        public string DinnerTime { get; set; }
  
        public bool HeavyMeal { get; set; }

        public bool Pain { get; set; }

        public string CreationDate { get; set; }
        public PreSleep()
        {
            //empty constructor  
        }
        public PreSleep(string profildid, bool smokeflag, bool drinkflag, bool coffeeflag, string dinnertime, bool heavymeal, bool pain)
        {
            ProfileId = ProfileId;
            SmokeFlag = smokeflag;
            DrinkFlag = drinkflag;
            CoffeeFlag = coffeeflag;
            DinnerTime = dinnertime;
            HeavyMeal = heavymeal;
            Pain = pain;
            CreationDate = DateTime.Now.ToString();
        }
    }
}