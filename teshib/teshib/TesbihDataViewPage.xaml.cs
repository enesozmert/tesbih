using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Xamarin.Essentials;

namespace teshib
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TesbihDataViewPage : ContentPage
    {
        public TesbihDataViewPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetPeopleAsync();
        }       

        private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listViewItemId = listView.ItemsSource;
            DisplayAlert("EEE", listViewItemId.ToString(), "EEE");
        }
    }
}