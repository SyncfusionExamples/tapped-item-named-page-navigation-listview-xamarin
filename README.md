# How to make navigation for a corresponding tapped item named page in Xamarin.Forms ListView (SfListView)

You can navigate to the particular xaml page based on the tapped item data (name of the page) using [Activator.CreateInstance](https://docs.microsoft.com/en-us/dotnet/api/system.activator.createinstance?view=netcore-3.1) in Xamarin.Forms [SfListView](https://help.syncfusion.com/xamarin/listview/overview?).

**XAML**

Defined [TapCommand](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~TapCommand.html?) in **ListView**.
``` xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:ListViewXamarin"
             xmlns:listView="clr-namespace:Syncfusion.ListView.XForms;assembly=Syncfusion.SfListView.XForms"
             x:Class="ListViewXamarin.MainPage">
 
    <ContentPage.BindingContext>
        <local:ContactsViewModel x:Name="viewModel"/>
    </ContentPage.BindingContext>
 
    <ContentPage.Content>
        <Grid>
            <listView:SfListView x:Name="list" TapCommand="{Binding OpenXamlPage}" ItemsSource="{Binding ContactsInfo}" SelectionMode="Single" AutoFitMode="DynamicHeight">
                <listView:SfListView.ItemTemplate>
                    <DataTemplate x:Name="ItemTemplate" >
                        <ViewCell>
                            <Grid x:Name="grid" RowSpacing="1">
                                <Grid.RowDefinitions>
                                    <RowDefinition Height="*" />
                                    <RowDefinition Height="1" />
                                </Grid.RowDefinitions>
                                <Grid RowSpacing="1">
                                    <Grid.ColumnDefinitions>
                                        <ColumnDefinition Width="50" />
                                        <ColumnDefinition Width="*" />
                                    </Grid.ColumnDefinitions>
                                    <Image Source="{Binding ContactImage}" VerticalOptions="Center" HorizontalOptions="Center" HeightRequest="50"/>
                                    <Grid Grid.Column="1" VerticalOptions="Center">
                                        <Grid.RowDefinitions>
                                            <RowDefinition Height="*" />
                                            <RowDefinition Height="*" />
                                        </Grid.RowDefinitions>
                                        <Label Text="{Binding ContactName}"/>
                                        <Label Grid.Row="1" Grid.Column="0" Text="{Binding ContactNumber}"/>
                                    </Grid>
                                </Grid>
                                <StackLayout Grid.Row="1" BackgroundColor="Black" HeightRequest="1"/>
                            </Grid>
                        </ViewCell>
                    </DataTemplate>
                </listView:SfListView.ItemTemplate>
            </listView:SfListView>
        </Grid>
    </ContentPage.Content>
</ContentPage>
```
**C#**

Using **Activator.CreateInstance** to navigate to created page instance dynamically.
``` c#
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
```
**Output**

![TappedPageNavigation](https://github.com/SyncfusionExamples/tapped-item-named-page-navigation-listview-xamarin/blob/master/ScreenShots/TappedPageNavigation.gif)
