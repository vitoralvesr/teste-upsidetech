using DijkstraEngine.Algorithm;
using DijkstraEngine.Entities;
using DijkstraWebApi.Models;
using DijkstraWebApi.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DijkstraWebApi.Controllers
{
    [RoutePrefix("api/dijkstra")]
    public class DijkstraController : ApiController
    {
        [AcceptVerbs("POST")]
        [Route("execute")]
        public List<VerticeModel> ExecutarDijkstra(DijkstraModel input)
        {
            DijkstraUtil util = new DijkstraUtil();
            GrafoModel grafo = util.MontaGrafoQuestao8();
            Dijkstra dijkstra = new Dijkstra(grafo);

            // executa o algoritmo de Dijkstra
            dijkstra.Executa(grafo.Vertices[input.Origem]);

            // extrai o menor caminho
            List<VerticeModel> menorCaminho = dijkstra.BuscaCaminho(grafo.Vertices[input.Destino]);

            if (menorCaminho == null || menorCaminho.Count <= 0)
            {
                return null;
            }

            return menorCaminho;
        }

        [AcceptVerbs("GET")]
        [Route("question1/{caminho}")]
        public int PrimeiraQuestao(string caminho)
        {
            DijkstraUtil util = new DijkstraUtil();
            GrafoModel grafo = util.MontaGrafoQuestao1a5(caminho);
            Dijkstra dijkstra = new Dijkstra(grafo);

            return dijkstra.RetornaDistanciaTrajeto();

            // executa o algoritmo de Dijkstra
            //dijkstra.Executa(grafo.Vertices[input.Origem]);

            // extrai o menor caminho
            //List<VerticeModel> menorCaminho = dijkstra.BuscaCaminho(grafo.Vertices[input.Destino]);

            //if (menorCaminho == null || menorCaminho.Count <= 0)
            //{
            //    return null;
            //}

            //return menorCaminho;
        }
    }
}
