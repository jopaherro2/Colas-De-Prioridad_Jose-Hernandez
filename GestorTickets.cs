using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Colas_De_Prioridad_Jose_Hernandez
{
    public class GestorTickets
    {
        private readonly MinHeap _colaPrioridad;
        private int _correlativoTicket;

        public GestorTickets()
        {
            _colaPrioridad = new MinHeap();
            _correlativoTicket = 1;
        }

        public string GenerarNuevoCodigo()
        {
            string codigo = $"TCK{_correlativoTicket:D4}";
            while (_colaPrioridad.ExisteCodigo(codigo))
            {
                _correlativoTicket++;
                codigo = $"TCK{_correlativoTicket:D4}";
            }
            return codigo;
        }

        public void RegistrarTicket(string cliente, string descripcion, int prioridad)
        {
            string codigo = GenerarNuevoCodigo();
            Ticket nuevo = new Ticket(codigo, cliente, descripcion, prioridad);
            _colaPrioridad.Insertar(nuevo);
            _correlativoTicket++;
        }

        public Ticket ObtenerSiguienteTicket() => _colaPrioridad.ObtenerSiguiente();

        public Ticket AtenderTicket() => _colaPrioridad.AtenderSiguiente();

        public List<Ticket> ObtenerTodosLosTickets() => _colaPrioridad.ObtenerListaActual();

        public Ticket BuscarTicket(string codigo) => _colaPrioridad.BuscarPorCodigo(codigo);

        public int ObtenerCantidadTickets() => _colaPrioridad.Cantidad;
    }
}