using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem
{
    public static class TicketSystemSecurity
    {
        internal static string GenerateSalt(int size)
        {
            var rngCsp = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rngCsp.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        internal static string GenerateSHA256Hash(string input, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            var sha256Hasher = new SHA256Managed();

            var hash = sha256Hasher.ComputeHash(bytes);

            return Convert.ToBase64String(hash);

        }

        public static bool DoesUserExist(TicketDB db, string username, string password)
        {
            var searchedUser = (from user in db.Users
                               where user.UserName == username
                               select user).SingleOrDefault();

            if (searchedUser == null) return false;

            string passwordHash = GenerateSHA256Hash(password, searchedUser.Salt);

            if (passwordHash == searchedUser.Password) return true;
            else return false;
        }

        public static bool DoesUserExist(TicketDB db, string username, string password, out User user)
        {
            user = (from u in db.Users
                    where u.UserName == username
                    select u).SingleOrDefault();

            if (user == null) return false;

            string passwordHash = GenerateSHA256Hash(password, user.Salt);

            if (passwordHash == user.Password) return true;
            else return false;
        }
    }
}
