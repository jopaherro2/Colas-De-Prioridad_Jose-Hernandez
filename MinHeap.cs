using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Colas_De_Prioridad_Jose_Hernandez
{
    public class MinHeap
    {
        private readonly List<Ticket> _heap;

        public MinHeap()
        {
            _heap = new List<Ticket>();
        }

        public int Cantidad => _heap.Count;

        public void Insertar(Ticket ticket)
        {
            _heap.Add(ticket);
            BubbleUp(_heap.Count - 1);
        }

        public Ticket ObtenerSiguiente()
        {
            if (_heap.Count == 0) return null;
            return _heap[0];
        }

        public Ticket AtenderSiguiente()
        {
            if (_heap.Count == 0) return null;

            Ticket raiz = _heap[0];
            Ticket ultimoNodo = _heap[_heap.Count - 1];
            _heap.RemoveAt(_heap.Count - 1);

            if (_heap.Count > 0)
            {
                _heap[0] = ultimoNodo;
                BubbleDown(0);
            }

            return raiz;
        }

        private void BubbleUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (_heap[index].Prioridad < _heap[parentIndex].Prioridad)
                {
                    Intercambiar(index, parentIndex);
                    index = parentIndex;
                }
                else
                {
                    break;
                }
            }
        }

        private void BubbleDown(int index)
        {
            int minIndex = index;
            int leftChild = 2 * index + 1;
            int rightChild = 2 * index + 2;

            if (leftChild < _heap.Count && _heap[leftChild].Prioridad < _heap[minIndex].Prioridad)
            {
                minIndex = leftChild;
            }

            if (rightChild < _heap.Count && _heap[rightChild].Prioridad < _heap[minIndex].Prioridad)
            {
                minIndex = rightChild;
            }

            if (index != minIndex)
            {
                Intercambiar(index, minIndex);
                BubbleDown(minIndex);
            }
        }

        private void Intercambiar(int i, int j)
        {
            Ticket temp = _heap[i];
            _heap[i] = _heap[j];
            _heap[j] = temp;
        }

        public Ticket BuscarPorCodigo(string codigo)
        {
            return _heap.Find(t => t.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase));
        }

        public List<Ticket> ObtenerListaActual()
        {
            return new List<Ticket>(_heap);
        }

        public bool ExisteCodigo(string codigo)
        {
            return _heap.Exists(t => t.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase));
        }
    }
}