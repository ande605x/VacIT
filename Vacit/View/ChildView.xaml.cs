﻿using System;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Vacit
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChildView : Page
    {
        public ChildView()
        {
            this.InitializeComponent();
        }


        private void ClickToAddChildView(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddChildView));
        }

        private void NameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {           
            //var combo = (ComboBox)sender;
            
            //var item = (ComboBoxItem)combo.SelectedItem;
            //ChildNameHeadline.Text = item.Content.ToString();
            
            //var item = combo.SelectedItem;
            //ChildNameHeadline.Text = item.ToString();
            
            

        }

        private void textBox1_Copy1_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
