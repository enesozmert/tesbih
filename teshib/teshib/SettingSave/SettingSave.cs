using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
namespace teshib
{
    public class SettingSave
    {
        public string getSave { get; set; }
        public void save(string name,string value)
        {
            Preferences.Set(name, value);
        }
        public void open(string name, string value)
        {
            getSave = Preferences.Get(name, value);
        }
    }
}
