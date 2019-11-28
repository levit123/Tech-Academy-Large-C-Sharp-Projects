using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarInsurance.Models
{
    public class Applicant
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CarYear { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public bool DUI { get; set; }
        public int Tickets { get; set; }
        public bool Coverage { get; set; }
        public float Quote { get; set; }
        
        /*calculates the monthly quote for the applicant, and saves it to the "Quote" variable*/
        public void GenerateQuote()
        {
            Quote = 50;

            DateTime current = new DateTime();
            int age = current.Year - DateOfBirth.Year;

            /*if the applicant is under 25 but over 18, add 25 dollars to the monthly total*/
            if (age < 25 && age > 18)
            {
                Quote = Quote + 25;
            }
            /*if the applicant is under 18, add 100 dollars to the monthly total*/
            else if (age < 18)
            {
                Quote = Quote + 100;
            }
            /*if the applicant is over 100, add 25 dollars to the monthly total*/
            else if (age > 100)
            {
                Quote = Quote + 25;
            }
            
            /*if the car's year is before 2000, add 25 dollars to the monthly total*/
            if (CarYear < 2000)
            {
                Quote = Quote + 25;
            }
            /*if the car's year is after 2015, add 25 dollars to the monthly total*/
            else if (CarYear > 2015)
            {
                Quote = Quote + 25;
            }

            /*if car's make is a Porsche, add 25 dollars to the monthly total*/
            if (CarMake == "Porsche")
            {
                Quote = Quote + 25;

                /*if the car's make is a Porsche and it's model is a 911 Carrera, and an additional 25 dollars*/
                if (CarModel == "911 Carrera")
                {
                    Quote = Quote + 25;
                }
            }

            /*add 10 dollars for each speeding ticket the applicant has*/
            Quote = Quote + (Tickets * 10);

            /*if the applicant has a DUI, increase monthly total by 25 percent*/
            if (DUI == true)
            {
                Quote = Quote + (Quote / 4);
            }

            /*if the applicant wants full coverage, increase monthly total by 50 percent*/
            if (Coverage == true)
            {
                Quote = Quote + (Quote / 2);
            }
        }
    }
}