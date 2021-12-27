using Hospital.Models;
using Dapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

using System.Threading.Tasks;

namespace Hospital.Controllers
{
    
    public class PatientController : Controller
    {
        private string connectionString = string.Empty;
        public PatientController()
        {
            connectionString = "Server=BHAVNAWKS727;Database=hospital;Trusted_Connection= True";
        }
        // GET: Demo  
        public IActionResult PatientDetails()
        {
            using var con = new SqlConnection(connectionString);
            con.Open();
            var All = con.Query<PatientDetails>("select * from Patientdetail").ToList();

            return View(All);
        }

        public IActionResult Delete(int Id)
        {
                using var con = new SqlConnection(connectionString);
            con.Open();
            var Delete = con.Query<PatientDetails>($"delete from Patientdetail where Id={Id}").ToList();

            return RedirectToAction("PatientDetails");
        }
        [HttpGet]
        public IActionResult Create()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult Create(PatientDetails patientDetails)
        {
            using var con = new SqlConnection(connectionString);
            con.Open();

            var Query = "insert into Patientdetail(Name, Age,Contact) values(@name, @age,@contact)";
            var dp = new DynamicParameters();
            dp.Add("@name", patientDetails.Name, System.Data.DbType.AnsiString, System.Data.ParameterDirection.Input, 255);
            dp.Add("@age", patientDetails.Age, System.Data.DbType.AnsiString, System.Data.ParameterDirection.Input, 255);
            dp.Add("@contact", patientDetails.Contact, System.Data.DbType.AnsiString, System.Data.ParameterDirection.Input, 255);
            int res = con.Execute(Query, dp);
            ViewBag.Message = "Data Inserted";
            return View();
        }

        public IActionResult Details(int Id)
        {
            using var con = new SqlConnection(connectionString);
            con.Open();
            var One = con.Query<PatientDetails>($"select * from Patientdetail where Id={Id}").FirstOrDefault();

            return View(One);
            
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            using var con = new SqlConnection(connectionString);
            con.Open();
            var One = con.Query<PatientDetails>($"select * from Patientdetail where Id={Id}").FirstOrDefault();

            return View(One);

            
        }
        [HttpPost]
        public IActionResult Edit(PatientDetails patientDetails)
        {
            using var con = new SqlConnection(connectionString);
            con.Open();

            var Query = "Update Patientdetail set Name = @name, Age=@age, Contact=@contact where Id=@Id";
            var dp = new DynamicParameters();
            dp.Add("@name", patientDetails.Name, System.Data.DbType.AnsiString, System.Data.ParameterDirection.Input, 255);
            dp.Add("@age", patientDetails.Age, System.Data.DbType.AnsiString, System.Data.ParameterDirection.Input, 255);
            dp.Add("@contact", patientDetails.Contact, System.Data.DbType.AnsiString, System.Data.ParameterDirection.Input, 255);
            dp.Add("@Id", patientDetails.Id);
            int res = con.Execute(Query, dp);
            ViewBag.Message = "Data Updated";
            return RedirectToAction("PatientDetails");
        }


    }
}
