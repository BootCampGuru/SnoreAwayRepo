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

namespace SnoreAway.Profile
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Profile : Page
    {
        public Profile()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
          

            //Load Profile Information
            if(App.UserId != 0)
            {
                DatabaseHelperClass Db_Helper = new DatabaseHelperClass();
                var profile = Db_Helper.ReadProfile(App.UserId);
                if (profile != null)
                {
                    txtFirstName.Text = profile.FirstName;
                    txtLastName.Text = profile.LastName;
                    TglSick.IsOn = profile.MedicationFlag;
                    TglDrink.IsOn = profile.DrinkFlag;
                    if (profile.DinnerTime != null)
                    {
                        var dinner = Convert.ToDateTime(profile.DinnerTime);
                        TimeSpan openTime = new TimeSpan(dinner.Hour, dinner.Minute, dinner.Second);
                    }
                   
                    TglSmoker.IsOn = profile.SmokeFlag;
                    TglCoffee.IsOn = profile.CoffeeFlag;
                    TglWorkOut.IsOn = profile.WorkoutFlag;
                }

            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage));
        }

        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();//Creating object for DatabaseHelperClass.cs from ViewModel/DatabaseHelperClass.cs    


            //Get Profile Information based on ID
             var profile = Db_Helper.ReadProfile(App.UserId);

            if (profile != null)
            {

                profile.FirstName = txtFirstName.Text;
                profile.LastName = txtLastName.Text;
                profile.MedicationFlag = TglSick.IsOn;
                profile.CoffeeFlag = TglCoffee.IsOn;
                profile.DrinkFlag = TglDrink.IsOn;
                profile.WorkoutFlag = TglWorkOut.IsOn;
                profile.SmokeFlag = TglSmoker.IsOn;

                TimeSpan openTime = new TimeSpan(tmpDinner.Time.Hours, tmpDinner.Time.Minutes, tmpDinner.Time.Seconds);
                profile.DinnerTime = openTime.ToString();

                Db_Helper.UpdateProfile(profile);
            
                MessageDialog messageDialog = new MessageDialog("Profile Updated");
                await messageDialog.ShowAsync();

            }
            else
            {

                Models.Profile newProfile = new Models.Profile();
                newProfile.UserId = App.UserId;
                newProfile.FirstName = txtFirstName.Text;
                newProfile.LastName = txtLastName.Text;
                newProfile.MedicationFlag = TglSick.IsOn;
                newProfile.DrinkFlag = TglDrink.IsOn;
                newProfile.DinnerTime = tmpDinner.Time.ToString();
                newProfile.SmokeFlag = TglSmoker.IsOn;
                newProfile.CoffeeFlag = TglCoffee.IsOn;
                newProfile.WorkoutFlag = TglWorkOut.IsOn;

                Db_Helper.Insert(newProfile);

                MessageDialog messageDialog = new MessageDialog("Profile Created");  
                await messageDialog.ShowAsync();
            }

            
          
        }
    }
}
