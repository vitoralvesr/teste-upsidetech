using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DijkstraWebApi.Models
{
    public class DijkstraModel
    {
        private int origem;
        private int destino;

        public DijkstraModel(int codigo, int destino)
        {
            this.Origem = codigo;
            this.Destino = destino;
        }

        public int Origem
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

        public int Destino
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
    }
}