using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Examen.Helpers;

namespace Examen.Repositorios
{
    public class EmpleadoRepositorio : BDHelper
    {

        #region singleton

            private static EmpleadoRepositorio _instancia = null;
            public static EmpleadoRepositorio instancia
            {
                get
                {
                    if (_instancia == null)
                        _instancia = new EmpleadoRepositorio();
                    return _instancia;
                }
            }

        #endregion

        public DataTable RegresaEmpleados()
        {
            return Select("SELECT E.Empleado_Id EmpleadoId, E.Nombre, E.Apellido_Paterno ApellidoPaterno, E.Apellido_Materno ApellidoMaterno, E.Fecha_Nacimiento FechaNacimiento, Es.Estatus_Id EstatusId, Es.Descripcion FROM Empleados E JOIN Estatus Es ON Es.Estatus_Id = E.Estatus_Id");
        }

        public DataTable RegresaEmpleado(int id)
        {
            return Select("SELECT E.Empleado_Id EmpleadoId, E.Nombre, E.Apellido_Paterno ApellidoPaterno, E.Apellido_Materno ApellidoMaterno, E.Fecha_Nacimiento FechaNacimiento, Es.Estatus_Id EstatusId, Es.Descripcion FROM Empleados E JOIN Estatus Es ON Es.Estatus_Id = E.Estatus_Id WHERE E.Empleado_Id = " + id.ToString());
        }

        public bool Insertar(List<SqlParameter> parametros)
        {
            return EjecutaSP("dbo.EmpleadoInsertar", parametros);
        }

        public bool Actualizar(List<SqlParameter> parametros)
        {
            return EjecutaSP("dbo.EmpleadoActualizar", parametros);
        }
        public bool Eliminar(List<SqlParameter> parametros)
        {
            return EjecutaSP("dbo.EmpleadoEliminar", parametros);
        }
    }
}