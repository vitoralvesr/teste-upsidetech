using System;
using System.Runtime.Serialization;

namespace DijkstraEngine.Entities
{
    [DataContract]
    public class VerticeModel
    {
        [DataMember]
        readonly private String id;
        [DataMember]
        readonly private String nome;
        
        public VerticeModel(String id, String nome)
        {
            this.id = id;
            this.nome = nome;
        }
        
        public override int GetHashCode()
        {
            const int _base = 31;
            int resultado = 1;
            resultado = _base * resultado + ((id == null) ? 0 : id.GetHashCode());
            return resultado;
        }

        public override bool Equals(Object obj)
        {
            if (this == obj)
                return true;
            if (obj == null)
                return false;
            if (!this.GetType().Equals(obj.GetType()))
                return false;
            VerticeModel outro = (VerticeModel)obj;
            if (id == null)
            {
                if (outro.id != null)
                    return false;
            }
            else if (!id.Equals(outro.id))
                return false;
            return true;
        }

        public override String ToString()
        {
            return nome;
        }
    }
}