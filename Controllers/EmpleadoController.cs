using Examen.Models;
using Examen.Repositorios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Examen.Controllers
{
    public class EmpleadoController : Controller
    {
        
        public ActionResult Index()
        {
            List<Empleado> lista = new List<Empleado>();
            EmpleadoRepositorio rep = EmpleadoRepositorio.instancia;
            using (DataTable dt = rep.RegresaEmpleados())
            {
                foreach (DataRow fila in dt.Rows)
                {
                    Empleado e = new Empleado();
                    e.Convierte(fila);
                    lista.Add(e);
                }
            }
            return View(lista);
        }
        
        public ActionResult Details(int id)
        {
            return View(RegresaEmpleado(id));
        }

        public ActionResult Create()
        {
            EstatusRepositorio repEst = EstatusRepositorio.instancia;
            using (DataTable dtEst = repEst.RegresaEstatus())
            {
                List<SelectListItem> ComboEstatus = new List<SelectListItem>();

                foreach (DataRow f in dtEst.Rows)
                {
                    ComboEstatus.Add(new SelectListItem { Text = f["Descripcion"].ToString(), Value = f["EstatusId"].ToString() });
                }

                ComboEstatus.Add(new SelectListItem { Text = "Seleccione", Value = "-1", Selected = true });

                ViewBag.Estatus = ComboEstatus;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(Empleado empleado)
        {
            EmpleadoRepositorio rep = EmpleadoRepositorio.instancia;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter("@Nombre", empleado.Nombre));
                parametros.Add(new SqlParameter("@ApellidoPaterno", empleado.ApellidoPaterno));
                parametros.Add(new SqlParameter("@ApellidoMaterno", empleado.ApellidoMaterno));
                parametros.Add(new SqlParameter("@FechaNacimiento", empleado.FechaNacimiento));
                parametros.Add(new SqlParameter("@EstatusId", empleado.EstatusId));
                
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
            return View(RegresaEmpleado(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, Empleado empleado)
        {
            EmpleadoRepositorio rep = EmpleadoRepositorio.instancia;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter("@Nombre", empleado.Nombre));
                parametros.Add(new SqlParameter("@ApellidoPaterno", empleado.ApellidoPaterno));
                parametros.Add(new SqlParameter("@ApellidoMaterno", empleado.ApellidoMaterno));
                parametros.Add(new SqlParameter("@FechaNacimiento", empleado.FechaNacimiento));
                parametros.Add(new SqlParameter("@EstatusId", empleado.EstatusId));
                parametros.Add(new SqlParameter("@EmpleadoId", empleado.EmpleadoId));

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
            return View(RegresaEmpleado(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, Empleado empleado)
        {
            EmpleadoRepositorio rep = EmpleadoRepositorio.instancia;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter("@EmpleadoId", id));
                if (!rep.Eliminar(parametros))
                    throw new Exception();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private Empleado RegresaEmpleado(int id)
        {
            EstatusRepositorio repEst = EstatusRepositorio.instancia;
            EmpleadoRepositorio rep = EmpleadoRepositorio.instancia;
            using (DataTable dtEst = repEst.RegresaEstatus())
            {
                using (DataTable dt = rep.RegresaEmpleado(id))
                {
                    DataRow fila2 = dt.Rows[0];

                    Empleado empleado = new Empleado();
                    empleado.Convierte(fila2);

                    List<SelectListItem> ComboEstatus = new List<SelectListItem>();
                    foreach (DataRow f in dtEst.Rows)
                    {
                        if (empleado.EstatusDescripcion == f["Descripcion"].ToString())
                        {
                            ComboEstatus.Add(new SelectListItem { Text = f["Descripcion"].ToString(), Value = f["EstatusId"].ToString(), Selected = true });
                        }
                        else
                        {
                            ComboEstatus.Add(new SelectListItem { Text = f["Descripcion"].ToString(), Value = f["EstatusId"].ToString(), Selected = false });
                        }
                    }

                    ViewBag.Estatus = ComboEstatus;

                    return empleado;
                }
            }
        }
    }
}
