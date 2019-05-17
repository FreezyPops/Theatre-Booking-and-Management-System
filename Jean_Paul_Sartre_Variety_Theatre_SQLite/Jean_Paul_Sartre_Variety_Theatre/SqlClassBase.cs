using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data;

namespace Jean_Paul_Sartre_Variety_Theatre
{
    public static class SqlClassBase
    {
        //public static SQLiteDataReader sqlReader;
        //private static SQLiteDataReader dataSet;
        public static String queryString;
        static SqlClassBase()
        {
            var temp = System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString();
            temp = System.IO.Directory.GetParent(temp).ToString();
            temp = temp + @"\Jean_Paul_Sartre_Variety_Theatre\JPSVdb.db";
            connectionString = "Data Source=" + temp;
        }

        public static string connectionString = "";

        public static string getConnectionString()
        {
            return connectionString;
        }      

        public static DataSet commitSqlQuerryNonParameterized(string pQueryString)
        {
            DataSet dataSet = new DataSet();
            using (SQLiteConnection connection = new SQLiteConnection(getConnectionString()))
            {
                SQLiteDataAdapter adaptor = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(pQueryString, connection);
                adaptor.SelectCommand = command;
                adaptor.Fill(dataSet);
            }
            return dataSet;
        }

        public static void setUpDbForTesting()
        {
            clearDb();
            addTestData();
        }

        private static void addTestData()
        {
            PlaysClass.addPlay("DeletedPlay",0);
            ShowingsClass.addShowing(1, "2017-06-24 17:00", 50, 25, 12.5);
            CustomerClass.newCustomer("DeletedCustomerFirstName","DeletedCustomerLastName","","");

            //BookingsClass bookingClass = new BookingsClass();
            //SeatBookingClass seatBookingClass = new SeatBookingClass("UpperCircle","A",10,1);
            //List<SeatBookingClass> seatBookingClassList = new List<SeatBookingClass>();
            //bookingClass.newBooking(1, "18/06/2017 12:00", 0, seatBookingClassList);
        }

        private static void clearDb()
        {
            queryString = "DELETE FROM Plays; DELETE FROM Showings;" +
                " DELETE FROM Seats; DELETE FROM Customers; DELETE FROM Bookings;" +
                " UPDATE sqlite_sequence SET seq = 0";
            commitSqlQuerryNonParameterized(queryString);
        }
    }
}