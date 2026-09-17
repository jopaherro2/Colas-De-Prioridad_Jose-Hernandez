using System;
using System.Collections.Generic;

namespace Colas_De_Prioridad_Jose_Hernandez
{
    public class MenuConsola
    {
        private readonly GestorTickets _gestor;

        public MenuConsola()
        {
            _gestor = new GestorTickets();
        }

        public void Iniciar()
        {
            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                MostrarEncabezado();
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1": RegistrarTicket(); break;
                    case "2": MostrarSiguienteTicket(); break;
                    case "3": AtenderTicket(); break;
                    case "4": MostrarColaPrioridad(); break;
                    case "5": BuscarTicket(); break;
                    case "6": MostrarCantidadTickets(); break;
                    case "7":
                        salir = true;
                        Console.WriteLine("\nSaliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida. Presione cualquier tecla para continuar.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void MostrarEncabezado()
        {
            Console.WriteLine("====================================================");
            Console.WriteLine("     SISTEMA DE GESTIÓN DE TICKETS DE SOPORTE");
            Console.WriteLine("                COLA DE PRIORIDAD");
            Console.WriteLine("====================================================\n");
            Console.WriteLine("[1] Registrar Ticket");
            Console.WriteLine("[2] Mostrar Siguiente Ticket");
            Console.WriteLine("[3] Atender Ticket");
            Console.WriteLine("[4] Mostrar Cola de Prioridad");
            Console.WriteLine("[5] Buscar Ticket");
            Console.WriteLine("[6] Mostrar Cantidad de Tickets");
            Console.WriteLine("[7] Salir\n");
            Console.WriteLine("----------------------------------------------------");
            Console.Write("Seleccione una opción: ");
        }

        private void RegistrarTicket()
        {
            Console.Clear();
            Console.WriteLine("====================================================");
            Console.WriteLine("                 REGISTRO DE TICKET");
            Console.WriteLine("====================================================\n");

            string codigoGenerado = _gestor.GenerarNuevoCodigo();
            Console.WriteLine($"Código del Ticket : {codigoGenerado}");

            Console.Write("Cliente           : ");
            string cliente = Console.ReadLine()?.Trim();

            Console.Write("Descripción       : ");
            string descripcion = Console.ReadLine()?.Trim();

            // Guía de niveles de prioridad en la pantalla de registro
            Console.WriteLine("\nNiveles de Prioridad: [1-Crítica | 2-Alta | 3-Media | 4-Baja | 5-Muy Baja]");

            int prioridad;
            while (true)
            {
                Console.Write("Prioridad (1-5)   : ");
                if (int.TryParse(Console.ReadLine(), out prioridad) && prioridad >= 1 && prioridad <= 5)
                {
                    break;
                }
                Console.WriteLine("-> Error: Debe ingresar un número entero entre 1 y 5.");
            }

            _gestor.RegistrarTicket(cliente, descripcion, prioridad);

            Console.WriteLine("\n----------------------------------------------------");
            Console.WriteLine("Ticket registrado exitosamente.");
            Console.WriteLine("----------------------------------------------------");
            Pausar();
        }

        private void MostrarSiguienteTicket()
        {
            Console.Clear();
            Console.WriteLine("====================================================");
            Console.WriteLine("             SIGUIENTE TICKET A ATENDER");
            Console.WriteLine("====================================================\n");

            Ticket ticket = _gestor.ObtenerSiguienteTicket();

            if (ticket == null)
            {
                Console.WriteLine("No hay tickets registrados en la cola de prioridad.");
            }
            else
            {
                Console.WriteLine("RESULTADO DE LA CONSULTA");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine($"Código del Ticket : {ticket.Codigo}");
                Console.WriteLine($"Cliente           : {ticket.Cliente}");
                Console.WriteLine($"Descripción       : {ticket.Descripcion}");
                Console.WriteLine($"Prioridad         : {ticket.ObtenerTextoPrioridad()}");
            }

            Console.WriteLine("----------------------------------------------------");
            Pausar();
        }

        private void AtenderTicket()
        {
            Console.Clear();
            Console.WriteLine("====================================================");
            Console.WriteLine("                   ATENDER TICKET");
            Console.WriteLine("====================================================\n");

            Ticket ticketAtendido = _gestor.AtenderTicket();

            if (ticketAtendido == null)
            {
                Console.WriteLine("No hay tickets pendientes para atender.");
            }
            else
            {
                Console.WriteLine("RESULTADO DE LA OPERACIÓN");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine($"Código del Ticket : {ticketAtendido.Codigo}");
                Console.WriteLine($"Cliente           : {ticketAtendido.Cliente}");
                Console.WriteLine($"Descripción       : {ticketAtendido.Descripcion}");
                Console.WriteLine($"Prioridad         : {ticketAtendido.ObtenerTextoPrioridad()}");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("Ticket atendido correctamente.");
            }

            Console.WriteLine("----------------------------------------------------");
            Pausar();
        }

        private void MostrarColaPrioridad()
        {
            Console.Clear();
            Console.WriteLine("====================================================");
            Console.WriteLine("               COLA DE PRIORIDAD ACTUAL");
            Console.WriteLine("====================================================\n");

            List<Ticket> lista = _gestor.ObtenerTodosLosTickets();

            if (lista.Count == 0)
            {
                Console.WriteLine("La cola de prioridad se encuentra vacía.");
            }
            else
            {
                Console.WriteLine($"{"Posición",-10} {"Ticket",-12} {"Cliente",-20} {"Prioridad",-10}");
                Console.WriteLine("----------------------------------------------------");

                for (int i = 0; i < lista.Count; i++)
                {
                    Console.WriteLine($"{i,-10} {lista[i].Codigo,-12} {lista[i].Cliente,-20} {lista[i].Prioridad,-10}");
                }

                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine($"Total de Tickets: {lista.Count}");
            }

            Pausar();
        }

        private void BuscarTicket()
        {
            Console.Clear();
            Console.WriteLine("====================================================");
            Console.WriteLine("                BÚSQUEDA DE TICKET");
            Console.WriteLine("====================================================\n");

            Console.Write("Ingrese el código del ticket: ");
            string codigo = Console.ReadLine()?.Trim();

            Ticket ticket = _gestor.BuscarTicket(codigo);

            Console.WriteLine("\nRESULTADO DE LA BÚSQUEDA");
            Console.WriteLine("----------------------------------------------------");

            if (ticket != null)
            {
                Console.WriteLine($"Código del Ticket : {ticket.Codigo}");
                Console.WriteLine($"Cliente           : {ticket.Cliente}");
                Console.WriteLine($"Descripción       : {ticket.Descripcion}");
                Console.WriteLine($"Prioridad         : {ticket.ObtenerTextoPrioridad()}");
            }
            else
            {
                Console.WriteLine($"El ticket con código '{codigo}' no fue encontrado.");
            }

            Console.WriteLine("----------------------------------------------------");
            Pausar();
        }

        private void MostrarCantidadTickets()
        {
            Console.Clear();
            Console.WriteLine("====================================================");
            Console.WriteLine("                CANTIDAD DE TICKETS");
            Console.WriteLine("====================================================\n");
            Console.WriteLine("TOTAL DE TICKETS REGISTRADOS");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine($"Cantidad de Tickets : {_gestor.ObtenerCantidadTickets()}");
            Console.WriteLine("----------------------------------------------------");
            Pausar();
        }

        private void Pausar()
        {
            Console.WriteLine("\nPresione cualquier tecla para regresar al menú...");
            Console.ReadKey();
        }
    }
}