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

namespace Jean_Paul_Sartre_Variety_Theatre
{
    public static class ValidationClass
    {
        public static bool PresenceChecker(string pDataToBeChecked)
        {
            if (pDataToBeChecked.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string TimeChecker(string pTimeToBeChecked)
        {
            char[] timeCharacterArray = new char[pTimeToBeChecked.Length];
            timeCharacterArray = pTimeToBeChecked.ToCharArray();

            if (timeCharacterArray.Length < 5)
            {
                return "Please format time as : HH: MM e.g. 06:30";
            }

            if (PresenceChecker(pTimeToBeChecked) == false)
            {
                return "Error: Please enter a time";
            }

            if (Char.IsDigit(timeCharacterArray[0]) == false || Char.IsDigit(timeCharacterArray[1]) == false)
            {
                return "Error: Hour format incorrect";
            }

            if (int.Parse(timeCharacterArray[0].ToString() + timeCharacterArray[1].ToString()) > 24)
            {
                return "Error: Time cannot be over 24 hours";
            }

            if (timeCharacterArray[2] != ':')
            {
                return "Error: Must enter colon ':' in between minutes and hours";
            }

            if (Char.IsDigit(timeCharacterArray[3]) == false || Char.IsDigit(timeCharacterArray[4]) == false)
            {
                return "Error: Minute format incorrect";
            }

            if (int.Parse(timeCharacterArray[3].ToString() + timeCharacterArray[4].ToString()) > 59)
            {
                return "Error: Time cannot be over 59 minutes";
            }
            else
            {
                return null;
            }
        }



        public static string FloatChecker(string pFloatToBeChecked)
        {
            string mFloatToBeChecked = pFloatToBeChecked;
            char[] floatToBeCheckedCharArray = new char[pFloatToBeChecked.Length];
            floatToBeCheckedCharArray = pFloatToBeChecked.ToCharArray();

            for (int i = 0; i < floatToBeCheckedCharArray.Length; i++)
            {
                if (floatToBeCheckedCharArray[i] == '.')
                {
                    continue;
                }
                if (char.IsDigit(floatToBeCheckedCharArray[i]) == false)
                {
                    return "Error: Entered values must be a number";
                }
            }

            return null;
        }

        public static string IntChecker(string pIntToBeChecked)
        {
            string mIntToBeChecked = pIntToBeChecked;
            char[] intToBeCheckedCharArray = new char[pIntToBeChecked.Length];
            intToBeCheckedCharArray = pIntToBeChecked.ToCharArray();

            for (int i = 0; i < intToBeCheckedCharArray.Length; i++)
            {
                if (char.IsDigit(intToBeCheckedCharArray[i]) == false)
                {
                    return "Error: Entered values must be a number";
                }
            }
            return null;
        }

        public static string HasNumbersInStringChecker(string pStringToBeChecked)
        {
            string mStringToBeChecked = pStringToBeChecked;

            char[] stringToBeCheckedCharArray = new char[mStringToBeChecked.Length];
            stringToBeCheckedCharArray = pStringToBeChecked.ToCharArray();

            for (int i = 0; i < stringToBeCheckedCharArray.Length; i++)
            {
                if (char.IsDigit(stringToBeCheckedCharArray[i]) == true)
                {
                    return ("Please only enter letters - Numbers are not allowed");
                }
            }
            return null;
        }
    }
}


