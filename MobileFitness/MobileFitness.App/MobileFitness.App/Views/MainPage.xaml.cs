namespace MobileFitness.App.Views
{
    using MobileFitness.App.ViewModels;
    using MobileFitness.Models;
    using System;

    using Xamarin.Forms;
    using Xamarin.Forms.PlatformConfiguration;
    using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Главна страница
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        /// <summary>
        /// Създава нова страница
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// Инициализира ViewModel за страницата
        /// </summary>
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

        /// <summary>
        /// Задава страницата по подразбиране
        /// </summary>
        private void SetDefaultPage()
        {
            var pages = this.Children.GetEnumerator();
            pages.MoveNext();
            pages.MoveNext();
            this.CurrentPage = pages.Current;
        }
    }
}