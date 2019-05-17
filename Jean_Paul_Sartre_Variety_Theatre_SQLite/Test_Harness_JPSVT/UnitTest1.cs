using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jean_Paul_Sartre_Variety_Theatre;
using System.Data;
using System.Collections.Generic;

namespace Test_Harness_JPSVT
{
    [TestClass]
    public class UnitTest1
    {
        DataSet dataSet = new DataSet();

        [TestMethod]
        public void testAddPlay()
        {
            SqlClassBase.setUpDbForTesting();


            PlaysClass.addPlay("Play1", 90);
            dataSet = PlaysClass.getPlayDetailsByName("Play1");

            string actualPlayName = dataSet.Tables[0].Rows[0]["Name"].ToString();
            int actualPlaylength = int.Parse(dataSet.Tables[0].Rows[0]["Length"].ToString());

            string expectedPlayName = "Play1";
            int expectedPlayLength = 90;

            Assert.AreEqual(expectedPlayName, actualPlayName);
            Assert.AreEqual(expectedPlayLength, actualPlaylength);
        }

        [TestMethod]
        public void testEditPlay()
        {
            PlaysClass.editPlay(2, "Play1Edited", 91);
            dataSet = PlaysClass.getPlayDetailsByName("Play1Edited");

            string actualPlayName = dataSet.Tables[0].Rows[0]["Name"].ToString();
            int actualPlaylength = int.Parse(dataSet.Tables[0].Rows[0]["Length"].ToString());

            string expectedPlayName = "Play1Edited";
            int expectedPlayLength = 91;

            Assert.AreEqual(expectedPlayName, actualPlayName);
            Assert.AreEqual(expectedPlayLength, actualPlaylength);
        }

        [TestMethod]
        public void testGetAllPlays()
        {
            dataSet = PlaysClass.getAllPlays();

            string actualPlayNameRow0 = dataSet.Tables[0].Rows[0]["Name"].ToString();
            int actualPlaylengthRow0 = int.Parse(dataSet.Tables[0].Rows[0]["Length"].ToString());
            string actualPlayNameRow1 = dataSet.Tables[0].Rows[1]["Name"].ToString();
            int actualPlaylengthRow1 = int.Parse(dataSet.Tables[0].Rows[1]["Length"].ToString());

            string expectedPlayNameRow0 = "DeletedPlay";
            int expectedPlayLengthRow0 = 0;
            string expectedPlayNameRow1 = "Play1Edited";
            int expectedPlayLengthRow1 = 91;

            Assert.AreEqual(expectedPlayNameRow0, actualPlayNameRow0);
            Assert.AreEqual(expectedPlayLengthRow0, actualPlaylengthRow0);
            Assert.AreEqual(expectedPlayNameRow1, actualPlayNameRow1);
            Assert.AreEqual(expectedPlayLengthRow1, actualPlaylengthRow1);
        }

        [TestMethod]
        public void testGetPlayById()
        {
            dataSet = PlaysClass.getPlayDetailsById(2);
            string actualPlayName = dataSet.Tables[0].Rows[0]["Name"].ToString();
            int actualPlaylength = int.Parse(dataSet.Tables[0].Rows[0]["Length"].ToString());

            string expectedPlayName = "Play1Edited";
            int expectedPlayLength = 91;

            Assert.AreEqual(expectedPlayName, actualPlayName);
            Assert.AreEqual(expectedPlayLength, actualPlaylength);
        }

        //[TestMethod]
        //public void testGetPlayByName()
        //{
        //    dataSet = testPlaysClass.getPlayDetailsByName("Play1Edited");

        //    string actualPlayName = dataSet.Tables[0].Rows[0]["Name"].ToString();
        //    int actualPlaylength = int.Parse(dataSet.Tables[0].Rows[0]["Length"].ToString());

        //    string expectedPlayName = "Play1Edited";
        //    int expectedPlayLength = 91;

