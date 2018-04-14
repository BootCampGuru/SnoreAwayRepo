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
            Frame rootFrame = Window.Current.Content as Frame;
            btnBack.IsEnabled = rootFrame.CanGoBack;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();

            }
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
                profile.DrinkFlag = TglDrink.IsOn;
                profile.DinnerTime = tmpDinner.ToString();
                profile.SmokeFlag = TglSmoker.IsOn;

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
                newProfile.DinnerTime = tmpDinner.ToString();
                newProfile.SmokeFlag = TglSmoker.IsOn;

                Db_Helper.Insert(newProfile);

                MessageDialog messageDialog = new MessageDialog("Profile Created");  
                await messageDialog.ShowAsync();
            }

            
          
        }
    }
}
