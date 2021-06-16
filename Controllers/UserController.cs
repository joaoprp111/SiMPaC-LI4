using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SiMPAC.Data;
using SiMPAC.Models;
using SiMPAC.Data;

namespace SiMPAC.Controllers
{
    public class UserController : Controller
    {
        // 1. *************RETRIEVE ALL User DETAILS ******************
        // GET: User
        public ActionResult Index()
        {
            IConnection connection = new Connection();
            connection.Fetch();

            IDataAccessObject<Utilizador> uDAO = new UtilizadorDAO(connection);

            ModelState.Clear();
            return View(uDAO.search(1));
        }

        // 2. *************ADD NEW User ******************
        // GET: User/Create
        public ActionResult Register()
        {
            Utilizador u = new Utilizador();
            return View(u);
        }

        [HttpPost]
        public ActionResult Register(Utilizador smodel)
        {
            IConnection connection = new Connection();
            connection.Fetch();

            try
            {
                if (ModelState.IsValid)
                {

                    IDataAccessObject<Utilizador> uDAO = new UtilizadorDAO(connection);
                    
                    uDAO.insert(smodel);

                    return RedirectToAction("Index");

                }
                return View();
            }
            catch (Exception e)
            {
                string message = e.Message; // or using e.InnerException.Message
                Console.WriteLine("{0} Exception caught.", e);
            }
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
            return View();
        }

        public ActionResult Login()
        {
            Utilizador u = new Utilizador();
            return View(u);
        }

        [HttpPost]
        public ActionResult Login(Utilizador smodel)
        {
            IConnection connection = new Connection();
            connection.Fetch();

            try
            {
                if (ModelState.IsValid)
                {

                    IDataAccessObject<Utilizador> uDAO = new UtilizadorDAO(connection);
                    
                    uDAO.insert(smodel);

                    return RedirectToAction("Index");

                }
                return View();
            }
            catch (Exception e)
            {
                string message = e.Message; // or using e.InnerException.Message
                Console.WriteLine("{0} Exception caught.", e);
            }
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
            return View();
        }


        // 3. ************* EDIT User DETAILS ******************
        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            IConnection connection = new Connection();
            connection.Fetch();
            IDataAccessObject<Utilizador> uDAO = new UtilizadorDAO(connection);

            return View(uDAO.search(1));
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Utilizador u)
        {
            try
            {
                IConnection connection = new Connection();
                connection.Fetch();
                IDataAccessObject<Utilizador> uDAO = new UtilizadorDAO(connection);
                uDAO.update(id, u);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // 4. ************* DELETE User DETAILS ******************
        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                IConnection connection = new Connection();
                connection.Fetch();
                IDataAccessObject<Utilizador> uDAO = new UtilizadorDAO(connection);
                if (uDAO.remove(id))
                {
                    ViewBag.AlertMsg = "User Deleted Successfully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
