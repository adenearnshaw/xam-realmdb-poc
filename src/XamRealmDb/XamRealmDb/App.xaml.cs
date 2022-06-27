using Xamarin.Forms;
using XamRealmDb.Views;

namespace XamRealmDb
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

    }
}
