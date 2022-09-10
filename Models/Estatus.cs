using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Examen.Models
{
    public class Estatus
    {
        public int EstatusId { get; set; }

        [Required]
        [StringLength(20)]
        public string Descripcion { get; set; }

        public void Convierte(DataRow fila)
        {
            EstatusId = fila["EstatusId"] == null ? 0 : Convert.ToInt32(fila["EstatusId"]);
            Descripcion = fila["Descripcion"] == null ? "" : fila["Descripcion"].ToString();
        }
    }
}