        //    Assert.AreEqual(expectedPlayName, actualPlayName);
        //    Assert.AreEqual(expectedPlayLength, actualPlaylength);
        //}





        [TestMethod]
        public void testAddShowing()
        {
            ShowingsClass.addShowing(2, "2017-06-20 11:00", 5, 2, 1);
            dataSet = ShowingsClass.getShowingByShowingId(2);

            string actualShowingDate = dataSet.Tables[0].Rows[0]["Date"].ToString();
            double actualShowingUpperCirclePrice = double.Parse(dataSet.Tables[0].Rows[0]["UpperCirclePrice"].ToString());
            double actualShowingDressCirclePrice = double.Parse(dataSet.Tables[0].Rows[0]["DressCirclePrice"].ToString());
            double actualShowingStallPrice = double.Parse(dataSet.Tables[0].Rows[0]["StallsPrice"].ToString());

            string expectedShowingDate = "2017-06-20 11:00";
            double expectedShowingUpperCirclePrice = 5;
            double expectedShowingDressCirclePrice = 2;
            double expectedShowingStallPrice = 1;

            Assert.AreEqual(expectedShowingDate, actualShowingDate);
            Assert.AreEqual(expectedShowingUpperCirclePrice, actualShowingUpperCirclePrice);
            Assert.AreEqual(expectedShowingDressCirclePrice, actualShowingDressCirclePrice);
            Assert.AreEqual(expectedShowingStallPrice, actualShowingStallPrice);
        }

        [TestMethod]
        public void testEditShowing()
        {
            ShowingsClass.editShowingDetails(2, "2017-06-22 12:00", 7, 2, 1);
            dataSet = ShowingsClass.getShowingByShowingId(2);

            string actualShowingDate = dataSet.Tables[0].Rows[0]["Date"].ToString();
            double actualShowingUpperCirclePrice = double.Parse(dataSet.Tables[0].Rows[0]["UpperCirclePrice"].ToString());
            double actualShowingDressCirclePrice = double.Parse(dataSet.Tables[0].Rows[0]["DressCirclePrice"].ToString());
            double actualShowingStallPrice = double.Parse(dataSet.Tables[0].Rows[0]["StallsPrice"].ToString());

            string expectedShowingDate = "2017-06-22 12:00";
            double expectedShowingUpperCirclePrice = 7;
            double expectedShowingDressCirclePrice = 2;
            double expectedShowingStallPrice = 1;

            Assert.AreEqual(expectedShowingDate, actualShowingDate);
            Assert.AreEqual(expectedShowingUpperCirclePrice, actualShowingUpperCirclePrice);
            Assert.AreEqual(expectedShowingDressCirclePrice, actualShowingDressCirclePrice);
            Assert.AreEqual(expectedShowingStallPrice, actualShowingStallPrice);
        }

