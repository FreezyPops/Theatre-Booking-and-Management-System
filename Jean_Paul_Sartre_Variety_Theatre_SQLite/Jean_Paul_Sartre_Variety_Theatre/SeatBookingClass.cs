using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jean_Paul_Sartre_Variety_Theatre
{
    public class SeatBookingClass
    {
        private string mSection;
        private string mRow;
        private int mNumber;
        private int mShowing_id;

        public SeatBookingClass(string pSection, string pRow ,int pNumber , int pShowing_Id)
        {
            setSection(pSection);
            setRow(pRow);
            setNumber(pNumber);
            setShowing_Id(pShowing_Id);
        }

        private void setSection(string pSection)
        {
            mSection = pSection;
        }

        private void setRow(string pRow)
        {
            mRow = pRow;
        }

        private void setNumber(int pNumber)
        {
            mNumber = pNumber;
        }

        private void setShowing_Id(int pShowing_Id)
        {
            mShowing_id = pShowing_Id;
        }

        public string getSection()
        {
            return mSection;
        }

        public string getRow()
        {
            return mRow;
        }

        public int getNumber()
        {
            return mNumber;
        }

        public int getShowing_Id()
        {
            return mShowing_id;
        }
    }
}
