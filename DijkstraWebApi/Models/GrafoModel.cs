using System.Collections.Generic;

namespace DijkstraWebApi.Models
{
    public class GrafoModel
    {
        private List<VerticeModel> vertices;
        private List<ArestaModel> arestas;

        public GrafoModel(List<VerticeModel> vertices, List<ArestaModel> arestas)
        {
            this.vertices = vertices;
            this.arestas = arestas;
        }

        public List<VerticeModel> Vertices
        {
            get
            {
                return vertices;
            }
            set
            {
                vertices = value;
            }
        }

        public List<ArestaModel> Arestas
        {
            get
            {
                return arestas;
            }
            set
            {
                arestas = value;
            }
        }
    }
}