        [TestMethod]
        public void testGetAllShowings()
        {
            dataSet = ShowingsClass.getAllShowings();

            string actualShowingDateRow0 = dataSet.Tables[0].Rows[0]["Date"].ToString();
            double actualShowingUpperCirclePriceRow0 = double.Parse(dataSet.Tables[0].Rows[0]["UpperCirclePrice"].ToString());
            double actualShowingDressCirclePriceRow0 = double.Parse(dataSet.Tables[0].Rows[0]["DressCirclePrice"].ToString());
            double actualShowingStallPriceRow0 = double.Parse(dataSet.Tables[0].Rows[0]["StallsPrice"].ToString());

            string actualShowingDateRow1 = dataSet.Tables[0].Rows[1]["Date"].ToString();
            double actualShowingUpperCirclePriceRow1 = double.Parse(dataSet.Tables[0].Rows[1]["UpperCirclePrice"].ToString());
            double actualShowingDressCirclePriceRow1 = double.Parse(dataSet.Tables[0].Rows[1]["DressCirclePrice"].ToString());
            double actualShowingStallPriceRow1 = double.Parse(dataSet.Tables[0].Rows[1]["StallsPrice"].ToString());

            string expectedShowingDateRow0 = "2017-06-24 17:00";
            double expectedShowingUpperCirclePriceRow0 = 50;
            double expectedShowingDressCirclePriceRow0 = 25;
            double expectedShowingStallPriceRow0 = 12.5;

            string expectedShowingDateRow1 = "2017-06-22 12:00";
            double expectedShowingUpperCirclePriceRow1 = 7;
            double expectedShowingDressCirclePriceRow1 = 2;
            double expectedShowingStallPriceRow1 = 1;

            Assert.AreEqual(expectedShowingDateRow0, actualShowingDateRow0);
            Assert.AreEqual(expectedShowingUpperCirclePriceRow0, actualShowingUpperCirclePriceRow0);
            Assert.AreEqual(expectedShowingDressCirclePriceRow0, actualShowingDressCirclePriceRow0);
            Assert.AreEqual(expectedShowingStallPriceRow0, actualShowingStallPriceRow0);

            Assert.AreEqual(expectedShowingDateRow1, actualShowingDateRow1);
            Assert.AreEqual(expectedShowingUpperCirclePriceRow1, actualShowingUpperCirclePriceRow1);
            Assert.AreEqual(expectedShowingDressCirclePriceRow1, actualShowingDressCirclePriceRow1);
            Assert.AreEqual(expectedShowingStallPriceRow1, actualShowingStallPriceRow1);
        }

        //[TestMethod]
        //public void testGetShowingById()
        //{
        //    dataSet = testShowingsClass.getShowingByShowingId(2);

        //    string actualShowingDate = dataSet.Tables[0].Rows[0]["Date"].ToString();
        //    double actualShowingUpperCirclePrice = double.Parse(dataSet.Tables[0].Rows[0]["UpperCirclePrice"].ToString());
        //    double actualShowingDressCirclePrice = double.Parse(dataSet.Tables[0].Rows[0]["DressCirclePrice"].ToString());
        //    double actualShowingStallPrice = double.Parse(dataSet.Tables[0].Rows[0]["StallsPrice"].ToString());

        //    string expectedShowingDate = "22/06/2017";
        //    double expectedShowingUpperCirclePrice = 7;
        //    double expectedShowingDressCirclePrice = 2;
        //    double expectedShowingStallPrice = 1;

        //    Assert.AreEqual(expectedShowingDate, actualShowingDate);
        //    Assert.AreEqual(expectedShowingUpperCirclePrice, actualShowingUpperCirclePrice);
        //    Assert.AreEqual(expectedShowingDressCirclePrice, actualShowingDressCirclePrice);
        //    Assert.AreEqual(expectedShowingStallPrice, actualShowingStallPrice);
        //}

        [TestMethod]
        public void testGetShowingsByPlay()
        {
            dataSet = ShowingsClass.getShowingForPlays(2);

            string actualShowingDate = dataSet.Tables[0].Rows[0]["Date"].ToString();
            double actualShowingUpperCirclePrice = double.Parse(dataSet.Tables[0].Rows[0]["UpperCirclePrice"].ToString());
            double actualShowingDressCirclePrice = double.Parse(dataSet.Tables[0].Rows[0]["DressCirclePrice"].ToString());
            double actualShowingStallPrice = double.Parse(dataSet.Tables[0].Rows[0]["StallsPrice"].ToString());

            string expectedShowingDate = "2017-06-22 12:00";
            double expectedShowingUpperCirclePrice = 7;
            double expectedShowingDressCirclePrice = 2;
            double expectedShowingStallPrice = 1;

            Assert.AreEqual(expectedShowingDate, actualShowingDate);
            Assert.AreEqual(expectedShowingUpperCirclePrice, actualShowingUpperCirclePrice);
            Assert.AreEqual(expectedShowingDressCirclePrice, actualShowingDressCirclePrice);
            Assert.AreEqual(expectedShowingStallPrice, actualShowingStallPrice);
        }

