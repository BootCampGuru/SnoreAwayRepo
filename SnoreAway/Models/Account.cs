using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnoreAway.Models
{
    public class Account
    {
        //The Id property is marked as the Primary Key  
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
        public string CreationDate { get; set; }
        public Account()
        {
            //empty constructor  
        }
        public Account(string username, string password)
        {
            UserName = username;
            Password = password;
            CreationDate = DateTime.Now.ToString();
        }
    }
}
