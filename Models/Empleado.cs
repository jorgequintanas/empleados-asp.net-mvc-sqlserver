using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Examen.Models
{
    public class Empleado
    {
        public int EmpleadoId { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Apellido Paterno")]
        [StringLength(50)]
        public string ApellidoPaterno { get; set; }

        [Required]
        [Display(Name = "Apellido Materno")]
        [StringLength(50)]
        public string ApellidoMaterno { get; set; }

        [Required]
        [Display(Name = "Fecha Nacimiento")]
        [DataType(DataType.Date, ErrorMessage = "Debe capturar formado de fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [Range(1, 99999, ErrorMessage = "El campo Estatus es obligatorio")]
        public int EstatusId { get; set; }

        [Required]
        [Display(Name = "Estatus")]
        public string EstatusDescripcion { get; set; }

        public void Convierte(DataRow fila)
        {
            EmpleadoId = fila["EmpleadoId"] == null ? 0 : Convert.ToInt32(fila["EmpleadoId"]);
            Nombre = fila["nombre"] == null ? "" : fila["nombre"].ToString();
            ApellidoPaterno = fila["ApellidoPaterno"] == null ? "" : fila["ApellidoPaterno"].ToString();
            ApellidoMaterno = fila["ApellidoMaterno"] == null ? "" : fila["ApellidoMaterno"].ToString();
            FechaNacimiento = fila["FechaNacimiento"] == null ? Convert.ToDateTime(DateTime.MinValue) : Convert.ToDateTime(fila["FechaNacimiento"].ToString());
            EstatusId = fila["EstatusId"] == null ? 0 : Convert.ToInt32(fila["EstatusId"]);
            EstatusDescripcion = fila["Descripcion"] == null ? "" : fila["Descripcion"].ToString();
        }
    }
}