        [TestMethod]
        public void testGetShowingByDate()
        {

        }

        [TestMethod]
        public void testGetShowingsBetweenDates()
        {

        }







        [TestMethod]
        public void testAddCustomer()
        {
            CustomerClass.newCustomer("CustomerFirstName", "CustomerLastName",
                "Customer1@gmail.com", "");
            dataSet = CustomerClass.getCustomerByLastName("CustomerLastName");

            string actualFirstName = dataSet.Tables[0].Rows[0]["First_Name"].ToString();
            string actualLastName = dataSet.Tables[0].Rows[0]["Last_Name"].ToString();
            string actualEmail = dataSet.Tables[0].Rows[0]["Email"].ToString();
            string actualGoldClubMembership = dataSet.Tables[0].Rows[0]["Membership_Expiry_Date"].ToString();

            string expectedFirstName = "CustomerFirstName";
            string expectedLastName = "CustomerLastName";
            string expectedEmail = "Customer1@gmail.com";
            string expectedGoldClubMembership = "";

            Assert.AreEqual(expectedFirstName, actualFirstName);
            Assert.AreEqual(expectedLastName, actualLastName);
            Assert.AreEqual(expectedEmail, actualEmail);
            Assert.AreEqual(expectedGoldClubMembership, actualGoldClubMembership);
        }

        [TestMethod]
        public void testEditCustomer()
        {
            CustomerClass.editCustomerDetails(2, "CustomerFirstNameEdited",
                "CustomerLastNameEdited", "Customer1@gmail.com", "2017-12-28");

            dataSet = CustomerClass.getCustomerByLastName("CustomerLastNameEdited");

            string actualFirstName = dataSet.Tables[0].Rows[0]["First_Name"].ToString();
            string actualLastName = dataSet.Tables[0].Rows[0]["Last_Name"].ToString();
            string actualEmail = dataSet.Tables[0].Rows[0]["Email"].ToString();
            string actualGoldClubMembership = dataSet.Tables[0].Rows[0]["Membership_Expiry_Date"].ToString();

            string expectedFirstName = "CustomerFirstNameEdited";
            string expectedLastName = "CustomerLastNameEdited";
            string expectedEmail = "Customer1@gmail.com";
            string expectedGoldClubMembership = "2017-12-28";

            Assert.AreEqual(expectedFirstName, actualFirstName);
            Assert.AreEqual(expectedLastName, actualLastName);
            Assert.AreEqual(expectedEmail, actualEmail);
            Assert.AreEqual(expectedGoldClubMembership, actualGoldClubMembership);
        }

        //[TestMethod]
        //public void testGetCustomerByLastName()
        //{

        //}

