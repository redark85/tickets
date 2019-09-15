using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaDeTickets.Models.ViewModels
{
    public class TablaViewModel
    {
        public int Id { get; set; }

        public int Tipo { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string NombrePersona { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "DNI Persona")]
        public string DniPersona { get; set; }
        public DateTime Tiempo { get; set; }
        [Display(Name = "Tiempo de espera Fila 1:")]
        public string TiempoF1 { get; set; }
        [Display(Name = "Tiempo de espera Fila 2:")]
        public string TiempoF2 { get; set; }
         
  
        public DateTime TiempoBase1 { get; set; }
     
        public DateTime TiempoBase2 { get; set; }
    }
}