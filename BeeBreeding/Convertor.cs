//Authors: Gowridevi Akasapu, Anju Thomas, Apurva Jain, Aditya Gupta, Emmanuel Kevin, Ravali Chilucoti, Amritpal Singh
//Copy Rights:      Conestoga College
//Group Number:     4

using System;
using System.Globalization;
using System.Windows.Data;

namespace BeeBreeding
{
    public class DistanceConvertor : IValueConverter
    {
        #region Constants
        public const int INVALID_INPUT_DISTANCE = -2;
        public const int EQUAL_DISTANCE = -1;
        public const string INVALID_MESSAGE = "Invalid input value";
        public const string VALID_MESSAGE = "Distance between the Maggot Cells is: ";
        #endregion

        // Converts the message into 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string message = VALID_MESSAGE;
            int distance = (int)value;
            if (distance == 0)
                message = "";
            else if (distance > 0)
                message += distance;
            else if (distance == EQUAL_DISTANCE)
                message += 0;
            else if (distance == INVALID_INPUT_DISTANCE)
                message = INVALID_MESSAGE;
            return message;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
