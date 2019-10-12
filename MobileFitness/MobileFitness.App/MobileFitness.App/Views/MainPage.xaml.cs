namespace MobileFitness.App.Views
{
    using MobileFitness.App.ViewModels;
    using MobileFitness.Models;
    using System;

    using Xamarin.Forms;
    using Xamarin.Forms.PlatformConfiguration;
    using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            Init();
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
            var pages = this.Children.GetEnumerator();
            pages.MoveNext();
            pages.MoveNext();
            this.CurrentPage = pages.Current;
        }
    }
}