using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace Jean_Paul_Sartre_Variety_Theatre
{
    public static class ShowingsClass
    {
        private static SQLiteDataReader sqlReader;
        private static String queryString;

        public static DataSet findAllShowingsOfPlay(int pId)
        {
            //queryString = "SELECT Date" +
            //       " From Plays, Showings" +
            //       " WHERE Plays.Play_Id = Showings.Play_Id" +
            //       " AND Plays.Play_Id = @pId";
            //   SQLiteCommand command = new SQLiteCommand(queryString, this.getConnectionString());

            //   this.getConnectionString().Open();
            //   command.Parameters.AddWithValue("@pId", pId);
            //   sqlReader = command.ExecuteReader();
            //   this.getConnectionString().Close();

            //   return sqlReader;
            DataSet dataSet = new DataSet();
            queryString = "SELECT * From Plays, Showings" +
                " WHERE Plays.Play_Id = Showings.Play_Id" +
                " AND Plays.Play_Id = @pId";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@pId", pId);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public static DataSet getAllShowings()
        {
            //queryString = "SELECT Showing_Id,Name, Length, Date" +
            //    " FROM Plays, Showings" +
            //    " WHERE Plays.Play_Id = Showings.Play_Id";
            queryString = "SELECT *" +
                " FROM Plays, Showings" +
                " WHERE Plays.Play_Id = Showings.Play_Id";
            //test
            return (SqlClassBase.commitSqlQuerryNonParameterized(queryString));
        }

        public static void addShowing(int pPlay_Id, string pDate, double pUpperCirclepricePrice, double pDressCirclePrice, double pStallsPrice)
        {
            queryString = "INSERT INTO Showings(Play_Id, Date, UpperCirclePrice, DressCirclePrice, StallsPrice) VALUES(@play_Id, @date, @upperCirclePrice, @dressCirclePrice, @stallsPrice)";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);

            connection.Open();
            command.Parameters.AddWithValue("@play_Id", pPlay_Id);
            command.Parameters.AddWithValue("@date", pDate);
            command.Parameters.AddWithValue("@upperCirclePrice", pUpperCirclepricePrice);
            command.Parameters.AddWithValue("@dressCirclePrice", pDressCirclePrice);
            command.Parameters.AddWithValue("@stallsPrice", pStallsPrice);
            command.ExecuteNonQuery();
            connection.Close();

            SeatsClass.generateSeats(getLastShowingId());
        }

        private static int getLastShowingId()
        {
            queryString = "select seq from sqlite_sequence where name=@tableName";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);

            connection.Open();
            command.Parameters.AddWithValue("@tableName", "Showings");
            sqlReader = command.ExecuteReader();

            int lastId = 0;
            while (sqlReader.Read())
            {
                lastId = int.Parse(sqlReader[0].ToString());
            }
            connection.Close();
            return lastId;
        }

        public static DataSet getShowingByShowingId(int pShowing_Id)
        {
            //queryString = "Select Name, Length, Date, Price" +
            //    " FROM Plays, Showings" +
            //    " WHERE Plays.Play_Id = Showings.Play_Id" +
            //    " AND Showing_Id = @showingId";
            //SQLiteCommand command = new SQLiteCommand(queryString, this.getConnectionString());

            //this.getConnectionString().Open();
            //command.Parameters.AddWithValue("@showingId", pShowing_Id);
            //sqlReader =  command.ExecuteReader();
            //this.getConnectionString().Close();

            //return sqlReader;

            DataSet dataSet = new DataSet();
            queryString = "Select *" +
                " FROM Plays, Showings" +
                " WHERE Plays.Play_Id = Showings.Play_Id" +
                " AND Showing_Id = @showingId";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@showingId", pShowing_Id);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public static DataSet getShowingForPlays(int pPlay_Id)
        {

            DataSet dataSet = new DataSet();
            queryString = "Select *" +
                " FROM Plays, Showings" +
                " WHERE Plays.Play_Id = Showings.Play_Id" +
                " AND Showings.Play_Id = @playId";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@playId", pPlay_Id);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public static DataSet deleteShowing(int pShowingId)
        {
            DataSet emailList = getBookingsLinkedToDeletingShowing(pShowingId);

            deleteBookingsLinkedToDeletingShowing(pShowingId);

            deleteSeatsLinkedToShowing(pShowingId);

            queryString = "DELETE FROM Showings WHERE Showing_Id = @showing_Id";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);

            connection.Open();
            command.Parameters.AddWithValue("@showing_Id", pShowingId);
            command.ExecuteNonQuery();
            connection.Close();

            return emailList;
        }

        public static void deleteSeatsLinkedToShowing(int pShowingId)
        {
            queryString = "DELETE FROM Seats Where Showing_Id = @showing_Id";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);
            connection.Open();
            command.Parameters.AddWithValue("@showing_Id", pShowingId);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static DataSet getBookingsLinkedToDeletingShowing(int pShowingId)
        {
            DataSet dataSet = new DataSet();
            string queryString = "Select Customers.Email" +
                " From Customers,Bookings,Seats,Showings" +
                " Where Showings.Showing_Id = Seats.Showing_Id" +
                " And Seats.Booking_Id = Bookings.Booking_Id" +
                " And Bookings.Customer_Id = Customers.Customer_Id" +
                " And Showings.Showing_Id = @showing_Id";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@showing_Id", pShowingId);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public static void deleteBookingsLinkedToDeletingShowing(int pShowingId)
        {
            DataSet dataSet = new DataSet();
            queryString = "SELECT Bookings.Booking_Id FROM Bookings, Seats, Showings" +
                " WHERE Showings.Showing_Id = Seats.Showing_Id" +
                " AND Seats.Booking_Id = Bookings.Booking_Id" +
                " And Showings.Showing_Id = @showing_Id";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@showing_Id", pShowingId);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }

            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                int booking_Id = int.Parse(dataSet.Tables[0].Rows[i]["Booking_Id"].ToString());
                queryString = "DELETE FROM Bookings Where Booking_Id = @booking_Id";
                SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@booking_Id", booking_Id);
                command.ExecuteNonQuery();
                connection.Close();
            }
            //   queryString = "DELETE FROM Bookings" +
            //       " INNER JOIN Seats ON Bookings.Booking_Id = Seats.Booking_Id" +
            //       " INNER JOIN Showings ON Seats.Showing_Id = Showings.Showing_Id";
            //       //" Where Showings.Showing_Id = @showing_Id";
            //   SQLiteConnection connection = new SQLiteConnection(this.getConnectionString());
            //   SQLiteCommand command = new SQLiteCommand(queryString, connection);

            //   connection.Open();
            //command.Parameters.AddWithValue("@showing_Id", pShowingId);
            //   command.ExecuteNonQuery();
            //   connection.Close();
        }

        public static DataSet getAllSeatsForShowing(int pShowingId)
        {
            //queryString = "SELECT Section, Row, Number, State" +
            //    " From Seats" +
            //    " Where Seats.Showing_Id = Showings.Showing_Id" +
            //    " And Seats.Showing_Id = @showing_Id";
            //SQLiteCommand command = new SQLiteCommand(queryString, this.getConnectionString());

            //this.getConnectionString().Open();
            //command.Parameters.AddWithValue("@showing_Id", pShowingId);
            //sqlReader =  command.ExecuteReader();
            //this.getConnectionString().Close();

            //return sqlReader;

            DataSet dataSet = new DataSet();
            queryString = "SELECT *" +
                " From Seats" +
                " Where Seats.Showing_Id = Showings.Showing_Id" +
                " And Seats.Showing_Id = @showing_Id";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@showing_Id", pShowingId);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public static void editShowingDetails(int pShowingId, string pDate, double pUpperCirclePrice, double pDressCirclePrice, double pStallsPrice)
        {
            queryString = "UPDATE Showings SET Date = @date, UpperCirclePrice = @upperCirclePrice," +
                " DressCirclePrice = @dressCirclePrice, StallsPrice = @stallsPrice" +
                " WHERE Showing_Id = @showingId";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);

            connection.Open();
            command.Parameters.AddWithValue("@showingId", pShowingId);
            command.Parameters.AddWithValue("@date", pDate);
            command.Parameters.AddWithValue("@upperCirclePrice", pUpperCirclePrice);
            command.Parameters.AddWithValue("@dressCirclePrice", pDressCirclePrice);
            command.Parameters.AddWithValue("@stallsPrice", pStallsPrice);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static DataSet getAllShowingsBetweenDates(string pStartDate, string pEndDate)
        {
            DataSet dataSet = new DataSet();
            string query = "Select * From Showings Where Date BETWEEN @startDate AND @endDate";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@startDate", pStartDate + " 00:00");
                command.Parameters.AddWithValue("@endDate", pEndDate + " 24:00");
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public static DataSet getAllShowingOnDate(string pDate)
        {
            DataSet dataSet = new DataSet();
            string queryString = "Select * From Showings Where Date BETWEEN @startDate AND @endDate";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@startDate", pDate + " 00:00");
                command.Parameters.AddWithValue("@endDate", pDate + " 24:00");
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;

        }

        public static Boolean makeShowings(DateTime pStartDate, DateTime pEndDate, string pStartTime, int pPlay_Id, double pUpperCirclepricePrice, double pDressCirclePrice, double pStallsPrice)
        {
            Boolean sqlQuerryState = true;
            DateTime showingDate = pStartDate;
            //int numberOfWeeks = int.Parse((pEndDate.Subtract(pStartDate).TotalDays / 7).ToString());
            double numberOfDays = pEndDate.Subtract(pStartDate).TotalDays;
            int dayCounter = 0;

            while (sqlQuerryState == true && dayCounter <= numberOfDays)
            {
                //string startDateTime = formatDateTimeToSqlLiteDateTimeString(showingDate, pStartTime);
                //string endDateTime = formatDateTimeToSqlLiteDateTimeString(showingDate, pEndTime);
                sqlQuerryState = checkIfShowingDateAviable(showingDate);
                showingDate = showingDate.AddDays(1);
                dayCounter = dayCounter + 1;
            }

            if (sqlQuerryState == true)
            {
                showingDate = pStartDate;
                for (int i = 0; i < dayCounter; i++)
                {
                    string showingDateTime = formatDateTimeToSqlLiteDateTimeString(showingDate, pStartTime);
                    addShowing(pPlay_Id, showingDateTime, pUpperCirclepricePrice, pDressCirclePrice, pStallsPrice);
                    showingDate = showingDate.AddDays(1);
                }
            }

            return sqlQuerryState;
        }

        private static bool checkIfShowingDateAviable(DateTime pDate)
        {
            string dateString = formatDateTimeToSqlLiteDateString(pDate);
            DataSet dataSet = getAllShowingOnDate(dateString);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
            //foreach (DataRow row in dataSet.Tables[0].Rows)
            //{
            //    if (overlap == false)
            //    {
            //        string newShowingStartDateTimeString = dateString + " " + pStartTime;
            //        string newShowingEndDateTimeString = dateString + " " + pEndTime;
            //        string existingShowingStartDateTimeString = row["Date"].ToString();
            //        dataSet = PlaysClass.getPlayDetailsById(int.Parse(row["Play_Id"].ToString()));
            //        int existingShowingLength = int.Parse(dataSet.Tables[0].Rows[0]["Length"].ToString());

            //        DateTime exisitingShowingStartDateTime = turnSqlLiteDateStringIntoDateTime(existingShowingStartDateTimeString);
            //        DateTime exisitingShowingEndDateTime = exisitingShowingStartDateTime.AddMinutes(existingShowingLength);

            //        DateTime newShowingStartDateTime = turnSqlLiteDateStringIntoDateTime(newShowingStartDateTimeString);
            //        DateTime newShowingEndDateTime = turnSqlLiteDateStringIntoDateTime(newShowingEndDateTimeString);

            //        overlap = newShowingStartDateTime < exisitingShowingEndDateTime && exisitingShowingStartDateTime < newShowingEndDateTime;
            //    }
            //}
            //DataSet dataSet = getAllShowingOnDate();

            //DataSet dataSet = getAllShowingsBetweenDates(pStart, pEnd);

            //if (overlap == false)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        public static DateTime turnSqlLiteDateStringIntoDateTime(string pDate)
        {
            char[] charArray = pDate.ToCharArray();
            if (charArray.Count() == 16)
            {
                int year = int.Parse(charArray[0].ToString() + charArray[1].ToString() + charArray[2].ToString() + charArray[3].ToString());
                int month = int.Parse(charArray[5].ToString() + charArray[6].ToString());
                int days = int.Parse(charArray[8].ToString() + charArray[9].ToString());
                int hours = int.Parse(charArray[11].ToString() + charArray[12].ToString());
                int minutes = int.Parse(charArray[14].ToString() + charArray[15].ToString());
               return new DateTime(year, month, days, hours, minutes, 00);
            }
            else
            {
                int year = int.Parse(charArray[0].ToString() + charArray[1].ToString() + charArray[2].ToString() + charArray[3].ToString());
                int month = int.Parse(charArray[5].ToString() + charArray[6].ToString());
                int days = int.Parse(charArray[8].ToString() + charArray[9].ToString());
                int hours = int.Parse("0" + charArray[11].ToString());
                int minutes = int.Parse(charArray[13].ToString() + charArray[14].ToString());
                return new DateTime(year, month, days, hours, minutes, 00);
            }
        }

        public static string formatDateTimeToSqlLiteDateTimeString(DateTime pDate, string pTime)
        {
            string month = (DateTime.Parse(pDate.ToString()).Month).ToString();
            string day = (DateTime.Parse(pDate.ToString()).Day).ToString();

            if (int.Parse(month) < 10)
            {
                if (int.Parse(day) < 10)
                {
                    return (DateTime.Parse(pDate.ToString()).Year).ToString() +
                        "-" +
                        "0" + month +
                        "-" +
                        "0" + day +
                        " " + pTime;
                }
                else
                {
                    return (DateTime.Parse(pDate.ToString()).Year).ToString() +
                        "-" +
                        "0" + month +
                        "-" +
                        (DateTime.Parse(pDate.ToString()).Day).ToString() +
                        " " + pTime;
                }
            }
            else
            {
                if (int.Parse(day) < 10)
                {
                    return (DateTime.Parse(pDate.ToString()).Year).ToString() +
                        "-" +
                        (DateTime.Parse(pDate.ToString()).Month).ToString() +
                        "-" +
                        "0" + day +
                        " " + pTime;
                }
                else
                {
                    return (DateTime.Parse(pDate.ToString()).Year).ToString() +
                        "-" +
                        (DateTime.Parse(pDate.ToString()).Month).ToString() +
                        "-" +
                        (DateTime.Parse(pDate.ToString()).Day).ToString() +
                        " " + pTime;
                }
            }

        }

        public static string formatDateTimeToSqlLiteDateString(DateTime pDate)
        {
            string month = (DateTime.Parse(pDate.ToString()).Month).ToString();
            string day = (DateTime.Parse(pDate.ToString()).Day).ToString();
            if (int.Parse(month) < 10)
            {
                if (int.Parse(day) < 10)
                {
                    return (DateTime.Parse(pDate.ToString()).Year).ToString() +
                        "-" +
                        "0" + month +
                        "-" +
                        "0" + day;
                }
                else
                {
                    return (DateTime.Parse(pDate.ToString()).Year).ToString() +
                        "-" +
                        "0" + month +
                        "-" +
                        (DateTime.Parse(pDate.ToString()).Day).ToString();
                }
            }
            else
            {
                if (int.Parse(day) < 10)
                {
                    return (DateTime.Parse(pDate.ToString()).Year).ToString() +
                        "-" +
                        (DateTime.Parse(pDate.ToString()).Month).ToString() +
                        "-" +
                        "0" + day;
                }
                else
                {
                    return (DateTime.Parse(pDate.ToString()).Year).ToString() +
                        "-" +
                        (DateTime.Parse(pDate.ToString()).Month).ToString() +
                        "-" +
                        (DateTime.Parse(pDate.ToString()).Day).ToString();
                }
            }
        }

        public static string replaceSqlDateTimeWithNewTime(string pDateTimeString, string pTime)
        {
            char[] charArray = pDateTimeString.ToCharArray();
            int year = int.Parse(charArray[0].ToString() + charArray[1].ToString() + charArray[2].ToString() + charArray[3].ToString());
            int month = int.Parse(charArray[5].ToString() + charArray[6].ToString());
            int days = int.Parse(charArray[8].ToString() + charArray[9].ToString());

            return (year + "-" + month + "-" + days + " " + pTime);
        }

        public static int getShowingIdByDate(string pDate)
        {
            DataSet dataset = getAllShowingOnDate(pDate);
            return int.Parse(dataset.Tables[0].Rows[0]["Showing_Id"].ToString());
        }

        public static string getTimeFromSqlDateSting(string pDateTime)
        {
            char[] chararray = pDateTime.ToCharArray();
            return chararray[11].ToString() + chararray[12].ToString() + chararray[13].ToString() + chararray[14].ToString() + chararray[15].ToString();
        }

        public static DataSet getAllShowingsPastCurrentDate()
        {
            string dateString = formatDateTimeToSqlLiteDateString(DateTime.Now);
            DataSet dataSet = new DataSet();
            string queryString = "Select * From Showings, Plays Where Date > date(@dateToCompare)" +
                " And Showings.Play_Id = Plays.Play_Id";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@dateToCompare", dateString + " 00:00");
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }
    }
}