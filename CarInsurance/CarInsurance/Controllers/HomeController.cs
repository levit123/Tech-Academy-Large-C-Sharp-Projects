using CarInsurance.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsurance.Controllers
{
    public class HomeController : Controller
    {
        /*string used to connect to the specified SQL database to pull info from and/or add info to*/
        private string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarInsurance;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Admin()
        {
            /*string that functions as an SQL query to read from the database*/
            string queryString = "Select * From Applicants";
            /*creates an empty list of applicants*/
            List<Applicant> applicants = new List<Applicant>();

            /*opens a connection to the SQL database using the string defined earlier*/
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                /*reads the data into the list of applicants*/
                while (reader.Read())
                {
                    /*creates a new applicant, which will be passed into the list*/
                    Applicant applicant = new Applicant();
                    /*reads data into the new applicant*/
                    applicant.Id = Convert.ToInt32(reader["Id"]);
                    applicant.FirstName = reader["FirstName"].ToString();
                    applicant.LastName = reader["LastName"].ToString();
                    applicant.Email = reader["Email"].ToString();
                    applicant.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    applicant.CarYear = Convert.ToInt32(reader["CarYear"]);
                    applicant.CarMake = reader["CarMake"].ToString();
                    applicant.CarModel = reader["CarModel"].ToString();
                    applicant.DUI = Convert.ToBoolean(reader["DUI"]);
                    applicant.Tickets = Convert.ToInt32(reader["Tickets"]);
                    applicant.Coverage = Convert.ToBoolean(reader["Coverage"]);
                    applicant.Quote = Convert.ToSingle(reader["Quote"]);
                    /*adds the applicant to the lsit of the applicants*/
                    applicants.Add(applicant);
                }
                /*closes the connection*/
                connection.Close();
            }
            return View(applicants);
        }
    }
}