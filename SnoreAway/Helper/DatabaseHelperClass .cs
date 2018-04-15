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
        //Create Table   
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

        //This functions are a good candidate for generics

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

        public Models.PreSleep InsertPreSleep(Models.PreSleep sleep)
        {

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conn.RunInTransaction(() =>
                {
                    conn.Insert(sleep);
                });

                return sleep;
            }
        }

        public Models.Session InsertSession(Models.Session sleep)
        {

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conn.RunInTransaction(() =>
                {
                    conn.Insert(sleep);
                });

                return sleep;
            }
        }
        public void InsertPostSleep(Models.PostSleep sleep)
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
        
        public bool HasAccontCreated(string username)
        {
            bool hasAccount = false;

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var existingAccount = conn.Query<Models.Account>("select * from Account where username ='" + username + "'").FirstOrDefault();
               
                if(existingAccount != null)
                {
                    hasAccount = true;
                }

                return hasAccount;
            }

        }
        public Models.Account ReadAccount(string username, string password)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var existingaccount = conn.Query<Models.Account>("select * from Account where username ='" + username + "' AND password = '" + password + "'").FirstOrDefault();
                return existingaccount;
            }
        }
        #endregion

        // Retrieve the specific profile from the database.     
        public Models.Profile ReadProfile(int contactid)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var existingconact = conn.Query<Models.Profile>("select * from Profile where UserId ='" + contactid + "'").FirstOrDefault();
                return existingconact;
            }
        }


        // Retrieve the specific presleep from the database.     
        public Models.PreSleep ReadPreSleep(int id)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var existingPreSleep = conn.Query<Models.PreSleep>("select * from PreSleep where SessionId ='" + id + "'").FirstOrDefault();
                return existingPreSleep;
            }
        }

        // Retrieve the specific postsleep from the database.     
        public Models.PostSleep ReadPostSleep(int id)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var existingPostSleep = conn.Query<Models.PostSleep>("select * from PostSleep where SessionId ='" + id + "'").FirstOrDefault();
                return existingPostSleep;
            }
        }


        public ObservableCollection<Models.Session> ReadAllSessions()
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    List<Models.Session> myCollection = conn.Table<Models.Session>().ToList<Models.Session>();
                    ObservableCollection<Models.Session> sessionList = new ObservableCollection<Models.Session>(myCollection);
                    return sessionList;
                }
            }
            catch
            {
                return null;
            }

        }
        public ObservableCollection<Models.Profile> ReadAllProfiles()
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

        #region public update region
        //Update existing Session

        public void UpdateSession(Models.Session ObjContact)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                var existingconact = conn.Query<Models.Profile>("select * from Session where Id =" + ObjContact.Id).FirstOrDefault();
                if (existingconact != null)
                {

                    conn.RunInTransaction(() =>
                    {
                        conn.Update(ObjContact);
                    });
                }

            }
        }

        //Update Pre Sleep
        public void UpdatePreSleep(Models.PreSleep ObjContact)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                var existingconact = conn.Query<Models.PreSleep>("select * from PreSleep where SessionId =" + ObjContact.SessionId).FirstOrDefault();
                if (existingconact != null)
                {

                    conn.RunInTransaction(() =>
                    {
                        conn.Update(ObjContact);
                    });
                }

            }
        }

        //Update Account
        public void Account(Models.Account ObjContact)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                var existingconact = conn.Query<Models.Account>("select * from Accout where username ='" + ObjContact.Id + "'" + " AND password = '" + ObjContact.Password + "'").FirstOrDefault();
                if (existingconact != null)
                {

                    conn.RunInTransaction(() =>
                    {
                        conn.Update(ObjContact);
                    });
                }

            }
        }
        //Update Post Sleep
        public void UpdatePostSleep(Models.PostSleep ObjContact)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                var existingconact = conn.Query<Models.PostSleep>("select * from PostSleep where SessionId =" + ObjContact.SessionId).FirstOrDefault();
                if (existingconact != null)
                {

                    conn.RunInTransaction(() =>
                    {
                        conn.Update(ObjContact);
                    });
                }

            }
        }

        //Update existing profile   
        public void UpdateProfile(Models.Profile ObjContact)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                var existingconact = conn.Query<Models.Profile>("select * from Profile where Id =" + ObjContact.Id).FirstOrDefault();
                if (existingconact != null)
                {

                    conn.RunInTransaction(() =>
                    {
                        conn.Update(ObjContact);
                    });
                }

            }
        }


        #endregion
        //Delete all profiles     
        public void DeleteAllProfiles()
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                conn.DropTable<Models.Profile>();
                conn.CreateTable<Models.Profile>();
                conn.Dispose();
                conn.Close();

            }
        }
        //Delete specific Profile     
        public void DeleteProfile(int Id)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                var existingconact = conn.Query<Models.Profile>("select * from Profile where Id =" + Id).FirstOrDefault();
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
