using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Vacit.View;
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

namespace Vacit
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MenuPage : Page
    {
        public MenuPage()
        {
            this.InitializeComponent();
            ChosenFrame.Navigate(typeof(ChildView));
            //TitleTextBlock.Text = "Financial";
           
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            SplitViewMenu.IsPaneOpen = !SplitViewMenu.IsPaneOpen;
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxItemOverview.IsSelected)
            {

                ChosenFrame.Navigate(typeof(ChildView));
                //TitleTextBlock.Text = "Financial";
            }
            else if (ListBoxItemAddChild.IsSelected)
            {
                ChosenFrame.Navigate(typeof(AddChildView));
                //TitleTextBlock.Text = "Food";
            }
            else if (VaccinesInfoMenuItem.IsSelected)
            {
                ChosenFrame.Navigate(typeof(VaccinesInfoView));
            }
            else if (DoctorsMenuItem.IsSelected)
            {
                ChosenFrame.Navigate(typeof(DoctorsView));
            }
        }
    }
}