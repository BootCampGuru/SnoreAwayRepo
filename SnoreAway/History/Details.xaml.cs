using SnoreAway.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SnoreAway.History
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Details : Page
    {
        public Details()
        {
            InitializeComponent();
        }

        private void HyperAccount_Click(object sender, RoutedEventArgs e)
        {
            //Open recording
            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();
            var session = Db_Helper.ReadSession(App.UserId);
            if (session != null)
            {
                mediaElement1.Source = new Uri(session.FileLocation);
                mediaElement1.Play();
            }

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {


            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();//Creating object for DatabaseHelperClass.cs from ViewModel/DatabaseHelperClass.cs    
            var preSleepSession = Db_Helper.ReadPreSleep(App.SessionId);
            var postSleepSession = Db_Helper.ReadPostSleep(App.SessionId);
            var session = Db_Helper.ReadSession(App.UserId);

            if(session != null)
            {
                StartTime.Text = session.StartTime;
                EndTime.Text = session.EndTime;
                Duration.Text = session.Duration;
            }


            if (preSleepSession != null)
            {

                Coffee.Text = preSleepSession.CoffeeFlag ? "Individual drank Coffee" : "";
                Smoke.Text = preSleepSession.SmokeFlag ? "Individual Smoked" : "";
                Drink.Text = preSleepSession.DrinkFlag ? "Individual Drank" : "";
                Medicine.Text = preSleepSession.Pain ? "Individual felt Pain or was Sick" : "";
                LargeMeal.Text = preSleepSession.HeavyMeal ? "Individual had a large meal" : "";
                DinnerTime.Text = preSleepSession.DinnerTime.ToString();



            }

            if(postSleepSession != null)
            {
                SleptWell.Text = postSleepSession.SleepWell ? "Individual slept well" : "Individual didn't sleep well"; 
                FeltFresh.Text = postSleepSession.OnTime ? "Individual felt fresh after waking up" : "Individual didn't feel fresh after waking up";
                WokeUp.Text = "Individual woke up " + postSleepSession.WakeNumber + " times";

            }

        }
    }
}
