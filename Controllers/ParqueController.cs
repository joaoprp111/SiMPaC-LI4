using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SiMPAC.Models;
using SiMPAC.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.ObjectModel;
using System.Text;
using SiMPAC.Data;

namespace SiMPAC.Controllers
{
    public class ParqueController : Controller
    {
        public ActionResult Parques()
        {
            IConnection connection = new Connection();
            connection.Fetch();

            IDataAccessObject<Parque> pDAO = new ParqueDAO(connection);

            ModelState.Clear();
            return View(pDAO.getAll().AsEnumerable());
        }

        public ActionResult Details(int key)
        {
            IConnection connection = new Connection();
            connection.Fetch();

            IDataAccessObject<Parque> pDAO = new ParqueDAO(connection);
            return View(pDAO.search(key));
        }

        public ActionResult Pesquisar()
        {
            return View();
        }

        public async Task<IActionResult> SearchResult(String Search)
        {
            IConnection connection = new Connection();
            connection.Fetch();

            IDataAccessObject<Parque> pDAO = new ParqueDAO(connection);
            List<Parque> ps = pDAO.getAll();
            
            foreach (Parque p in ps)
            {
                if (p.Nome.Contains(Search))
                {
                    return View(p);
                }
            }
            return View("Não existe o parque!");
        }
    }
}
