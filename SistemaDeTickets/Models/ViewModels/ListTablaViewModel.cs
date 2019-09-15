using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDeTickets.Models.ViewModels
{
    public class ListTablaViewModel
    {

        public int Id { get; set; }
        public int Tipo { get; set; }
        public string NombrePersona { get; set;}
        public string DniPersona { get; set; }
        public DateTime Tiempo { get; set; }

    }
}