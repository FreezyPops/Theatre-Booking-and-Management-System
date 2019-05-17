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
    /// Interaction logic for NewsletterEditor.xaml
    /// </summary>
    public partial class NewsletterEditorWindow : Window
    {
        public NewsletterEditorWindow()
        {
            InitializeComponent();
        }

        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LoadGoldClubMembers_button_Click(object sender, RoutedEventArgs e)
        {
            this.GoldClubMembers_txt.Text = "";
            DataSet dataSet = CustomerClass.getCustomersGoldClubMembers();
            foreach(DataRow row in dataSet.Tables[0].Rows)
            {
                this.GoldClubMembers_txt.Text = this.GoldClubMembers_txt.Text + row["First_Name"].ToString() + " " + row["Last_Name"].ToString() +  " - " + row["Email"].ToString() + "\n";

            }
        }

        private void LoadNewsletters_button_Click(object sender, RoutedEventArgs e)
        {
            DataSet dataset = ShowingsClass.getAllShowingsPastCurrentDate();
            foreach (DataRow row in dataset.Tables[0].Rows)
            {
                this.Newsletters_txt.Text = this.Newsletters_txt.Text + row["Name"].ToString() + " " + row["Date"].ToString() + "\n";
            }
        }
    }

}
