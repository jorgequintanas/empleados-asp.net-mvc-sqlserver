using Examen.Models;
using Examen.Repositorios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Examen.Controllers
{
    public class EstatusController : Controller
    {
        
        public ActionResult Index()
        {
            List<Estatus> lista = new List<Estatus>();
            EstatusRepositorio rep = EstatusRepositorio.instancia;
            using (DataTable dt = rep.RegresaEstatus())
            {
                foreach (DataRow fila in dt.Rows)
                {
                    Estatus e = new Estatus();
                    e.Convierte(fila);
                    lista.Add(e);
                }
            }
            return View(lista);
        }

        public ActionResult Details(int id)
        {

            return View(RegresaEstatus(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Estatus estatus)
        {
            EstatusRepositorio rep = EstatusRepositorio.instancia;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter("@Descripcion", estatus.Descripcion));
                
                if (!rep.Insertar(parametros))
                    throw new Exception();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
                
        public ActionResult Edit(int id)
        {
            return View(RegresaEstatus(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, Estatus estatus)
        {
            EstatusRepositorio rep = EstatusRepositorio.instancia;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter("@Descripcion", estatus.Descripcion));
                parametros.Add(new SqlParameter("@EstatusId", estatus.EstatusId));

                if (!rep.Actualizar(parametros))
                    throw new Exception();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View(RegresaEstatus(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, Estatus estatus)
        {
            EstatusRepositorio rep = EstatusRepositorio.instancia;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter("@EstatusId", id));
                if (!rep.Eliminar(parametros))
                    throw new Exception();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private Estatus RegresaEstatus(int id)
        {
            EstatusRepositorio rep = EstatusRepositorio.instancia;
            using (DataTable dt = rep.RegresaEstatus(id))
            {
                DataRow fila = dt.Rows[0];
                Estatus e = new Estatus();
                e.Convierte(fila);
                return e;
            }
        }
    }
}
