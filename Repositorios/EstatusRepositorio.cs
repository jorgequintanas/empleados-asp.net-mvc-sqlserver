using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Examen.Helpers;

namespace Examen.Repositorios
{
    public class EstatusRepositorio : BDHelper
    {

        #region singleton

        private static EstatusRepositorio _instancia = null;
        public static EstatusRepositorio instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new EstatusRepositorio();
                return _instancia;
            }
        }
        #endregion

        public DataTable RegresaEstatus()
        {
            return Select("SELECT Estatus_Id EstatusId, Descripcion FROM Estatus");
        }

        public DataTable RegresaEstatus(int id)
        {
            return Select("SELECT Estatus_Id EstatusId, Descripcion FROM Estatus WHERE Estatus_Id = " + id.ToString());
        }

        public bool Insertar(List<SqlParameter> parametros)
        {
            return EjecutaSP("dbo.EstatusInsertar", parametros);
        }

        public bool Actualizar(List<SqlParameter> parametros)
        {
            return EjecutaSP("dbo.EstatusActualizar", parametros);
        }
        public bool Eliminar(List<SqlParameter> parametros)
        {
            return EjecutaSP("dbo.EstatusEliminar", parametros);
        }
    }
}