using DijkstraEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DijkstraWebApi.Models
{
    [DataContract]
    public class CaminhoModel
    {
        [DataMember]
        private List<VerticeModel> caminho;
        [DataMember]
        private int distancia;

        public CaminhoModel(List<VerticeModel> caminho, int distancia)
        {
            this.Caminho = caminho;
            this.Distancia = distancia;
        }

        public List<VerticeModel> Caminho
        {
            get
            {
                return caminho;
            }
            set
            {
                caminho = value;
            }
        }

        public int Distancia
        {
            get
            {
                return distancia;
            }
            set
            {
                distancia = value;
            }
        }
    }
}