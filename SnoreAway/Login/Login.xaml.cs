using SnoreAway.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
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

namespace SnoreAway.Login
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            btnBack.IsEnabled = rootFrame.CanGoBack;
        }

        private void HyperAccount_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(CreateAccount));
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
            //Look for user name and password
            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();//Creating object for DatabaseHelperClass.cs from ViewModel/DatabaseHelperClass.cs    
            if (txtUserName.Text != "" & txtPassword.Text != "")
            {

                using (SHA256 shaHash = SHA256.Create())
                {

                    var password = Hashing.GetSha256Hash(shaHash, txtPassword.Text);
                    var account = Db_Helper.ReadAccount(txtUserName.Text, password);
                    if (account != null)
                    {
                        App.UserId = account.Id;
                        Frame.Navigate(typeof(Profile.Profile));//after adding new user redirect to profile page   
                    }
                    else
                    {
                        MessageDialog messageDialog = new MessageDialog("Invalid username and password");//Text should not be empty    
                        await messageDialog.ShowAsync();
                    }
                }

            }
            else
            {
                MessageDialog messageDialog = new MessageDialog("Please fill both fields");//Text should not be empty    
                await messageDialog.ShowAsync();
            }


        }
    }
}
