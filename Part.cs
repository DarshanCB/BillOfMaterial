using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOM_Importer_V2
{
    public class SubPart
    {
        public string ItemNumber;
        public int quantity;
    }

    public class ManufacturerPartName
    {
        public string ItemNumber;
        public string Manufacturer;
    }

    public class Part: ItemType
    {

        List<SubPart> sub_parts = new List<SubPart>();

        List<ManufacturerPartName> manufacturerParts = new List<ManufacturerPartName>();

        public string ItemNumber { get; set; }

        public string Description { get; set; }

        public bool HasManufacturerParts()
        {
            bool bRet = false; ;
            if (manufacturerParts.Count > 0)
            {
                bRet = true;
            }

            return bRet;
        }

        public bool HasSubParts()
        {
            bool bRet = false; ;
            if (sub_parts.Count>0)
            {
                bRet = true;
            }

            return bRet;
        }

        public int GetNumberOfManufacturerParts()
        {
            return manufacturerParts.Count;
        }

        public int GetNumberOfSubParts()
        {
            return sub_parts.Count;
        }

        public bool ManufacturerPartExists(string newItemNumber)
        {
            bool ret = false;

            ret = manufacturerParts.Exists(x => x.ItemNumber == newItemNumber);

            return ret;
        }

        public bool SubPartExists(string newItemNumber)
        {
            bool ret = false;

            ret = sub_parts.Exists(x => x.ItemNumber == newItemNumber);

            return ret;
        }

        public void AddManufacturerPart(ManufacturerPartName manufacturerPart)
        {
            if (!ManufacturerPartExists(manufacturerPart.ItemNumber))
            {
                manufacturerParts.Add(manufacturerPart);
                Log.Write("manufacturer part " + manufacturerPart.ItemNumber + " from " + manufacturerPart.Manufacturer + " added to " + ItemNumber);
            }
            else
            {
                Log.Write("manufacturer part " + manufacturerPart.ItemNumber + " from " + manufacturerPart.Manufacturer + " already exists in " + ItemNumber);
            }
        }

        public void AddSubPart(SubPart subPart)
        {
            if (!SubPartExists(subPart.ItemNumber))
            {
                sub_parts.Add(subPart);
                Log.Write("sub part " + subPart.ItemNumber + " added to " + ItemNumber);
            }
            else
            {
                Log.Write("sub part " + subPart.ItemNumber + " already exists in " + ItemNumber);
            }
        }
        
        public string GetManufacturerPartItemNumber ( int idx)
        {
            string sRet = "";
            if (idx< manufacturerParts.Count)
            {
                sRet = manufacturerParts[idx].ItemNumber;
            }

            return sRet;
        }

        public string GetManufacturerName(int idx)
        {
            string sRet = "";
            if (idx < manufacturerParts.Count)
            {
                sRet = manufacturerParts[idx].Manufacturer;
            }

            return sRet;
        }

		public ManufacturerPartName GetManufacturerPartName (int idx)
		{
			ManufacturerPartName oRet = null;
            if (idx< manufacturerParts.Count)
            {
                oRet = manufacturerParts[idx];
            }

            return oRet;
		}

        public string GetSubPartItemNumber(int idx)
        {
            string sRet = "";
            if (idx < sub_parts.Count)
            {
                sRet = sub_parts[idx].ItemNumber;
            }

            return sRet;
        }

        public int GetSubPartQuantity(int idx)
        {
            int nRet = 0;
            if (idx < sub_parts.Count)
            {
                nRet = sub_parts[idx].quantity;
            }

            return nRet;
        }
        
        public SubPart GetSubPart(int idx)
        {
            SubPart oRet = null;
            if (idx < sub_parts.Count)
            {
                oRet = sub_parts[idx];
            }

            return oRet;       
        }
    }
}
