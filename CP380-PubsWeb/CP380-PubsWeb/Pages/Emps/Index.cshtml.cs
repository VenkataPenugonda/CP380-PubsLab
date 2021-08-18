using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models = CP380_PubsLab.Models;

namespace CP380_PubsWeb.Pages.Emps
{
    public class IndexModel : PageModel
    {
        private readonly Models.PubsDbContext _db = new Models.PubsDbContext();

        [BindProperty]
        public List<Models.Employee> EmpList { get; set; }
        public async Task<IActionResult> OnGet(Int16 jobId)
        {
            EmpList = await _db.Employees.Where(emp => emp.Jobs.job_id == jobId).ToListAsync();
            return Page();
        }
    }
}
