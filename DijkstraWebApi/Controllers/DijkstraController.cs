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
using System.Web.Http.Cors;

namespace DijkstraWebApi.Controllers
{
    [RoutePrefix("api/dijkstra")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DijkstraController : ApiController
    {
        [AcceptVerbs("POST")]
        [Route("execute")]
        public CaminhoModel ExecutarDijkstra(DijkstraModel input)
        {
            DijkstraUtil util = new DijkstraUtil();
            GrafoModel grafo = util.MontaGrafo(null);
            Dijkstra dijkstra = new Dijkstra(grafo);

            // executa o algoritmo de Dijkstra
            dijkstra.Executa(grafo.Vertices[input.Origem]);

            // extrai o menor caminho
            List<VerticeModel> menorCaminho = dijkstra.BuscaCaminho(grafo.Vertices[input.Destino]);

            if (menorCaminho == null || menorCaminho.Count <= 0)
            {
                return null;
            }

            //monta string para buscar a distancia do percurso
            string caminho = "";
            foreach (VerticeModel vertice in menorCaminho)
            {
                caminho += "_" + vertice.ToString().Replace("Nodo_", "");
            }
            grafo = util.MontaGrafo(caminho.Substring(1));
            dijkstra = new Dijkstra(grafo);

            int distanciaPercurso = dijkstra.RetornaDistanciaTrajeto();

            return new CaminhoModel(menorCaminho, distanciaPercurso);
        }

        [AcceptVerbs("GET")]
        [Route("questions/{caminho}")]
        public int PrimeiraQuestao(string caminho)
        {
            DijkstraUtil util = new DijkstraUtil();
            GrafoModel grafo = util.MontaGrafo(caminho);
            Dijkstra dijkstra = new Dijkstra(grafo);

            return dijkstra.RetornaDistanciaTrajeto();
        }
    }
}
