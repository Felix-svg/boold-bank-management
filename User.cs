using System;

namespace BloodBankManagement
{
    public class User
    {
        private static int s_donorID = 1000;
        public string DonorID { get; set; }
        public string DonorName { get; set; }
        public string MobileNumber { get; set; }
        public BloodGroup BloodGroup { get; set; }
        public int Age { get; set; }
        public DateTime LastDonationDate { get; set; }

        public User(string donorNane, string mobileNumber, BloodGroup bloodGroup, int age, DateTime lastDonationDate)
        {
            DonorID = $"UID{s_donorID++}";
            DonorName = donorNane;
            MobileNumber = mobileNumber;
            BloodGroup = bloodGroup;
            Age = age;
            LastDonationDate = lastDonationDate;
        }
    }
}