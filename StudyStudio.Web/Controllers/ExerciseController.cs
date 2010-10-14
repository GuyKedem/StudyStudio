using System.Web.Mvc;
using StudyStudio.Infrastructure.Commands;
using StudyStudio.Infrastructure.Queries;
using StudyStudio.Web.Models.Exercise;

namespace StudyStudio.Web.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly IExerciseCommandService _commandService;
        private readonly IExerciseQueryService _queryService;

        public ExerciseController(IExerciseCommandService commandService, IExerciseQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        //
        // GET: /Exercise/Browse
        public ActionResult Browse()
        {
            var exercises = _queryService.BrowseExercises();
            return View(new BrowseModel {SearchResults = exercises});
        }

        public ActionResult Index()
        {
            return RedirectToAction("Browse");
        }

        //
        // GET: /Exercise/Details/5

        public ActionResult Details(int id)
        {
            return RedirectToAction("Edit");
        }
        
        public ActionResult Create()
        {
            return RedirectToAction("Edit", new EditModel());
        }

        //
        // GET: /Exercise/Edit/
 
        public ActionResult Edit(string id)
        {
            return View(new EditModel {Id = id});
        }

        //
        // POST: /Exercise/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, string body)
        {
            try
            {
                _commandService.CreateExercise(body);
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Exercise/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Exercise/Delete/5

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