        [TestMethod]
        public void testGetAllCustomers()
        {
            dataSet = CustomerClass.getAllCustomers();

            string actualFirstNameRow0 = dataSet.Tables[0].Rows[0]["First_Name"].ToString();
            string actualLastNameRow0 = dataSet.Tables[0].Rows[0]["Last_Name"].ToString();
            string actualEmailRow0 = dataSet.Tables[0].Rows[0]["Email"].ToString();
            string actualGoldClubMembershipRow0 = dataSet.Tables[0].Rows[0]["Membership_Expiry_Date"].ToString();

            string expectedFirstNameRow0 = "DeletedCustomerFirstName";
            string expectedLastNameRow0 = "DeletedCustomerLastName";
            string expectedEmailRow0 = "";
            string expectedGoldClubMembershipRow0 = "";

            string actualFirstNameRow1 = dataSet.Tables[0].Rows[1]["First_Name"].ToString();
            string actualLastNameRow1 = dataSet.Tables[0].Rows[1]["Last_Name"].ToString();
            string actualEmailRow1 = dataSet.Tables[0].Rows[1]["Email"].ToString();
            string actualGoldClubMembershipRow1 = dataSet.Tables[0].Rows[1]["Membership_Expiry_Date"].ToString();

            string expectedFirstNameRow1 = "CustomerFirstNameEdited";
            string expectedLastNameRow1 = "CustomerLastNameEdited";
            string expectedEmailRow1 = "Customer1@gmail.com";
            string expectedGoldClubMembershipRow1 = "2017-12-28";

            Assert.AreEqual(expectedFirstNameRow0, actualFirstNameRow0);
            Assert.AreEqual(expectedLastNameRow0, actualLastNameRow0);
            Assert.AreEqual(expectedEmailRow0, actualEmailRow0);
            Assert.AreEqual(expectedGoldClubMembershipRow0, actualGoldClubMembershipRow0);

            Assert.AreEqual(expectedFirstNameRow1, actualFirstNameRow1);
            Assert.AreEqual(expectedLastNameRow1, actualLastNameRow1);
            Assert.AreEqual(expectedEmailRow1, actualEmailRow1);
            Assert.AreEqual(expectedGoldClubMembershipRow1, actualGoldClubMembershipRow1);
        }

        [TestMethod]
        public void testGetAllGoldClubMembers()
        {
            dataSet = CustomerClass.getCustomersGoldClubMembers();

            string actualFirstName = dataSet.Tables[0].Rows[0]["First_Name"].ToString();
            string actualLastName = dataSet.Tables[0].Rows[0]["Last_Name"].ToString();
            string actualEmail = dataSet.Tables[0].Rows[0]["Email"].ToString();
            string actualGoldClubMembership = dataSet.Tables[0].Rows[0]["Membership_Expiry_Date"].ToString();

            string expectedFirstName = "CustomerFirstNameEdited";
            string expectedLastName = "CustomerLastNameEdited";
            string expectedEmail = "Customer1@gmail.com";
            string expectedGoldClubMembership = "2017-12-28";

            Assert.AreEqual(expectedFirstName, actualFirstName);
            Assert.AreEqual(expectedLastName, actualLastName);
            Assert.AreEqual(expectedEmail, actualEmail);
            Assert.AreEqual(expectedGoldClubMembership, actualGoldClubMembership);
        }

        [TestMethod]
        public void testGetAllGoldCLubMembersExpiring()
        {

        }

        [TestMethod]
        public void testGetAllNonGoldClubMembers()
        {
            dataSet = CustomerClass.getCustomersNonGoldCLubMembers();

            string actualFirstName = dataSet.Tables[0].Rows[0]["First_Name"].ToString();
            string actualLastName = dataSet.Tables[0].Rows[0]["Last_Name"].ToString();
            string actualEmail = dataSet.Tables[0].Rows[0]["Email"].ToString();
            string actualGoldClubMembership = dataSet.Tables[0].Rows[0]["Membership_Expiry_Date"].ToString();

            string expectedFirstName = "DeletedCustomerFirstName";
            string expectedLastName = "DeletedCustomerLastName";
            string expectedEmail = "";
            string expectedGoldClubMembership = "";

            Assert.AreEqual(expectedFirstName, actualFirstName);
            Assert.AreEqual(expectedLastName, actualLastName);
            Assert.AreEqual(expectedEmail, actualEmail);
            Assert.AreEqual(expectedGoldClubMembership, actualGoldClubMembership);
        }






