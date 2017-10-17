using System;

namespace DijkstraWebApi.Models
{
    public class ArestaModel
    {
        private String id;
        private VerticeModel origem;
        private VerticeModel destino;
        private int peso;

        public ArestaModel(String id, VerticeModel origem, VerticeModel destino, int peso)
        {
            this.id = id;
            this.origem = origem;
            this.destino = destino;
            this.peso = peso;
        }

        public String Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public VerticeModel Destino
        {
            get
            {
                return destino;
            }
            set
            {
                destino = value;
            }
        }

        public VerticeModel Origem
        {
            get
            {
                return origem;
            }
            set
            {
                origem = value;
            }
        }

        public int Peso
        {
            get
            {
                return peso;
            }
            set
            {
                peso = value;
            }
        }

        public override String ToString()
        {
            return origem + " " + destino;
        }
    }
}