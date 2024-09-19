using System;

namespace BloodBankManagement
{
    public enum BloodGroup
    {
        A_Postive,
        B_Positive,
        O_Positive,
        AB_Positive
    }

    public class Donation
    {
        private static int s_donationID = 1000;
        public string DonationID { get; set; }
        public string DonorID { get; set; }
        public DateTime DonationDate { get; set; }
        public double Weight { get; set; }
        public double BloodPressure { get; set; }
        public double HaemoglobinCount { get; set; }
        public BloodGroup BloodGroup { get; set; }

        public Donation(string donorID, DateTime donationDate, double weight, double bloodPressure, double haemoglobinCount, BloodGroup bloodGroup)
        {
            DonationID = $"DID{s_donationID++}";
            DonorID = donorID;
            DonationDate = donationDate;
            Weight = weight;
            BloodPressure = bloodPressure;
            HaemoglobinCount = haemoglobinCount;
            BloodGroup = bloodGroup;
        }
    }
}