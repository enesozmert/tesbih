using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace teshib
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            if (Preferences.ContainsKey("is_vibrate") == true)
            {
                if (Preferences.Get("is_vibrate", string.Empty) == "1")
                {
                    toggledVibrate.IsToggled = true;
                }
                else if (Preferences.Get("is_vibrate", string.Empty) == "0")
                {
                    toggledVibrate.IsToggled = false;
                }
            }
        }
        private void TapGestureRecognizer_Tapped_dataClear(object sender, EventArgs e)
        {
            if (dataClear.IsVisible == true)
            {
                dataClear.IsVisible = false;
            }
            else
            {
                dataClear.IsVisible = true;
            }
            selecetClickOrSwipe.IsVisible = false;
            about.IsVisible = false;
            publicSettings.IsVisible = false;
        }

        private void TapGestureRecognizer_Tapped_selecetClickOrSwipe(object sender, EventArgs e)
        {
            dataClear.IsVisible = false;
            if (selecetClickOrSwipe.IsVisible == true)
            {
                selecetClickOrSwipe.IsVisible = false;
            }
            else
            {
                selecetClickOrSwipe.IsVisible = true;
            }
            about.IsVisible = false;
            publicSettings.IsVisible = false;
        }

        private void TapGestureRecognizer_Tapped_about(object sender, EventArgs e)
        {
            dataClear.IsVisible = false;
            selecetClickOrSwipe.IsVisible = false;
            publicSettings.IsVisible = false;
            if (about.IsVisible == true)
            {
                about.IsVisible = false;
            }
            else
            {
                about.IsVisible = true;
            }
        }
        private void TapGestureRecognizer_Tapped_publicSettings(object sender, EventArgs e)
        {
            if (publicSettings.IsVisible == true)
            {
                publicSettings.IsVisible = false;
            }
            else
            {
                publicSettings.IsVisible = true;
            }
            selecetClickOrSwipe.IsVisible = false;
            about.IsVisible = false;
            dataClear.IsVisible = false;
        }
        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if (toggledData.IsToggled == true)
            {
                //Delete Table  
                App.Database.DeleteTable();
                Navigation.PushAsync(new MainPage());
                toggledData.IsToggled = false;
            }
        }

        private void toggledDatalevel_Toggled(object sender, ToggledEventArgs e)
        {
            if (toggledDatalevel.IsToggled == true)
            {
                if (Preferences.ContainsKey("kayitlevel") == true)
                {
                    //Preferences.Remove("kayitlevel");
                    Preferences.Set("kayitlevel", "0");
                    toggledData.IsToggled = false;
                    Navigation.PushAsync(new MainPage());
                }
            }
        }

        private void Button_Clicked_Click_select(object sender, EventArgs e)
        {
            Preferences.Set("selecet_click_swipe", "0");
            Navigation.PushAsync(new MainPage());
        }

        private void Button_Clicked_Swipe_select(object sender, EventArgs e)
        {
            Preferences.Set("selecet_click_swipe", "1");
            Navigation.PushAsync(new MainPage());
        }
        private void Button_Clicked_Shake_select(object sender, EventArgs e)
        {
            Preferences.Set("selecet_click_swipe", "2");
            Navigation.PushAsync(new MainPage());
        }

        private void toggledVibrate_Toggled(object sender, ToggledEventArgs e)
        {
            if (Preferences.ContainsKey("is_vibrate") == true)
            {
                if (toggledVibrate.IsToggled == true)
                {
                    //Preferences.Remove("kayitlevel");
                    Preferences.Set("is_vibrate", "1");
                    toggledVibrate.IsToggled = true;
                }
                else if (toggledVibrate.IsToggled == false)
                {
                    Preferences.Set("is_vibrate", "0");
                    toggledVibrate.IsToggled = false;
                    Navigation.PushAsync(new MainPage());
                }
            }
        }
    }
}