using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace Jean_Paul_Sartre_Variety_Theatre
{
    public static class PlaysClass
    {
        private static SQLiteDataReader sqlReader;
        private static String queryString;

        public static bool checkPlayExists(string pName)
        {
            DataSet dataSet = new DataSet();
            queryString = "SELECT * FROM Plays WHERE Name = @name";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@name", pName);
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

        public static void addPlay(string pName, int pLength)
        {
            queryString = "INSERT INTO Plays(Name, Length) VALUES(@name, @length)";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);

            connection.Open();
            command.Parameters.AddWithValue("@name", pName);
            command.Parameters.AddWithValue("@length", pLength);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static DataSet getPlayDetailsById(int pId)
        {
            //SQLiteCommand command = new SQLiteCommand(queryString, this.getConnectionString());

            //this.getConnectionString().Open();
            //command.Parameters.AddWithValue("@playId", pId);
            //sqlReader =  command.ExecuteReader();
            //this.getConnectionString().Close();

            //return sqlReader;

            DataSet dataSet = new DataSet();
            queryString = "SELECT * FROM Plays WHERE Play_Id = @playId";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@playId", pId);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public static DataSet getPlayDetailsByName(string pPlayName)
        {
            //queryString = "SELECT * FROM Plays WHERE Name = @playName";
            //SQLiteCommand command = new SQLiteCommand(queryString, this.getConnectionString());

            //this.getConnectionString().Open();
            //command.Parameters.AddWithValue("@playName", pPlayName);
            //sqlReader = command.ExecuteReader();
            //this.getConnectionString().Close();

            //return sqlReader;

            DataSet dataSet = new DataSet();
            queryString = "SELECT * FROM Plays WHERE Name = @playName";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@playName", pPlayName);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        public static void deletePlay(int pPlay_Id)
        {
	        changeShowingsOfPlayToDeletedShowing(pPlay_Id);
            queryString = "DELETE FROM Plays WHERE Play_Id = @play_Id";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);

            connection.Open();
            command.Parameters.AddWithValue("@play_Id", pPlay_Id);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private static void changeShowingsOfPlayToDeletedShowing(int pPlay_Id)
        {
            int deletedPlayId = getIdOfDeletePlayRecord();
            DataSet showingsToDelete = ShowingsClass.getAllShowingsPastCurrentDate();
            foreach(DataRow row in showingsToDelete.Tables[0].Rows)
            {
                ShowingsClass.deleteShowing(int.Parse(row["Showing_Id"].ToString()));
            }

            queryString = "UPDATE Showings Set Play_Id = @deletedPlayId WHERE Play_Id = @play_Id";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);

            connection.Open();
            command.Parameters.AddWithValue("@play_Id", pPlay_Id);
            command.Parameters.AddWithValue("@deletedPlayId", deletedPlayId);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private static int getIdOfDeletePlayRecord()
        {
            //queryString = "Select Play_Id From Plays Where Name = @deletedPlayName";
            //SQLiteConnection connection = new SQLiteConnection(this.getConnectionString());
            //SQLiteCommand command = new SQLiteCommand(queryString, connection);

            //connection.Open();
            //command.Parameters.AddWithValue("@deletedPlayName", "DeletedPlay");
            //sqlReader = command.ExecuteReader();

            //int deletedPlayId = 0;
            //try
            //{
            //    while (sqlReader.Read())
            //    {
            //        deletedPlayId = int.Parse(sqlReader["Play_Id"].ToString());
            //    }
            //}
            //catch (Exception)
            //{

            //    makeDeletePlayRecord();
            //    deletedPlayId = getIdOfDeletePlayRecord();
            //}
            //connection.Close();
            //return deletedPlayId;

            DataSet dataSet = new DataSet();
            queryString = "Select Play_Id From Plays Where Name = @deletedPlayName";
            using (SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.AddWithValue("@deletedPlayName", "DeletedPlay");
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }

            int deletedPlayId;

            if (dataSet.Tables[0].Rows.Count == 0)
            {
                makeDeletePlayRecord();
                deletedPlayId = getIdOfDeletePlayRecord();
            }
            else
            {
                deletedPlayId = int.Parse(dataSet.Tables[0].Rows[0]["Play_Id"].ToString());
            }
            return deletedPlayId;
        }

        private static void makeDeletePlayRecord()
        {
            queryString = "INSERT INTO Plays(Name, Length) VALUES(@name, @length)";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);

            connection.Open();
            command.Parameters.AddWithValue("@name", "DeletedPlay");
            command.Parameters.AddWithValue("@length", 0);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void editPlay(int pPlay_Id, string pName, int pLength)
        {
            queryString = "UPDATE Plays SET Name = @name, Length = @length WHERE Play_Id = @play_Id";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);

            connection.Open();
            command.Parameters.AddWithValue("@play_Id", pPlay_Id);
            command.Parameters.AddWithValue("@name", pName);
            command.Parameters.AddWithValue("@length", pLength);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static DataSet getAllPlays()
        {
            queryString = "SELECT * FROM Plays";
            return (SqlClassBase.commitSqlQuerryNonParameterized(queryString));
        }

        private static int getLastPlayId()
        {
            queryString = "select seq from sqlite_sequence where name=@tableName";
            SQLiteConnection connection = new SQLiteConnection(SqlClassBase.getConnectionString());
            SQLiteCommand command = new SQLiteCommand(queryString, connection);

            connection.Open();
            command.Parameters.AddWithValue("@tableName", "Plays");
            sqlReader = command.ExecuteReader();
            connection.Close();

            int lastId = 0;
            while (sqlReader.Read())
            {
                lastId = int.Parse(sqlReader[0].ToString());
            }
            return lastId;
        }

    }
}