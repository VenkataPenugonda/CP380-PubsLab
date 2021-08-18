using CP380_PubsLab.Models;
using System;
using System.Linq;

namespace CP380_PubsLab
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbcontext = new Models.PubsDbContext())
            {
                if (dbcontext.Database.CanConnect())
                {
                    Console.WriteLine("Yes, I can connect");
                }

                // 1:Many practice
                //
                // TODO: - Loop through each employee
                //       - For each employee, list their job description (job_desc, in the jobs table)
                var allEmployees = dbcontext.Employees
                    .Select(employee => new { ID = employee.emp_id, FirstName = employee.fname, LastName = employee.lname ,JobTitle = employee.Jobs.job_desc })
                    .ToList();
                
                Console.WriteLine("\n::All Employees::");
                foreach (var i in allEmployees)
                {
                    Console.WriteLine($"Employee ID: {i.ID}, Employee Name: {i.FirstName} {i.LastName}, Job Description: {i.JobTitle}");
                }

                // TODO: - Loop through all of the jobs
                //       - For each job, list the employees (first name, last name) that have that job
                var allJobs = dbcontext.Jobs
                    .Select(job => new { ID = job.job_id, Title = job.job_desc, AllEmployees = job.Employee.Select(e => new { FirstName = e.fname, LastName = e.lname } ) })
                    .ToList();

                Console.WriteLine("\n:: Job Titles and their Employees ::");
                foreach (var i in allJobs)
                {
                    Console.WriteLine($"Job ID: {i.ID}, Job Title: {i.Title}");
                    foreach (var employee in i.AllEmployees)
                    {
                        Console.WriteLine($"  -> {employee.FirstName},{employee.LastName}");
                    }
                }

                // Many:many practice
                //
                // TODO: - Loop through each Store
                //       - For each store, list all the titles sold at that store
                //
                // e.g.
                //  Bookbeat -> The Gourmet Microwave, The Busy Executive's Database Guide, Cooking with Computers: Surreptitious Balance Sheets, But Is It User Friendly?
                var allStores = dbcontext.Stores
                    .Select(store => new { StoreTitle = store.stor_name, Titles = store.Sales.Select(sl => sl.Titles.title) })
                    .ToList();

                Console.WriteLine("\n:: Store -> Titles ::");
                foreach (var i in allStores)
                {
                    var concatTitles = String.Join(",", i.Titles);
                    Console.WriteLine($"\n{i.StoreTitle} -> {concatTitles}");
                }

                // TODO: - Loop through each Title
                //       - For each title, list all the stores it was sold at
                //
                // e.g.
                //  The Gourmet Microwave -> Doc-U-Mat: Quality Laundry and Books, Bookbeat
                var allTitles = dbcontext.Titles
                    .Select(title => new { TitleName = title.title, StoresList = title.Sales.Select(sl => sl.Stores.stor_name) })
                    .ToList();

                Console.WriteLine("\n:: Titles -> Store ::");
                foreach (var title in allTitles)
                {
                    var concatStores = String.Join(",", title.StoresList);
                    Console.WriteLine($"\n{title.TitleName} -> {concatStores}");
                }
                Console.ReadLine();
            }
        }
    }
}
