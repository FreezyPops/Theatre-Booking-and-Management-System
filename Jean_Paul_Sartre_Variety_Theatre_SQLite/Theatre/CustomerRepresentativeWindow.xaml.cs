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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CustomerRepresentativeWindow: Window
    {
        DataSet dataSet = new DataSet();
        DataTable seatBookingsDataset = new DataTable();
        //dataset = get plays do somthing with it
        //dataset = get customers now no longer hold previous data only hold the new data

            
        public CustomerRepresentativeWindow()
        {
            InitializeComponent();
            SelectDate_Booking.BlackoutDates.AddDatesInPast();

            seatBookingsDataset.Columns.Add("Section");
            seatBookingsDataset.Columns.Add("Row");
            seatBookingsDataset.Columns.Add("Number");
            DataGridBasket.ItemsSource = seatBookingsDataset.DefaultView;
        }

        private void fillDatePicker(DatePicker pDatePicker, int pId)
        {
            pDatePicker.BlackoutDates.Clear();
            pDatePicker.BlackoutDates.AddDatesInPast();
            List<DateTime> dateList = new List<DateTime>();
            DataSet dataSet = ShowingsClass.getShowingForPlays(pId);
            //DataSet dataSet = ShowingsClass.getAllShowings();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                int tempShowingId = int.Parse(row["Showing_Id"].ToString());
                DateTime showingDate = ShowingsClass.turnSqlLiteDateStringIntoDateTime(row["Date"].ToString());
                if (showingDate >= DateTime.Now)
                {
                    dateList.Add(showingDate);
                }
            }
            dateList.Sort();

            //if (dateList.Count != 0)
            //{
            //    DateTime nextStartDate = DateTime.Now.AddDays(-1);
            //    int count = 1;
            //    foreach (DateTime showingDateFromList in dateList)
            //    {
            //        if (count < dateList.Count)
            //        {
            //            nextStartDate = showingDateFromList;
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
            //    pDatePicker.BlackoutDates.Add(new CalendarDateRange(dateList[count - 1].AddDays(1), nextStartDate.AddYears(500)));
            //    pDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.Now, dateList[0].AddDays(-1)));
            //}
            //else
            //{
            //    pDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.Now, DateTime.Now.AddYears(500)));
            //}

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
        }

        //---------------------------------------------- Booking Tab ------------------------------------------------------//

        public void Choose_Play_Combo_Click(object sender, EventArgs e) //Populates combo box with Play data when clicked
        {
            this.ChoosePlay_combo.Items.Clear();
            dataSet = PlaysClass.getAllPlays();
            this.ChoosePlay_combo.SelectedValuePath = "Key";
            this.ChoosePlay_combo.DisplayMemberPath = "Value";
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                this.ChoosePlay_combo.Items.Add(new KeyValuePair<int, string>(int.Parse(row["Play_Id"].ToString()),row["Name"].ToString()));
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ChoosePlay_combo.SelectedValue != null)
            {
                int chosenPlayID = int.Parse(this.ChoosePlay_combo.SelectedValue.ToString());
                fillDatePicker(SelectDate_Booking, chosenPlayID);
            }

            //List<DateTime> dateList = new List<DateTime>();
            //int chosenPlayID = int.Parse(this.ChoosePlay_combo.SelectedValue.ToString());
            //DataSet dataSet = ShowingsClass.getShowingForPlays(chosenPlayID);
            //foreach(DataRow row in dataSet.Tables[0].Rows)
            //{
            //    int tempShowingId = int.Parse(row["Showing_Id"].ToString());
            //    DateTime showingDate = ShowingsClass.turnSqlLiteDateStringIntoDateTime(row["Date"].ToString());
            //    if (showingDate >= DateTime.Now)
            //    {
            //        dateList.Add(showingDate);
            //    }
            //}
            //dateList.Sort();
            //DateTime nextStartDate = DateTime.Now;
            //foreach (DateTime showingDateFromList in dateList)
            //{
            //    this.SelectDate_Booking.BlackoutDates.Add(new CalendarDateRange(nextStartDate, showingDateFromList.AddDays(-1)));
            //    nextStartDate = showingDateFromList.AddDays(1);
            //}
            //this.SelectDate_Booking.BlackoutDates.Add(new CalendarDateRange(nextStartDate, nextStartDate.AddYears(500)));
        }

        private void UpperCircle_radio_Checked(object sender, RoutedEventArgs e)
        {
            if (ChoosePlay_combo.Items.Count == 0)
            {
                MessageBox.Show("Please search for a showing before selecting seats");
            }
            else
            {
                this.Rows_combo.Items.Clear();
                this.DressCircle_radio.IsChecked = false;
                this.Stalls_radio.IsChecked = false;
                string sqlDateString = ShowingsClass.formatDateTimeToSqlLiteDateString((DateTime)SelectDate_Booking.SelectedDate);
                int showingId = ShowingsClass.getShowingIdByDate(sqlDateString);
                dataSet = SeatsClass.getAllAvailableRowByAreaShowingId(showingId, "UpperCircle");
                int numberOfSeatsLeftForArea = SeatsClass.getNumOfSeatsLeftInAreaForShowing(showingId, "UpperCircle");
                this.SeatsRemaining_lbl.Content = "Seats remaining: " + numberOfSeatsLeftForArea;
                fillRowDropDown(dataSet);
            }            
        }

        private void DressCircle_radio_Checked(object sender, RoutedEventArgs e)
        {
            if (ChoosePlay_combo.Items.Count == 0)
            {
                MessageBox.Show("Please search for a showing before selecting seats");
            }
            else
            {
                this.Rows_combo.Items.Clear();
                this.UpperCircle_radio.IsChecked = false;
                this.Stalls_radio.IsChecked = false;
                string sqlDateString = ShowingsClass.formatDateTimeToSqlLiteDateString((DateTime)SelectDate_Booking.SelectedDate);
                int showingId = ShowingsClass.getShowingIdByDate(sqlDateString);
                dataSet = SeatsClass.getAllAvailableRowByAreaShowingId(showingId, "DressCircle");
                int numberOfSeatsLeftForArea = SeatsClass.getNumOfSeatsLeftInAreaForShowing(showingId, "DressCircle");
                this.SeatsRemaining_lbl.Content = "Seats remaining: " + numberOfSeatsLeftForArea;
                fillRowDropDown(dataSet);
            }                
        }

        private void Stalls_radio_Checked(object sender, RoutedEventArgs e)
        {
            if (ChoosePlay_combo.Items.Count == 0)
            {
                MessageBox.Show("Please search for a showing before selecting seats");
            }
            else
            {
                this.Rows_combo.Items.Clear();
                this.UpperCircle_radio.IsChecked = false;
                this.DressCircle_radio.IsChecked = false;
                string sqlDateString = ShowingsClass.formatDateTimeToSqlLiteDateString((DateTime)SelectDate_Booking.SelectedDate);
                int showingId = ShowingsClass.getShowingIdByDate(sqlDateString);
                dataSet = SeatsClass.getAllAvailableRowByAreaShowingId(showingId, "Stall");
                int numberOfSeatsLeftForArea = SeatsClass.getNumOfSeatsLeftInAreaForShowing(showingId, "Stall");
                this.SeatsRemaining_lbl.Content = "Seats remaining: " + numberOfSeatsLeftForArea;
                fillRowDropDown(dataSet);
            }
        }

        private void fillRowDropDown(DataSet RowDataSet)
        {
            foreach (DataRow row in RowDataSet.Tables[0].Rows)
            {
                this.Rows_combo.Items.Add(row["Row"].ToString());
            }
        }

        private void Row_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.Rows_combo.Items.Count != 0)
            {
                this.Seat_combo.Items.Clear();
                string sqlDateString = ShowingsClass.formatDateTimeToSqlLiteDateString((DateTime)SelectDate_Booking.SelectedDate);
                int showingId = ShowingsClass.getShowingIdByDate(sqlDateString);

                if (UpperCircle_radio.IsChecked == true)
                {
                    dataSet = SeatsClass.getAllAvailableNumbersByAreaRowShowingId(showingId, "UpperCircle", this.Rows_combo.SelectedValue.ToString());
                    fillNumberDropDown(dataSet);
                }
                else if (DressCircle_radio.IsChecked == true)
                {
                    dataSet = SeatsClass.getAllAvailableNumbersByAreaRowShowingId(showingId, "DressCircle", this.Rows_combo.SelectedValue.ToString());
                    fillNumberDropDown(dataSet);
                }
                else
                {
                    dataSet = SeatsClass.getAllAvailableNumbersByAreaRowShowingId(showingId, "Stall", this.Rows_combo.SelectedValue.ToString());
                    fillNumberDropDown(dataSet);
                }
            }
        }

        private void fillNumberDropDown(DataSet RowDataSet)
        {
            foreach (DataRow row in RowDataSet.Tables[0].Rows)
            {
                this.Seat_combo.Items.Add(row["Number"].ToString());
            }
        }

        private void SurnameSearch_Booking_button_Click(object sender, RoutedEventArgs e)
        {
            string surName = Surname_Booking_txt.Text;
            if (surName.Length == 0)
            {
                MessageBox.Show("Please enter a surname to search for");
            }
            else
            {
                dataSet = CustomerClass.getCustomerByLastName(surName);
                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Customer not found - Try entering another play or check spelling");
                }
                else
                {
                    fillSurnameDropDown(dataSet);
                }
            }                        
        }

        private void fillSurnameDropDown(DataSet SurNameDataSet)
        {
            this.Surname_Booking_Combo.Items.Clear();
            this.Surname_Booking_Combo.SelectedValuePath = "Key";
            this.Surname_Booking_Combo.DisplayMemberPath = "Value";

            foreach (DataRow row in SurNameDataSet.Tables[0].Rows)
            {
                //string firstName = row["First_Name"].ToString();
                this.Surname_Booking_Combo.Items.Add(new KeyValuePair<int, string>(int.Parse(row["Customer_Id"].ToString()), row["First_Name"].ToString() + " " + row["Last_Name"].ToString() + " - " + row["Email"]));
            }
        }

        private void AddToBasket_button_Click(object sender, RoutedEventArgs e)
        {
            if (ChoosePlay_combo.Items.Count == 0)
            {
                MessageBox.Show("Please Choose a play");
            }
            else
            {
                if (SelectDate_Booking.SelectedDate == null)
                {
                    MessageBox.Show("Please enter a showing date");
                }
                else
                {
                    if (UpperCircle_radio.IsChecked == false && DressCircle_radio.IsChecked == false && Stalls_radio.IsChecked == false)
                    {
                        MessageBox.Show("Please check a seating area");
                    }
                    else
                    {
                        if (Rows_combo.SelectedValue == null)
                        {
                            MessageBox.Show("Please select a seat row");
                        }
                        else
                        {
                            if (Seat_combo.SelectedValue == null)
                            {
                                MessageBox.Show("Please select a seat number");
                            }
                            else
                            {
                                string area;

                                if (UpperCircle_radio.IsChecked == true)
                                {
                                    area = "UpperCircle";
                                }
                                else if (DressCircle_radio.IsChecked == true)
                                {
                                    area = "DressCircle";
                                }
                                else
                                {
                                    area = "Stall";
                                }

                                string sqlDateString = ShowingsClass.formatDateTimeToSqlLiteDateString((DateTime)SelectDate_Booking.SelectedDate);
                                int showingId = ShowingsClass.getShowingIdByDate(sqlDateString);

                                string row = this.Rows_combo.SelectedValue.ToString();
                                int num = int.Parse(this.Seat_combo.SelectedValue.ToString());

                                //DataRow rowCheck = seatBookingsDataset.NewRow();
                                //rowCheck["Section"] = area;
                                //rowCheck["Row"] = row;
                                //rowCheck["Number"] = num;
                                bool cannAddToBasket = true;
                                foreach (DataRow loopRow in seatBookingsDataset.Rows)
                                {
                                    if (loopRow["Section"].ToString() == area && loopRow["Row"].ToString() == row && int.Parse(loopRow["Number"].ToString()) == num)
                                    {
                                        cannAddToBasket = false;
                                    }
                                }
                                if (cannAddToBasket == false)
                                {
                                    MessageBox.Show("Seat already in basket");
                                }
                                else
                                {
                                    if (seatBookingsDataset.Rows.Count < 6)
                                    {
                                        seatBookingsDataset.Rows.Add(area, row, num);
                                        this.UpperCircle_radio.IsChecked = false;
                                        this.DressCircle_radio.IsChecked = false;
                                        this.Stalls_radio.IsChecked = false;
                                        this.Rows_combo.Items.Clear();
                                        this.Seat_combo.Items.Clear();
                                    }
                                }

                                //SeatBookingClass seatBookingObj = new SeatBookingClass(area, row, num, showingId);
                            }
                        }
                    }
                }
            }
        }

        private void DataGridBasket_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int dataGridSelectedIndex = DataGridBasket.SelectedIndex;
            DataGridBasket.SelectedIndex = dataGridSelectedIndex;
        }

        private void RemoveFromBasket_button_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridBasket.SelectedItem == null)
            {
                MessageBox.Show("Please select before deleteing from basket");
            }
            else
            {
                if (DataGridBasket.Items.Count == 1)
                {
                    MessageBox.Show("There is nothing to delete");
                }
                else if (DataGridBasket.SelectedIndex != 1)
                {
                    int dataGridSelectedIndex = DataGridBasket.SelectedIndex;
                    seatBookingsDataset.Rows[dataGridSelectedIndex].Delete();
                }
            }

            //if (ChoosePlay_combo.Items.Count == 0)
            //{
            //    MessageBox.Show("Please select a showing and fill out all other fields before selecting 'Add to Basket");
            //}
            //else
            //{
            //    int dataGridSelectedIndex = DataGridBasket.SelectedIndex;
            //    seatBookingsDataset.Rows[dataGridSelectedIndex].Delete();
            //}
        }

        private void BookPlay_button_Click(object sender, RoutedEventArgs e)
        {
            if (ChoosePlay_combo.Items.Count == 0)
            {
                MessageBox.Show("Please select a showing and fill out all other fields before selecting 'Add to Basket");
            }
            else
            {
                string sqlDateString = ShowingsClass.formatDateTimeToSqlLiteDateString((DateTime)SelectDate_Booking.SelectedDate);
                int showingId = ShowingsClass.getShowingIdByDate(sqlDateString);
                int customerId = int.Parse(this.Surname_Booking_Combo.SelectedValue.ToString());
                List<SeatBookingClass> seatBookingList = new List<SeatBookingClass>();
                foreach (DataRow row in seatBookingsDataset.Rows)
                {
                    SeatBookingClass seatBookingObj = new SeatBookingClass(row["Section"].ToString(), row["Row"].ToString(), int.Parse(row["Number"].ToString()), showingId);
                    seatBookingList.Add(seatBookingObj);
                }
                //int pCustomerId, string pDate, int pPaid, List<SeatBookingClass> SeatsToBookList
                BookingsClass.newBooking(customerId, ShowingsClass.formatDateTimeToSqlLiteDateString(DateTime.Now), 0, seatBookingList);
                MessageBox.Show("New booking added");
            }
            
        }

        //---------------------------------------------- Manage Booking Tab ------------------------------------------------------//
        private void SurnameSearch_ManageBooking_button_Click(object sender, RoutedEventArgs e)
        {
            if (Surname_ManageBooking_combo.Items.Count != 0)
            {
                Surname_ManageBooking_combo.Items.Clear();
            }
            string surName = Surname_ManageBooking_txt.Text.ToString();
            if (surName.Length == 0)
            {
                MessageBox.Show("Please enter a surname to search for");
            }
            else
            {
                dataSet = CustomerClass.getCustomerByLastName(surName);
                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Customer not found - Try entering another play or check spelling");
                }
                else
                {
                    fillSurnameDropDown(dataSet);
                    this.Surname_ManageBooking_combo.Items.Clear();
                    this.Surname_ManageBooking_combo.SelectedValuePath = "Key";
                    this.Surname_ManageBooking_combo.DisplayMemberPath = "Value";

                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        this.Surname_ManageBooking_combo.Items.Add(new KeyValuePair<int, string>(int.Parse(row["Customer_Id"].ToString()), row["First_Name"].ToString() + " " + row["Last_Name"].ToString() + " - " + row["Email"]));
                    }
                }                
            }
        }

        private void Customer_Search_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {                     
            int chosenCustomerID = int.Parse(this.Surname_ManageBooking_combo.SelectedValue.ToString());
            DataSet customerBookingsdataSet = CustomerClass.getAllBookkingsMadeByCustomer(chosenCustomerID);
            this.AvailableBookings_DataGrid.ItemsSource = customerBookingsdataSet.Tables[0].DefaultView;
            //int totalPrice = 0;

            //foreach (DataRow row in dataSet.Tables[0].Rows)
            //{
            //    //Need to sort out total cost to continue this part
            //}
        }

        private void DeleteBooking_button_Click(object sender, RoutedEventArgs e)
        {
            if (Surname_ManageBooking_combo.Items.Count == 0)
            {
                MessageBox.Show("Please search for a customer to be deleted");
            }
            else
            {
                int chosenCustomerID = int.Parse(this.Surname_ManageBooking_combo.SelectedValue.ToString());
                if (CustomerClass.getAllBookkingsMadeByCustomer(chosenCustomerID).Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("No Booking available to be deleted");
                }
                else
                {
                    if (AvailableBookings_DataGrid.SelectedIndex == 1)
                    {
                        MessageBox.Show("Empty row - Please select a valid booking");
                    }
                    else
                    {
                        if (AvailableBookings_DataGrid.SelectedItem == null)
                        {
                            MessageBox.Show("No Booking selected");
                        }
                        else
                        {
                            DataRowView chosenRow = (DataRowView)AvailableBookings_DataGrid.SelectedItem;
                            int chosenId = int.Parse(chosenRow["Booking_Id"].ToString());
                            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
                            if (messageBoxResult == MessageBoxResult.Yes)
                            {
                                BookingsClass.deleteBooking(chosenId);
                                MessageBox.Show("Booking deleted");
                            }
                            else
                            {
                                MessageBox.Show("Delete cancelled");
                            }
                        }
                    }                    
                }
            }                        
        }

        private void MakePayment_button_Click(object sender, RoutedEventArgs e)
        {
            if (Surname_ManageBooking_combo.Items.Count == 0)
            {
                MessageBox.Show("Please search for a customer to make payment");
            }
            else
            {
                int chosenCustomerID = int.Parse(this.Surname_ManageBooking_combo.SelectedValue.ToString());
                if (AvailableBookings_DataGrid.SelectedIndex == 1) 
                {
                    MessageBox.Show("Empty row - Please select a valid booking");
                }
                else
                {
                    if (CustomerClass.getAllBookkingsMadeByCustomer(chosenCustomerID).Tables[0].Rows.Count == 0)
                    {
                        MessageBox.Show("No Booking available for payment");
                    }
                    else
                    {
                        DataRowView chosenRow = (DataRowView)AvailableBookings_DataGrid.SelectedItem;
                        int chosenId = int.Parse(chosenRow["Booking_Id"].ToString());
                        BookingsClass.payBooking(chosenId);
                        MessageBox.Show("Payment complete");
                    }
                }                
            }            
        }

        //---------------------------------------------- Add Member Tab ------------------------------------------------------//
        private void CreateMember_button_Click(object sender, RoutedEventArgs e)
        {
            string mFirstName = Forename_AddMember_txt.Text.ToString();
            if (mFirstName.Length == 0)
            {
                MessageBox.Show("Please enter a Forename");
            }
            else
            {
                string stringToBeChecked = ValidationClass.HasNumbersInStringChecker(mFirstName);
                if (stringToBeChecked != null)
                {
                    MessageBox.Show("Forename cannot contain numbers");
                }
                else
                {
                    string mLastName = Surname_AddMember_txt.Text.ToString();
                    if (mLastName.Length == 0)
                    {
                        MessageBox.Show("Please enter a Surname");
                    }
                    else
                    {
                        stringToBeChecked = ValidationClass.HasNumbersInStringChecker(mLastName);
                        if (stringToBeChecked != null)
                        {
                            MessageBox.Show("Surname cannot contain numbers");
                        }
                        else
                        {
                            string mEmail = Email_AddMember_txt.Text.ToString();
                            if (mEmail.Length == 0)
                            {
                                MessageBox.Show("Please enter an Email");
                            }
                            else
                            {
                                if (CustomerClass.checkIfEmailExits(mEmail) == true)
                                {
                                    MessageBox.Show("This email already exists - Please enter another email");
                                }
                                else
                                {
                                    if (GoldMember_checkbox.IsChecked == true)
                                    {
                                        var currentTime = ShowingsClass.formatDateTimeToSqlLiteDateString(DateTime.Now.AddMonths(12));
                                        string mMembershipExpiryDate = currentTime.ToString();

                                        CustomerClass.newCustomer(mFirstName, mLastName, mEmail, mMembershipExpiryDate);
                                        MessageBox.Show("Customer added");
                                    }
                                    else
                                    {
                                        CustomerClass.newCustomer(mFirstName, mLastName, mEmail, null);
                                        MessageBox.Show("Customer added");
                                    }
                                }
                            }                            
                        }
                    }                    
                }                      
            }
        }

        //---------------------------------------------- Upgrade Member Tab ------------------------------------------------------//

        private void SurnameSearch_UpgradeMember_buttonClick(object sender, RoutedEventArgs e)
        {
            string surname = Surname_UpgradeMember_txt.Text.ToString();
            if (surname.Length == 0)
            {
                MessageBox.Show("Please enter a customer to search for");
            }
            else
            {
                dataSet = CustomerClass.getCustomerByLastName(Surname_UpgradeMember_txt.Text.ToString());
                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Customer not found - Try entering another customer name or check spelling");
                }
                else
                {
                    this.SurnameSearch_UpgradeMember_combo.Items.Clear();
                    this.SurnameSearch_UpgradeMember_combo.SelectedValuePath = "Key";
                    this.SurnameSearch_UpgradeMember_combo.DisplayMemberPath = "Value";

                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        this.SurnameSearch_UpgradeMember_combo.Items.Add(new KeyValuePair<int, string>(int.Parse(row["Customer_Id"].ToString()), row["First_Name"].ToString() + " " + row["Last_Name"].ToString() + " - " + row["Email"]));
                    }
                }                
            }            
        }

        private void SurnameSearch_UpgradeMember_combo_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (SurnameSearch_UpgradeMember_combo.Items.Count != 0)
            {
                int chosenCustomerID = int.Parse(this.SurnameSearch_UpgradeMember_combo.SelectedValue.ToString());
                dataSet = CustomerClass.getCustomerById(chosenCustomerID);
                //this.Forename_UpgradeMember_txt.Text = dataSet.Tables[0].Rows[0]["First_Name"].ToString();
                //this.Email_UpgradeMember_txt.Text = dataSet.Tables[0].Rows[0]["Email"].ToString();
            }
            //int chosenCustomerID = int.Parse(this.SurnameSearch_UpgradeMember_combo.SelectedValue.ToString());
            //dataSet = CustomerClass.getAllBookkingsMadeByCustomer(chosenCustomerID);
        }

        private void UpgradeMember_button_Click(object sender, RoutedEventArgs e)
        {
            if (SurnameSearch_UpgradeMember_combo.Items.Count == 0)
            {
                MessageBox.Show("Please enter a customer name to search for");
            }
            else
            {
                int chosenCustomerID = int.Parse(this.SurnameSearch_UpgradeMember_combo.SelectedValue.ToString());
                dataSet = CustomerClass.getCustomerById(chosenCustomerID);

                CustomerClass.editCustomerDetails(chosenCustomerID, dataSet.Tables[0].Rows[0]["First_Name"].ToString(), dataSet.Tables[0].Rows[0]["Last_Name"].ToString(), dataSet.Tables[0].Rows[0]["Email"].ToString(), ShowingsClass.formatDateTimeToSqlLiteDateString(DateTime.Now.AddMonths(12)));
                MessageBox.Show("Customer changed to gold club member");
            }
            
        }

        //---------------------------------------------- Edit/Delete Member  Tab ------------------------------------------------------//

        private void SurnameSearch_EditDel_buttonClick(object sender, RoutedEventArgs e)
        {
            string surname = Surname_EditDel_txt.Text.ToString();
            if (surname.Length == 0)
            {
                MessageBox.Show("Please enter a customer to search for");
            }
            else
            {
                dataSet = CustomerClass.getCustomerByLastName(Surname_EditDel_txt.Text.ToString());
                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Customer not found - Try entering another customer name or check spelling");
                }
                else
                {
                    this.Surname_EditDel_combo.Items.Clear();
                    this.Surname_EditDel_combo.SelectedValuePath = "Key";
                    this.Surname_EditDel_combo.DisplayMemberPath = "Value";

                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        this.Surname_EditDel_combo.Items.Add(new KeyValuePair<int, string>(int.Parse(row["Customer_Id"].ToString()), row["First_Name"].ToString() + " " + row["Last_Name"].ToString() + " - " + row["Email"]));
                    }
                }         
            }        
        }

        private void Surname_EditDel_comboSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Surname_EditDel_combo.Items.Count == 0)
            {
                
            }
            else
            {
                int chosenCustomerID = int.Parse(this.Surname_EditDel_combo.SelectedValue.ToString());
                //dataSet = CustomerClass.getAllBookkingsMadeByCustomer(chosenCustomerID);
                dataSet = CustomerClass.getCustomerById(chosenCustomerID);

                this.Surname2_EditDel_txt.Text = dataSet.Tables[0].Rows[0]["Last_Name"].ToString();
                this.Forename_EditDel_txt.Text = dataSet.Tables[0].Rows[0]["First_Name"].ToString();
                this.Email_EditDel_txt.Text = dataSet.Tables[0].Rows[0]["Email"].ToString();
            }            
        }

        private void EditMember_button_Click(object sender, RoutedEventArgs e)
        {
            if (Surname_EditDel_combo.Items.Count == 0)
            {
                MessageBox.Show("Please enter a customer to search for");
            }
            else
            {
                string mFirstName = Forename_EditDel_txt.Text.ToString();
                if (mFirstName.Length == 0)
                {
                    MessageBox.Show("Please enter a forename");
                }
                else
                {
                    string stringToBeChecked = ValidationClass.HasNumbersInStringChecker(mFirstName);
                    if (stringToBeChecked != null)
                    {
                        MessageBox.Show("Forename cannot contain numbers");
                    }
                    else
                    {
                        string mLastName = Surname2_EditDel_txt.Text.ToString();
                        if (mLastName.Length == 0)
                        {
                            MessageBox.Show("Please enter a Surname");
                        }
                        else
                        {
                            stringToBeChecked = ValidationClass.HasNumbersInStringChecker(mLastName);
                            if (stringToBeChecked != null)
                            {
                                MessageBox.Show("Surname cannot contain numbers");
                            }
                            else
                            {
                                string mEmail = Email_EditDel_txt.Text.ToString();
                                if (mEmail.Length == 0)
                                {
                                    MessageBox.Show("Please enter an Email");
                                }
                                else
                                {
                                    if (CustomerClass.checkIfEmailExits(mEmail) == true)
                                    {
                                        MessageBox.Show("This email already exists - Please enter another email");
                                    }
                                    else
                                    {
                                        int chosenCustomerID = int.Parse(this.Surname_EditDel_combo.SelectedValue.ToString());
                                        dataSet = CustomerClass.getCustomerById(chosenCustomerID);
                                        string membershipExpiryDate = dataSet.Tables[0].Rows[0]["Membership_Expiry_Date"].ToString();

                                        CustomerClass.editCustomerDetails(chosenCustomerID, mFirstName, mLastName, mEmail, membershipExpiryDate);
                                        MessageBox.Show("Customer edited");
                                    }                                                                                                   
                                }
                            }                   
                        }
                    }                    
                }            
            }         
        }

        private void DeleteMember_button_Click(object sender, RoutedEventArgs e)
        {
            if (Surname_EditDel_txt.Text.ToString().Length == 0)
            {
                MessageBox.Show("Please enter a customer to search for");
            }
            else
            {
                int chosenCustomerID = int.Parse(this.Surname_EditDel_combo.SelectedValue.ToString());
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    CustomerClass.deleteCustomer(chosenCustomerID);
                    MessageBox.Show("Booking deleted");
                }
                else
                {
                    MessageBox.Show("Delete cancelled");
                }
            }
        }
        
    }
}
