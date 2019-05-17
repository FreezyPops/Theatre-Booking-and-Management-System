using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace Jean_Paul_Sartre_Variety_Theatre
{
    public static class BookingsClass
    {
        private static SQLiteDataReader sqlReader;
        private static String queryString;

        public static void newBooking(int pCustomerId, string pDate, int pPaid, List<SeatBookingClass> SeatsToBookList)
        {
            queryString = "INSERT INTO Bookings(Customer_Id, Date_Of_Booking, Paid) VALUES(@customerId, @dateOfBooking, @paid)";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);

            connection.Open();
            command.Parameters.AddWithValue("@customerId", pCustomerId);
            command.Parameters.AddWithValue("@dateOfBooking", pDate);
            command.Parameters.AddWithValue("@paid", pPaid);
            command.ExecuteNonQuery();
            connection.Close();

            int booking_id = getLastBookingId();
	        bookSeats(booking_id,SeatsToBookList);

            float price = getTotalPriceOfBooking(booking_id);
            insertTotalPriceForBooking(booking_id, price);
        }

        private static void bookSeats(int booking_id, List<SeatBookingClass> SeatsToBookList)
        {
            int showing_Id = SeatsToBookList[0].getShowing_Id();
            for (int i = 0; i < SeatsToBookList.Count; i++)
            {
                string section = SeatsToBookList[i].getSection();
                string row = SeatsToBookList[i].getRow();
                int number = SeatsToBookList[i].getNumber();
		        SeatsClass.bookSeat(section, row, number, showing_Id, booking_id);
            }
        }

        private static int getLastBookingId()
        {
	        queryString = "select seq from sqlite_sequence where name=@tableName";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);

            connection.Open();
            command.Parameters.AddWithValue("@tableName", "Bookings");
            sqlReader = command.ExecuteReader();

            int lastId = 0;
            while (sqlReader.Read())
            {
                lastId = int.Parse(sqlReader[0].ToString());
            }
            connection.Close();
            return lastId;
        }

        public static void deleteBooking(int pBookingId)
        {
            setSeatsLinkedToBookingToEmpty(pBookingId);
            queryString = "Delete From Bookings" +
                " Where Booking_Id = @bookingId";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);

            connection.Open();
            command.Parameters.AddWithValue("@bookingId", pBookingId);
            command.ExecuteNonQuery();
            connection.Close(); 
        }

        private static void setSeatsLinkedToBookingToEmpty(int pBookingId)
        {
            queryString = "UPDATE Seats SET Booking_Id = 0" +
                " Where Booking_Id = @bookingId";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);

            connection.Open();
            command.Parameters.AddWithValue("@bookingId", pBookingId);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private static DataSet getSeatsLinkedToBooking(int pBookingId)
        {
            //queryString = "Select Section, Row, Number" +
            //    " From Bookings, Seats" +
            //    " Where Bookings.Booking_Id = Seats.Booking_Id" +
            //    " And Bookings.Booking_Id = @bookingID";
            //SQLiteConnection connection = new SQLiteConnection(this.getConnectionString());
            //SQLiteCommand command = new SQLiteCommand(queryString, connection);

            //connection.Open();
            //command.Parameters.AddWithValue("@booking_Id", pBookingId);
            //sqlReader = command.ExecuteReader();
            //connection.Close();

            //return sqlReader;

            DataSet dataSet = new DataSet();
            queryString = "Select *" +
                " From Seats" +
                " Where Bookings.Booking_Id = Seats.Booking_Id" +
                " And Bookings.Booking_Id = @bookingID";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@booking_Id", pBookingId);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public static DataSet getBookingDetailsById(int pBooking_Id)
        {
            DataSet dataSet = new DataSet();
            queryString = "SELECT * FROM Bookings, Customers" +
                " WHERE Bookings.Booking_Id = @booking_Id" +
                " AND Bookings.Customer_Id = Customers.Customer_Id";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@booking_Id", pBooking_Id);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public static void payBooking(int pBookingId)
        {
            queryString = "UPDATE Bookings SET Paid = @paid WHERE Booking_Id = @booking_Id";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);

            connection.Open();
	        command.Parameters.AddWithValue("@paid", 1);
            command.Parameters.AddWithValue("@booking_Id", pBookingId);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static DataSet getAllBookingsForCustomer(int pCustomerId)
        {
            DataSet dataSet = new DataSet();
            queryString = "SELECT * FROM Bookings, Seats" +
                " WHERE Customer_Id = @customer_Id";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@customer_Id", pCustomerId);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public static DataSet getAllBookingsForShowing(int pShowing_Id)
        {
            DataSet dataSet = new DataSet();
            queryString = "SELECT * FROM Bookings, Seats, Showings" +
                " WHERE Bookings.Booking_Id = Seats.Booking_Id" +
                " And Seats.Showing_Id = Showings.Showing_Id" +
                " And Showings.Showing_Id = @showing_Id";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@showing_Id", pShowing_Id);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public static DataSet getAllBookingsBetweenDates(string pStartDate, string pEndDate)
        {
            DataSet dataSet = new DataSet();
            string query = "Select * From Bookings Where Date_Of_Booking BETWEEN @startDate AND @endDate";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@startDate", pStartDate + " 00:00");
                command.Parameters.AddWithValue("@endDate", pEndDate + "24:00");
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;

        }

        public static DataSet getAllBookingsOnDate(string pDate)
        {
            DataSet dataSet = new DataSet();
            string query = "Select * From Bookings Where Date_Of_Booking BETWEEN @startDate AND @endDate";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@startDate", pDate + " 00:00");
                command.Parameters.AddWithValue("@endDate", pDate + " 24:00");
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public static float getTotalPriceOfBooking(int pBookingId)
        {
            DataSet dataSet = new DataSet();
            string query = "Select UpperCirclePrice, DressCirclePrice, StallsPrice From Showings, Seats, Bookings" + 
                " Where Seats.Showing_Id = Showings.Showing_Id" +
                " And Seats.Booking_Id = Bookings.Booking_Id" +
                " And Bookings.Booking_Id = @bookingId";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@bookingId", pBookingId);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }

            float UpperCirclePrice = float.Parse(dataSet.Tables[0].Rows[0]["UpperCirclePrice"].ToString());
            float DreesCirclePrice = float.Parse(dataSet.Tables[0].Rows[0]["DressCirclePrice"].ToString());
            float StallsPrice = float.Parse(dataSet.Tables[0].Rows[0]["StallsPrice"].ToString());

            dataSet = new DataSet();
            query = "Select * From Seats Where Booking_Id = @bookingId";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@bookingId", pBookingId);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }

            float totalPrice = 0;

            foreach(DataRow row in dataSet.Tables[0].Rows)
            {
                if (row["Section"].ToString() == "Stall")
                {
                    totalPrice = totalPrice + StallsPrice;
                }
                else if (row["Section"].ToString() == "DressCircle")
                {
                    totalPrice = totalPrice + DreesCirclePrice;
                }
                else
                {
                    totalPrice = totalPrice + UpperCirclePrice;
                }
            }

            return totalPrice;
        }

        public static void insertTotalPriceForBooking(int pBookingId, float pTotalPrice)
        {
            queryString = "UPDATE Bookings SET Total_Amount = @totalAmount WHERE Booking_Id = @booking_Id";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);

            connection.Open();
            command.Parameters.AddWithValue("@totalAmount", pTotalPrice);
            command.Parameters.AddWithValue("@booking_Id", pBookingId);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static DataSet getAllCustomerBookingsForShowingsPastCurrentDate(int pCustomerId)
        {
            string currentDateString = ShowingsClass.formatDateTimeToSqlLiteDateString(DateTime.Now);
            DataSet dataSet = new DataSet();
            string queryString = "Select Bookings.Booking_Id From Bookings, Customers, Seats, Showings" +
                " Where Bookings.Customer_Id = Customers.Customer_Id" +
                " AND Bookings.Booking_Id = Seats.Booking_Id" +
                " AND Seats.Showing_Id = Showings.Showing_Id" +
                " AND Customers.Customer_Id = @customerId" +
                " AND Showings.Date > date(@currentDate)";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@currentDate", currentDateString);
                command.Parameters.AddWithValue("@customerId", pCustomerId);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }
    }
}