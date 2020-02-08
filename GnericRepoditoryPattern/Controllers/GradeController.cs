using GenericRepositoryPatternServices.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GenericRepositoryPatternWeb.Controllers
{
    public class GradeController : Controller
    {
        private IGradeRepository _gradeRepository;
        public GradeController(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }
        public ActionResult Index()
        {
            var data = _gradeRepository.GetAll();
            return Content("");
        }
    }
}