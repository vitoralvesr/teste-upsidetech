using DijkstraEngine.Entities;
using DijkstraEngine.Exception;
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
            // cria uma copia dos arrays, assim podemos trabalhar neles sem alterar o Grafo
            this.nodo = new List<VerticeModel>(grafo.Vertices);
            this.arestas = new List<ArestaModel>(grafo.Arestas);
        }

        public int RetornaDistanciaTrajeto()
        {
            nodosResolvidos = new HashSet<VerticeModel>();
            nodosNaoResolvidos = new HashSet<VerticeModel>();

            int distancia = 0;
            for (int i = 0; i < nodo.Count - 1; i++)
            {
                List<VerticeModel> vizinhos = BuscaNodosVizinhos(nodo[i]);
                if (vizinhos.Exists(w => w.ToString().Equals(nodo[i + 1].ToString())))
                {
                    distancia += BuscaDistancia(nodo[i], nodo[i+1]);
                }
                else
                {
                    throw new PathNotFoundException("Rota não existente");
                }
            }
            return distancia;
        }

        public void Executa(VerticeModel origem)
        {
            nodosResolvidos = new HashSet<VerticeModel>();
            nodosNaoResolvidos = new HashSet<VerticeModel>();
            distancia = new Dictionary<VerticeModel, int>();
            antecessores = new Dictionary<VerticeModel, VerticeModel>();

            //cria uma distancia inicial 0, ja que origem até origem tem distancia = 0
            distancia.Add(origem, 0);

            //lista de nodos que ainda não foram testados
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
            //busca os nodos vizinhos (os quais o nodo principal tem acesso)
            List<VerticeModel> nodosAdjacentes = BuscaNodosVizinhos(nodo);

            foreach (VerticeModel nodoDestino in nodosAdjacentes)
            {
                if (BuscaMenorDistancia(nodoDestino) > BuscaMenorDistancia(nodo)
                        + BuscaDistancia(nodo, nodoDestino))
                {
                    distancia.Add(nodoDestino, BuscaMenorDistancia(nodo)
                            + BuscaDistancia(nodo, nodoDestino));
                    antecessores.Add(nodoDestino, nodo);
                    nodosNaoResolvidos.Add(nodoDestino);
                }
            }
        }

        private int BuscaDistancia(VerticeModel nodo, VerticeModel nodoDestino)
        {
            foreach(ArestaModel aresta in arestas)
            {
                if (aresta.Origem.Equals(nodo)
                        && aresta.Destino.Equals(nodoDestino))
                {
                    return aresta.Peso;
                }
            }
            throw new System.Exception();
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
            if (distancia.TryGetValue(destino, out int d))
            {
                return d;
            }
            else
            {
                return int.MaxValue;
            }
        }

        /*
         * Esse método retorna o caminho da origem até o destino ou Null caso o caminho seja impossível
         */
        public List<VerticeModel> BuscaCaminho(VerticeModel nodoDestino)
        {
            LinkedList<VerticeModel> caminho = new LinkedList<VerticeModel>();
            VerticeModel aux = nodoDestino;

            // checa se o caminho existe, verificando a exitencia do destino nos nodos encontrados
            if (!antecessores.TryGetValue(aux, out VerticeModel verticeRetornado))
            {
                return null;
            }
            caminho.AddLast(aux);
            while (antecessores.TryGetValue(aux, out verticeRetornado))
            {
                aux = antecessores[aux];
                caminho.AddLast(aux);
            }

            // coloca a lista na ordem correta
            return caminho.Reverse().ToList();
        }
    }
}
