using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Xamarin.Forms;
using XamRealmDb.ViewModels;

namespace XamRealmDb.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = Ioc.Default.GetService<MainViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as MainViewModel).OnAppearingCommand.Execute(null);
        }
    }
}
