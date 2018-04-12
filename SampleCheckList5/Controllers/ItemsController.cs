using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SampleCheckList5.Models;

namespace SampleCheckList5.Controllers
{
    public class ItemsController : Controller
    {
        private Sprint db = new Sprint();

        // GET: Items
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.Project);
            return View(items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            Item item;
            List<string> s = new List<string>();
            using (SqlConnection con = new SqlConnection("Data Source=OPTIMUS-216\\ENTERPRISE2017;Initial Catalog=Sprint2;User ID=sa;Password=qwerty"))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Items WHERE [Project ID]=" + id))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    { 
                        
                        while(reader.Read())
                        {
                            s.Add(reader[2].ToString());
                        }
                    }
                    /* cmd.Connection = con;
                    con.Open();
                    try
                    {
                        int abc = int.Parse(cmd.ExecuteScalar().ToString());
                        System.Diagnostics.Debug.WriteLine("Values are not: " + abc);
                        item = db.Items.Find(abc);
                    }
                    catch(Exception e)
                    {
                       return HttpNotFound();
                    */
                }
            }
           /* if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
             
            if (item == null)
            {
                return HttpNotFound();
            }*/
            return View(s);
        }

        // GET: Items/Create
        public ActionResult Create(int id)
        {
            System.Diagnostics.Debug.WriteLine("ID IS: "+ id);
            ViewBag.Project_ID = new SelectList(db.Projects, "CheckList_ID", "CheckList_Name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Items_ID,Project_ID,Items_Name")] Item item)
        {
            
            if (ModelState.IsValid)
            {
                //db.Items.Add(item);
                //db.SaveChanges();
                System.Diagnostics.Debug.WriteLine("INSERT INTO Item("+item.Project_ID+", "+item.Items_Name+") ");
                using (SqlConnection con = new SqlConnection("Data Source=OPTIMUS-216\\ENTERPRISE2017;Initial Catalog=Sprint2;User ID=sa;Password=qwerty"))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Items ([Project ID],[Items Name]) values("+item.Project_ID+",'"+item.Items_Name+"') "))
                    {
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("Index", "Projects");
            }

            ViewBag.Project_ID = new SelectList(db.Projects, "CheckList_ID", "CheckList_Name", item.Project_ID);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.Project_ID = new SelectList(db.Projects, "CheckList_ID", "CheckList_Name", item.Project_ID);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Items_ID,Project_ID,Items_Name")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Project_ID = new SelectList(db.Projects, "CheckList_ID", "CheckList_Name", item.Project_ID);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
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
