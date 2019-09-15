using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDeTickets.Models;
using SistemaDeTickets.Models.ViewModels;

namespace SistemaDeTickets.Controllers
{
    public class TablaController : Controller
    {
        //
        // GET: /Tabla/

        public ActionResult Index()
        {
            List<ListTablaViewModel> lst;
            using (CrudEntities db = new CrudEntities())
            {
                lst = (from d in db.Tickets
                       select new ListTablaViewModel
                       {
                           Id = d.id,
                           Tipo = d.tipo,
                           Tiempo = d.tiempo,
                           NombrePersona = d.nombrePersona,
                           DniPersona = d.idPersona
                       }).ToList();


            }

            return View(lst);
        }

        public ActionResult Nuevo()
        {
            TablaViewModel lsta = new TablaViewModel();

            using (CrudEntities db = new CrudEntities())
            {
                var lst1 = db.Tickets.SqlQuery("Select top 1 t.* from Tickets t where t.tipo = 1 order by t.id DESC ").ToList();
                var lst2 = db.Tickets.SqlQuery("Select top 1 t.* from Tickets t where t.tipo = 2 order by t.id DESC ").ToList();

                DateTime ahora = System.DateTime.Now;

                if (lst1.Count != 0)
                {
                    DateTime fechaF1 = lst1.First().tiempo;
                    lsta.TiempoF1 = CalcularTiempo(Convert.ToInt32((fechaF1 - ahora).TotalSeconds));
                    lsta.TiempoBase1 = lst1.First().tiempo;
                }
                
                

                if (lst2.Count != 0)
                {
                    DateTime fechaF2 = lst2.First().tiempo;
                    lsta.TiempoF2 = CalcularTiempo(Convert.ToInt32((fechaF2 - ahora).TotalSeconds));
                    lsta.TiempoBase2 = lst2.First().tiempo;


                }     

               
            }

            return View(lsta);
        }


        [HttpPost]
        public ActionResult Nuevo(TablaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (CrudEntities db = new CrudEntities())
                    {
                        var oTabla = new Tickets();
                        if (model.Tipo == 1)
                        {
                            if (model.TiempoBase1 < DateTime.Now)
                            {
                                model.Tiempo = DateTime.Now.AddMinutes(2);
                            }
                            else
                            {
                                model.Tiempo = model.TiempoBase1.AddMinutes(2);
                            }
                           
                        }
                        else
                        {
                            if (model.TiempoBase2 < DateTime.Now)
                            {
                                model.Tiempo = DateTime.Now.AddMinutes(3);
                            }
                            else
                            {
                                model.Tiempo = model.TiempoBase2.AddMinutes(3);
                            }
                        }
                        
                        oTabla.nombrePersona = model.NombrePersona;
                        oTabla.tiempo = model.Tiempo;
                        
                        oTabla.idPersona = model.DniPersona;
                        oTabla.tipo = model.Tipo;


                        db.Tickets.Add(oTabla);

                        db.SaveChanges();

                    }

                    return Redirect("/");

                }


                return View(model);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            TablaViewModel model = new TablaViewModel();

            using (CrudEntities db = new CrudEntities())
            {
                var oTabla = db.Tickets.Find(id);
                db.Tickets.Remove(oTabla);
                db.SaveChanges();
            }
            return Redirect("~/Tabla");
        }


        public string CalcularTiempo(int segundos)
        {
            string horaTurno = "00:00:00";

            if (segundos >= 0)
            {
                int minutosH = segundos / 60;
                int rSegundo = segundos % 60;
                int rHora = minutosH / 60;
                int rMinutos = minutosH % 60;

                string sHora = rHora.ToString();
                string sMinutos = rMinutos.ToString();
                string sSegundos = rSegundo.ToString();
                if (rHora < 10)
                {
                    sHora = "0" + rHora.ToString();
                }
                if (rMinutos < 10)
                {
                    sMinutos = "0" + rMinutos.ToString();
                }
                if (rSegundo < 10)
                {
                    sSegundos = "0" + rSegundo.ToString();
                }

                horaTurno = sHora + ":" + sMinutos + ":" + sSegundos;


            }
            return horaTurno;
        }


    }
}