        [TestMethod]
        public void testAddBooking()
        {
            SeatBookingClass booking1 = new SeatBookingClass("Stall", "M", 5, 2);
            List<SeatBookingClass> SeatsToBookList = new List<SeatBookingClass>();
            SeatsToBookList.Add(booking1);
            BookingsClass.newBooking(2, "2017-06-20 12:00", 0, SeatsToBookList);

            dataSet = BookingsClass.getBookingDetailsById(1);

            int actualCustomerId = int.Parse(dataSet.Tables[0].Rows[0]["Customer_Id"].ToString());
            string actualBookingDate = dataSet.Tables[0].Rows[0]["Date_Of_Booking"].ToString();
            int actualPaid = int.Parse(dataSet.Tables[0].Rows[0]["Paid"].ToString());

            dataSet = SeatsClass.getAllSeatsForBooking(1);

            string actualSection = dataSet.Tables[0].Rows[0]["Section"].ToString();
            string actualRow = dataSet.Tables[0].Rows[0]["Row"].ToString();
            int actualNumber = int.Parse(dataSet.Tables[0].Rows[0]["Number"].ToString());
            int actualShowingId = int.Parse(dataSet.Tables[0].Rows[0]["Showing_Id"].ToString());
            int actualBookingId = int.Parse(dataSet.Tables[0].Rows[0]["Booking_Id"].ToString());

            int expectedCustomerId = 2;
            string expectedBookingDate = "2017-06-20 12:00";
            int expectedPaid = 0;
            string expectedSection = "Stall";
            string expectedRow = "M";
            int expectedNumber = 5;
            int expectedShowingId = 2;
            int expectedBookingId = 1;

            Assert.AreEqual(expectedCustomerId, actualCustomerId);
            Assert.AreEqual(expectedBookingDate, actualBookingDate);
            Assert.AreEqual(expectedPaid, actualPaid);
            Assert.AreEqual(expectedSection, actualSection);
            Assert.AreEqual(expectedRow, actualRow);
            Assert.AreEqual(expectedNumber, actualNumber);
            Assert.AreEqual(expectedShowingId, actualShowingId);
            Assert.AreEqual(expectedBookingId, actualBookingId);
        }

        [TestMethod]
        public void testPayBooking()
        {
            BookingsClass.payBooking(1);
            dataSet = BookingsClass.getBookingDetailsById(1);

            int actualPaid = int.Parse(dataSet.Tables[0].Rows[0]["Paid"].ToString());

            int expectedPaid = 1;

            Assert.AreEqual(expectedPaid, actualPaid);
        }

        [TestMethod]
        public void testGetAllBookingsForCustomer()
        {
            dataSet = BookingsClass.getAllBookingsForCustomer(2);

            int actualBooking_Id = int.Parse(dataSet.Tables[0].Rows[0]["Booking_Id"].ToString());
            int actualCustomer_Id = int.Parse(dataSet.Tables[0].Rows[0]["Customer_Id"].ToString());
            string actualDateOfBooking = dataSet.Tables[0].Rows[0]["Date_Of_Booking"].ToString();
            int actualPaid = int.Parse(dataSet.Tables[0].Rows[0]["Paid"].ToString());

            int expectedBookingId = 1;
            int expectedCustomer_Id = 2;
            string expectedDateOfBooking = "2017-06-20 12:00";
            int expectedPaid = 1;

            Assert.AreEqual(expectedBookingId, actualBooking_Id);
            Assert.AreEqual(expectedCustomer_Id, actualCustomer_Id);
            Assert.AreEqual(expectedDateOfBooking, actualDateOfBooking);
            Assert.AreEqual(expectedPaid, actualPaid);
        }

        [TestMethod]
        public void testGetAllBookingsForShowing()
        {
            dataSet = BookingsClass.getAllBookingsForShowing(2);

            int actualBooking_Id = int.Parse(dataSet.Tables[0].Rows[0]["Booking_Id"].ToString());
            int actualCustomer_Id = int.Parse(dataSet.Tables[0].Rows[0]["Customer_Id"].ToString());
            string actualDateOfBooking = dataSet.Tables[0].Rows[0]["Date_Of_Booking"].ToString();
            int actualPaid = int.Parse(dataSet.Tables[0].Rows[0]["Paid"].ToString());

            int expectedBookingId = 1;
            int expectedCustomer_Id = 2;
            string expectedDateOfBooking = "2017-06-20 12:00";
            int expectedPaid = 1;

            Assert.AreEqual(expectedBookingId, actualBooking_Id);
            Assert.AreEqual(expectedCustomer_Id, actualCustomer_Id);
            Assert.AreEqual(expectedDateOfBooking, actualDateOfBooking);
            Assert.AreEqual(expectedPaid, actualPaid);
        }





