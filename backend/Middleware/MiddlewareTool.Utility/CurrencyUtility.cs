using System.Text;

namespace MiddlewareTool.Utility
{
    /// <summary>
    /// Currency Utility
    /// </summary>
    public sealed class CurrencyUtility
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1118:Utility classes should not have public constructors", Justification = "<Pending>")]
        public CurrencyUtility()
        {
        }

        /// <summary>
        /// Convert Currency To Words
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ConvertCurrencyToWords(double number)
        {
            string temp = number.ToString();
            var result = ConvertToWords(temp);
            return result;
        }
        /// <summary>
        /// Convert ToWords
        /// </summary>
        /// <param name="numb"></param>
        /// <returns></returns>
        private static String ConvertToWords(String numb)
        {
            try
            {
                String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
                String endStr = "đồng";
                int decimalPlace = numb.IndexOf(".");
                if (decimalPlace > 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = "and";// just to separate whole numbers from points/cents    
                        endStr = "Paisa " + endStr;//Cents    
                        pointStr = ConvertDecimals(points);
                    }
                }
                val = String.Format("{0} {1}{2} {3}", ConvertWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
                return val.First().ToString().ToUpper() + String.Join("", val.Skip(1));
            }
            catch
            {
                return string.Empty;
            }

        }
        /// <summary>
        /// Convert Decimals
        /// </summary>
        /// <param name="number">number</param>
        /// <returns></returns>
        private static String ConvertDecimals(String number)
        {
            StringBuilder cd = new StringBuilder();
            string digit = "", engOne = "";
            for (int i = 0; i < number.Length; i++)
            {
                digit = number[i].ToString();
                if (digit.Equals("0"))
                {
                    engOne = "không";
                }
                else
                {
                    engOne = ones(digit);
                }
                cd.Append(" " + engOne);
            }
            return cd.ToString();
        }


        /// <summary>
        /// Convert Whole Number
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1854:Unused assignments should be removed", Justification = "")]
        private static String ConvertWholeNumber(String Number)
        {
            string word = "";
            try
            {
                bool beginsZero = false;//tests for 0XX    
                bool isDone = false;//test if already translated    
                double dblAmt = (Convert.ToDouble(Number));
                if (dblAmt > 0)
                {//test for zero or digit zero in a nuemric    
                    beginsZero = Number.StartsWith("0");

                    int numDigits = Number.Length;
                    int pos = 0;//store digit grouping    
                    String place = "";//digit grouping name:hundres,thousand,etc...    
                    switch (numDigits)
                    {
                        case 1://ones' range    

                            word = ones(Number);
                            isDone = true;
                            break;
                        case 2://tens' range    
                            word = tens(Number);
                            isDone = true;
                            break;
                        case 3://hundreds' range    
                            pos = (numDigits % 3) + 1;
                            place = " trăm ";
                            break;
                        case 4://thousands' range    
                        case 5:
                        case 6:
                            pos = (numDigits % 4) + 1;
                            place = " ngàn ";
                            break;
                        case 7://millions' range    
                        case 8:
                        case 9:
                            pos = (numDigits % 7) + 1;
                            place = " triệu ";
                            break;
                        case 10://Billions's range    
                        case 11:
                        case 12:

                            pos = (numDigits % 10) + 1;
                            place = " tỷ ";
                            break;
                        //add extra case options for anything above Billion...    
                        default:
                            isDone = true;
                            break;
                    }
                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)    
                        if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")
                        {
                            try
                            {
                                word = ConvertWholeNumber(Number.Substring(0, pos)) + place + ConvertWholeNumber(Number.Substring(pos));
                            }
                            catch { word = string.Empty; }
                        }
                        else
                        {
                            word = ConvertWholeNumber(Number.Substring(0, pos)) + ConvertWholeNumber(Number.Substring(pos));
                        }

                    }
                    //ignore digit grouping names    
                    if (word.Trim().Equals(place.Trim())) { word = ""; }
                }
            }
            catch
            {
                return string.Empty;
            }
            return word.Trim();
        }
        /// <summary>
        /// ones
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        private static String ones(String Number)
        {
            int _Number = Convert.ToInt32(Number);
            String name = "";
            switch (_Number)
            {

                case 1:
                    name = "một";
                    break;
                case 2:
                    name = "hai";
                    break;
                case 3:
                    name = "ba";
                    break;
                case 4:
                    name = "bốn";
                    break;
                case 5:
                    name = "năm";
                    break;
                case 6:
                    name = "sáu";
                    break;
                case 7:
                    name = "bảy";
                    break;
                case 8:
                    name = "tám";
                    break;
                case 9:
                    name = "chín";
                    break;
                default:
                    break;
            }
            return name;
        }
        /// <summary>
        /// tens
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        private static String tens(String Number)
        {
            int _Number = Convert.ToInt32(Number);
            String name = null;
            switch (_Number)
            {
                case 10:
                    name = "mười";
                    break;
                case 11:
                    name = "mười một";
                    break;
                case 12:
                    name = "mười hai";
                    break;
                case 13:
                    name = "mười ba";
                    break;
                case 14:
                    name = "mười bốn";
                    break;
                case 15:
                    name = "mười lăm";
                    break;
                case 16:
                    name = "mười sáu";
                    break;
                case 17:
                    name = "mười bảy";
                    break;
                case 18:
                    name = "mười tám";
                    break;
                case 19:
                    name = "mười chín";
                    break;
                case 20:
                    name = "hai mươi";
                    break;
                case 30:
                    name = "ba mươi";
                    break;
                case 40:
                    name = "bốn mươi";
                    break;
                case 50:
                    name = "năm mươi";
                    break;
                case 60:
                    name = "sáu mươi";
                    break;
                case 70:
                    name = "bảy mươi";
                    break;
                case 80:
                    name = "tám mươi";
                    break;
                case 90:
                    name = "chín mươi";
                    break;
                default:
                    if (_Number > 0)
                    {
                        name = tens(Number.Substring(0, 1) + "0") + " " + ones(Number.Substring(1));
                    }
                    break;
            }
            return name;
        }
    }
}
