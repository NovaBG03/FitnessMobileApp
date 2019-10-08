namespace MobileFitness.App.Views
{
    using MobileFitness.App.ViewModels;
    using System;

    using Xamarin.Forms;
    using Xamarin.Forms.PlatformConfiguration;
    using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        private UserToRegister userDto;

        public MainPage(UserToRegister userDto)
        {
            InitializeComponent();
            Init();

            this.userDto = userDto;
        }

        private void Init()
        {
            On<Android>()
                .SetToolbarPlacement(ToolbarPlacement.Bottom);

            SetDefaultPage();

            this.UnselectedTabColor = Constants.UnselectedTabColor;
            this.SelectedTabColor = Constants.SelectedTabColor;

            this.BarBackgroundColor = Constants.BarBackgroundColor;
            this.BarTextColor = Constants.BackgroundColor;
        }

        private void SetDefaultPage()
        {
            var pages = Children.GetEnumerator();
            pages.MoveNext();
            pages.MoveNext();
            CurrentPage = pages.Current;
        }
    }
}