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
    public sealed partial class WeeklyView : Page
    {
        WeeklyController controller;
        public WeeklyView()
        {
            this.InitializeComponent();
            this.NavigationCacheMode =
        Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            controller = new WeeklyController(e.Parameter);
            this.InitListsView();
            this.ResizeCalendar();
        }

        private void InitListsView()
        {
            Title.DataContext = controller.name;
            WorkoutName.DataContext = controller.exercise.name;
            WorkoutImage.DataContext = controller.exercise.imageFixedThumbPath;

            MealName.DataContext = controller.meal.name;
            MealImage.DataContext = controller.meal.imageFixedThumbPath;

            IngredientsList.ItemsSource = controller.meal.ingredientsList;
            //ShowNextAcitivty();
        }

        private void ResizeCalendar()
        {
            var bounds = Window.Current.Bounds;
            double width = bounds.Width;

            FakeCalendar.Width = bounds.Width;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.ShowNextAcitivty();
        }

        private void MealChecked(object sender, RoutedEventArgs e)
        {
            this.ShowNextAcitivty();
        }

        private void WorkoutChecked(object sender, RoutedEventArgs e)
        {
            this.ShowNextAcitivty();
        }

        private void ShowNextAcitivty()
        {
            controller.UpdateActivity();
            //ActivityName.DataContext = controller.activityName;
            //ActivityImage.DataContext = controller.activityImage;
        }

        private void BackTapped(object sender, TappedRoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private void MealTapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Views.MealView), controller.meal);
        }

        private void WorkoutTapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Views.MealView), controller.exercise);
        }

        private void ActivityTapped(object sender, TappedRoutedEventArgs e)
        {

            FrameworkElement block = sender as FrameworkElement;
            object sendObj;
            bool isMeal = controller.IsObjectMeal(block.DataContext, out sendObj);
            if (isMeal)
            {
                this.Frame.Navigate(typeof(Views.MealView), sendObj);
            }
            else
            {
                this.Frame.Navigate(typeof(Views.MealView), sendObj);
            }
        }
    }
}
