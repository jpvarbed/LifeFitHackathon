using System;
using System.Collections.Generic;
using System.ComponentModel;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace LifeFitApp.Views
{
    public sealed partial class TitleBar : UserControl
    {
        public TitleBar()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public string title
        {
            get { return (string)GetValue(titleProperty); }
            set {
                SetValueDp(titleProperty, value);
                //ChromeBox.Height = TitleBox.Height;
            }
        }

        public static readonly DependencyProperty titleProperty =
            DependencyProperty.Register("title", typeof(string),
                typeof(TitleBar), null);

        public event DependencyPropertyChangedEventHandler PropertyChanged;
        void SetValueDp(DependencyProperty property, object value,
            [System.Runtime.CompilerServices.CallerMemberNameAttribute] String p = null)
            {
                SetValue(property, value);
                //if (PropertyChanged != null)
                //{
                //    PropertyChanged(this, new DependencyPropertyChangedEventArgs(value, p));
                //}
            }
    }
}
