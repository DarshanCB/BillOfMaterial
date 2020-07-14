using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOM_Importer_V2
{
    public class BOM
    {
        List <Manufacturer> Manufacturers = new List<Manufacturer>();
        List<ManufacturerPart> ManufacturerParts = new List<ManufacturerPart>();
        List<Part> Parts = new List<Part>();


        //here we get a string list 
        // Baugruppe; Index; Struktur; Pos; Teil; Teileart; Bezeichnung; Anzahl; Menge brutto; ME; Mengenfaktor; Verschn.faktor; Hersteller; HSTName; HST_Status; HST_Bez; HST_BestellNr

        const int manufacturerPos = 13;
        const int manufacturerPartPos = 16;
        const int partDescriptionPos = 6;
        const int PartNumberPos = 4;
        const int StatePos = 14;
        const int ParentPartPos = 0;
        const int PartCountInParentPos = 7;


        public void AnalyzeLine(string[] values)
        {
            try
            {
                string PartDescription = values[partDescriptionPos];
                string ManufacturerName = values[manufacturerPos];
                string ParentPart = values[ParentPartPos];

                int PartCountInParent = 0;
                if (values[PartCountInParentPos].Length > 0)
                {
                    Convert.ToInt32(values[PartCountInParentPos]);
                }

                if (!ManufacturerExists(ManufacturerName) && ManufacturerName.Length > 0)
                {
                    Log.Write("add new manufacturer " + ManufacturerName);
                    Manufacturer newManufacturer = new Manufacturer();
                    newManufacturer.Name = ManufacturerName;
                    newManufacturer.State = "released";

                    Manufacturers.Add(newManufacturer);
                }

                string ManufacturerPart = values[manufacturerPartPos];
                if (!ManufacturerPartExists(ManufacturerPart) && ManufacturerPart.Length > 0)
                {
                    Log.Write("add new manufacturer part " + ManufacturerPart);
                    ManufacturerPart newManufacturerPart = new ManufacturerPart();
                    newManufacturerPart.ItemNumber = ManufacturerPart;
                    newManufacturerPart.Name = PartDescription.Split('/')[0];
                    newManufacturerPart.Manufacturer = ManufacturerName;
                    newManufacturerPart.Description = PartDescription;
                    newManufacturerPart.State = values[StatePos];

                    ManufacturerParts.Add(newManufacturerPart);
                }


                string PartNumber = values[PartNumberPos];
                if (!PartExists(PartNumber) && PartNumber.Length > 0)
                {
                    Log.Write("add new part " + PartNumber);
                    Part newPart = new Part();

                    newPart.ItemNumber = PartNumber;
                    newPart.Description = PartDescription;
                    newPart.Name = PartDescription.Split('/')[0];

                    Parts.Add(newPart);

                    AddManufacturerPart(PartNumber, ManufacturerPart, ManufacturerName);

                    AddPartToParent(ParentPart, PartNumber, PartCountInParent);

                }
                else
                {
                    //just add another manufacturer part to part
                    AddManufacturerPart(PartNumber, ManufacturerPart, ManufacturerName);
                }
            }
            catch (Exception ex)
            {
                Log.Write("exception in AnalyzeLine " + ex.ToString());
            }
        }


        #region Parts
        public int GetNumberOfParts()
        {
            return Parts.Count;
        }

        public int GetNumberOfPartsWithSubParts()
        {
            int nRet = 0;

            nRet = Parts.Where(x => x.HasSubParts()).Count();

            return nRet;
        }

        private bool PartExists(string newItemNumber)
        {
            bool ret = false;

            ret = Parts.Exists(x => x.ItemNumber == newItemNumber);

            return ret;
        }

        public Part GetPartByIdx(int idx)
        {
            if (idx < Parts.Count)
            {
                return Parts[idx];
            }
            else
            {
                return null;
            }
        }

        public Part GetPart(string ItemNumber)
        {
            Part part = Parts.Find(x => x.ItemNumber.Equals(ItemNumber));

            if (part != null)
            {
                return part;
            }
            else
            {
                return null;
            }
        }


        private void AddPartToParent(string parentName, string itemNumber, int quantity)
        {
            if (PartExists(parentName))
            {
                Part parentPart = Parts.Find(x => x.ItemNumber.Equals(parentName));

                SubPart subPart = new SubPart();

                subPart.ItemNumber = itemNumber;
                subPart.quantity = quantity;

                parentPart.AddSubPart(subPart);


            }
            else
            {
                Log.Write("parent part " + parentName + " does not exist ");
            }
        }

        private void AddManufacturerPart( string itemNumber, string manufacturerPartItemNumber, string manufacturerName)
        {
            if (PartExists(itemNumber))
            {
                if (manufacturerPartItemNumber.Length > 0)
                {
                    Part part = Parts.Find(x => x.ItemNumber.Contains(itemNumber));

                    ManufacturerPartName manufacturerPart = new ManufacturerPartName();

                    manufacturerPart.ItemNumber = manufacturerPartItemNumber;
                    manufacturerPart.Manufacturer = manufacturerName;

                    part.AddManufacturerPart(manufacturerPart);
                }
                else
                {
                    Log.Write("manufacturer part ItemNumber too short ");
                }

            }
            else
            {
                Log.Write("part does not exist " + itemNumber);
            }
        }

        #endregion



        #region ManufacturerParts
        public int GetNumberOfManufacturerParts()
        {
            return ManufacturerParts.Count;
        }

        private bool ManufacturerPartExists(string newItemNumber)
        {
            bool ret = false;

            ret = ManufacturerParts.Exists(x => x.ItemNumber == newItemNumber);

            return ret;
        }

        public ManufacturerPart GetManufacturerPart(int idx)
        {
            if (idx < ManufacturerParts.Count)
            {
                return ManufacturerParts[idx];
            }
            else
            {
                return null;
            }
        }


        #endregion



        #region Manufacturers
        public int GetNumberOfManufacturers()
        {
            return Manufacturers.Count;
        }


        private bool ManufacturerExists(string newName)
        {
            bool ret = false;

            ret = Manufacturers.Exists(x => x.Name == newName);

            return ret;
        }

        public ManufacturerPart GetManufacturerPart(string ItemNumber, string manufacturerName)
        {
            //first all with ItemNumber
            List<ManufacturerPart> manufacturerPartFull = ManufacturerParts.FindAll(x => x.ItemNumber.Equals(ItemNumber));
            //second all with correct manufacturername
            List<ManufacturerPart> manufacturerPart = manufacturerPartFull.FindAll(x => x.Manufacturer.Equals(manufacturerName));

			if (manufacturerPart.Count>1) 
			{
				Log.Write("!!! " + manufacturerPart.Count + " manufacturer parts found for  " + ItemNumber + " and  " + manufacturerName);
			}


            if (manufacturerPart != null && manufacturerPart.Count==1)
            {
                return manufacturerPart[0];
            }
            else
            {
                return null;
            }
        }

        public Manufacturer GetManufacturer(int idx)
        {
            if (idx < Manufacturers.Count)
            {
                return Manufacturers[idx];
            }
            else
            {
                return null;
            }
        }

        public string GetManufacturerID(string name)
        {
            Manufacturer manufacturer = Manufacturers.Find(x => x.Name.Contains(name));

            if (manufacturer != null)
            {
                return manufacturer.Aras_ID;
            }
            else
            {
                return "";
            }
        }


        #endregion
    }
}
