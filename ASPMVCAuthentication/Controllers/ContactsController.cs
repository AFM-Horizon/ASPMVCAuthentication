using ASPMVCAuthentication.Data.Repositories;
using ASPMVCAuthentication.Models;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ASPMVCAuthentication.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        private IRepository<Contact> _repository = new ContactsRepository();

        public async Task<ActionResult> Index()
        {
            return View(await _repository.GetAll());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var contact = await _repository.GetById(id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            return View(contact);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Phone,Email,Birthday")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                await _repository.Create(contact);
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var contact = await _repository.GetById(id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Phone,Email,Birthday")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                await _repository.Update(contact);
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var contact = await _repository.GetById(id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            return View(contact);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            await _repository.DeleteById(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
