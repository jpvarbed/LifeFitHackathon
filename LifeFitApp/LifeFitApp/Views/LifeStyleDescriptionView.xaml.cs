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
    public sealed partial class LifeStyleDescriptionView : Page
    {
        LifeStyleDescriptionController controller;
        public LifeStyleDescriptionView()
        {
            this.InitializeComponent();
            this.NavigationCacheMode =
        Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            controller = new LifeStyleDescriptionController(e.Parameter);
            this.InitListsView();
        }

        private void InitListsView()
        {
            mainImage.DataContext = controller;
            workoutTimeTextBlock.DataContext = controller;
            mealTimeTextBlock.DataContext = controller;

            Title.DataContext = controller;
            listDescription.DataContext = controller;
            MealsList.ItemsSource = controller.mealPlan.meals;
            WorkoutsList.ItemsSource = controller.exercisePlan.exercises;

            this.ResizeLists();
        }

        private void ResizeLists()
        {
            var bounds = Window.Current.Bounds;
            double height = bounds.Height;
            double width = bounds.Width;

            MealsList.Width = width;
            WorkoutsList.Width = width;
            summaryList.Width = width;
            listDescription.Width = width;
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
            FrameworkElement block = sender as FrameworkElement;
            this.Frame.Navigate(typeof(Views.MealView), block.DataContext);
        }

        private void WorkoutTapped(object sender, TappedRoutedEventArgs e)
        {
            FrameworkElement block = sender as FrameworkElement;
            this.Frame.Navigate(typeof(Views.WorkoutView), block.DataContext);
        }
    }
}
