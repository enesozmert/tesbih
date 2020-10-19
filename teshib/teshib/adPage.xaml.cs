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
    public partial class adPage : ContentPage
    {
        public adPage()
        {
            InitializeComponent();
        }
        private async Task WaitAndExecute(int milisec, Action actionToExecute)
        {
            await Task.Delay(milisec);
            actionToExecute();
        }
        protected override void OnAppearing()
        {

            Uri contoso = new Uri("https://efyaz.com/uri/Gonderi/uri_banner.aspx");

#pragma warning disable CS4014 // Bu çağrı beklenmediği için, çağrı tamamlanmadan önce geçerli yöntemin yürütülmesine devam ediliyor
            OpenBrowser(contoso);
#pragma warning restore CS4014 // Bu çağrı beklenmediği için, çağrı tamamlanmadan önce geçerli yöntemin yürütülmesine devam ediliyor
#pragma warning disable CS4014 // Bu çağrı beklenmediği için, çağrı tamamlanmadan önce geçerli yöntemin yürütülmesine devam ediliyor
            WaitAndExecute(30000, () => MainPageNavigate());
#pragma warning restore CS4014 // Bu çağrı beklenmediği için, çağrı tamamlanmadan önce geçerli yöntemin yürütülmesine devam ediliyor
        }
        public void MainPageNavigate()
        {
            Navigation.PushAsync(new MainPage());
        }
        public async Task OpenBrowser(Uri uri)
        {
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
    }
}