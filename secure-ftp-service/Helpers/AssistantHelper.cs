using Newtonsoft.Json.Linq;
using secure_ftp_service.Core.Models;
using System.Globalization;
using System.Reflection;

namespace secure_ftp_service.Helpers
{
    /// <summary>
    /// It is extension class or helper class, which can be used to throughout the app.
    /// </summary>
    public class AssistantHelper
    {
        #region IsNotNull Methods
        public Boolean IsNotNull(object obj)
        {

            return obj != null;
        }

        bool IsAnyNullOrEmpty(object data)
        {
            foreach (PropertyInfo pi in data.GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(string))
                {
                    
                    if (string.IsNullOrEmpty(Convert.ToString(pi.GetValue(data))))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        
        #endregion

        #region IsNull Methods
        public Boolean IsNull(Object obj)
        {
            return obj == null;
        }
        #endregion

        #region IsEmpty Method
        public Boolean IsEmpty(string obj)
        {
            return obj == String.Empty;
        }
        #endregion

        #region NullOrEmpty Methods
        public Boolean IsNullOrEmpty(String str)
        {
            return String.IsNullOrEmpty(str);
        }
        public Boolean IsNotNullOrEmpty(String str)
        {
            return !String.IsNullOrEmpty(str);
        }

        public Boolean IsNotNullOrEmptyList(List<object> obj)
        {
            return IsNotNull(obj) && MoreThanZero(obj.Count);
        }
        #endregion

        #region IsFalse
        public Boolean IsFalse(Boolean val)
        {
            return !val;
        }
        #endregion

        #region Zero Length Check
        public Boolean IsZero(int val)
        {
            return val.Equals(0);
        }

        public Boolean IsNotZero(int val)
        {
            return !val.Equals(0);
        }

        public Boolean MoreThanZero(int val)
        {
            return val > 0;
        }
        public Boolean MoreThanOne(int val)
        {
            return val > 1;
        }
        public Boolean MoreThanZero(long val)
        {
            return val > 0;
        }

        #endregion

        #region Negative Value Check
        public Boolean IsNegative(int val)
        {
            return val < 0;
        }
        public Boolean IsNotNegative(int val)
        {
            return val >= 0;
        }
        #endregion

        #region String value Equality Check
        public bool ContainsWord(String Source, String ToCheck, StringComparison oComp)
        {
            return Source?.IndexOf(ToCheck, oComp) >= 0;
        }
        #endregion

        #region Get All Months
        public List<KeyValuePair<Int32, String>> GetAllMonths()
        {
            var monthList = new List<KeyValuePair<int, String>>();
            for (var i = 1; i <= 12; i++)
            {
                if (DateTimeFormatInfo.CurrentInfo != null)
                    monthList.Add(new KeyValuePair<Int32, String>(i, DateTimeFormatInfo.CurrentInfo.GetMonthName(i)));
            }
            return monthList;
        }
        #endregion

        #region JToken Data Validation
        public bool IsValueExist(JToken item)
        {
            bool result = false;
            try
            {

                result = item != null && (item.HasValues);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        // JToken item's null check using the method GetStringData.
        public string GetStringData(JToken item)
        {
            string strVal = String.Empty;
            try
            {
                if (item != null)
                {
                    strVal = item.ToString();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return strVal;
        }

        // JToken item's null check using the method GetLongData.
        public long GetLongData(JToken item)
        {
            long number = 0;
            try
            {
                if (item != null)
                {
                    long.TryParse(item.ToString(), out number);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return number;
        }

        // JToken item's null check using the method GetDecimalData.
        public decimal GetDecimalData(JToken item)
        {
            decimal number = 0;
            try
            {
                if (item != null)
                {
                    decimal.TryParse(item.ToString(), out number);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return number;
        }

        // JToken item's null check using the method GetIntData.
        public int GetIntData(JToken item)
        {
            int number = 0;
            try
            {
                if (item != null)
                {
                    int.TryParse(item.ToString(), out number);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return number;
        }
        #endregion

        #region Checking a certain value whether digit or not using IsAllDigits method.
        private bool IsAllDigits(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }
        #endregion

        #region Converting the DateTime value into UnixTimestamp value using the method ToUnixTimestamp.
        public long ToUnixTimestamp(DateTime value)
        {
            return (long)Math.Truncate((value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
        }
        #endregion

        #region String to Camel, Pascal, and Proper case conversion method
        // Convert the string to Pascal case.
        public string ToPascalCase(string the_string)
        {
            // If there are 0 or 1 characters, just return the string.
            if (the_string == null) return string.Empty;
            if (the_string.Length < 2) return the_string.ToUpper();

            // Split the string into words.
            string[] words = the_string.Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            // Combine the words.
            string result = "";
            foreach (string word in words)
            {
                result +=
                    word.Substring(0, 1).ToUpper() +
                    word.Substring(1);
            }

            return result;
        }

        // Convert the string to camel case.
        public string ToCamelCase(string the_string)
        {
            // If there are 0 or 1 characters, just return the string.
            if (the_string == null || the_string.Length < 2)
                return string.Empty;

            // Split the string into words.
            string[] words = the_string.Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            // Combine the words.
            string result = words[0].ToLower();
            for (int i = 1; i < words.Length; i++)
            {
                result +=
                    words[i].Substring(0, 1).ToUpper() +
                    words[i].Substring(1);
            }

            return result;
        }

        // Capitalize the first character and add a space before
        // each capitalized letter (except the first character).
        public string ToProperCase(string the_string)
        {
            // If there are 0 or 1 characters, just return the string.
            if (the_string == null) return string.Empty;
            if (the_string.Length < 2) return the_string.ToUpper();

            // Start with the first character.
            string result = the_string.Substring(0, 1).ToUpper();

            // Add the remaining characters.
            for (int i = 1; i < the_string.Length; i++)
            {
                if (char.IsUpper(the_string[i])) result += " ";
                result += the_string[i];
            }

            return result;
        }

        public string FirstCharToLower(string pasCamWord)
        {
            switch (pasCamWord)
            {
                case null: throw new ArgumentNullException(nameof(pasCamWord));
                case "": throw new ArgumentException($"{nameof(pasCamWord)} {ConstantSupplier.CANNOT_BE_EMPTY_MSG}", nameof(pasCamWord));
                default: return pasCamWord.First().ToString().ToLower() + pasCamWord.Substring(1);
            }
        }
        #endregion

        #region Create json object to save to client side
        public string CreateJObjectToSave(Dictionary<string, dynamic> dicMapperBusinessDomain, string[] constants, List<Dictionary<string, dynamic>> dicDetails)
        {
            JObject oBusiness;

            if (dicDetails != null)
            {
                // Removing the extra keyvaluepair from the particular dictionary (e.g. sales order), where dictionary will be used to creat json object to save.
                var keysWithMatchingValues = dicMapperBusinessDomain.Where(d => d.Key.Contains(constants[0]))
                               .Select(kvp => kvp.Key).ToList();

                foreach (var key in keysWithMatchingValues)
                {
                    dicMapperBusinessDomain.Remove(key);
                    break;
                }

                // Creating and returning json object with 2 levels.
                oBusiness =
                new JObject(
                    new JProperty(constants[1],
                       new JObject(
                           from item in dicMapperBusinessDomain
                           select new JProperty(item.Key, item.Value),
                           new JProperty(constants[2],
                                new JArray(
                                    from child in dicDetails
                                    select new JObject(
                                        from childField in child
                                        select new JProperty(childField.Key, childField.Value)))))));
            }
            else
            {
                // Creating and returning json object with 1 level.
                oBusiness =
                new JObject(
                    new JProperty(constants[0],
                       new JObject(
                           from item in dicMapperBusinessDomain
                           select new JProperty(item.Key, item.Value))));
            }

            return oBusiness.ToString();
        }
        #endregion

        #region Modify Dictionary Items to integer
        public Dictionary<string, dynamic> ConvertDictionaryItems(Dictionary<string, dynamic> dicMapperBusinessDomain, string[] constants)
        {
            Dictionary<string, dynamic> temp = new();
            List<string> keyMatchingValues = new();

            foreach (var constant in constants)
            {

                temp.Add(dicMapperBusinessDomain.Where(d => d.Key.Contains(constant)).Select(kvp => kvp.Key).FirstOrDefault(),
                    dicMapperBusinessDomain.Where(d => d.Key.Contains(constant)).Select(kvp => Convert.ToInt32(kvp.Value)).FirstOrDefault());
            }


            foreach (var constant in constants)
            {
                var item = dicMapperBusinessDomain.Where(d => d.Key.Contains(constant)).Select(kvp => kvp.Key).FirstOrDefault();
                if(item != null)
                keyMatchingValues.Add(item);
            }

            foreach (var key in keyMatchingValues)
            {
                dicMapperBusinessDomain.Remove(key);

            }

            foreach (var item in temp)
            {
                dicMapperBusinessDomain.Add(item.Key, item.Value);
            }

            return dicMapperBusinessDomain;


        }
        #endregion

        #region Converting JArray Item value string to Boolean & String
        public void ConvertResponseItemValueToBoolean(JArray oJArrayBizDomain)
        {
            foreach (var item in oJArrayBizDomain.Children())
            {

                var itemProperties = item.Children<JProperty>();
                foreach (var itemProp in itemProperties)
                {
                    itemProp.Value = itemProp.Value.ToString().Equals(ConstantSupplier.STRING_YES) ? true :
                        itemProp.Value.ToString().Equals(ConstantSupplier.STRING_NO) ? false : itemProp.Value;

                }
            }
        }

        public void ConvertResponseItemValueToBooleanAndString(JArray oJArrayBizDomain, string[] constants)
        {
            foreach (var item in oJArrayBizDomain.Children())
            {

                var itemProperties = item.Children<JProperty>();
                foreach (var itemProp in itemProperties)
                {
                    //itemProp.Value = itemProp.Value.ToString().Equals(ConstantSupplier.STRING_YES) ? true :
                    //    itemProp.Value.ToString().Equals(ConstantSupplier.STRING_NO) ? false : itemProp.Value;
                    itemProp.Value = itemProp.Value.ToString().Equals(ConstantSupplier.STRING_YES) ? true :
                        itemProp.Value.ToString().Equals(ConstantSupplier.STRING_NO) ? false : itemProp.Value.Type == JTokenType.Null ? ConstantSupplier.EMPTY_STRING : itemProp.Value;

                    foreach (var constant in constants)
                    {
                        itemProp.Value = itemProp.Name.Equals(constant) ? Convert.ToString(itemProp.Value) : itemProp.Value;
                    }

                }
            }
        }
        #endregion

        #region Extract Value out of any given substring
        public string GetStringFromResponse(string input, string strFrom, string strTo)
        {
            return input.Substring((input.IndexOf(strFrom) + strFrom.Length), ((input.IndexOf(strTo)) - (input.IndexOf(strFrom) + strFrom.Length)));
        }
        #endregion

        public string FtpDirectory(string rootDirectory)
        {
            rootDirectory = rootDirectory.Trim('/');
            return string.Format(@"/{0}/", rootDirectory);
        }

        public string CombineDirectory(string rootDirectory, string childDirectory)
        {
            rootDirectory = rootDirectory.Trim('/');
            childDirectory = childDirectory.Trim('/');
            return string.Format(@"/{0}/{1}/", rootDirectory, childDirectory);
        }

        public string CombineFile(string rootDirectory, string filePathOrName)
        {
            rootDirectory = rootDirectory.Trim('/'); ;
            filePathOrName = filePathOrName.Trim('/'); ;
            return string.Format(@"/{0}/{1}", rootDirectory, filePathOrName);
        }

        public string ServerDetails(string host, string port, string userName, string type = ConstantSupplier.REMOTE_TYPE_FTP)
        {
            return String.Format("Type: '{3}' Host:'{0}' Port:'{1}' User:'{2}'", host, port, userName, type);
        }

        public List<string> GetFiles(string path)
        {
            List<string> list = new();
            foreach (var filename in Directory.GetFiles(path))
            {
                list.Add(filename);
            }
            return list;
        }


    }
}
