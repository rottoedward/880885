using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Msit115BestOne.Models;
using PagedList;

namespace Msit115BestOne.Areas.Boards.Controllers
{
    public class BoardsController : Controller
    {
        private ManPowerEntities db = new ManPowerEntities();

        public ActionResult Index(string sortBy,int?page)
        {
            
            ViewBag.SortStartDateTime = string.IsNullOrEmpty(sortBy) ? "StartDateTime" : "";
            var board = db.Board.AsQueryable();
            switch (sortBy)
            {
                case "StartDateTime":
                    board = board.OrderBy(b => b.StartDateTime);
                    break;
                default:                  
                    board = board.OrderByDescending(b => b.StartDateTime);
                    break; 
            }
            return PartialView(board.ToList().ToPagedList(page??1,5));
        }
        public ActionResult Create()
        {
            int mid;
            if (Session["MEMBERID"] == null)
            {
                return RedirectToAction("Login", "Member", new { area = "MyMember" });
            }
            else
            {
                mid = (int)(Session["MEMBERID"]);
            }

            if (Request.Form.Count > 0)
            {
                //Session["MEMBERID"] = 3;
              mid = (int)Session["MEMBERID"];
                Board _board = new Board();
                _board.MemberID = mid;       // (int)Session["MEMBERID"];
                _board.CaseTitle = Request.Form["CaseTitle"];
                _board.CaseContent = Request.Form["CaseContent"];
                _board.StartDateTime = DateTime.Now;
                db.Board.Add(_board);
                db.SaveChanges();
                return RedirectToAction("Index","Home",new { area=""});
            }
            return View();

        }

        [HttpGet]
        public ActionResult Edit(int id=0)
        {
            return View(db.Board.Find(id));
        }

 
        [HttpPost]
        public ActionResult Edit()
        {
            int mid = (int)Session["MEMBERID"];
            Board _board = new Board();
                _board.BoardID = Convert.ToInt32(Request.Form["BoardID"]);
                _board.MemberID = mid;              // (int)Session["MEMBERID"];
            _board.CaseTitle = Request.Form["CaseTitle"];
                _board.CaseContent = Request.Form["CaseContent"];
            _board.StartDateTime = DateTime.Now;

                db.Entry(_board).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Board board = db.Board.Find(id);
            if (board == null)
            {
                return HttpNotFound();
            }
            return View(board);
        }

        // POST: Boards/Boards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Board board = db.Board.Find(id);
            db.Board.Remove(board);
            db.SaveChanges();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
