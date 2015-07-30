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
using LifeFitApp.Controller;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LifeFitApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LifeStyleListView : Page
    {
        LifeStyleListController controller;
        public LifeStyleListView()
        {
            this.InitializeComponent();
            this.NavigationCacheMode =
        Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            controller = new LifeStyleListController(e.Parameter);
            this.InitListsView();
        }

        private void InitListsView()
        {
            StyleListTitleBox.DataContext = controller;
            styleList.ItemsSource = controller.lifeLists;
        }

        private void BackTapped(object sender, TappedRoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private void ItemTapped(object sender, TappedRoutedEventArgs e)
        {
            FrameworkElement block = sender as FrameworkElement;
            this.Frame.Navigate(typeof(Views.LifeStyleDescriptionView), block.DataContext);
        }
    }
}
