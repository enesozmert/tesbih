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
    public partial class BeatsPage : ContentPage
    {
        int sel=0;
        public int kayitlevel;
        public string kayitBeadsName;
        public int labelLevelReplce;
        public BeatsPage()
        {
            InitializeComponent();
            sel = 0;
            imageBeatsView.Source = Convert.ToString("beads" + sel + ".png");
            int sonuc = 0;
            sonuc = sel * 2;

            labelSpeed.Text = Convert.ToString("Tesbih çekme hızı: " + sonuc);
            labelLevel.Text = Convert.ToString("Zikir: " + sonuc * 5000);
            labelLevelReplce = Convert.ToInt32(labelLevel.Text.Remove(0, 6));
            isLevelButton(labelLevelReplce);

        }
        protected override void OnAppearing()
        {
            //Sayfa açılınca
            //NavigationPage.SetHasNavigationBar(this, false);

        }
        private void TapGestureRecognizer_Tapped_left(object sender, EventArgs e)
        {
            if (sel >= 50)
            {
                sel = 49;
                labelSpeed.Text = Convert.ToString("Tesbih çekme hızı: " + 50);
                labelLevel.Text = Convert.ToString("Zikir: " + 500000);
            }
            sel++;
            imageBeatsView.Source = Convert.ToString("beads" + sel + ".png");
            //DisplayAlert("", Convert.ToString("Beads" + sel + ".png"), "1");
            int sonuc = 0;
            sonuc = sel * 2;

            labelSpeed.Text = Convert.ToString("Tesbih çekme hızı: " + sonuc);
            labelLevel.Text = Convert.ToString("Zikir: " + sonuc * 5000);
            labelLevelReplce = Convert.ToInt32(labelLevel.Text.Remove(0, 6));
            isLevelButton(labelLevelReplce);
        }

        private void TapGestureRecognizer_Tapped_right(object sender, EventArgs e)
        {
            if (sel <= 0)
            {
                sel = 1;
                labelSpeed.Text = Convert.ToString("Tesbih çekme hızı " + 0);
                labelLevel.Text = Convert.ToString("Zikir: " + 0);
            }

            sel--;
            imageBeatsView.Source = Convert.ToString("beads" + sel + ".png");
            int sonuc = 0;
            sonuc = sel * 2;

            labelSpeed.Text = Convert.ToString("Tesbih çekme hızı: " + sonuc);
            labelLevel.Text = Convert.ToString("Zikir: " + sonuc * 5000);
            labelLevelReplce = Convert.ToInt32(labelLevel.Text.Remove(0, 6));
            isLevelButton(labelLevelReplce);
        }
        public void isLevelButton(Int32 labelLevelVal)
        {
            if (Preferences.ContainsKey("kayitlevel") == true)
            {
                kayitlevel = Convert.ToInt32(Preferences.Get("kayitlevel", string.Empty));
            }
            if (kayitlevel >= labelLevelVal)
            {
                buttonSeletBeads.IsVisible = true;
            }
            else
            {
                buttonSeletBeads.IsVisible = false;
            }
        }

        private void buttonSeletBeads_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("kayit_beads_name", Convert.ToString(imageBeatsView.Source).Replace("File: ",""));
            Navigation.PushAsync(new MainPage());
        }
    }
}