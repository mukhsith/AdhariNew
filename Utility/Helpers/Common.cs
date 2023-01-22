using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Utility.Enum;

namespace Utility.Helpers
{
    public static class Common
    {
        public class TokenInfo
        {
            public string FullName { get; set; }
            public int RoleId { get; set; }
            public int UserId { get; set; }
            public string ErrorMessage { get; set; }
        }      
        public static Guid GenrateActivationKey()
        {
            return Guid.NewGuid();
        }
        public static string GenerateRandomNo(int minVal = 10000000, int maxVal = 99999999)
        {
            Random random = new();
            return random.Next(minVal, maxVal).ToString();
        }
        public static string RandomString(int size, bool isLowerCase = false, bool isAlphaNumeric = false)
        {
            Random random = new();
            string chars = Constants.AlphaChars;

            if (isAlphaNumeric)
            {
                chars = Constants.AlphaNumericChars;
            }

            string randomString = new string(Enumerable.Repeat(chars, size)
                                 .Select(s => s[random.Next(s.Length)]).ToArray());

            randomString = isLowerCase ? randomString.ToLower() : randomString;

            return randomString;
        }
        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public static string ConvertHtmlToPlainText(string source)
        {
            if (source != null)
            {
                char[] array = new char[source.Length];
                int arrayIndex = 0;
                bool inside = false;

                for (int i = 0; i < source.Length; i++)
                {
                    char let = source[i];
                    if (let == '<')
                    {
                        inside = true;
                        continue;
                    }
                    if (let == '>')
                    {
                        inside = false;
                        continue;
                    }
                    if (!inside)
                    {
                        array[arrayIndex] = let;
                        arrayIndex++;
                    }
                }
                return new string(array, 0, arrayIndex);
            }
            return string.Empty;
        }
        public static string StringLimit(string inputText, int limit, bool includeDots = true)
        {
            string newText = string.Empty;

            if (limit > inputText.Length)
                newText = inputText;
            else
            {
                newText = inputText.Substring(0, limit);

                if (includeDots)
                    newText += "...";
            }

            return newText;
        }
        public static int? ConvertTextToIntOptional(string text)
        {

            if (int.TryParse(text, out int result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        public static int ConvertTextToInt(string text)
        {

            if (int.TryParse(text, out int result))
            {
                return result;
            }
            else
            {
                return 0;
            }

        }
        public static bool? ConvertTextToBoolean(string text)
        {

            if (bool.TryParse(text, out bool result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Convert calendar date [day/mon/year] to System DateTime format
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DateTime? ConvertTextToDate(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                {
                    return null;
                }

                var arrDate = text.Split("/");
                int.TryParse(arrDate[0], out int day);
                int.TryParse(arrDate[1], out int month);
                int.TryParse(arrDate[2], out int year);
                var newDate = new DateTime(year, month, day);
                return newDate;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Convert calendar date [year/mon/day] to System DateTime format
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DateTime? ConvertYYYYMMDDTextToDate(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                {
                    return null;
                }

                var arrDate = text.Split("/");
                int.TryParse(arrDate[0], out int year );
                int.TryParse(arrDate[1], out int month);
                int.TryParse(arrDate[2], out int day);
                var newDate = new DateTime(year, month, day);
                return newDate;
            }
            catch
            {
                return null;
            }
        }
        public static DateTime? ConvertTextToDateYYMMDD(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                {
                    return null;
                }

                var arrDate = text.Split("/");
                int.TryParse(arrDate[0], out int year);
                int.TryParse(arrDate[1], out int month);
                int.TryParse(arrDate[2], out int day);
                var newDate = new DateTime(year, month, day);
                return newDate;
            }
            catch
            {
                return null;
            }
        }
        public static string GetRandomNumber()
        {
            Random ran = new Random();

            String b = "abcdefghijklmnopqrstuvwxyz0123456789";
            String sc = "!@#$%^&*~";

            int length = 32;

            String random = "";

            for (int i = 0; i < length; i++)
            {
                int a = ran.Next(b.Length); //string.Lenght gets the size of string
                random = random + b.ElementAt(a);
            }
            for (int j = 0; j < 2; j++)
            {
                int sz = ran.Next(sc.Length);
                random = random + sc.ElementAt(sz);
            }

            return random;
        }
        public static int GetDayId(DateTime date)
        {
            if (date.DayOfWeek.ToString() == DayTitle.Sunday.ToString())
            {
                return (int)DayTitle.Sunday;
            }
            else if (date.DayOfWeek.ToString() == DayTitle.Monday.ToString())
            {
                return (int)DayTitle.Monday;
            }
            else if (date.DayOfWeek.ToString() == DayTitle.Tuesday.ToString())
            {
                return (int)DayTitle.Tuesday;
            }
            else if (date.DayOfWeek.ToString() == DayTitle.Wednesday.ToString())
            {
                return (int)DayTitle.Wednesday;
            }
            else if (date.DayOfWeek.ToString() == DayTitle.Thursday.ToString())
            {
                return (int)DayTitle.Thursday;
            }
            else if (date.DayOfWeek.ToString() == DayTitle.Friday.ToString())
            {
                return (int)DayTitle.Friday;
            }
            else if (date.DayOfWeek.ToString() == DayTitle.Saturday.ToString())
            {
                return (int)DayTitle.Saturday;
            }

            return 0;
        }
        public static void SaveExceptionError(string errorMessage)
        {
            try
            {
                var line = Environment.NewLine;
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "ExceptionLog");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                filepath = filepath + @"\ExceptionLog_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + ".txt";
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }

                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(errorMessage);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception e)
            {

            }
        }
        public static void SaveExceptionError(Exception ex)
        {
            try
            {
                var line = Environment.NewLine;
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "ExceptionLog");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                filepath = filepath + @"\ExceptionLog_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + ".txt";
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }

                string errorMessage = ex.Message;

                var innerException = ex.InnerException;
                if (innerException != null)
                {
                    do
                    {
                        errorMessage = errorMessage + ", " + innerException.Message;
                        innerException = innerException.InnerException;
                    }
                    while (innerException != null);
                }

                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(errorMessage);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception e)
            {

            }
        }
        public static void SaveMasterCardRequestResponseLog(string log)
        {
            try
            {
                var line = Environment.NewLine;
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "MasterCardRequestResponseLog");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                filepath = filepath + @"\GBRequestResponseLog_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + ".txt";
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }

                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine("-----------Log Details on " + " " + DateTime.Now.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(log);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
