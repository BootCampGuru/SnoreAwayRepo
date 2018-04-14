using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnoreAway.Helper
{
    public class DatabaseHelperClass
    {
        //Create Tabble   
        public void CreateDatabase(string DB_PATH)
        {
            if (!CheckFileExists(DB_PATH).Result)
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DB_PATH))
                {
                    conn.CreateTable<Models.Account>();
                    conn.CreateTable<Models.Profile>();
                    conn.CreateTable<Models.PreSleep>();
                    conn.CreateTable<Models.PostSleep>();
                    conn.CreateTable<Models.Session>();
                    conn.CreateTable<Models.History>();

                }
            }
        }
        private async Task<bool> CheckFileExists(string fileName)
        {
            try
            {
                var store = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region Public Insertion Functions

        public Models.Account InsertAccount(Models.Account account)
        {

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conn.RunInTransaction(() =>
                {
                    conn.Insert(account);
                });
            }

            return account;
        }

  

        // Insert the new Profile in the Profile table.   
        public void Insert(Models.Profile objContact)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conn.RunInTransaction(() =>
                {
                    conn.Insert(objContact);
                });
            }
        }

        public void InsertPreSleep(Models.PreSleep sleep)
        {

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conn.RunInTransaction(() =>
                {
                    conn.Insert(sleep);
                });
            }
        }

        #endregion

        #region public read regions

        public Models.Session ReadSession(int id)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var existingsession = conn.Query<Models.Session>("select * from Session where id =" + id).FirstOrDefault();
                return existingsession;
            }
        }

        public Models.Account ReadAccount(string username, string password)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var existingaccount = conn.Query<Models.Account>("select * from Account where username =" + username + " AND password = " + password).FirstOrDefault();
                return existingaccount;
            }
        }
        #endregion

        // Retrieve the specific contact from the database.     
        public Models.Profile ReadProfile(int contactid)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var existingconact = conn.Query<Models.Profile>("select * from Profile where UserId =" + contactid).FirstOrDefault();
                return existingconact;
            }
        }
        public ObservableCollection<Models.Profile> ReadAllContacts()
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    List<Models.Profile> myCollection = conn.Table<Models.Profile>().ToList<Models.Profile>();
                    ObservableCollection<Models.Profile> ContactsList = new ObservableCollection<Models.Profile>(myCollection);
                    return ContactsList;
                }
            }
            catch
            {
                return null;
            }

        }
        //Update existing conatct   
        public void UpdateProfile(Models.Profile ObjContact)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                var existingconact = conn.Query<Models.Profile>("select * from Contacts where Id =" + ObjContact.Id).FirstOrDefault();
                if (existingconact != null)
                {

                    conn.RunInTransaction(() =>
                    {
                        conn.Update(ObjContact);
                    });
                }

            }
        }
        //Delete all contactlist or delete Contacts table     
        public void DeleteAllContact()
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                conn.DropTable<Models.Profile>();
                conn.CreateTable<Models.Profile>();
                conn.Dispose();
                conn.Close();

            }
        }
        //Delete specific contact     
        public void DeleteContact(int Id)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                var existingconact = conn.Query<Models.Profile>("select * from Contacts where Id =" + Id).FirstOrDefault();
                if (existingconact != null)
                {
                    conn.RunInTransaction(() =>
                    {
                        conn.Delete(existingconact);
                    });
                }
            }
        }
    }
}
