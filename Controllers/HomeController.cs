using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using atomicflair.Models;

namespace atomicflair.Controllers
{
	[HandleError]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{			
			return View(new AtomicUserViewModel());
		}

		public ActionResult About()
		{
			return View();
		}    
	}
}
