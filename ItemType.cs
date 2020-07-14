using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOM_Importer_V2
{
    public class ItemType : IEquatable<Part>
    {
        public string State { get; set; }

        public string Name { get; set; }

        public int Id { get; set; }

        public string Aras_ID { get; set; }

        public string Unit { get; set; }

        public string Make_Buy { get; set; }

        public override string ToString()
        {
            return "ID: " +Id + "   Name: " + Name;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Part objAsPart = obj as Part;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return Id;
        }
        public bool Equals(Part other)
        {
            if (other == null) return false;
            return (this.Id.Equals(other.Id));
        }

    }
}
