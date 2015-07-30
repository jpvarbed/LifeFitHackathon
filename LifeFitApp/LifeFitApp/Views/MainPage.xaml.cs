using LifeFitApp.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LifeFitApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainPageController mainPageController;
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode =
Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            this.InitMainPage();
        }

        private void InitMainPage()
        {
            mainPageController = new MainPageController();
            this.InitIcon();
        }

        private void InitIcon()
        {
            var bounds = Window.Current.Bounds;
            double height = bounds.Height;
            double width = bounds.Width;

            mainIcon.Width = bounds.Width;
            if (mainIcon.Height > bounds.Height)
            {
                mainIcon.Height = .75f * bounds.Height;
            }
        }

        public void tapAction()
        {
            mainPageController.UpdateModel();
        }

        private void ItemTapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Views.LifeStyleGenreView));
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Views.LifeStyleGenreView));
        }
    }
}
