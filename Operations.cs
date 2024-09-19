using System;
using System.Collections.Generic;

namespace BloodBankManagement
{
    public class Operations
    {
        private static List<User> users = [];
        private static List<Donation> donations = [];
        private static User currentLoggedInUser;

        public static void MainMenu()
        {
            bool flag = true;

            do
            {
                Console.WriteLine("Choose an option to continue");
                Console.WriteLine("1. User Registration\n2. User Login\n3. Exit");

                string userChoice = Console.ReadLine().Trim();
                if (userChoice == "1")
                {
                    UserRegistration();
                }
                else if (userChoice == "2")
                {
                    UserLogin();
                }
                // else if (userChoice == "3")
                // {
                //     FetchDonorDetails();
                // }
                else if (userChoice == "3")
                {
                    flag = false;
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(1);
                }
            } while (flag);
        }

        public static void UserRegistration()
        {
            Console.WriteLine("Please fill in these details\n");
            Console.WriteLine("Enter your name");
            string donorName = Console.ReadLine();
            Console.WriteLine("Enter your mobile number");
            string mobileNumber = Console.ReadLine();
            Console.WriteLine("Enter your blood group");
            BloodGroup bloodGroup = Enum.Parse<BloodGroup>(Console.ReadLine(), true);
            Console.WriteLine("Enter your age");
            int age = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the date of your last blood donation (dd/MM/yyyy)");
            DateTime lastDonation = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

            User user = new(donorName, mobileNumber, bloodGroup, age, lastDonation);
            Console.WriteLine($"Your Donor ID is {user.DonorID}");
            users.Add(user);
        }

        public static void UserLogin()
        {
            Console.WriteLine("Enter your Donor ID");
            string donorID = Console.ReadLine().ToUpper().Trim();

            bool flag = true;
            foreach (User user in users)
            {
                if (donorID == user.DonorID)
                {
                    flag = false;
                    currentLoggedInUser = user;
                    SubMenu();
                    break;
                }
            }
            if (flag)
            {
                Console.WriteLine("Invalid Donor ID");
                UserLogin();
            }
        }

        public static void SubMenu()
        {
            Console.WriteLine("Choose an option to continue");
            Console.WriteLine("1. Donate Blood\n2. Donation History\n3. Next Eligible Date\n4. Fetch Donor Details\n5. Exit");

            string userChoice = Console.ReadLine().Trim();
            if (userChoice == "1")
            {
                DonateBlood();
            }
            else if (userChoice == "2")
            {
                DonationHistory();
            }
            else if (userChoice == "3")
            {
                NextEligibleDate();
            }
            else if (userChoice == "4")
            {
                FetchDonorDetails();
            }
            else if (userChoice == "5")
            {
                MainMenu();
            }
        }

        public static void DonateBlood()
        {
            bool flag = true;
            foreach (User user in users)
            {
                if (user.DonorID == currentLoggedInUser.DonorID)
                {
                    flag = false;
                    Console.WriteLine("Enter your weight");
                    double weight = double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter your blood pressure");
                    double bloodPressure = double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter your Haemoglobin count");
                    double haemoglobinCount = double.Parse(Console.ReadLine());

                    if (weight > 50 && bloodPressure < 130 && haemoglobinCount > 13)
                    {
                        TimeSpan diff = DateTime.Now - user.LastDonationDate;
                        if (diff.TotalDays > 186)
                        {
                            Donation donation = new(currentLoggedInUser.DonorID, DateTime.Now, weight, bloodPressure, haemoglobinCount, user.BloodGroup);
                            donations.Add(donation);
                            Console.WriteLine($"Blood donated successfully. Donation ID {donation.DonationID}\nYou can donate blood again on {donation.DonationDate.AddMonths(6).ToString("dd/MM/yyyy")}");
                        }
                    }
                    break;
                }
            }
            if (flag)
            {
                Console.WriteLine("Blood donation failed");
            }
            SubMenu();
        }

        public static void DonationHistory()
        {
            bool flag = true;
            foreach (Donation donation in donations)
            {
                if (donation.DonorID == currentLoggedInUser.DonorID)
                {
                    flag = false;
                    Console.Write($"Donation ID: {donation.DonationID} | Donor ID: {donation.DonorID} | Donation Date: {donation.DonationDate} | Blood Group: {donation.BloodGroup}\n");
                    break;
                }
            }
            if (flag)
            {
                Console.WriteLine("No donation history found");
            }
            SubMenu();
        }

        public static void FetchDonorDetails()
        {
            bool flag = true;
            foreach (User user in users)
            {
                if (user.DonorID == currentLoggedInUser.DonorID)
                {
                    Console.WriteLine("Enter your Blood Group");
                    BloodGroup bloodGroup = Enum.Parse<BloodGroup>(Console.ReadLine(), true);

                    bool flag1 = true;
                    foreach (Donation donation in donations)
                    {
                        flag1 = false;
                        if (donation.DonorID == user.DonorID && user.BloodGroup == bloodGroup)
                        {
                            Console.Write($"Donor's Name: {currentLoggedInUser.DonorName} | Phone Number: {currentLoggedInUser.MobileNumber}\n");
                        }
                    }
                    if (flag1)
                    {
                        Console.WriteLine("Failed to fetch donor details");
                    }
                }
            }
            if (flag)
            {
                Console.WriteLine("User not found");
            }
            SubMenu();
        }

        public static void NextEligibleDate()
        {
            bool flag = true;
            foreach (Donation donation in donations)
            {
                int count = 0;
                if (donation.DonorID == currentLoggedInUser.DonorID)
                {
                    flag = false;
                    count++;
                    if (count == 2)
                    {
                        Console.WriteLine($"You recently donated blood on {currentLoggedInUser.LastDonationDate}");
                    }
                    else
                    {
                        Console.WriteLine($"Your next donation date is {currentLoggedInUser.LastDonationDate.AddMonths(6).ToString("dd/MM/yyyy")}");
                    }
                    break;
                }
            }
            if (flag)
            {
                Console.WriteLine("No donation history found");
            }
            SubMenu();
        }

        public static void DefaultData()
        {
            // create default user
            User user1 = new("John", "8484848", BloodGroup.O_Positive, 30, DateTime.Parse("25/08/2022"));
            User user2 = new("Jane", "4747447", BloodGroup.AB_Positive, 30, DateTime.Parse("30/09/2022"));
            users.Add(user1);
            users.Add(user2);

            //create default donation
            Donation donation1 = new(user1.DonorID, DateTime.Parse("10/10/2022"), 73, 120, 14, BloodGroup.O_Positive);
            Donation donation2 = new(user2.DonorID, DateTime.Parse("11/07/2022"), 73, 120, 14, BloodGroup.AB_Positive);
            donations.Add(donation1);
            donations.Add(donation2);
        }
    }
}