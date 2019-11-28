using CarInsurance.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsurance.Controllers
{
    public class ApplicantsController : Controller
    {
        /*string used to connect to the specified SQL database to pull info from and/or add info to*/
        private string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarInsurance;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // GET: Applicants
        //displays the list of current applicant's and their info*/
        public ActionResult Index()
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

        public ActionResult Add()
        {
            return View();
        }

        /*adds an applicant and their info to the SQL database*/
        [HttpPost]
        public ActionResult Add(Applicant applicant)
        {
            string queryString = @"Insert into Applicants (FirstName, LastName, Email, DateOfBirth, CarYear, CarMake, CarModel, DUI, Tickets, Coverage, Quote) Values (@FirstName, @LastName, @Email, @DateOfBirth, @CarYear, @CarMake, @CarModel, @DUI, @Tickets, @Coverage, @Quote)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                /*creates an empty SQL command*/
                SqlCommand command = new SqlCommand(queryString, connection);
                /*prepares parameters for the SQL command*/
                command.Parameters.Add("@FirstName", SqlDbType.VarChar);
                command.Parameters.Add("@LastName", SqlDbType.VarChar);
                command.Parameters.Add("@Email", SqlDbType.VarChar);
                command.Parameters.Add("@DateOfBirth", SqlDbType.DateTime);
                command.Parameters.Add("@CarYear", SqlDbType.Int);
                command.Parameters.Add("@CarMake", SqlDbType.VarChar);
                command.Parameters.Add("@CarModel", SqlDbType.VarChar);
                command.Parameters.Add("@DUI", SqlDbType.Bit);
                command.Parameters.Add("@Tickets", SqlDbType.Int);
                command.Parameters.Add("@Coverage", SqlDbType.Bit);
                command.Parameters.Add("@Quote", SqlDbType.Float);

                /*reads data into the parameters of the SQL command*/
                command.Parameters["@FirstName"].Value = applicant.FirstName;
                command.Parameters["@LastName"].Value = applicant.LastName;
                command.Parameters["@Email"].Value = applicant.Email;
                command.Parameters["@DateOfBirth"].Value = applicant.DateOfBirth;
                command.Parameters["@CarYear"].Value = applicant.CarYear;
                command.Parameters["@CarMake"].Value = applicant.CarMake;
                command.Parameters["@CarModel"].Value = applicant.CarModel;
                command.Parameters["@DUI"].Value = applicant.DUI;
                command.Parameters["@Tickets"].Value = applicant.Tickets;
                command.Parameters["@Coverage"].Value = applicant.Coverage;
                /*calls the Applicant class method to calculate the monthly quote*/
                applicant.GenerateQuote();
                /*reads the applicant's quote into the "Quote" parameter of the SQL command*/
                command.Parameters["@Quote"].Value = applicant.Quote;

                /*opens the connection and executes the SQL query using the "command" variable,
                 * which we added parameters and their values to just above here; this query
                 * adds the applicant's info to the SQL database*/
                connection.Open();
                command.ExecuteNonQuery();

                /*closes the connection to the SQL database*/
                connection.Close();
            }
            /*returns to the Index page of the website*/
            return RedirectToAction("Index");
        }

        /*pulls up the details about an applicant and their info*/
        public ActionResult Details(int Id)
        {
            string queryString = "Select * From Applicants where Id = @Id";
            Applicant applicant = new Applicant();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                /*creates an SQL command that searches for an applicant by first name*/
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@Id", SqlDbType.Int);

                /*puts the specified first name into the SQL command parameters*/
                command.Parameters["@Id"].Value = Id;

                /*opens connection to the SQL database*/
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                /*reads info on the applicant from the database into the Applicant variable*/
                while (reader.Read())
                {
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
                }
                /*closes the connection*/
                connection.Close();
            }
            /*returns the Applicant and their details*/
            return View(applicant);
        }

        public ActionResult Edit(int id)
        {
            string queryString = "Select * From Applicants where id = @id";
            Applicant applicant = new Applicant();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@id", SqlDbType.Int);

                command.Parameters["@id"].Value = id;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
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
                }
                connection.Close();
            }
            return View(applicant);
        }

        [HttpPost]
        public ActionResult Edit(Applicant applicant)
        {
            string queryString = @"Update Applicants set FirstName = @FirstName, LastName = @LastName, Email = @Email, DateOfBirth = @DateOfBirth, CarYear = @CarYear, CarMake = @CarMake, CarModel = @CarModel, DUI = @DUI, Tickets = @Tickets, Coverage = @Coverage, Quote = @Quote where Id = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                /*creates an empty SQL command*/
                SqlCommand command = new SqlCommand(queryString, connection);
                /*prepares parameters for the SQL command*/
                command.Parameters.Add("@Id", SqlDbType.Int);
                command.Parameters.Add("@FirstName", SqlDbType.VarChar);
                command.Parameters.Add("@LastName", SqlDbType.VarChar);
                command.Parameters.Add("@Email", SqlDbType.VarChar);
                command.Parameters.Add("@DateOfBirth", SqlDbType.DateTime);
                command.Parameters.Add("@CarYear", SqlDbType.Int);
                command.Parameters.Add("@CarMake", SqlDbType.VarChar);
                command.Parameters.Add("@CarModel", SqlDbType.VarChar);
                command.Parameters.Add("@DUI", SqlDbType.Bit);
                command.Parameters.Add("@Tickets", SqlDbType.Int);
                command.Parameters.Add("@Coverage", SqlDbType.Bit);
                command.Parameters.Add("@Quote", SqlDbType.Float);

                /*reads data into the parameters of the SQL command*/
                command.Parameters["@Id"].Value = applicant.Id;
                command.Parameters["@FirstName"].Value = applicant.FirstName;
                command.Parameters["@LastName"].Value = applicant.LastName;
                command.Parameters["@Email"].Value = applicant.Email;
                command.Parameters["@DateOfBirth"].Value = applicant.DateOfBirth;
                command.Parameters["@CarYear"].Value = applicant.CarYear;
                command.Parameters["@CarMake"].Value = applicant.CarMake;
                command.Parameters["@CarModel"].Value = applicant.CarModel;
                command.Parameters["@DUI"].Value = applicant.DUI;
                command.Parameters["@Tickets"].Value = applicant.Tickets;
                command.Parameters["@Coverage"].Value = applicant.Coverage;
                /*calls the Applicant class method to calculate the monthly quote*/
                applicant.GenerateQuote();
                /*reads the applicant's quote into the "Quote" parameter of the SQL command*/
                command.Parameters["@Quote"].Value = applicant.Quote;

                connection.Open();
                command.ExecuteNonQuery();

                connection.Close();
            }
            return RedirectToAction("Index");
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