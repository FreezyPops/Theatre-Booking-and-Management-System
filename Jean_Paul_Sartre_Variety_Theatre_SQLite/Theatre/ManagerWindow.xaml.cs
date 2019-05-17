using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using Jean_Paul_Sartre_Variety_Theatre;

namespace Theatre
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
        }
        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Generate2_button_Click(object sender, RoutedEventArgs e)
        {
            DataSet dataSet = ShowingsClass.getAllShowingsPastCurrentDate();
            this.Report_txt.ItemsSource = dataSet.Tables[0].DefaultView;
            this.Report_txt.Columns[0].Visibility = Visibility.Collapsed;
            this.Report_txt.Columns[1].Visibility = Visibility.Collapsed;
            this.Report_txt.Columns[6].Visibility = Visibility.Collapsed;
        }

        private void Generate3_button_Click(object sender, RoutedEventArgs e)
        {
            DataSet dataSet = CustomerClass.getCustomersMembershipExpiry();
            this.Report_txt.ItemsSource = dataSet.Tables[0].DefaultView;
        }

        private void Generate1_button_Click(object sender, RoutedEventArgs e)
        {
            DataSet dataSet = SeatsClass.seatsSoldUnsold();
            this.Report_txt.ItemsSource = dataSet.Tables[0].DefaultView;
            this.Report_txt.Columns[0].Visibility = Visibility.Collapsed;
            this.Report_txt.Columns[1].Visibility = Visibility.Collapsed;
        }
    }
}
