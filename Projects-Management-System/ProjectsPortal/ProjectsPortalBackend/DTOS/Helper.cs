using System;
using System.Net;
using System.Net.Sockets;
using ProjectsPortalBackend.Data;
using ProjectsPortalBackend.Models;

namespace ProjectsPortalBackend.DTOS
{
    public class Helper
    {
        //GET IP OF CURRENT USER
        private static string GetIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        //LOG GENERATOR
        public static void createLog(ProjectContext _context, bool log_situation, int user_id, string process, string statement)
        {
            DateTime localDate = DateTime.Now;
            LogOperations logger = new LogOperations();

            logger.LogSituation = log_situation;
            logger.UserID = user_id;
            logger.Process = process;
            logger.Statement = statement;
            logger.DateTime = localDate;
            logger.UserIp = GetIP();

            _context.LogOperations.Add(logger);
        }

        //**PASSWORD encryptions AND decryptions**//

        //encryption
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        //decryption
        public static string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

    }
}
