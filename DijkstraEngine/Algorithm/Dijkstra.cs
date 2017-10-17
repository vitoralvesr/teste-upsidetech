using DijkstraEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraEngine.Algorithm
{
    public class Dijkstra
    {
        private readonly List<VerticeModel> nodo;
        private readonly List<ArestaModel> arestas;
        private HashSet<VerticeModel> nodosResolvidos;
        private HashSet<VerticeModel> nodosNaoResolvidos;
        private Dictionary<VerticeModel, VerticeModel> antecessores;
        private Dictionary<VerticeModel, int> distancia;

        public Dijkstra(GrafoModel grafo)
        {
            // create a copy of the array so that we can operate on this array
            this.nodo = new List<VerticeModel>(grafo.Vertices);
            this.arestas = new List<ArestaModel>(grafo.Arestas);
        }

        public void Executa(VerticeModel origem)
        {
            nodosResolvidos = new HashSet<VerticeModel>();
            nodosNaoResolvidos = new HashSet<VerticeModel>();
            distancia = new Dictionary<VerticeModel, int>();
            antecessores = new Dictionary<VerticeModel, VerticeModel>();
            distancia.Add(origem, 0);
            nodosNaoResolvidos.Add(origem);
            while (nodosNaoResolvidos.Count > 0)
            {
                VerticeModel node = BuscaMenor(nodosNaoResolvidos);
                nodosResolvidos.Add(node);
                nodosNaoResolvidos.Remove(node);
                EncontraDistanciasMinimas(node);
            }
        }

        private void EncontraDistanciasMinimas(VerticeModel nodo)
        {
            List<VerticeModel> nodosAdjacentes = BuscaNodosVizinhos(nodo);
            foreach (VerticeModel nodoAlvo in nodosAdjacentes)
            {
                if (BuscaMenorDistancia(nodoAlvo) > BuscaMenorDistancia(nodo)
                        + BuscaDistancia(nodo, nodoAlvo))
                {
                    distancia.Add(nodoAlvo, BuscaMenorDistancia(nodo)
                            + BuscaDistancia(nodo, nodoAlvo));
                    antecessores.Add(nodoAlvo, nodo);
                    nodosNaoResolvidos.Add(nodoAlvo);
                }
            }
        }

        private int BuscaDistancia(VerticeModel nodo, VerticeModel nodoAlvo)
        {
            foreach(ArestaModel aresta in arestas)
            {
                if (aresta.Origem.Equals(nodo)
                        && aresta.Destino.Equals(nodoAlvo))
                {
                    return aresta.Peso;
                }
            }
            throw new Exception("Should not happen");
        }

        private List<VerticeModel> BuscaNodosVizinhos(VerticeModel nodo)
        {
            List<VerticeModel> vizinhos = new List<VerticeModel>();
            foreach(ArestaModel aresta in arestas)
            {
                if (aresta.Origem.Equals(nodo)
                        && !Resolvido(aresta.Destino))
                {
                    vizinhos.Add(aresta.Destino);
                }
            }
            return vizinhos;
        }

        private VerticeModel BuscaMenor(HashSet<VerticeModel> vertices)
        {
            VerticeModel minimo = null;
            foreach(VerticeModel vertice in vertices)
            {
                if (minimo == null)
                {
                    minimo = vertice;
                }
                else
                {
                    if (BuscaMenorDistancia(vertice) < BuscaMenorDistancia(minimo))
                    {
                        minimo = vertice;
                    }
                }
            }
            return minimo;
        }

        private bool Resolvido(VerticeModel vertice)
        {
            return nodosResolvidos.Contains(vertice);
        }

        private int BuscaMenorDistancia(VerticeModel destino)
        {
            int d;
            if (distancia.TryGetValue(destino, out d))
            {
                return d;
            }
            else
            {
                return int.MaxValue;
            }
        }

        /*
         * This method returns the path from the source to the selected target and
         * NULL if no path exists
         */
        public List<VerticeModel> BuscaCaminho(VerticeModel nodoAlvo)
        {
            LinkedList<VerticeModel> caminho = new LinkedList<VerticeModel>();
            VerticeModel aux = nodoAlvo;

            // check if a path exists
            VerticeModel verticeRetornado = null;
            if (!antecessores.TryGetValue(aux, out verticeRetornado))
            {
                return null;
            }
            caminho.AddLast(aux);
            while (antecessores.TryGetValue(aux, out verticeRetornado))
            {
                aux = antecessores[aux];
                caminho.AddLast(aux);
            }

            // Put it into the correct order
            return caminho.Reverse().ToList();
        }
    }
}
