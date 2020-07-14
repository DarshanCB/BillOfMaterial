using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOM_Importer_V2
{
    public class ManufacturerPart : ItemType
    {
        public string ItemNumber { get; set; }


        public string Manufacturer { get; set; }
        public string Description { get; set; }

    }
}
