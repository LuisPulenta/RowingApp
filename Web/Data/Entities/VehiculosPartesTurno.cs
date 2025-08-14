using System;
using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class VehiculosPartesTurno
    {
        [Key]
        public int IDTurno { get; set; }
        public int IdUser { get; set; }
        public DateTime? FechaCarga { get; set; }
        public string Numcha { get; set; }
        public string CodVehiculo { get; set; }
        public string AsignadoActual { get; set; }
        public DateTime? FechaTurno { get; set; }
        public int? HoraTurno { get; set; }
        public string TextoBreve { get; set; }
        public DateTime? FechaConfirmaTurno { get; set; }
        public int? IDUserConfirma { get; set; }
        public DateTime? FechaTurnoConfirmado { get; set; }
        public int? HoraTurnoConfirmado { get; set; }
        public string Grupo { get; set; }
        public string Causante { get; set; }
        public int? VehiculoRetirado { get; set; }
        public int? IdVehiculoParteTaller { get; set; }
    }
}
