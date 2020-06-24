using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewXamarin
{
    public class ContactsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Contacts> ContactsInfo { get; set; }

        public Command OpenXamlPage { get; set; }

        public ContactsViewModel()
        {
            OpenXamlPage = new Command(OnOpenXamlPage);
            ContactsInfo = new ObservableCollection<Contacts>();
            Random r = new Random();
            for (int i = 0; i < CustomerNames.Length; ++i) 
            {
                var contact = new Contacts(CustomerNames[i], r.Next(720, 799).ToString() + " - " + r.Next(3010, 3999).ToString());
                contact.ContactImage = ImageSource.FromResource("ListViewXamarin.Images.Image" + i + ".png", typeof(MainPage));
                ContactsInfo.Add(contact);
            }
        }

        private void OnOpenXamlPage(object e)
        {
            string page = ((e as Syncfusion.ListView.XForms.ItemTappedEventArgs).ItemData as Contacts).ContactName;
            //"{namespace}.{class name}, {assembly name}"
            //Example "ListViewXamarin.Views.Katie, ListViewXamarin"
            string pageStr = "ListViewXamarin.Views." + page + ", ListViewXamarin";
            var objType = Type.GetType(pageStr);
            var newPage = Activator.CreateInstance(objType) as ContentPage;
            newPage.BindingContext = (e as Syncfusion.ListView.XForms.ItemTappedEventArgs).ItemData as Contacts;
            App.Current.MainPage.Navigation.PushAsync(newPage);
        }

        string[] CustomerNames = new string[] {
            "Irene",
            "Katie",
            "Michael",
            "Fiona",
            "Jennifer",
            "Torrey",
            "Holly",
            "Bill",
        };

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
