using System;

namespace Colas_De_Prioridad_Jose_Hernandez
{
    public class Ticket
    {
        public string Codigo { get; set; }
        public string Cliente { get; set; }
        public string Descripcion { get; set; }
        public int Prioridad { get; set; }

        public Ticket(string codigo, string cliente, string descripcion, int prioridad)
        {
            Codigo = codigo;
            Cliente = cliente;
            Descripcion = descripcion;
            Prioridad = prioridad;
        }

        public string ObtenerTextoPrioridad()
        {
            return Prioridad switch
            {
                1 => "1 (Crítica)",
                2 => "2 (Alta)",
                3 => "3 (Media)",
                4 => "4 (Baja)",
                5 => "5 (Muy Baja)",
                _ => $"{Prioridad} (Desconocida)"
            };
        }
    }
}