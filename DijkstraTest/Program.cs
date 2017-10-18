using DijkstraEngine.Algorithm;
using DijkstraEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraTest
{
    class Program
    {
        public static List<VerticeModel> nodos;
        public static List<ArestaModel> arestas;

        static void Main(string[] args)
        {
            nodos = new List<VerticeModel>();
            arestas = new List<ArestaModel>();
            for (int i = 0; i < 8; i++)
            {
                VerticeModel location = new VerticeModel("Node_" + i, "Node_" + i);
                nodos.Add(location);
            }

            addLane("Aresta_0", 0, 1, 5);
            addLane("Aresta_1", 0, 3, 5);
            addLane("Aresta_2", 0, 4, 7);
            addLane("Aresta_3", 1, 2, 4);
            addLane("Aresta_4", 2, 3, 8);
            addLane("Aresta_5", 2, 4, 2);
            addLane("Aresta_6", 3, 2, 8);
            addLane("Aresta_7", 3, 4, 6);
            addLane("Aresta_8", 4, 1, 3);

            //        A distância da cidade A até a cidade B é 5.
            //        A distância da cidade A até a cidade D é 5.
            //        A distância da cidade A até a cidade E é 7.
            //        A distância da cidade B até a cidade C é 4.
            //        A distância da cidade C até a cidade D é 8.
            //        A distância da cidade C até a cidade E é 2.
            //        A distância da cidade D até a cidade C é 8.
            //        A distância da cidade D até a cidade E é 6.
            //        A distância da cidade E até a cidade B é 3.

            // Lets check from location Loc_1 to Loc_10
            GrafoModel graph = new GrafoModel(nodos, arestas);
            Dijkstra dijkstra = new Dijkstra(graph);
            dijkstra.Executa(nodos[0]);
            List<VerticeModel> path = dijkstra.BuscaCaminho(nodos[2]);

            if (path == null || path.Count <= 0)
            {
                throw new Exception();
            }
        }

        //adiciona um trajeto para ser inserido no Grafo. (ex.: A distância da cidade A (origemAresta) até a cidade B (destinoAresta) é 5 (duração))
        public static void AdicionaTrajeto(String id, int origemAresta, int destinoAresta,
                int duracao)
        {
            ArestaModel faixa = new ArestaModel(id, nodos[origemAresta], nodos[destinoAresta], duracao);
            arestas.Add(faixa);
        }
    }
}
