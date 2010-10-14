using System.Collections.Generic;
using System.Web.Mvc;
using StudyStudio.Infrastructure.Commands;
using StudyStudio.Infrastructure.Queries;

namespace StudyStudio.Web.Controllers
{
    public class AssignmentController : Controller
    {
        readonly IAssignmentCommandService _commandService;

        public AssignmentController(IAssignmentCommandService commandService, IAssignmentQueryService queryService)
        {
            _commandService = commandService;
        }

        //
        // GET: /Assignment/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Assignment/Details/5

        public ActionResult Details(string id)
        {
            return View();
        }

        //
        // GET: /Assignment/Create

        public ActionResult Create()
        {
            return View();
        }
        
        //
        // GET: /Assignment/Edit/5
 
        public ActionResult Edit(string id)
        {
            return View();
        }

        //
        // POST: /Assignment/Edit/5

        [HttpPost]
        public ActionResult Create(IList<string> exerciseIds)
        {
            try
            {
                _commandService.CreateAssignment(exerciseIds);
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Assignment/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Assignment/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}