        [TestMethod]
        public void testDeletePlay()
        {
            PlaysClass.deletePlay(2);
            dataSet = ShowingsClass.getShowingByShowingId(2);

            int actualPlayId = int.Parse(dataSet.Tables[0].Rows[0]["Play_Id"].ToString());

            int expectedPlayId = 1;

            Assert.AreEqual(expectedPlayId, actualPlayId);
        }

        [TestMethod]
        public void testDeleteShowing()
        {
            dataSet = ShowingsClass.deleteShowing(2);

            string actualEmail = dataSet.Tables[0].Rows[0]["Email"].ToString();
            string expectedEmail = "Customer1@gmail.com";

            dataSet = SeatsClass.getSeatDetails("Stall", "M", 5, 2);
            int actualRowCountSeats = dataSet.Tables[0].Rows.Count;
            int expectedRowCountSeats = 0;

            dataSet = BookingsClass.getBookingDetailsById(1);
            int actualRowCountBookings = dataSet.Tables[0].Rows.Count;
            int expectedRowCountBookings = 0;

            Assert.AreEqual(expectedRowCountSeats, actualRowCountSeats);
            Assert.AreEqual(expectedEmail, actualEmail);
            Assert.AreEqual(expectedRowCountBookings, actualRowCountBookings);
        }

        [TestMethod]
        public void testDeleteBooking()
        {
            SeatBookingClass booking1 = new SeatBookingClass("Stall", "M", 5, 1);
            List<SeatBookingClass> SeatsToBookList = new List<SeatBookingClass>();
            SeatsToBookList.Add(booking1);
            BookingsClass.newBooking(1, "2017-06-28 12:00", 0, SeatsToBookList);

            SeatBookingClass booking2 = new SeatBookingClass("Stall", "M", 6, 1);
            SeatsToBookList[0] = booking2;
            BookingsClass.newBooking(2, "2017-06-27 12:00", 0, SeatsToBookList);

            BookingsClass.deleteBooking(2);
            dataSet = BookingsClass.getBookingDetailsById(2);
            int actualBookingRowCount = dataSet.Tables[0].Rows.Count;
            int expectedBookingRowCount = 0;

            dataSet = SeatsClass.getSeatDetails("Stall", "M", 5, 1);
            int actualSeatBookingId = int.Parse(dataSet.Tables[0].Rows[0]["booking_Id"].ToString());
            int epectedSeatBookingId = 0;

            Assert.AreEqual(expectedBookingRowCount, actualBookingRowCount);
            Assert.AreEqual(epectedSeatBookingId, actualSeatBookingId);
        }

        [TestMethod]
        public void testDeleteCustomer()
        {
            //testCustomersClass.deleteCustomer(1);

            //dataSet = testBookingsClass.getBookingDetailsById(3);

            //int actualDataSetCount = dataSet.Tables[0].Rows.Count;
            //int expectedDataSetCount = 0;

            //Assert.AreEqual(expectedDataSetCount, actualDataSetCount);

            //delete customer 2
            //check the booking pointed at customer 2
            //should now be pointed customer i the deleted customer

            CustomerClass.deleteCustomer(2);
            dataSet = BookingsClass.getBookingDetailsById(3);
            int actualCustomerId = int.Parse(dataSet.Tables[0].Rows[0]["Customer_Id"].ToString());
            int expectedCustomerId = 1;

            Assert.AreEqual(expectedCustomerId, actualCustomerId);
        }
    }
}
