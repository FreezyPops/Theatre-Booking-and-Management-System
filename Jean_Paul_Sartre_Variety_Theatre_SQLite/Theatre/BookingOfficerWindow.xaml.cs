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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class BookingOfficerWindow : Window
    {
        DataSet dataSet;
        public BookingOfficerWindow()
        {
            InitializeComponent();
            fillDatePicker(this.SelectDate_EditDelShow);
            this.StartDate_AddShowing.BlackoutDates.AddDatesInPast();
            this.EndDate_AddShowing.BlackoutDates.AddDatesInPast();
            //DateTime nextStartDate = DateTime.Now;
            //dataSet = ShowingsClass.getAllShowings();
            //this.SelectDate_EditDelShow.BlackoutDates.AddDatesInPast();
            //foreach (DataRow row in dataSet.Tables[0].Rows)
            //{
            //    DateTime showingDateFromList = ShowingsClass.turnSqlLiteDateStringIntoDateTime(row["Date"].ToString());
            //    this.SelectDate_EditDelShow.BlackoutDates.Add(new CalendarDateRange(nextStartDate, showingDateFromList.AddDays(-1)));
            //    nextStartDate = showingDateFromList.AddDays(1);
            //}
            //this.SelectDate_EditDelShow.BlackoutDates.Add(new CalendarDateRange(nextStartDate, nextStartDate.AddYears(500)));
        }
        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UpperCircle_AddShowing_txt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void fillDatePicker(DatePicker pDatePicker)
        {
            pDatePicker.BlackoutDates.Clear();
            pDatePicker.BlackoutDates.AddDatesInPast();
            List<DateTime> dateList = new List<DateTime>();
            DataSet dataSet = ShowingsClass.getAllShowingsPastCurrentDate();

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                dateList.Add(ShowingsClass.turnSqlLiteDateStringIntoDateTime(row["Date"].ToString()));
            }
            dateList.Sort();

            if (dateList.Count == 0)
            {
                pDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.Now, DateTime.Now.AddYears(1)));
            }
            else
            {
                DateTime startDate = DateTime.Now;
                for (int i = 0; i < dateList.Count; i++)
                {
                    if (startDate.Date != dateList[i].Date)
                    {
                        pDatePicker.BlackoutDates.Add(new CalendarDateRange(startDate, dateList[i].AddDays(-1)));
                    }     
                    startDate = dateList[i].AddDays(1);
                }
                pDatePicker.BlackoutDates.Add(new CalendarDateRange(startDate, startDate.AddMonths(12)));
            }


            //pDatePicker.BlackoutDates.Clear();
            //pDatePicker.BlackoutDates.AddDatesInPast();
            //List<DateTime> dateList = new List<DateTime>();
            //DataSet dataSet = ShowingsClass.getAllShowings();
            //foreach (DataRow row in dataSet.Tables[0].Rows)
            //{
            //    int tempShowingId = int.Parse(row["Showing_Id"].ToString());
            //    DateTime showingDate = ShowingsClass.turnSqlLiteDateStringIntoDateTime(row["Date"].ToString());
            //    if (showingDate >= DateTime.Now)
            //    {
            //        dateList.Add(showingDate);
            //    }
            //}
            //dateList.Sort();
            //if (dateList.Count != 0)
            //{
            //    DateTime nextStartDate = DateTime.Now;
            //    bool addDate = true;
            //    int count = 1;
            //    foreach (DateTime showingDateFromList in dateList)
            //    {
            //        nextStartDate = showingDateFromList;
            //        if (count < dateList.Count)
            //        {
            //            //nextStartDate = showingDateFromList;
            //            if (showingDateFromList.AddDays(1).Date == dateList[count].Date)
            //            {
            //                //next date in list is for next day so do nothing
            //            }
            //            else
            //            {
            //                pDatePicker.BlackoutDates.Add(new CalendarDateRange(nextStartDate.AddDays(1), dateList[count].AddDays(-1)));
            //            }
            //            count = count + 1;
            //        }
            //    }
            //    pDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.Now, dateList[0].AddDays(-1)));
            //    //pDatePicker.BlackoutDates.Add(new CalendarDateRange(dateList[dateList.Count - 1].AddDays(1), dateList[dateList.Count - 1].AddMonths(12)));
            //    try
            //    {
            //        pDatePicker.BlackoutDates.Add(new CalendarDateRange(dateList[count - 1].AddDays(1), nextStartDate.AddYears(1)));
            //    }
            //    catch (Exception)
            //    {
            //        //pDatePicker.BlackoutDates.Add(new CalendarDateRange(dateList[count - 1].AddDays(1), nextStartDate.AddYears(1)));
            //        pDatePicker.BlackoutDates.Add(new CalendarDateRange(nextStartDate.AddDays(1), nextStartDate.AddYears(1)));
            //    }
            //}
            //else
            //{
            //    pDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.Now, DateTime.Now.AddYears(500)));
            //}
        }

        //-----------------------------------Add Showing ---------------------------------//
        private void AddShowingTabSearchPlay(object sender, RoutedEventArgs e)
        {
            this.Play_AddShowing_combo.Items.Clear();            
            if (Play_Name_AddShowing_txt.Text.ToString() == "")
            {
                MessageBox.Show("Please enter the name of the play to search for");
            }
            else
            {
                dataSet = PlaysClass.getPlayDetailsByName(this.Play_Name_AddShowing_txt.Text.ToString());
                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Play not found - Try entering another play or check spelling");
                }
                else
                {
                    this.Play_AddShowing_combo.SelectedValue = "Key";
                    this.Play_AddShowing_combo.DisplayMemberPath = "Value";
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        this.Play_AddShowing_combo.Items.Add(new KeyValuePair<int, string>(int.Parse(row["Play_Id"].ToString()), row["Name"].ToString()));
                    }
                }
            }
                   
        }

        private void CreateShowing_button_Click(object sender, RoutedEventArgs e)
        {
            if (Play_AddShowing_combo.Items.Count == 0)
            {
                MessageBox.Show("Please enter a play to search for");
            }
            else
            {
                KeyValuePair<int, string> test = (KeyValuePair<int, string>)this.Play_AddShowing_combo.SelectedValue;
                int playId = int.Parse((test.Key).ToString());

                dataSet = PlaysClass.getPlayDetailsById(playId);
                int playLength = int.Parse(dataSet.Tables[0].Rows[0]["Length"].ToString());

                string startTime = StartTime_AddShowing_txt.Text;
                string timeToBeChecked = ValidationClass.TimeChecker(startTime);

                if (timeToBeChecked != null)
                {
                    MessageBox.Show(timeToBeChecked);
                }
                else
                {
                    //var startTimeCharArray = startTime.ToCharArray();
                    //string startHour = startTimeCharArray[0].ToString() + startTimeCharArray[1].ToString();
                    //string startMinutes = startTimeCharArray[3].ToString() + startTimeCharArray[4].ToString();

                    //DateTime startDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                    //    int.Parse(startHour), int.Parse(startMinutes), 0);

                    //DateTime endDateTime = startDateTime.AddMinutes(playLength);
                    //string endTime = endDateTime.Hour + ":" + endDateTime.Minute;

                    DateTime? startDate = this.StartDate_AddShowing.SelectedDate;
                    DateTime? endDate = this.EndDate_AddShowing.SelectedDate;
                    if (startDate > endDate)
                    {
                        MessageBox.Show("Start date must be earlier than End date");
                    }
                    else
                    {
                        string priceToBeChecked = UpperCircle_AddShowing_txt.Text.ToString();
                        priceToBeChecked = ValidationClass.FloatChecker(priceToBeChecked);
                        if (priceToBeChecked != null)
                        {
                            MessageBox.Show(priceToBeChecked);
                        }
                        else
                        {
                            float upperCirclePrice = float.Parse(this.UpperCircle_AddShowing_txt.Text.ToString());

                            priceToBeChecked = DressCircle_AddShowing_txt.Text.ToString();
                            priceToBeChecked = ValidationClass.FloatChecker(priceToBeChecked);
                            if (priceToBeChecked != null)
                            {
                                MessageBox.Show(priceToBeChecked);
                            }
                            else
                            {
                                float dressCirclePrice = float.Parse(this.DressCircle_AddShowing_txt.Text.ToString());

                                priceToBeChecked = Stalls_AddShowing_txt.Text.ToString();
                                priceToBeChecked = ValidationClass.FloatChecker(priceToBeChecked);
                                if (priceToBeChecked != null)
                                {
                                    MessageBox.Show(priceToBeChecked);
                                }
                                else
                                {
                                    float stallPrice = float.Parse(this.Stalls_AddShowing_txt.Text.ToString());
                                    if (this.StartDate_AddShowing.SelectedDate == null || this.EndDate_AddShowing.SelectedDate == null)
                                    {
                                        MessageBox.Show("please enter start and end date");
                                    }
                                    else
                                    {
                                        bool sqlQuerryState = ShowingsClass.makeShowings((DateTime)startDate, (DateTime)endDate, startTime, playId, upperCirclePrice, dressCirclePrice, stallPrice);
                                        if (sqlQuerryState == true)
                                        {
                                            MessageBox.Show("Showing added");
                                            fillDatePicker(this.SelectDate_EditDelShow);
                                        }
                                        else
                                        {
                                            MessageBox.Show("conclict with current showing");
                                        }
                                    }
                                }
                            }
                        }
                    }                    
                }
            }                       
        }

        //-----------------------------------Add Showing ---------------------------------//

        //--------------------------------Edit/Delete Showing-----------------------------//
        private void SelectDate_EditDelShow_SelectionDateChanged(object sender, RoutedEventArgs e)
        {
            if (SelectDate_EditDelShow.SelectedDate != null)
            {
                int showingId = ShowingsClass.getShowingIdByDate(ShowingsClass.formatDateTimeToSqlLiteDateString((DateTime)SelectDate_EditDelShow.SelectedDate));
                dataSet = ShowingsClass.getShowingByShowingId(showingId);

                string dateTime = dataSet.Tables[0].Rows[0]["Date"].ToString();
                this.TimeOfShowing_EditDeleteShowing_txt.Text = ShowingsClass.getTimeFromSqlDateSting(dateTime);
                this.UpperCircle_EditDelShow_txt.Text = dataSet.Tables[0].Rows[0]["UpperCirclePrice"].ToString();
                this.DressCircle_EditDelShow_txt.Text = dataSet.Tables[0].Rows[0]["DressCirclePrice"].ToString();
                this.Stalls_EditDelShow_txt.Text = dataSet.Tables[0].Rows[0]["StallsPrice"].ToString();
            }
        }

        private void DeleteShowing_button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectDate_EditDelShow.SelectedDate == null)
            {
                MessageBox.Show("Please select a date for the required showing");
            }
            else
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    ShowingsClass.deleteShowing(ShowingsClass.getShowingIdByDate(ShowingsClass.formatDateTimeToSqlLiteDateString((DateTime)SelectDate_EditDelShow.SelectedDate)));
                    MessageBox.Show("Showing deleted");
                    this.Close();
                    BookingOfficerWindow newBookingOfficerWindow = new BookingOfficerWindow();
                    newBookingOfficerWindow.Show();
                    //fillDatePicker(this.SelectDate_EditDelShow);
                }
                else
                {
                    MessageBox.Show("Delete cancelled");
                }
            }
        }

        private void EditShowing_button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectDate_EditDelShow.SelectedDate == null)
            {
                MessageBox.Show("Please select a date for the required showing");
            }
            else
            {
                int showingId = ShowingsClass.getShowingIdByDate(ShowingsClass.formatDateTimeToSqlLiteDateString((DateTime)SelectDate_EditDelShow.SelectedDate));
                string startTime = this.TimeOfShowing_EditDeleteShowing_txt.Text;
                string timeToBeChecked = ValidationClass.TimeChecker(startTime);

                if (timeToBeChecked != null)
                {
                    MessageBox.Show(timeToBeChecked);
                }
                else
                {
                    double upperCirclePrice = double.Parse(this.UpperCircle_EditDelShow_txt.Text);
                    string priceToBeChecked = UpperCircle_EditDelShow_txt.Text.ToString();
                    priceToBeChecked = ValidationClass.FloatChecker(priceToBeChecked);
                    if (priceToBeChecked != null)
                    {
                        MessageBox.Show(priceToBeChecked);
                    }
                    else
                    {
                        double dressCirclePrice = double.Parse(this.DressCircle_EditDelShow_txt.Text);
                        priceToBeChecked = DressCircle_EditDelShow_txt.Text.ToString();
                        priceToBeChecked = ValidationClass.FloatChecker(priceToBeChecked);
                        if (priceToBeChecked != null)
                        {
                            MessageBox.Show(priceToBeChecked);
                        }
                        else
                        {
                            double stallPrice = double.Parse(this.Stalls_EditDelShow_txt.Text);
                            priceToBeChecked = Stalls_EditDelShow_txt.Text.ToString();
                            priceToBeChecked = ValidationClass.FloatChecker(priceToBeChecked);
                            if (priceToBeChecked != null)
                            {
                                MessageBox.Show(priceToBeChecked);
                            }
                            else
                            {
                                dataSet = ShowingsClass.getShowingByShowingId(showingId);
                                string unedditedSqlDateTimeString = dataSet.Tables[0].Rows[0]["Date"].ToString();
                                string newDateTimeSqlString = ShowingsClass.replaceSqlDateTimeWithNewTime(unedditedSqlDateTimeString, startTime);
                                ShowingsClass.editShowingDetails(showingId, newDateTimeSqlString, upperCirclePrice, dressCirclePrice, stallPrice);
                                MessageBox.Show("Showing edited");
                            }
                        }
                    }
                }
            }
            
            
        }
        //--------------------------------Edit/Delete Showing-----------------------------//

        //-------------------------------------Add Play-----------------------------------//
        private void CreatePlay_AddPlay_button_Click(object sender, RoutedEventArgs e)
        {
            string playName = this.PlayName_AddPlay_txt.Text;
            if (playName.Length == 0)
            {
                MessageBox.Show("Please enter a Play name");
            }
            else
            {
                if (PlaysClass.checkPlayExists(playName) == true)
                {
                    MessageBox.Show("This Play already exists");
                }
                else
                {
                    string playLengthToBeChecked = Length_AddPlay_txt.Text.ToString();
                    if (playLengthToBeChecked.Length == 0)
                    {
                        MessageBox.Show("Please enter a {Play length");
                    }
                    else
                    {
                        playLengthToBeChecked = ValidationClass.IntChecker(playLengthToBeChecked);
                        if (playLengthToBeChecked != null)
                        {
                            MessageBox.Show(playLengthToBeChecked);
                        }
                        else
                        {
                            try
                            {
                                int playLengthToBeCheckedInt = int.Parse(Length_AddPlay_txt.Text.ToString());
                                if (playLengthToBeCheckedInt > 400)
                                {
                                    MessageBox.Show("Play length too high - max length 400 minutes");
                                }
                                else
                                {
                                    int playLength = int.Parse(this.Length_AddPlay_txt.Text.ToString());
                                    PlaysClass.addPlay(playName, playLength);
                                    this.PlayName_AddPlay_txt.Text = "";
                                    this.Length_AddPlay_txt.Text = "";
                                    MessageBox.Show("Play added");
                                }
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Play length too high - max length 400 minutes");
                            }                                                     
                        }
                    }
                }                                               
            }            
        }
        //-------------------------------------Add Play-----------------------------------//

        //--------------------------------Edit/Delete Play--------------------------------//
        private void PlaySearch_EditDelPlay_button_Click(object sender, RoutedEventArgs e)
        {
            this.Play_EditDelPlay_combo.Items.Clear();
            string playNameSearchBy = this.PlayName_EditDelPlay_txt.Text;
            if (playNameSearchBy.Length == 0)
            {
                MessageBox.Show("Please eter a play to search for");
            }
            else
            {
                dataSet = PlaysClass.getPlayDetailsByName(playNameSearchBy);
                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Play not found - Try entering another play or check spelling");
                }
                else
                {
                    this.Play_EditDelPlay_combo.SelectedValue = "Key";
                    this.Play_EditDelPlay_combo.DisplayMemberPath = "Value";
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        this.Play_EditDelPlay_combo.Items.Add(new KeyValuePair<int, string>(int.Parse(row["Play_Id"].ToString()), row["Name"].ToString()));
                    }
                }                
            }
        }            

        private void EditPlay_button_Click(object sender, RoutedEventArgs e)
        {
            if (Play_EditDelPlay_combo.Items.Count == 0)
            {
                MessageBox.Show("Please enter a Play to search for");
            }
            else
            {
                KeyValuePair<int, string> test = (KeyValuePair<int, string>)this.Play_EditDelPlay_combo.SelectedValue;
                int playId = int.Parse((test.Key).ToString());
                string playLengthToBeChecked = this.Length_EditDelPlay_txt.Text.ToString();
                if (playLengthToBeChecked.Length == 0)
                {
                    MessageBox.Show("Please enter a play length");
                }
                else
                {
                    playLengthToBeChecked = ValidationClass.IntChecker(playLengthToBeChecked);
                    if (playLengthToBeChecked != null)
                    {
                        MessageBox.Show(playLengthToBeChecked);
                    }
                    else
                    {
                        try
                        {
                            int playLengthToBeCheckedInt = int.Parse(Length_EditDelPlay_txt.Text.ToString());
                            if (playLengthToBeCheckedInt > 400)
                            {
                                MessageBox.Show("Play length too high - max length 400 minutes");
                            }
                            else
                            {
                                int newPlayLength = int.Parse(this.Length_EditDelPlay_txt.Text);
                                PlaysClass.editPlay(playId, this.PlayName_EditDelPlay_txt.Text, newPlayLength);

                                this.Length_EditDelPlay_txt.Text = "";
                                this.Play_EditDelPlay_combo.Items.Clear();
                                this.PlayName_EditDelPlay_txt.Text = "";
                                MessageBox.Show("Play edited");
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Play length too high - max legth 400 minutes");
                        }                       
                    }                    
                }
            }            
        }

        private void DeletePlay_button_Click(object sender, RoutedEventArgs e)
        {
            if (Play_EditDelPlay_combo.Items.Count == 0)
            {
                MessageBox.Show("Please enter a Play to search for");
            }
            else
            {
                KeyValuePair<int, string> test = (KeyValuePair<int, string>)this.Play_EditDelPlay_combo.SelectedValue;
                int playId = int.Parse((test.Key).ToString());

                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    PlaysClass.deletePlay(playId);

                    this.Length_EditDelPlay_txt.Text = "";
                    this.Play_EditDelPlay_combo.Items.Clear();
                    this.PlayName_EditDelPlay_txt.Text = "";
                    MessageBox.Show("Play deleted");
                }
                else
                {
                    MessageBox.Show("Delete cancelled");
                }
            }
        }

        private void Play_EditDelPlay_combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.Play_EditDelPlay_combo.SelectedItem != null)
            {
                KeyValuePair<int, string> test = (KeyValuePair<int, string>)this.Play_EditDelPlay_combo.SelectedValue;
                int playId = int.Parse((test.Key).ToString());

                dataSet = PlaysClass.getPlayDetailsById(playId);
                this.Length_EditDelPlay_txt.Text = dataSet.Tables[0].Rows[0]["Length"].ToString();
            }
        }
        //--------------------------------Edit/Delete Play--------------------------------//
    }
}

