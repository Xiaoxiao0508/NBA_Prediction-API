using System;

namespace NBA_API.Models
{
    public class AspNetUsers
    {
       

        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public string EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberConfirmed { get; set; }
        public string TwoFactorEnabled { get; set; }
        public string LockoutEnd { get; set; }
        public string LockoutEnabled { get; set; }
        public string AccessFailedCount { get; set; }
         public AspNetUsers()
        {
        }

        public AspNetUsers(string id, string userName, string normalizedUserName, string email, string normalizedEmail, string emailConfirmed, string passwordHash, string securityStamp, string concurrencyStamp, string phoneNumber, string phoneNumberConfirmed, string twoFactorEnabled, string lockoutEnd, string lockoutEnabled, string accessFailedCount)
        {
            Id = id;
            UserName = userName;
            NormalizedUserName = normalizedUserName;
            Email = email;
            NormalizedEmail = normalizedEmail;
            EmailConfirmed = emailConfirmed;
            PasswordHash = passwordHash;
            SecurityStamp = securityStamp;
            ConcurrencyStamp = concurrencyStamp;
            PhoneNumber = phoneNumber;
            PhoneNumberConfirmed = phoneNumberConfirmed;
            TwoFactorEnabled = twoFactorEnabled;
            LockoutEnd = lockoutEnd;
            LockoutEnabled = lockoutEnabled;
            AccessFailedCount = accessFailedCount;
        }
    }
}
