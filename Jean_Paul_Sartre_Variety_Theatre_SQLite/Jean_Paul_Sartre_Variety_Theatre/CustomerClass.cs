using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace Jean_Paul_Sartre_Variety_Theatre
{
    public static class CustomerClass
    {
        private static SQLiteDataReader sqlReader;
        private static String queryString;

        public static void newCustomer(string pFirstName, string pLastName, string pEmail, string pMembershipExpiryDate)
        {
            //if (pMembershipExpiryDate == System.DateTime.MinValue)
            //{
            //	queryString = "INSERT INTO Customers(First_Name, Last_Name, Email, Membership_Expiry_Date) VALUES(@firstName, @lastName, @email, @membershipExpiryDate)";
            //    SQLiteConnection connection = new SQLiteConnection(this.getConnectionString());
            //    SQLiteCommand command = new SQLiteCommand(queryString, connection);

            //    connection.Open();
            //	command.Parameters.AddWithValue("@firstName", pFirstName);
            //    command.Parameters.AddWithValue("@lastName", pLastName);
            //    command.Parameters.AddWithValue("@email", pEmail);
            //    command.Parameters.AddWithValue("@membershipExpiryDate", null);
            //	command.ExecuteNonQuery();
            //    connection.Close();
            //}
            //else
            //{
            //	queryString = "INSERT INTO dbo.Customers(First_Name, Last_Name, Email, Membership_Expiry_Date) VALUES(@firstName, @lastName, @email, @membershipExpiryDate)";
            //    SQLiteConnection connection = new SQLiteConnection(this.getConnectionString());
            //    SQLiteCommand command = new SQLiteCommand(queryString, connection);

            //    connection.Open();
            //	command.Parameters.AddWithValue("@firstName", pFirstName);
            //    command.Parameters.AddWithValue("@lastName", pLastName);
            //    command.Parameters.AddWithValue("@email", pEmail);
            //    command.Parameters.AddWithValue("@membershipExpiryDate", pMembershipExpiryDate);
            //	command.ExecuteNonQuery();
            //    connection.Close();
            //}

            queryString = "INSERT INTO Customers(First_Name, Last_Name, Email, Membership_Expiry_Date) VALUES(@firstName, @lastName, @email, @membershipExpiryDate)";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);

            connection.Open();
            command.Parameters.AddWithValue("@firstName", pFirstName);
            command.Parameters.AddWithValue("@lastName", pLastName);
            command.Parameters.AddWithValue("@email", pEmail);
            command.Parameters.AddWithValue("@membershipExpiryDate", pMembershipExpiryDate);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void editCustomerDetails(int pCustomerId, string pFirstName, string pLastName, string pEmail, string pMembershipExpiryDate)
        {
            queryString = "UPDATE Customers SET First_name = @firstName, Last_Name = @lastName, Email = @email, Membership_Expiry_Date = @membershipExpiryDate" +
                " WHERE Customer_Id = @customerId";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);

            connection.Open();
            command.Parameters.AddWithValue("@firstName", pFirstName);
            command.Parameters.AddWithValue("@lastName", pLastName);
            command.Parameters.AddWithValue("@email", pEmail);
            command.Parameters.AddWithValue("@membershipExpiryDate", pMembershipExpiryDate);
            command.Parameters.AddWithValue("@customerId", pCustomerId);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static DataSet getAllBookkingsMadeByCustomer(int pCustomerId)
        {
            //queryString = "Select Date_Of_Booking, Date, Price, Name, Length, Section, Row, Number" +
            //    " From Bookings, Seats, Showings, Plays" +
            //    " Where Customers.Customer_Id = Bookings.Customer_Id" +
            //    " And Bookings.Booking_Id = Seats.Bookings_Id" +
            //    " And Seats.Showing_Id = Showings.Showing_Id" +
            //    " And Showings.Play_Id = Plays.Play_Id" +
            //    " And Customers.Customer_Id = @customerId";
            //SQLiteCommand command = new SQLiteCommand(queryString, this.getConnectionString());

            //this.getConnectionString().Open();
            //command.Parameters.AddWithValue("@customerId", pCustomerId);
            //sqlReader = command.ExecuteReader();
            //this.getConnectionString().Close();

            //return sqlReader;

            DataSet dataSet = new DataSet();
            queryString = "Select Bookings.Booking_Id, Date_Of_Booking, Total_Amount, Paid, Name, Length, Date, Section, Row, Number" +
                " From Bookings, Seats, Showings, Plays , Customers" +
                " Where Customers.Customer_Id = Bookings.Customer_Id" +
                " And Bookings.Booking_Id = Seats.Booking_Id" +
                " And Seats.Showing_Id = Showings.Showing_Id" +
                " And Showings.Play_Id = Plays.Play_Id" +
                " And Customers.Customer_Id = @customerId";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@customerId", pCustomerId);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public static void deleteCustomer(int pCustomerId)
        {
            DataSet dataSet = BookingsClass.getAllCustomerBookingsForShowingsPastCurrentDate(pCustomerId);
            foreach(DataRow row in dataSet.Tables[0].Rows)
            {
                BookingsClass.deleteBooking(int.Parse(row["Booking_Id"].ToString()));
            }

            setAllBookingsToDeletedCustomer(pCustomerId);

            queryString = "Delete From Customers" +
                " Where Customer_Id = @customerId";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);

            connection.Open();
            command.Parameters.AddWithValue("@customerId", pCustomerId);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private static void setAllBookingsToDeletedCustomer(int pCustomerId)
        {
            int deletedCustomerId = getIdOfDeleteCustomerRecord();

            queryString = "UPDATE Bookings Set Customer_Id = @deletedCustomerRecordId" +
                " Where Customer_Id = @customerId";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);

            connection.Open();
            command.Parameters.AddWithValue("@customerId", pCustomerId);
            command.Parameters.AddWithValue("@deletedCustomerRecordId", deletedCustomerId);
            command.ExecuteNonQuery();
            connection.Close();
        }

	    private static int getIdOfDeleteCustomerRecord()
	    {
         //   queryString = "Select Customer_Id From Customers Where Last_Name = @deletedPlayName";
         //   SQLiteConnection connection = new SQLiteConnection(this.getConnectionString());
         //   SQLiteCommand command = new SQLiteCommand(queryString, connection);

         //   connection.Open();
         //   command.Parameters.AddWithValue("@deletedPlayName", "DeletedCustomerLastName");
         //   sqlReader = command.ExecuteReader();

	        //int deletedCustomerRecordId = 0;
         //   try
         //   {
         //       while (sqlReader.Read())
         //       {
         //           deletedCustomerRecordId = int.Parse(sqlReader["Customer_Id"].ToString());
         //           connection.Close();
         //       }
         //   }
         //   catch (Exception)
         //   {
         //       makeDeletePlayRecord();
         //       deletedCustomerRecordId = getIdOfDeleteCustomerRecord();
         //   }
         //   connection.Close();
         //   return deletedCustomerRecordId;

            DataSet dataSet = new DataSet();
            queryString = "Select Customer_Id From Customers Where Last_Name = @deletedPlayName";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@deletedPlayName", "DeletedCustomerLastName");
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }

            int deletedCustomerId;

            if (dataSet.Tables[0].Rows.Count == 0)
            {
                makeDeleteCustomerRecord();
                deletedCustomerId = getIdOfDeleteCustomerRecord();
            }
            else
            {
                deletedCustomerId = int.Parse(dataSet.Tables[0].Rows[0]["Customer_Id"].ToString());
            }
            return deletedCustomerId;
        }


        private static void makeDeleteCustomerRecord()
        {
            queryString = "INSERT INTO Customers(First_Name, Last_Name, Email, Membership_Expiry_Date) VALUES(@firstName, @lastName, @email, @membershipExpiryDate)";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);

            connection.Open();
            command.Parameters.AddWithValue("@firstName", "DeletedCustomerFirstName");
            command.Parameters.AddWithValue("@lastName", "DeletedCustomerLastName");
            command.Parameters.AddWithValue("@email", "DeletedCustomerEmail");
            command.Parameters.AddWithValue("@membershipExpiryDate", null);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static DataSet getAllCustomers()
        {
            queryString = "Select * From Customers";
            return(SqlClassBase.commitSqlQuerryNonParameterized(queryString));
        }

        public static DataSet getCustomerByLastName(string pLastName)
        {	    
	        //queryString = "Select * From Customers Where Last_Name = @lastName";
         //   SQLiteCommand command = new SQLiteCommand(queryString, this.getConnectionString());

         //   this.getConnectionString().Open();
         //   command.Parameters.AddWithValue("@lastName", pLastName);
         //   sqlReader = command.ExecuteReader();
         //   this.getConnectionString().Close();    

         //   return sqlReader;

            DataSet dataSet = new DataSet();
            queryString = "Select * From Customers Where Last_Name = @lastName";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@lastName", pLastName);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public static DataSet getCustomerById(int pId)
        {
            DataSet dataSet = new DataSet();
            queryString = "Select * From Customers Where Customer_Id = @customerId";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@customerId", pId);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public static DataSet getCustomersGoldClubMembers()
        {
            queryString = "Select * From Customers" +
                " Where Membership_Expiry_Date IS NOT ''";
            return SqlClassBase.commitSqlQuerryNonParameterized(queryString);
        }

        public static DataSet getCustomersNonGoldCLubMembers()
        {
            queryString = "Select * From Customers" +
                " Where MemberShip_Expiry_Date IS ''";
            return SqlClassBase.commitSqlQuerryNonParameterized(queryString);
        }

        public static DataSet getCustomersMembershipExpiry()
        {
            string startDate = ShowingsClass.formatDateTimeToSqlLiteDateString(DateTime.Now);
            string endDate = ShowingsClass.formatDateTimeToSqlLiteDateString(DateTime.Now.AddMonths(1));
            DataSet dataSet = new DataSet();
            string query = "Select * From Customers Where Membership_Expiry_Date BETWEEN date(@startDate) AND date(@endDate)";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@startDate", startDate + " 00:00");
                command.Parameters.AddWithValue("@endDate", endDate + " 24:00");
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public static void upgradeCustomer(int pCustomerId)
        {
            string memberShipExpiryDate = ShowingsClass.formatDateTimeToSqlLiteDateString(DateTime.Now.AddMonths(12));
            queryString = "UPDATE Customers SET Membership_Expiry_Date = @membershipExpiryDate" +
                " WHERE Customer_Id = @customerId";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);

            connection.Open();
            command.Parameters.AddWithValue("@membershipExpiryDate", memberShipExpiryDate);
            command.Parameters.AddWithValue("@customerId", pCustomerId);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static bool checkIfEmailExits(string pEmail)
        {
            DataSet dataSet = new DataSet();
            string query = "Select * From Customers Where Email = @email";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@email", pEmail);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }

            if (dataSet.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}