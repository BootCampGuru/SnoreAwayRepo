using SnoreAway.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SnoreAway.Start
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PreSleep : Page
    {
        public PreSleep()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {


            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();//Creating object for DatabaseHelperClass.cs from ViewModel/DatabaseHelperClass.cs    
            var preSleepSession = Db_Helper.ReadPreSleep(App.SessionId);

            if (preSleepSession != null)
            {
                TglCoffee.IsOn = preSleepSession.CoffeeFlag;
                TglDrink.IsOn = preSleepSession.DrinkFlag;
                TglMeal.IsOn = preSleepSession.HeavyMeal;
                TglSick.IsOn = preSleepSession.Pain;
                TglSmoker.IsOn = preSleepSession.SmokeFlag;
                TglMedicine.IsOn = preSleepSession.MedicineFlag;
                TglWorkOut.IsOn = preSleepSession.WorkoutFlag;
                var dinnerTime = Convert.ToDateTime(preSleepSession.DinnerTime);
                TimeSpan timeSpan = new TimeSpan(dinnerTime.Hour, dinnerTime.Minute, dinnerTime.Second);
                tmpDinner.Time = timeSpan;


            }
            Frame rootFrame = Window.Current.Content as Frame;
            btnBack.IsEnabled = rootFrame.CanGoBack;
        }
        private async void btnSubmit_ClickAsync(object sender, RoutedEventArgs e)
        {
            //Create a new session
            //Create a new Presleep item and link it to the session
            Models.Session session = new Models.Session();
            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();

            if (App.SessionId == 0)
            {
              
                session.StartTime = DateTime.Now.ToString();
                session.ProfileId = App.UserId;
                session.CreationDate = DateTime.Now.ToString();
                var newSession = Db_Helper.InsertSession(session);
            }
            else
            {
                session = Db_Helper.ReadSession(App.SessionId);
               
            }


            if (session.Id != 0)
            {
                App.SessionId = session.Id;
                var preSleepSession = Db_Helper.ReadPreSleep(App.SessionId);

                if (preSleepSession != null)
                {
                    session.FileLocation = @"Session_" + session.Id;
                    session.CreationDate = DateTime.Now.ToString();
                    preSleepSession.WorkoutFlag = TglWorkOut.IsOn;
                    preSleepSession.CoffeeFlag = TglCoffee.IsOn;
                    preSleepSession.DrinkFlag = TglDrink.IsOn;
                    preSleepSession.HeavyMeal = TglMeal.IsOn;
                    preSleepSession.Pain = TglSick.IsOn;
                    preSleepSession.SmokeFlag = TglSmoker.IsOn;
                    preSleepSession.MedicineFlag = TglMedicine.IsOn;
                    preSleepSession.CreationDate = DateTime.Now.ToString();
                    preSleepSession.ProfileId = session.ProfileId;
                    TimeSpan openTime = new TimeSpan(tmpDinner.Time.Hours, tmpDinner.Time.Minutes, tmpDinner.Time.Seconds);
                    preSleepSession.DinnerTime = tmpDinner.Time.ToString();

                    Db_Helper.UpdateSession(session);
                    Db_Helper.UpdatePreSleep(preSleepSession);
                }
                else
                {
                    App.SessionId = session.Id;
                    //files will be saved in the MyMusic location
                    //but possible extension is to use the configuration file in the future
                    session.FileLocation = @"Session_" + session.Id;
                    session.CreationDate = DateTime.Now.ToString();
                    //Create PreSleep

                    Models.PreSleep preSleep = new Models.PreSleep();
                    preSleep.CreationDate = DateTime.Now.ToString();
                    preSleep.ProfileId = session.ProfileId;
                    preSleep.MedicineFlag = TglMedicine.IsOn;
                    preSleep.WorkoutFlag = TglWorkOut.IsOn;
                    preSleep.CoffeeFlag = TglCoffee.IsOn;
                    preSleep.DrinkFlag = TglDrink.IsOn;
                    preSleep.HeavyMeal = TglMeal.IsOn;
                    preSleep.Pain = TglSick.IsOn;
                    preSleep.SmokeFlag = TglSmoker.IsOn;
                    TimeSpan openTime = new TimeSpan(tmpDinner.Time.Hours, tmpDinner.Time.Minutes, tmpDinner.Time.Seconds);
                    preSleep.DinnerTime = tmpDinner.Time.ToString();
                    preSleep.SessionId = session.Id;
                    Db_Helper.UpdateSession(session);
                    Db_Helper.InsertPreSleep(preSleep);
                }
            }
            else
            {

                MessageDialog messageDialog = new MessageDialog("Unable to create session");
                await messageDialog.ShowAsync();
            }

            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(SnoreLab));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();

            }
        }
    }
}
