using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSLiteDemo.Initializer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Add Employees
            var client = new RestClient("http://localhost:62276/");

            //Add Employees
            var request = new RestRequest("employee/create", Method.POST);
            request.AddParameter("EmployeeID", 1);
            request.AddParameter("FirstName", "John");
            request.AddParameter("LastName", "Smith");
            request.AddParameter("DateOfBirth", new DateTime(1983, 12, 1));
            request.AddParameter("JobTitle", "General Manager");

            client.Execute(request);

            request = new RestRequest("employee/create", Method.POST);
            request.AddParameter("EmployeeID", 2);
            request.AddParameter("FirstName", "Maggie");
            request.AddParameter("LastName", "Franks");
            request.AddParameter("DateOfBirth", new DateTime(1990, 3, 12));
            request.AddParameter("JobTitle", "Shift Manager");

            client.Execute(request);

            request = new RestRequest("employee/create", Method.POST);
            request.AddParameter("EmployeeID", 3);
            request.AddParameter("FirstName", "Reggie");
            request.AddParameter("LastName", "Martinez");
            request.AddParameter("DateOfBirth", new DateTime(1990, 3, 12));
            request.AddParameter("JobTitle", "Line Cook");

            client.Execute(request);

            request = new RestRequest("locations/create", Method.POST);
            request.AddParameter("LocationID", 1);
            request.AddParameter("StreetAddress", "1234 S Main St");
            request.AddParameter("City", "Anywhere");
            request.AddParameter("State","KS");
            request.AddParameter("PostalCode", "67203");

            client.Execute(request);

            request = new RestRequest("locations/create", Method.POST);
            request.AddParameter("LocationID", 2);
            request.AddParameter("StreetAddress", "578 W Central Ave");
            request.AddParameter("City", "Anywhere");
            request.AddParameter("State", "KS");
            request.AddParameter("PostalCode", "67203");

            client.Execute(request);

            request = new RestRequest("locations/assignemployee", Method.POST);
            request.AddParameter("LocationID", 1);
            request.AddParameter("EmployeeID", 1);

            client.Execute(request);

            request = new RestRequest("locations/assignemployee", Method.POST);
            request.AddParameter("LocationID", 1);
            request.AddParameter("EmployeeID", 3);

            client.Execute(request);

            request = new RestRequest("locations/assignemployee", Method.POST);
            request.AddParameter("LocationID", 2);
            request.AddParameter("EmployeeID", 2);

            client.Execute(request);

            request = new RestRequest("locations/assignemployee", Method.POST);
            request.AddParameter("LocationID", 2);
            request.AddParameter("EmployeeID", 3);

            client.Execute(request);
            Console.ReadKey();
        }
    }
}
