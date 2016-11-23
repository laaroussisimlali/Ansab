using Ansab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;
namespace Ansab.Controllers
{
    public class HomeController : Controller
    {
        //TODO: initialize db only if specified group or person defined
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int? GroupId, int? PersonId)
        {
            var indexVM = new IndexVM();
            if (GroupId != null)
            {
                Group group = db.Groups.SingleOrDefault(x => x.Id == GroupId);
                if (group == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
                indexVM.GroupVM = new GroupVM(group);
                return View(indexVM);
            }
            else if (PersonId != null)
            {
                Person person = db.People.Include(x => x.Relations1).Include(x => x.Relations2).SingleOrDefault(x => x.Id == PersonId);
                if (person == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
                indexVM.PersonVM = new PersonVM(person);
                indexVM.PersonVM.GetFathers(db);
                return View(indexVM);
            }

            return View(new IndexVM());
        }



    }
}