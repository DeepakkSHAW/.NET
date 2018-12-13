using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using StudentsAPI.Models;


//namespace StudentsAPI.Controllers
//{
//    [Students("application/json")]
//    [Route("api/Students")]
//    public class StudentsController : Controller
//    {
//        // GET: /<controller>/

//        //public IActionResult Index()
//        //{
//        //    return View();
//        //}

//        List<Students>Students = new List<Students>()
//        {
//            new Students() { StudentID =1, }
//        }
//    }
//}
namespace StudentsAPI.Controllers
{
    [StudentsAPI.Models.Students("application/json")]
    [Route("api/Students")]

    public class StudentsController : Controller
    {

    }
}