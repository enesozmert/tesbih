using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Drawing;
using System.Threading;
using System.Timers;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace teshib
{
    public partial class MainPage : ContentPage
    {
        public int stepperVal = 0;
        public int kayitlevel = 0;
        public int speedVal = 0;
        public string kayitBeadsName;
        public int selecet_click_swipe_key;
        public bool isVibrate;
        public MainPage()
        {
            InitializeComponent();
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
            entryZikr.Text = Preferences.Get("zikr", string.Empty);
            if (Preferences.ContainsKey("kayitlevel") == true)
            {
                kayitlevel = Convert.ToInt32(Preferences.Get("kayitlevel", string.Empty));
            }
            else
            {
                kayitlevel = 0;
            }
            if (Preferences.ContainsKey("selecet_click_swipe") == true)
            {
                if (Preferences.Get("selecet_click_swipe", string.Empty) == "1")
                {
                    selecet_click_swipe_key = 1;
                    DragDropTop.CanDrag = true;
                    DragDropBottom.AllowDrop = true;
                    if (Accelerometer.IsMonitoring == true)
                    {
                        Accelerometer.Stop();
                    }
                }
                else if (Preferences.Get("selecet_click_swipe", string.Empty) == "0")
                {
                    selecet_click_swipe_key = 0;
                    DragDropTop.CanDrag = false;
                    DragDropBottom.AllowDrop = false;
                    if (Accelerometer.IsMonitoring == true)
                    {
                        Accelerometer.Stop();
                    }
                }
                else if (Preferences.Get("selecet_click_swipe", string.Empty) == "2")
                {
                    selecet_click_swipe_key = 2;
                    DragDropTop.CanDrag = false;
                    DragDropBottom.AllowDrop = false;
                    Accelerometer.Start(SensorSpeed.Fastest);
                }
            }
            else
            {
                Preferences.Set("selecet_click_swipe", "1");
            }
            if (Preferences.ContainsKey("is_vibrate") == true)
            {
                if (Preferences.Get("is_vibrate", string.Empty) == "1")
                {
                    isVibrate = true;
                }
                else if (Preferences.Get("is_vibrate", string.Empty) == "0")
                {
                    isVibrate = false;
                }
            }
            else
            {
                Preferences.Set("is_vibrate", "0");
            }
            isBeads();
            imageBeatsTop.Source = Convert.ToString(kayitBeadsName);
            imageBeatsBottom.Source = null;

            labelDateName.Text = "Bu gün " + System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.GetDayName(System.DateTime.Now.DayOfWeek);

            if (stepperVal > 0 && !string.IsNullOrWhiteSpace(entryZikr.Text))
            {
                imageSave.IsVisible = true;
            }
            else
            {
                imageSave.IsVisible = false;
            }
        }
        public void isVibrateVoid(bool isVibrate)
        {
            if (isVibrate == true)
            {
                var duration = TimeSpan.FromSeconds(1);
                Vibration.Vibrate(duration);
            }
            else
            {
                Vibration.Cancel();
            }

        }
        void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            // Çalıştır sallama olayı
            //3. sistem
            if (selecet_click_swipe_key == 2)
            {
                isBeads();
                if (!string.IsNullOrWhiteSpace(entryZikr.Text))
                {
                    imageBeatsTop.Source = null;
                    imageBeatsBottom.Source = Convert.ToString(kayitBeadsName);
                    //DisplayAlert("title", Convert.ToString(imageBeatsBottom.Source), "string");
                }
                else
                {
                    imageBeatsTop.Source = Convert.ToString(kayitBeadsName);
                    imageBeatsBottom.Source = null;
                    labelElhamdulillah.Text = "Zikr-i Şerifi Yazınız!";
                }
#pragma warning disable CS4014 // Bu çağrı beklenmediği için, çağrı tamamlanmadan önce geçerli yöntemin yürütülmesine devam ediliyor
                WaitAndExecute(speedVal, () => ResetTesbih());
#pragma warning restore CS4014 // Bu çağrı beklenmediği için, çağrı tamamlanmadan önce geçerli yöntemin yürütülmesine devam ediliyor
            }
        }
        protected override void OnAppearing()
        {
            //Sayfa açılınca
            NavigationPage.SetHasNavigationBar(this, false);
            isBeads();
#pragma warning disable CS4014 // Bu çağrı beklenmediği için, çağrı tamamlanmadan önce geçerli yöntemin yürütülmesine devam ediliyor
            WaitAndExecute(speedVal * 60, () => adPageNavigate());
#pragma warning restore CS4014 // Bu çağrı beklenmediği için, çağrı tamamlanmadan önce geçerli yöntemin yürütülmesine devam ediliyor
        }
        public void adPageNavigate()
        {
            Navigation.PushAsync(new adPage());
        }
        private void stepperTesbih_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            //stepperTesbih.Value = (double)stepperVal;
            isBeads();
            ResetTesbih();
        }
        private async Task WaitAndExecute(int milisec, Action actionToExecute)
        {
            await Task.Delay(milisec);
            actionToExecute();
        }
        private void DropGestureRecognizer_DragOver(object sender, DragEventArgs e)
        {
            if (selecet_click_swipe_key == 1)
            {
                isBeads();
                imageBeatsTop.Source = Convert.ToString(kayitBeadsName);
                imageBeatsBottom.Source = null;
                labelElhamdulillah.TextColor = System.Drawing.Color.Red;
            }
        }
        private async void DropGestureRecognizer_Drop(object sender, DropEventArgs e)
        {
            //1 sistem.
            if (selecet_click_swipe_key == 1)
            {
                isBeads();
                if (!string.IsNullOrWhiteSpace(entryZikr.Text))
                {
                    imageBeatsTop.Source = null;
                    imageBeatsBottom.Source = Convert.ToString(kayitBeadsName);
                    //DisplayAlert("title", Convert.ToString(imageBeatsBottom.Source), "string");
                }
                else
                {
                    imageBeatsTop.Source = Convert.ToString(kayitBeadsName);
                    imageBeatsBottom.Source = null;
                    labelElhamdulillah.Text = "Zikr-i Şerifi Yazınız!";
                }
                await WaitAndExecute(speedVal, () => ResetTesbih());
            }

        }
        private void ResetTesbih()
        {
            isBeads();
            if (!string.IsNullOrWhiteSpace(entryZikr.Text))
            {
                imageBeatsTop.Source = Convert.ToString(kayitBeadsName);
                imageBeatsBottom.Source = null;
                kayitlevel += 1;
                Preferences.Set("kayitlevel", Convert.ToString(kayitlevel));
                stepperVal += 1;
                //stepperTesbih.Value = stepperVal;
                //DisplayAlert("a",Convert.ToString(stepperVal) , "a");
                labelElhamdulillah.Text = "Allah kabul etsin" + " 99'da " + Convert.ToString(stepperVal) + " level: " + kayitlevel;
                labelElhamdulillah.TextColor = System.Drawing.Color.Black;
                isVibrateVoid(isVibrate);
                if (!string.IsNullOrWhiteSpace(entryZikr.Text) && stepperVal == stepperTesbih.Maximum)
                {
                    App.Database.SavePersonAsync(new Person
                    {
                        Name = entryZikr.Text,
                        Piece = (int)stepperTesbih.Value
                    });
                }
                if (stepperVal >= stepperTesbih.Maximum)
                {
                    entryZikr.Text = "";
                    stepperVal = 0;
                    //stepperTesbih.Value = 0;
                }
                if (stepperVal > 0)
                {
                    imageSave.IsVisible = true;
                }
                else
                {
                    imageSave.IsVisible = false;
                }
            }
            else
            {
                imageBeatsTop.Source = Convert.ToString(kayitBeadsName);
                imageBeatsBottom.Source = null;
                labelElhamdulillah.Text = "Zikr-i Şerifi Yazınız!";
            }
        }

        private void TapGestureRecognizer_Tapped_beadsnew(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TesbihDataViewPage());
        }

        private void TapGestureRecognizer_Tapped_beadsbox(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BeatsPage());
        }

        private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
        {
            if (selecet_click_swipe_key == 1)
            {
                isBeads();
                imageBeatsTop.Source = Convert.ToString(kayitBeadsName);
                imageBeatsBottom.Source = null;
                labelElhamdulillah.TextColor = System.Drawing.Color.Red;
            }
        }

        private void TapGestureRecognizer_Tapped_settings(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage());
        }

        private void entryZikr_TextChanged(object sender, TextChangedEventArgs e)
        {
            Preferences.Set("zikr", entryZikr.Text.ToString());
        }
        public void isBeads()
        {
            if (Preferences.ContainsKey("kayit_beads_name") == true)
            {
                kayitBeadsName = Preferences.Get("kayit_beads_name", string.Empty);
            }
            else
            {
                Preferences.Set("kayit_beads_name", "beads0.png");
                kayitBeadsName = "beads0.png";
            }
            //DisplayAlert("444", Convert.ToString(kayitBeadsName), "444");
            //int sonuc = 0;
            //sonuc = Convert.ToInt32(Preferences.Get("kayitlevel", string.Empty));
            //if (sonuc < 10000)
            //{
            //    Preferences.Set("kayit_beads_name", "beads0.png");
            //    kayitBeadsName = "beads0.png";
            //}

            int sonuc1;
            sonuc1 = Convert.ToInt32(kayitBeadsName.Remove(0, 5).Replace(".png", ""));
            //DisplayAlert("44", Convert.ToString(sonuc1), "44");
            if (sonuc1 != 0)
            {
                speedVal = 10000000 / (Convert.ToInt32(kayitBeadsName.Remove(0, 5).Replace(".png", "")) * 10000);
            }
            else if (sonuc1 == 0)
            {
                speedVal = 1500;
            }

            //DisplayAlert("44",speedVal.ToString(),"44");
        }

        private void TapGestureRecognizer_Tapped_save(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(entryZikr.Text) && stepperVal!=0)
            {
                App.Database.SavePersonAsync(new Person
                {
                    Name = entryZikr.Text,
                    Piece = (int)stepperVal
                });
                adPageNavigate();
            }

        }

        private void TapGestureRecognizer_Tapped_selecet_clicked(object sender, EventArgs e)
        {
            //2. sistem
            if (selecet_click_swipe_key == 0)
            {
                isBeads();
                if (!string.IsNullOrWhiteSpace(entryZikr.Text))
                {
                    imageBeatsTop.Source = null;
                    imageBeatsBottom.Source = Convert.ToString(kayitBeadsName);
                    //DisplayAlert("title", Convert.ToString(imageBeatsBottom.Source), "string");
                }
                else
                {
                    imageBeatsTop.Source = Convert.ToString(kayitBeadsName);
                    imageBeatsBottom.Source = null;
                    labelElhamdulillah.Text = "Zikr-i Şerifi Yazınız!";
                }
#pragma warning disable CS4014 // Bu çağrı beklenmediği için, çağrı tamamlanmadan önce geçerli yöntemin yürütülmesine devam ediliyor
                WaitAndExecute(speedVal, () => ResetTesbih());
#pragma warning restore CS4014 // Bu çağrı beklenmediği için, çağrı tamamlanmadan önce geçerli yöntemin yürütülmesine devam ediliyor
            }
        }
    }
}
