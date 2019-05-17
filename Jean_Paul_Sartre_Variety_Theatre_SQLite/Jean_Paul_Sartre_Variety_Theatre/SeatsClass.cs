using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace Jean_Paul_Sartre_Variety_Theatre
{
    public static class SeatsClass
    {

        private static Dictionary<string, int> StallDictonary = new Dictionary<string, int>();
        private static Dictionary<string, int> DressCirleDictonary = new Dictionary<string, int>();
        private static Dictionary<string, int> UpperCircleDictonary = new Dictionary<string, int>();

        private static SQLiteDataReader sqlReader;
        private static String queryString;

        static SeatsClass()
        {
            FillDirtionaries();
        }

        private static void FillDirtionaries()
        {
            FillStallDictonary();
            FillDressCirleDictonary();
            FillUpperCircleDictonary();
        }

        private static void FillStallDictonary()
        {
            StallDictonary.Add("A", 17);
            StallDictonary.Add("B", 18);
            StallDictonary.Add("C", 20);
            StallDictonary.Add("D", 22);
            StallDictonary.Add("E", 22);
            StallDictonary.Add("F", 22);
            StallDictonary.Add("G", 22);
            StallDictonary.Add("H", 22);
            StallDictonary.Add("J", 22);
            StallDictonary.Add("K", 20);
            StallDictonary.Add("L", 20);
            StallDictonary.Add("M", 16);

        }
        private static void FillDressCirleDictonary()
        {
            DressCirleDictonary.Add("A", 35);
            DressCirleDictonary.Add("B", 37);
            DressCirleDictonary.Add("C", 19);
            DressCirleDictonary.Add("D", 23);
            DressCirleDictonary.Add("E", 23);
        }
        private static void FillUpperCircleDictonary()
        {
            UpperCircleDictonary.Add("A", 22);
            UpperCircleDictonary.Add("B", 25);
            UpperCircleDictonary.Add("C", 25);
            UpperCircleDictonary.Add("D", 25);
            UpperCircleDictonary.Add("E", 10);
        }

        public static void generateSeats(int pShowing_Id)
        {
            generateStallSeats(pShowing_Id);
            generateDressCircleSeats(pShowing_Id);
            generateUpperCircleSeats(pShowing_Id);
        }

        private static void generateStallSeats(int pShowing_Id)
        {
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            using (connection)
            {
                connection.Open();
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    string seatSection = "Stall";
                    for (int i = 0; i < StallDictonary.Count(); i++)
                    {
                        for (int seatNumber = 1; seatNumber < StallDictonary.Values.ElementAt(i) + 1; seatNumber++)
                        {
                            string seatRow = StallDictonary.Keys.ElementAt(i);
                            queryString = "INSERT INTO Seats(Section, Row, Number, Showing_Id, Booking_Id) VALUES(@section, @row, @number, @showing_Id, @booking_Id)";
                            SQLiteCommand command = new SQLiteCommand(queryString, connection);
                            command.Parameters.AddWithValue("@section", seatSection);
                            command.Parameters.AddWithValue("@row", seatRow);
                            command.Parameters.AddWithValue("@number", seatNumber);
                            command.Parameters.AddWithValue("@showing_Id", pShowing_Id);
                            command.Parameters.AddWithValue("@booking_Id", 0);
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
                connection.Close();

            }
            //string seatSection = "Stall";
            //for (int i = 0; i < StallDictonary.Count(); i++)
            //{
            //    for (int seatNumber = 1; seatNumber < StallDictonary.Values.ElementAt(i) + 1; seatNumber++)
            //    {
            //        string seatRow = StallDictonary.Keys.ElementAt(i);
            //	    queryString = "INSERT INTO Seats(Section, Row, Number, Showing_Id, Booking_Id) VALUES(@section, @row, @number, @showing_Id, @booking_Id)";
            //        SQLiteCommand command = new SQLiteCommand(queryString, connection);

            //        connection.Open();
            //        command.Parameters.AddWithValue("@section", seatSection);
            //        command.Parameters.AddWithValue("@row", seatRow);
            //        command.Parameters.AddWithValue("@number", seatNumber);
            //        command.Parameters.AddWithValue("@showing_Id", pShowing_Id);
            //        command.Parameters.AddWithValue("@booking_Id", null);
            //        command.ExecuteNonQuery();
            //        connection.Close();
            //    }
            //}
        }
        
        private static void generateDressCircleSeats(int pShowing_Id)
        {
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            using (connection)
            {
                connection.Open();
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    string seatSection = "DressCircle";
                    for (int i = 0; i < DressCirleDictonary.Count(); i++)
                    {
                        for (int seatNumber = 1; seatNumber < DressCirleDictonary.Values.ElementAt(i) + 1; seatNumber++)
                        {
                            string seatRow = DressCirleDictonary.Keys.ElementAt(i);
                            queryString = "INSERT INTO Seats(Section, Row, Number, Showing_Id, Booking_Id) VALUES(@section, @row, @number, @showing_Id, @booking_Id)";
                            SQLiteCommand command = new SQLiteCommand(queryString, connection);

                            command.Parameters.AddWithValue("@section", seatSection);
                            command.Parameters.AddWithValue("@row", seatRow);
                            command.Parameters.AddWithValue("@number", seatNumber);
                            command.Parameters.AddWithValue("@showing_Id", pShowing_Id);
                            command.Parameters.AddWithValue("@booking_Id", 0);
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
                connection.Close();

            }

            //string seatSection = "DressCircle";
            //for (int i = 0; i < DressCirleDictonary.Count(); i++)
            //{
            //    for (int seatNumber = 1; seatNumber < DressCirleDictonary.Values.ElementAt(i) + 1; seatNumber++)
            //    {
            //        string seatRow = DressCirleDictonary.Keys.ElementAt(i);
            //        queryString = "INSERT INTO Seats(Section, Row, Number, Showing_Id, Booking_Id) VALUES(@section, @row, @number, @showing_Id, @booking_Id)";
            //        SQLiteConnection connection = new SQLiteConnection(this.getConnectionString());
            //        SQLiteCommand command = new SQLiteCommand(queryString, connection);

            //        connection.Open();
            //        command.Parameters.AddWithValue("@section", seatSection);
            //        command.Parameters.AddWithValue("@row", seatRow);
            //        command.Parameters.AddWithValue("@number", seatNumber);
            //        command.Parameters.AddWithValue("@showing_Id", pShowing_Id);
            //        command.Parameters.AddWithValue("@booking_Id", null);
            //        command.ExecuteNonQuery();
            //        connection.Close();
            //    }
            //}
        }

        private static void generateUpperCircleSeats(int pShowing_Id)
        {
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            using (connection)
            {
                connection.Open();
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    string seatSection = "UpperCircle";
                    for (int i = 0; i < UpperCircleDictonary.Count(); i++)
                    {
                        for (int seatNumber = 1; seatNumber < UpperCircleDictonary.Values.ElementAt(i) + 1; seatNumber++)
                        {
                            string seatRow = UpperCircleDictonary.Keys.ElementAt(i);
                            queryString = "INSERT INTO Seats(Section, Row, Number, Showing_Id, Booking_Id) VALUES(@section, @row, @number, @showing_Id, @booking_Id)";
                            SQLiteCommand command = new SQLiteCommand(queryString, connection);

                            command.Parameters.AddWithValue("@section", seatSection);
                            command.Parameters.AddWithValue("@row", seatRow);
                            command.Parameters.AddWithValue("@number", seatNumber);
                            command.Parameters.AddWithValue("@showing_Id", pShowing_Id);
                            command.Parameters.AddWithValue("@booking_Id", 0);
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
                connection.Close();

            }

            //string seatSection = "UpperCircle";
            //for (int i = 0; i < UpperCircleDictonary.Count(); i++)
            //{
            //    for (int seatNumber = 1; seatNumber < UpperCircleDictonary.Values.ElementAt(i) + 1; seatNumber++)
            //    {
            //        string seatRow = UpperCircleDictonary.Keys.ElementAt(i);
            //        queryString = "INSERT INTO Seats(Section, Row, Number, Showing_Id, Booking_Id) VALUES(@section, @row, @number, @showing_Id, @booking_Id)";
            //        SQLiteConnection connection = new SQLiteConnection(this.getConnectionString());
            //        SQLiteCommand command = new SQLiteCommand(queryString, connection);

            //        connection.Open();
            //        command.Parameters.AddWithValue("@section", seatSection);
            //        command.Parameters.AddWithValue("@row", seatRow);
            //        command.Parameters.AddWithValue("@number", seatNumber);
            //        command.Parameters.AddWithValue("@showing_Id", pShowing_Id);
            //        command.Parameters.AddWithValue("@booking_Id", null);
            //        command.ExecuteNonQuery();
            //        connection.Close();
            //    }
            //}
        }

        public static void cancelSeat(string pSection, string pRow, int pNumber, int pShowingId)
        {
            queryString = "UPDATE Seats SET Booking_Id = @booking_Id" +
            " WHERE Section = @section" +
            " AND Row = @row" +
            " AND Number = @number" +
            " AND Showing_Id = @Showing_Id";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);

            connection.Open();
            command.Parameters.AddWithValue("@booking_Id", 0);
            command.Parameters.AddWithValue("@section", pSection);
            command.Parameters.AddWithValue("@row", pRow);
            command.Parameters.AddWithValue("@number", pNumber);
            command.Parameters.AddWithValue("@Showing_Id", pShowingId);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void bookSeat(string pSection, string pRow, int pNumber, int pShowingId, int pBooking_Id)
        {
	        queryString = "UPDATE Seats SET Booking_Id = @booking_Id" +
                " WHERE Section = @section" +
                " AND Row = @row" +
                " AND Number = @number" +
                " AND Showing_Id = @showing_Id";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);

            connection.Open();
            command.Parameters.AddWithValue("@booking_Id", pBooking_Id);
            command.Parameters.AddWithValue("@section", pSection);
            command.Parameters.AddWithValue("@row", pRow);
            command.Parameters.AddWithValue("@number", pNumber);
            command.Parameters.AddWithValue("@showing_Id", pShowingId);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static DataSet getSeatDetails(string pSection, string pRow, int pNumber, int pShowingId)
        {
	        //queryString = "SELECT * FROM Seats WHERE Section = @section" +
         //       " AND Row = @row" +
         //       " AND Number = @number" +
         //       " AND Showing_Id = @showingID";
         //   SQLiteCommand command = new SQLiteCommand(queryString, this.getConnectionString());

         //   this.getConnectionString().Open();
         //   command.Parameters.AddWithValue("@section", pSection);
         //   command.Parameters.AddWithValue("@row", pRow);
         //   command.Parameters.AddWithValue("@number", pNumber);
         //   command.Parameters.AddWithValue("@Showing_Id", pShowingId);
         //   command.ExecuteNonQuery();
         //   this.getConnectionString().Close();
         //   return sqlReader;

            DataSet dataSet = new DataSet();
            queryString = "SELECT * FROM Seats" +
                " WHERE Section = @section" +
                " AND Row = @row" +
                " AND Number = @number" +
                " AND Showing_Id = @Showing_Id";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@section", pSection);
                command.Parameters.AddWithValue("@row", pRow);
                command.Parameters.AddWithValue("@number", pNumber);
                command.Parameters.AddWithValue("@Showing_Id", pShowingId);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public static DataSet getAllSeatsForShowing(int pShowingId)
        {
	        //queryString = "SELECT * FROM Seats" +
         //       " Where Showing_Id = @showingID";
         //   SQLiteCommand command = new SQLiteCommand(queryString, this.getConnectionString());

         //   this.getConnectionString().Open();
         //   command.Parameters.AddWithValue("@Showing_Id", pShowingId);
         //   command.ExecuteNonQuery();
         //   this.getConnectionString().Close();
         //   return sqlReader;

            DataSet dataSet = new DataSet();
            queryString = "SELECT * FROM Seats" +
                " Where Showing_Id = @showingID";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@Showing_Id", pShowingId);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public static DataSet getAllSeatsForShowingStall(int pShowingId)
        {
            DataSet dataSet = new DataSet();
            queryString = "SELECT * FROM Seats" +
                " Where Showing_Id = @showingID" +
                " And Section = @section";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@Showing_Id", pShowingId);
                command.Parameters.AddWithValue("@section", "Stall");
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public static DataSet getAllSeatsForShowingDressCircle(int pShowingId)
        {
            DataSet dataSet = new DataSet();
            queryString = "SELECT * FROM Seats" +
                " Where Showing_Id = @showingID" +
                " And Section = @section";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@Showing_Id", pShowingId);
                command.Parameters.AddWithValue("@section", "DressCircle");
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public static DataSet getAllSeatsForShowingUpperCircle(int pShowingId)
        {
            DataSet dataSet = new DataSet();
            queryString = "SELECT * FROM Seats" +
                " Where Showing_Id = @showingID" +
                " And Section = @section";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@Showing_Id", pShowingId);
                command.Parameters.AddWithValue("@section", "UpperCircle");
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public static DataSet getAllAvailableRowByAreaShowingId(int pShowingId, string pArea)
        {
            DataSet dataSet = new DataSet();
            queryString = "SELECT DISTINCT Row FROM Seats" +
                " Where Showing_Id = @showingId" +
                " And Section = @section" +
                " And Booking_Id = 0";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@showingId", pShowingId);
                command.Parameters.AddWithValue("@section", pArea);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public static DataSet getAllAvailableNumbersByAreaRowShowingId(int pShowingId, string pArea, string pRow)
        {
            DataSet dataSet = new DataSet();
            queryString = "SELECT Number FROM Seats" +
                " Where Showing_Id = @showingId" +
                " And Section = @section" +
                " And Row = @row" +
                " And Booking_Id = 0";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@showingId", pShowingId);
                command.Parameters.AddWithValue("@section", pArea);
                command.Parameters.AddWithValue("@row", pRow);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public static DataSet getAllSeatsForBooking(int pBookingId)
        {
            DataSet dataSet = new DataSet();
            queryString = "SELECT * FROM Seats" +
                " Where Booking_Id = @booking_Id";
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

        public static int getNumOfSeatsLeftInAreaForShowing(int pShowingId, string pArea)
        {
            DataSet dataSet = new DataSet();
            queryString = "SELECT * FROM Seats" +
                " Where Showing_Id = @showingId" +
                " And Section = @section" +
                " And Booking_Id = 0";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@showingId", pShowingId);
                command.Parameters.AddWithValue("@section", pArea);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }

            return dataSet.Tables[0].Rows.Count;
        }
        public static DataSet seatsSoldUnsold()
        {
            DataSet dataSet = new DataSet();
            queryString = "SELECT * FROM Showings, Plays" +
                " Where Plays.Play_Id = Showings.Play_Id";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }


            dataSet.Tables[0].Columns.Add("SeatsSold", typeof(int));
            dataSet.Tables[0].Columns.Add("SeatsUnSold", typeof(int));
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                int showingId = int.Parse(dataSet.Tables[0].Rows[i]["Showing_Id"].ToString());
                int numOfSeatsSold =  getNumOfSoldSeatsForShowing(showingId);
                int numOfUnSeatsSold = getNumOfUnSoldSeatsForShowing(showingId);
                dataSet.Tables[0].Rows[i]["SeatsSold"] = numOfSeatsSold;
                dataSet.Tables[0].Rows[i]["SeatsUnSold"] = numOfUnSeatsSold;
            }
            return dataSet;
        }

        public static int getNumOfSoldSeatsForShowing(int pShowingId)
        {
            DataSet dataSet = new DataSet();
            queryString = "SELECT * FROM Seats" +
                " Where Showing_Id = @showingId" +
                " And Booking_Id != 0";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@showingId", pShowingId);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }

            return dataSet.Tables[0].Rows.Count;
        }

        public static int getNumOfUnSoldSeatsForShowing(int pShowingId)
        {
            DataSet dataSet = new DataSet();
            queryString = "SELECT * FROM Seats" +
                " Where Showing_Id = @showingId" + 
                " And Booking_Id = 0";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@showingId", pShowingId);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }

            return dataSet.Tables[0].Rows.Count;
        }
    }
}