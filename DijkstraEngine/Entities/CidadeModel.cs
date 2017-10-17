using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DijkstraEngine.Entities
{
    public class CidadeModel
    {
        private string codigo;

        public CidadeModel(string codigo)
        {
            this.Codigo = codigo;
        }

        public string Codigo
        {
            get
            {
                return codigo;
            }
            set
            {
                codigo = value;
            }
        }
    }
}