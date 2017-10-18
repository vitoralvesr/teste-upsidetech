using DijkstraEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DijkstraWebApi.Util
{
    public class DijkstraUtil
    {
        private List<VerticeModel> nodos;
        private List<ArestaModel> arestas;

        public GrafoModel MontaGrafoQuestao8()
        {
            nodos = new List<VerticeModel>();
            arestas = new List<ArestaModel>();

            //cria a lista de nodos do grafo (no caso do teste, 9 nodos)
            for (int i = 0; i < 8; i++)
            {
                VerticeModel location = new VerticeModel("Nodo_" + i, "Nodo_" + i);
                nodos.Add(location);
            }

            //adiciona as ligações possíveis (ex.: A --- 5 ---> B)
            AdicionaAresta("Trajeto_0", 0, 1, 5);
            AdicionaAresta("Trajeto_1", 0, 3, 5);
            AdicionaAresta("Trajeto_2", 0, 4, 7);
            AdicionaAresta("Trajeto_3", 1, 2, 4);
            AdicionaAresta("Trajeto_4", 2, 3, 8);
            AdicionaAresta("Trajeto_5", 2, 4, 2);
            AdicionaAresta("Trajeto_6", 3, 2, 8);
            AdicionaAresta("Trajeto_7", 3, 4, 6);
            AdicionaAresta("Trajeto_8", 4, 1, 3);

            // cria o Grafo com os Nodos e Arestas criados
            return new GrafoModel(nodos, arestas);
        }

        public GrafoModel MontaGrafoQuestao1a5(string caminho)
        {
            string[] caminhoArray = caminho.Split('_');
            nodos = new List<VerticeModel>();
            arestas = new List<ArestaModel>();

            //cria a lista de nodos do grafo
            for (int i = 0; i < caminhoArray.Length; i++)
            {
                VerticeModel location = new VerticeModel("Nodo_" + caminhoArray[i], "Nodo_" + caminhoArray[i]);
                nodos.Add(location);
            }

            //adiciona as ligações possíveis (ex.: A --- 5 ---> B)
            AdicionaAresta("Trajeto_0", 0, 1, 5);
            AdicionaAresta("Trajeto_1", 0, 3, 5);
            AdicionaAresta("Trajeto_2", 0, 4, 7);
            AdicionaAresta("Trajeto_3", 1, 2, 4);
            AdicionaAresta("Trajeto_4", 2, 3, 8);
            AdicionaAresta("Trajeto_5", 2, 4, 2);
            AdicionaAresta("Trajeto_6", 3, 2, 8);
            AdicionaAresta("Trajeto_7", 3, 4, 6);
            AdicionaAresta("Trajeto_8", 4, 1, 3);

            // cria o Grafo com os Nodos e Arestas criados
            return new GrafoModel(nodos, arestas);
        }

        //adiciona um trajeto para ser inserido no Grafo. (ex.: A distância da cidade A (origemAresta) até a cidade B (destinoAresta) é 5 (duração))
        private void AdicionaAresta(String id, int origemAresta, int destinoAresta, int duracao)
        {
            //verifica se há de fato um vertice representando a origem e outro representando o destino da aresta
            VerticeModel origem = new VerticeModel("Nodo_" + origemAresta, "Nodo_" + origemAresta);
            VerticeModel destino = new VerticeModel("Nodo_" + destinoAresta, "Nodo_" + destinoAresta);

            if (nodos.Contains(origem)
                && nodos.Contains(destino))
            {
                ArestaModel faixa = new ArestaModel(id, nodos[nodos.IndexOf(origem)], nodos[nodos.IndexOf(destino)], duracao);
                arestas.Add(faixa);
            }
        }
    }
}