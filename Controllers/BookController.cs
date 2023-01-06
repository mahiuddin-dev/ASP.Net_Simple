using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CrudApplication.Models;
using MySql.Data.MySqlClient;

namespace CrudApplication.Controllers
{
    public class BookController : Controller
    {
        //string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\C#\CrudApplication\App_Data\CRUD_DB.mdf;Integrated Security=True";
        string conString = "server=localhost;uid=root;pwd=Manik@7488;database=new_data";
        // GET: Book
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                con.Open();
                string q = "Select * from book";
                MySqlDataAdapter da = new MySqlDataAdapter(q, con);
                da.Fill(dt);
            }
            return View(dt);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View(new Book());
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(Book book)
        {
            try
            {
                // TODO: Add insert logic here
              using (MySqlConnection con = new MySqlConnection(conString))
                {
                    con.Open();
                    string q = $"INSERT INTO Book (name, author, publish) VALUES ('{book.name}', '{book.author}', '{book.publish}')";
                    MySqlCommand cmd = new MySqlCommand(q, con);
                    cmd.ExecuteNonQuery();
                }
                return RedirectToAction("Index");
            }
            catch(Exception exc)
            {
                return View();
            }
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            Book book = new Book();
            DataTable datatable = new DataTable();
            using(MySqlConnection con = new MySqlConnection(conString))
            {
                con.Open();
                string q = "SELECT * FROM Book WHERE ID="+id;
                MySqlDataAdapter dt = new MySqlDataAdapter(q, con);
                dt.Fill(datatable);
            }

            if(datatable.Rows.Count == 1)
            {
                book.id = Convert.ToInt32(datatable.Rows[0][0].ToString());
                book.name = datatable.Rows[0][1].ToString();
                book.author = datatable.Rows[0][2].ToString();
                book.publish = datatable.Rows[0][3].ToString();
                return View(book);
            }

            return RedirectToAction("Index");
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Book book)
        {
            try
            {
                // TODO: Add update logic here
                using(MySqlConnection con = new MySqlConnection(conString))
                {
                    con.Open();
                    string q = "UPDATE Book SET name=@name, author=@author, publish=@publish WHERE ID=@id";
                    MySqlCommand cmd = new MySqlCommand(q, con);
                    cmd.Parameters.AddWithValue("@id",book.id);
                    cmd.Parameters.AddWithValue("@name",book.name);
                    cmd.Parameters.AddWithValue("@author",book.author);
                    cmd.Parameters.AddWithValue("@publish", book.publish);
                    cmd.ExecuteNonQuery();
                }
                return RedirectToAction("Index");
            }
            catch (Exception exc)
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            return RedirectToAction("Index");
        }
    }
}
