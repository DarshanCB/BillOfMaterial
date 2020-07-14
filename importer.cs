
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.IO;

namespace BOM_Importer_V2
{
    public class ImportOptions
    {
        public bool WithManufacturers;
        public bool WithManufacturerParts;
        public bool WithParts;
        public bool WithBOM;
    }

    public class Importer
    {

        private BOM BOM2import = null;
        protected Main parentForm = null;

        private ArasInterface ArasIfc = null;
        private ImportOptions importOptions = null;

        private string innovatorUrl = "";
        private string innovatorUsername = "";
        private string innovatorPassword = "";
        private string innovatorDatabase = "";

        public Importer()
        {
            innovatorUrl = Properties.Settings.Default.ARAS_URL.ToString();
            innovatorUsername = Properties.Settings.Default.ARAS_DB_Username.ToString();
            innovatorDatabase = Properties.Settings.Default.ARAS_DB_Name.ToString();

        }


        public int NoOfNewManufacturer = 0;
        public int NoOfUpdatedManufacturer = 0;
        public int NoOfErrorManufacturer = 0;
        public int NoOfNewManufacturerParts = 0;
        public int NoOfUpdatedManufacturerParts = 0;
        public int NoOfErrorManufacturerParts = 0;
        public int NoOfNewParts = 0;
        public int NoOfUpdatedParts = 0;
        public int NoOfErrorParts = 0;
        public int NoOfNewBOM = 0;
        public int NoOfUpdatedBOM = 0;
        public int NoOfErrorBOM = 0;


        public void Start(Main parent, BOM bom, ImportOptions options)
        {
            Log.Write("start import");
            BOM2import = bom;
            parentForm = parent;

            if (ArasIfc == null)
            {
                ArasIfc = new ArasInterface();
            }
            importOptions = options;

            parentForm.StartProgressbar(0, 100);

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                DoImport();
            }).Start();
        }


        private void DoImport()
        {
            Log.Write("thread started");
            parentForm.UpdateProgressbar(5);

            NoOfNewManufacturer = 0;
            NoOfUpdatedManufacturer = 0;
            NoOfErrorManufacturer = 0;
            NoOfNewManufacturerParts = 0;
            NoOfUpdatedManufacturerParts = 0;
            NoOfErrorManufacturerParts = 0;
            NoOfNewParts = 0;
            NoOfUpdatedParts = 0;
            NoOfErrorParts = 0;
            NoOfNewBOM = 0;
            NoOfUpdatedBOM = 0;
            NoOfErrorBOM = 0;

            StartImport();
        }

       


        private void StartImport()
        {
            string token = ArasIfc.GetToken();

            if (token.Length > 0)
            {
                parentForm.UpdateProgressbar(10);
                parentForm.UpdateStatistic();

                if (importOptions.WithManufacturers)
                {
                    //test only; not needed
                    GetListOfManufacturers(token);

                    ImportManufacturers(token);
                }
                else
                {
                    Log.Write("Import of Manufacturers skipped");
                }
                parentForm.UpdateProgressbar(30);
                parentForm.UpdateStatistic();

                if (importOptions.WithManufacturerParts)
                {
                    //test only; not needed
                    GetListOfManufacturerParts(token);

                    ImportManufacturerParts(token);
                }
                else
                {
                    Log.Write("Import of ManufacturerParts skipped");
                }
                parentForm.UpdateProgressbar(60);
                parentForm.UpdateStatistic();

                if (importOptions.WithParts)
                {
                    //test only; not needed
                    GetListOfParts(token);

                    ImportParts(token);

                }
                else
                {
                    Log.Write("Import of Parts skipped");
                }
                parentForm.UpdateProgressbar(80);
                parentForm.UpdateStatistic();

                if (importOptions.WithBOM)
                {
                    ImportBOM(token);
                }

                parentForm.UpdateProgressbar(100);
                parentForm.UpdateStatistic();

                parentForm.UpdateStatusLabel("import done.");

            }
            else
            {
                Log.Write("no token, import skipped completely");
                parentForm.UpdateStatusLabel("import skipped");
            }
        }

        #region Manufacturer

      
        private string GetManufacturerName(string token, string ARAS_id)
        {
            string manufacturer = "";
            HttpResponseMessage response = ArasIfc.GET_Response("Manufacturer Part('" + ARAS_id + "')?$expand=manufacturer", token);

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                var jsonObj = JObject.Parse(result);

                string sTemp = (string)jsonObj["manufacturer"].ToString();
                if (sTemp.Length > 0)
                {
                    manufacturer = (string)jsonObj["manufacturer"]["name"];
                }
                else
                {
                    Log.Write("!!! manufacturer part with ID " + ARAS_id + " has no manufacturer");
                }
            }
            else
            {
                Log.Write("error in query - Status: " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
            }

            return manufacturer;
        }


        public static string GetManufacturer_id(string manufacturer, string response)
        {
            var result = JObject.Parse(response);
            var req_value = (JArray)result["value"];
            ManufacturerPart manufacturerPart = new ManufacturerPart();


            foreach (var value in req_value)
            {
                string list_manufacturer_id = (string)value["id"];
                string list_manufacturer = (string)value["name"];
                if (manufacturer == list_manufacturer)
                {
                    manufacturerPart.Aras_ID = list_manufacturer_id;
                    break;

                }

            }

            return manufacturerPart.Aras_ID;
        }

        private void GetListOfManufacturers(string token)
        {
            Log.Write("get list of manufacturers");
            HttpResponseMessage response = ArasIfc.GET_Response("Manufacturer", token);

            if (response.IsSuccessStatusCode)
            {
                Log.Write(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Log.Write("error - Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
            }

            Log.Write("list done");
        }


        private void ImportManufacturers(string token)
        {

            for (int m = 0; m < BOM2import.GetNumberOfManufacturers(); m++)
            {
                Manufacturer manufacturer = BOM2import.GetManufacturer(m);

                if (manufacturer != null)
                {
                    Log.Write("import " + manufacturer.Name);

                    if (ManufacturerExists(manufacturer, token))
                    {
                        //just update
                        UpdateManufacturer(manufacturer, token);
                    }
                    else
                    {
                        //create new manufacturer
                        CreateManufacturer(manufacturer, token);
                    }
                }
                else
                {
                    Log.Write("import manufacturer " + m + " not possible");
                }
            }

        }

        public void UpdateManufacturer(Manufacturer manufacturer, string token)
        {
            /*
            "created_on": "2019-10-10T08:01:06",
            "generation": "1",
            "id": "360ECC7BDFCE4DAC96D6E0433C104524",
            "is_current": "1",
            "is_released": "0",
            "keyed_name": "Samsung",
            "major_rev": "A",
            "modified_on": "2019-10-10T08:01:06",
            "new_version": "1",
            "not_lockable": "0",
            "state": "Preliminary",
            "name": "Samsung"
            */


            var items = new Dictionary<string, string>();
            items.Add("name", manufacturer.Edit_Name);
            items.Add("state",manufacturer.State);

            HttpResponseMessage manufact_response = ArasIfc.GET_Response("Manufacturer", token);
            string manufResult = manufact_response.Content.ReadAsStringAsync().Result;
            manufacturer.Aras_ID = Importer.GetManufacturer_id(manufacturer.Name, manufResult);

            items.Add("manufacturer", manufacturer.Aras_ID);

            if (manufacturer.Aras_ID != null && manufacturer.Aras_ID.Length > 0)
            {
                HttpResponseMessage response = ArasIfc.PATCH_Response(items, "Manufacturer('" + manufacturer.Aras_ID + "')", token);

                if (response.IsSuccessStatusCode)
                {
                    Log.Write(manufacturer.Name + " updated");
                    NoOfUpdatedManufacturer++;
                }
                else
                {
                    Log.Write("error updating " + manufacturer.Name + " Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
                    NoOfErrorManufacturer++;
                }
            }
            else
            {
                Log.Write("can not update manufacturer, unknown Aras ID for " + manufacturer.Name);
                NoOfErrorManufacturer++;
            }
        }


        public void PromoteManufacturer(Manufacturer manufacturer, string token)
        {
           

            var items = new Dictionary<string, string>();
            items.Add("state", manufacturer.State);

            HttpResponseMessage manufact_response = ArasIfc.GET_Response("Manufacturer", token);
            string manufResult = manufact_response.Content.ReadAsStringAsync().Result;
            manufacturer.Aras_ID = Importer.GetManufacturer_id(manufacturer.Name, manufResult);

            items.Add("@aras.id", manufacturer.Aras_ID);
            items.Add("@odata.type", innovatorUrl + "server/odata/$metadata#Manufacturer");

            if (manufacturer.Aras_ID != null && manufacturer.Aras_ID.Length > 0)
            {
                HttpResponseMessage response = ArasIfc.POST_Response(items, "method.PromoteItem", token);

                if (response.IsSuccessStatusCode)
                {
                    Log.Write(manufacturer.Name + " Promote Manufacturer is successful");
                    NoOfUpdatedManufacturer++;
                }
                else
                {
                    Log.Write("error in Promote manufacturer " + manufacturer.Name + " Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
                    NoOfErrorManufacturer++;
                }
            }
            else
            {
                Log.Write("can not promote manufacturer, unknown Aras ID for " + manufacturer.Name);
                NoOfErrorManufacturer++;
            }
        }

       


        public void CreateManufacturer(Manufacturer manufacturer, string token)
        {
            /*
            "created_on": "2019-10-10T08:01:06",
            "generation": "1",
            "id": "360ECC7BDFCE4DAC96D6E0433C104524",
            "is_current": "1",
            "is_released": "0",
            "keyed_name": "Samsung",
            "major_rev": "A",
            "modified_on": "2019-10-10T08:01:06",
            "new_version": "1",
            "not_lockable": "0",
            "state": "Preliminary",
            "name": "Samsung"
            */
            string sRet = "";
            var items = new Dictionary<string, string>();
            string result = "";
            string newName = manufacturer.Name.Replace("&", "\u0026");

            items.Add("name", newName);
            items.Add("state", "Released");

            HttpResponseMessage response = ArasIfc.POST_Response(items, "Manufacturer", token);

            if (response.IsSuccessStatusCode)
            {
                sRet = manufacturer.Name + " created";
                Log.Write(sRet);

                result = response.Content.ReadAsStringAsync().Result;

                var jsonObj = JObject.Parse(result);
                string id = (string)jsonObj["id"];

                sRet += jsonObj.ToString();

                manufacturer.Aras_ID = id;
                NoOfNewManufacturer++;
            }
            else
            {
                sRet = "error creating " + manufacturer.Name + " Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result;
                Log.Write(sRet);

                NoOfErrorManufacturer++;
            }

          
        }

        private void DeleteManufacturer(Manufacturer manufacturer, string token)
        {

            HttpResponseMessage response = ArasIfc.GET_Response("Manufacturer?$filter=name eq '" + manufacturer.Name + "'", token);

            if (response.IsSuccessStatusCode)
            {
                Log.Write(manufacturer.Name + " got data, now deleting");

                //get id and store it in object
                string result = response.Content.ReadAsStringAsync().Result;

                var jsonObj = JObject.Parse(result);
                var values = (JArray)jsonObj["value"];

                if (values.Count == 0)
                {
                    Log.Write(manufacturer + " does not exist");

                }
                else if (values.Count > 0)
                {
                    //get id and store it in object
                    string id = "";
                    foreach (var item_number in values)
                    {
                        id = (string)item_number["id"];

                        response = ArasIfc.DELETE_Response("Manufacturer('" + id + "')", token);

                        if (response.IsSuccessStatusCode)
                        {
                            Log.Write(manufacturer + " deleted");

                        }
                        else
                        {
                            Log.Write("error deleting " + manufacturer + " Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
                        }

                    }
                }
            }
            else
            {
                Log.Write("error deleting " + manufacturer + " Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
            }
        }


        private void DeleteAllManufacturer(string token)
        {
            parentForm.UpdateStatusLabel("deleting manufacturer, please wait");
            parentForm.StopProgressbar();
            //get list of all manufacturer
            HttpResponseMessage response = ArasIfc.GET_Response("Manufacturer", token);

            int cntDeleted = 0;
            int cntNotDeleted = 0;



            if (response.IsSuccessStatusCode)
            {
                Log.Write(" got data, now deleting all manufacturer");

                //get id and store it in object
                string result = response.Content.ReadAsStringAsync().Result;

                var jsonObj = JObject.Parse(result);
                var values = (JArray)jsonObj["value"];

                if (values.Count == 0)
                {
                    Log.Write(" does not exist");
                    parentForm.UpdateStatusLabel("done, nothing to delete");

                }
                else if (values.Count > 0)
                {
                    parentForm.StartProgressbar(0, values.Count);
                    int cnt = 0;

                    foreach (var item_number in values)
                    {
                        string id = (string)item_number["id"];
                        string name = (string)item_number["name"];

                        response = ArasIfc.DELETE_Response("Manufacturer('" + id + "')", token);
                        cnt++;
                        parentForm.UpdateProgressbar(cnt);

                        if (response.IsSuccessStatusCode)
                        {
                            Log.Write(name + " deleted");
                            cntDeleted++;

                        }
                        else
                        {
                            Log.Write("error deleting " + name + " Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
                            cntNotDeleted++;
                        }

                    }

                    parentForm.UpdateStatusLabel(cntDeleted + " deleted / " + cntNotDeleted + " not deleted");
                }
            }
            else
            {
                Log.Write("error deleting  Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
                parentForm.UpdateStatusLabel("done");
            }
        }






        private bool ManufacturerExists(Manufacturer manufacturer, string token)
        {
            bool ret = false;

            //mit StartsWith prüfen, wenn Sonderzeichen enthalten
            //Herstellername is unique but can include special characters
            //string newName = manufacturer.Name.Replace("&", "\u0026");


            HttpResponseMessage response;
            char[] NonSupportedChars = { '&', '#' };

            if (manufacturer.Name.IndexOfAny(NonSupportedChars) > 0)
            {
                response = ArasIfc.GET_Response("Manufacturer?$filter=startswith(name, '" + manufacturer.Name.Substring(0, manufacturer.Name.IndexOfAny(NonSupportedChars)) + "')", token);
            }
            else
            {
                response = ArasIfc.GET_Response("Manufacturer?$filter=name eq '" + manufacturer.Name + "'", token);
            }
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                var jsonObj = JObject.Parse(result);
                var values = (JArray)jsonObj["value"];

                if (values.Count == 0)
                {
                    Log.Write(manufacturer.Name + " does not exist");

                }
                if (values.Count > 0)
                {
                    //get id and store it in object
                    string id = "";
                    foreach (var item_number in values)
                    {
                        id = (string)item_number["id"];
                        string name = (string)item_number["name"];

                        if (name == manufacturer.Name)
                        {
                            //auf Übereinstimmung prüfen wegen StartsWith
                            manufacturer.Aras_ID = id;
                            
                            Log.Write(manufacturer.Name + " already exist " + id);
                            ret = true;
                        }
                        else
                        {
                            Log.Write(manufacturer.Name + " does not exist, found only  " + name);
                            ret = false;
                        }
                    }
                }
            }
            else
            {
                Log.Write(manufacturer.Name + " does not exist - Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
            }
            return ret;
        }


        #endregion

        #region Parts

        public static string GetPart_id(string item_number)
        {
            ArasInterface ArasIfc = new ArasInterface();
            HttpResponseMessage response = ArasIfc.GET_Response("Part?$filter=item_number eq '" + item_number + "'", ArasIfc.GetToken());


            string result = response.Content.ReadAsStringAsync().Result;
            var req = JObject.Parse(result);
            var rt = (JArray)req["value"];
            Part Part_value = new Part();



            foreach (var value in rt)
            {
                Part_value.Aras_ID = (string)value["id"];

            }

            return Part_value.Aras_ID;

        }

        public void PromotePart(Part part, string token)
        {


            var items = new Dictionary<string, string>();
            items.Add("state", part.State);
            items.Add("@aras.id", part.Aras_ID);
            items.Add("@odata.type", innovatorUrl + "server/odata/$metadata#Part");





            if (part.Aras_ID != null && part.Aras_ID.Length > 0)
            {
                HttpResponseMessage response = ArasIfc.POST_Response(items, "method.PromoteItem", token);

                if (response.IsSuccessStatusCode)
                {
                    Log.Write(part.ItemNumber + " Promote part is successful");
                    NoOfUpdatedParts++;
                }
                else
                {
                    Log.Write("error in promoting part " + part.Name + " Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
                    NoOfErrorParts++;
                }
            }
            else
            {
                Log.Write("can not promote part, unknown Aras ID for " + part.ItemNumber);
                NoOfErrorParts++;
            }
        }

        private void GetListOfParts(string token)
        {
            Log.Write("get list of parts");
            HttpResponseMessage response = ArasIfc.GET_Response("Part", token);

            if (response.IsSuccessStatusCode)
            {
                Log.Write(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Log.Write("error - Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
            }

            Log.Write("list done");
        }


        private void ImportParts(string token)
        {

            for (int m = 0; m < BOM2import.GetNumberOfParts(); m++)
            {
                Part part = BOM2import.GetPartByIdx(m);

                if (part != null)
                {
                    string sLog = "import " + part.ItemNumber + " " + part.Name;
                    if (part.HasSubParts() || part.HasManufacturerParts())
                    {
                        sLog += " with " + part.GetNumberOfSubParts() + " sub parts and " + part.GetNumberOfManufacturerParts() + " relations to manufacturer parts";
                    }

                    Log.Write(sLog);

                    if (PartExists(part, token))
                    {
                        //just update
                        UpdatePart(part, token);

                        if (part.HasManufacturerParts())
                        {
                            //to do: update relation to manufacturer part
                            Log.Write("to do: update relation to manufacturer parts ");
                        }
                    }
                    else
                    {
                        //create new part
                        CreatePart(part, token);


                        if (part.HasManufacturerParts())
                        {
                            Log.Write("part " + part.ItemNumber + " has " + part.GetNumberOfManufacturerParts() + " manufacturer parts");
                            for (int i = 0; i < part.GetNumberOfManufacturerParts(); i++)
                            {
                                //get the item number
                                //string manufacturePartItemNumber = part.GetManufacturerPartItemNumber(i);
                                //string manufacturerName = part.GetManufacturerName(i);

                                ManufacturerPartName manufacturerPartName = part.GetManufacturerPartName(i);

                                //get the object based on item number
                                ManufacturerPart manufacturerPart = BOM2import.GetManufacturerPart(manufacturerPartName.ItemNumber, manufacturerPartName.Manufacturer);
                                CreateRelationPart2ManufacturerPart(part, manufacturerPart, token);
                            }
                        }
                    }
                }
                else
                {
                    Log.Write("import Part " + m + "not possible");
                }
            }
        }

        private void ImportBOM(string token)
        {
            for (int m = 0; m < BOM2import.GetNumberOfParts(); m++)
            {
                Part part = BOM2import.GetPartByIdx(m);

                if (part != null && part.HasSubParts())
                {

                    Log.Write("part " + part.ItemNumber + " has " + part.GetNumberOfSubParts() + " sub parts");
                    for (int i = 0; i < part.GetNumberOfSubParts(); i++)
                    {
                        //get the item number
                        //string subPartItemNumber = part.GetSubPartItemNumber(i);
                        //int quantity = part.GetSubPartQuantity(i);

                        SubPart subPartName = part.GetSubPart(i);

                        //get the object 
                        Part subPart = BOM2import.GetPart(subPartName.ItemNumber);
                        CreateRelationPart2SubPart(part, subPart, subPartName.quantity, token);
                    }
                }
            }
        }

        public string CreatePart(Part part, string token)
        {
            /*
            "created_on": "2019-11-08T09:02:29",
            "description": "Kondensator 100n 10% 0603 // SMD / 50V / X7R // Multilayer Ceramic Capacitor // -55°C bis +125°C",
            "generation": "14",
            "has_change_pending": "0",
            "id": "CE9AF4D3518944EA8773B248243D6E9C",
            "is_current": "1",
            "is_released": "0",
            "keyed_name": "101115",
            "major_rev": "B",
            "make_buy": "Make",
            "modified_on": "2019-12-09T14:27:47",
            "name": "Kondensator 100n 10% 0603 ",
            "new_version": "0",
            "not_lockable": "0",
            "state": "Preliminary",
            "unit": "EA",
            "item_number": "101115"
            */

            string result = "";
            var items = new Dictionary<string, string>();
            items.Add("item_number", part.ItemNumber);
            items.Add("name", part.Name);
            items.Add("description", part.Description);
            items.Add("make_buy", part.Make_Buy);
            items.Add("unit", part.Unit);

            //always released
            items.Add("state", part.State);


            HttpResponseMessage response = ArasIfc.POST_Response(items, "Part", token);

            if (response.IsSuccessStatusCode)
            {
                Log.Write(part.ItemNumber + " created part");

                result = Test_GetPartData(part.ItemNumber, true);

                NoOfNewParts++;

            }
            else
            {
                Log.Write("error creating " + part.Name + " Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
                NoOfErrorParts++;
            }

            return result;
        }

        public void UpdatePart(Part part, string token)
        {
            /*
            "created_on": "2019-11-08T09:02:29",
            "description": "Kondensator 100n 10% 0603 // SMD / 50V / X7R // Multilayer Ceramic Capacitor // -55°C bis +125°C",
            "generation": "14",
            "has_change_pending": "0",
            "id": "CE9AF4D3518944EA8773B248243D6E9C",
            "is_current": "1",
            "is_released": "0",
            "keyed_name": "101115",
            "major_rev": "B",
            "make_buy": "Make",
            "modified_on": "2019-12-09T14:27:47",
            "name": "Kondensator 100n 10% 0603 ",
            "new_version": "0",
            "not_lockable": "0",
            "state": "Preliminary",
            "unit": "EA",
            "item_number": "101115"
            */

           
            part.Aras_ID = Importer.GetPart_id(part.ItemNumber);


            var items = new Dictionary<string, string>();
            items.Add("name", part.Name);
            items.Add("description", part.Description);
            items.Add("make_buy", part.Make_Buy);
            items.Add("unit", part.Unit);

            
           


            if (part.Aras_ID != null && part.Aras_ID.Length > 0)
            {
                HttpResponseMessage response = ArasIfc.PATCH_Response(items, "Part('" + part.Aras_ID + "')", token);

                if (response.IsSuccessStatusCode)
                {
                    Log.Write(part.ItemNumber + " updated part");
                    NoOfUpdatedParts++;
                }
                else
                {
                    Log.Write("error updating " + part.Name + " part Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
                    NoOfErrorParts++;
                }
            }
            else
            {
                Log.Write("can not update part, unknown Aras ID for " + part.ItemNumber);
                NoOfErrorParts++;
            }
        }

        private bool PartExists(Part part, string token)
        {
            bool ret = false;

            //item_number must be unique for parts
            HttpResponseMessage response = ArasIfc.GET_Response("Part?$filter=item_number eq '" + part.ItemNumber + "'", token);

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                var jsonObj = JObject.Parse(result);
                var values = (JArray)jsonObj["value"];

                if (values.Count == 0)
                {
                    Log.Write(part.ItemNumber + " " + part.Name + " does not exist");
                }
                else if (values.Count > 0)
                {
                    //get id and store it in object
                    string id = "";
                    string itemnumber = "";
                    foreach (var item_number in values)
                    {
                        id = (string)item_number["id"];
                        itemnumber = (string)item_number["item_number"];

                        //check if it is the right one
                        if (itemnumber == part.ItemNumber)
                        {

                            part.Aras_ID = id;

                            Log.Write(part.ItemNumber + " " + part.Name + " already exist " + id);
                            ret = true;
                        }
                    }

                    //just one log!!
                    if (!ret)
                    {
                        Log.Write(part.ItemNumber + " " + part.Name + " found something else ");
                    }

                }
            }
            else
            {
                Log.Write(part.ItemNumber + " " + part.Name + " does not exist - Status: " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
            }

            return ret;
        }

        private void DeletePart(Part Part, string token)
        {

            HttpResponseMessage response = ArasIfc.GET_Response("Part?$filter=Item_number eq '" + Part.ItemNumber + "'", token);

            if (response.IsSuccessStatusCode)
            {
                Log.Write(Part.Name + " got data, now deleting");


                //get id and store it in object
                string result = response.Content.ReadAsStringAsync().Result;

                var jsonObj = JObject.Parse(result);
                var values = (JArray)jsonObj["value"];

                if (values.Count == 0)
                {
                    Log.Write(Part.Name + " does not exist");

                }
                else if (values.Count > 0)
                {
                    //get id and store it in object
                    string id = "";
                    foreach (var item_number in values)
                    {
                        id = (string)item_number["id"];

                        response = ArasIfc.DELETE_Response("Part('" + id + "')", token);

                        if (response.IsSuccessStatusCode)
                        {

                            Log.Write(Part.Name + " deleted");

                        }
                        else
                        {
                            Log.Write("error deleting " + Part.Name + " Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
                        }

                    }


                }
            }
            else
            {
                Log.Write("error deleting " + Part.Name + " Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
            }


        }

        private void DeleteAllParts(string token)
        {
            parentForm.UpdateStatusLabel("deleting parts, please wait");
            parentForm.StopProgressbar();

            //get list of all parts
            HttpResponseMessage response = ArasIfc.GET_Response("Part", token);
            int cntDeleted = 0;
            int cntNotDeleted = 0;



            if (response.IsSuccessStatusCode)
            {
                Log.Write(" got data, now deleting all parts");

                //get id and store it in object
                string result = response.Content.ReadAsStringAsync().Result;

                var jsonObj = JObject.Parse(result);
                var values = (JArray)jsonObj["value"];

                if (values.Count == 0)
                {
                    Log.Write(" does not exist");
                    parentForm.UpdateStatusLabel("done, nothing to delete");

                }
                else if (values.Count > 0)
                {
                    parentForm.StartProgressbar(0, values.Count);
                    int cnt = 0;

                    foreach (var item_number in values)
                    {
                        string id = (string)item_number["id"];
                        string name = (string)item_number["item_number"];

                        response = ArasIfc.DELETE_Response("Part('" + id + "')", token);

                        cnt++;
                        parentForm.UpdateProgressbar(cnt);

                        if (response.IsSuccessStatusCode)
                        {
                            Log.Write(name + " deleted");
                            cntDeleted++;


                        }
                        else
                        {
                            Log.Write("error deleting " + name + " Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
                            cntNotDeleted++;
                        }

                    }
                    parentForm.UpdateStatusLabel(cntDeleted + " deleted / " + cntNotDeleted + " not deleted");
                }
            }
            else
            {
                Log.Write("error deleting  Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
                parentForm.UpdateStatusLabel("done");
            }
        }

        #endregion

        #region ManufacturerParts

        private void GetListOfManufacturerParts(string token)
        {
            Log.Write("get list of manufacturer parts");
            HttpResponseMessage response = ArasIfc.GET_Response("Manufacturer Part", token);

            if (response.IsSuccessStatusCode)
            {
                Log.Write(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Log.Write("error - Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
            }

            Log.Write("list done");
        }


        private void ImportManufacturerParts(string token)
        {

            for (int m = 0; m < BOM2import.GetNumberOfManufacturerParts(); m++)
            {
                ManufacturerPart manufacturerPart = BOM2import.GetManufacturerPart(m);

                if (manufacturerPart != null)
                {
                    Log.Write("import " + manufacturerPart.ItemNumber + " " + manufacturerPart.Name);

                    if (ManufacturerPartExists(manufacturerPart, token))
                    {
                        //just update
                        UpdateManufacturerPart(manufacturerPart, token);

                    }
                    else
                    {
                        //create new manufacturer
                        CreateManufacturerPart(manufacturerPart, token);
                    }
                }
                else
                {
                    Log.Write("import manufacturer Part " + m + "not possible");
                }
            }
        }

        private string CheckState(string org)
        {
            //to do: states definition...
            string sRet = "";
            if (org != null && org.Contains("zugelassen"))
            {
                sRet = "Released";
            }
            else if (org != null && org.Contains("abgelehnt"))
            {
                sRet = "Superseded";
            }
            else if (org != null && org.Contains("gesperrt"))
            {
                sRet = "Superseded";
            }
            else if (org != null && org.Contains("abgek�ndigt"))
            {
                sRet = "Superseded";
            }
            else if (org != null && org.Contains("Referenz"))
            {
                sRet = "Released";
            }
            else if (org != null && org.Contains("limitiert"))
            {
                sRet = "Released";
            }
            else
            {
                Log.Write("!?! unknown state for manufacturer part " + org);
            }

            return sRet;
        }

        

        public static string GetManufacturerPart_id(string item_number)
        {
            ArasInterface ArasIfc = new ArasInterface();
            HttpResponseMessage response = ArasIfc.GET_Response("Manufacturer Part?$filter=item_number eq '" + item_number + "'", ArasIfc.GetToken());

           
                string result = response.Content.ReadAsStringAsync().Result;
                var req = JObject.Parse(result);
                var req_value = (JArray)req["value"];
                ManufacturerPart manufacturerPart = new ManufacturerPart();



                foreach (var value in req_value)
                {
                    manufacturerPart.Aras_ID = (string)value["id"];
                    string list_manufacturer = (string)value["name"];

                }

                return manufacturerPart.Aras_ID;
            
        }





        public string CreateManufacturerPart(ManufacturerPart manufacturerPart, string token)
        {
            

            string getmanupart_result = "";
            HttpResponseMessage manufact_response = ArasIfc.GET_Response("Manufacturer", token);
            string manufResult = manufact_response.Content.ReadAsStringAsync().Result;
            

            var items = new Dictionary<string, string>();
            items.Add("item_number", manufacturerPart.ItemNumber);
            items.Add("name", manufacturerPart.Name);
            items.Add("description", manufacturerPart.Description);
            items.Add("state", CheckState(manufacturerPart.State));
            items.Add("manufacturer", Importer.GetManufacturer_id(manufacturerPart.Manufacturer,manufResult));

            HttpResponseMessage response = ArasIfc.POST_Response(items, "Manufacturer Part", token);

            if (response.IsSuccessStatusCode)
            {
                Log.Write(manufacturerPart.Name + " created");

                 getmanupart_result = Test_GetManufacturerPartData(manufacturerPart.ItemNumber);

                //get id and store it in object
                string result = response.Content.ReadAsStringAsync().Result;

                var jsonObj = JObject.Parse(result);
                string id = (string)jsonObj["id"];

                manufacturerPart.Aras_ID = id;
                

                NoOfNewManufacturerParts++;
            }
            else
            {
                Log.Write("error creating " + manufacturerPart.Name + " Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
                NoOfErrorManufacturerParts++;
            }
            return getmanupart_result;
        }

        public void UpdateManufacturerPart(ManufacturerPart manufacturerPart, string token)
        {

            string getmanupart_result = "";
            manufacturerPart.Aras_ID = Importer.GetManufacturerPart_id(manufacturerPart.ItemNumber);

            var items = new Dictionary<string, string>();
            items.Add("name", manufacturerPart.Name);
            items.Add("description", manufacturerPart.Description);
            //items.Add("state", CheckState(manufacturerPart.State));
            //items.Add("manufacturer", manufacturerPart.Aras_ID);

            if (manufacturerPart.Aras_ID != null && manufacturerPart.Aras_ID.Length > 0)
            {
           
                HttpResponseMessage response = ArasIfc.PATCH_Response(items, "Manufacturer Part('" + manufacturerPart.Aras_ID + "')", token);

                if (response.IsSuccessStatusCode)
                {
                    Log.Write(manufacturerPart.Name + " updated manufacturer part");
                    NoOfUpdatedManufacturerParts++;
                }
                else
                {
                    Log.Write("error updating " + manufacturerPart.Name + " manufacturer part Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
                    NoOfErrorManufacturerParts++;
                }
            }
            else
            {
                Log.Write("can not update manufacturer part, unknown Aras ID for " + manufacturerPart.ItemNumber);
                NoOfErrorManufacturerParts++;
            }
        }


        private void DeleteManufacturerPart(ManufacturerPart manufacturer_Part, string token)
        {
            
            HttpResponseMessage response = ArasIfc.GET_Response("Manufacturer Part?$filter=Item_number eq '" + manufacturer_Part.ItemNumber + "'", token);

            if (response.IsSuccessStatusCode)
            {
                Log.Write(manufacturer_Part.Name + " got data, now deleting");
                

                //get id and store it in object
                string result = response.Content.ReadAsStringAsync().Result;

                var jsonObj = JObject.Parse(result);
                var values = (JArray)jsonObj["value"];

                if (values.Count == 0)
                {
                    Log.Write(manufacturer_Part.Name + " does not exist");

                }
                else if (values.Count > 0)
                {
                    //get id and store it in object
                    string id = "";
                    foreach (var item_number in values)
                    {
                        id = (string)item_number["id"];

                        response = ArasIfc.DELETE_Response("Manufacturer Part('" + id + "')", token);

                        if (response.IsSuccessStatusCode)
                        {
                            
                            Log.Write(manufacturer_Part.Name + " deleted");

                        }
                        else
                        {
                            Log.Write("error deleting " + manufacturer_Part.Name + " Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
                        }

                    }

                   
                }
            }
            else
            {
                Log.Write("error deleting " + manufacturer_Part.Name + " Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
            }

          
        }

       
        private bool ManufacturerPartExists(ManufacturerPart manufacturerPart, string token)
        {
            bool ret = false;

            //to do: ItemNumber und Hersteller prüfen; PCB kann von unterschiedlichen Herstellern die gleiche ItemNumber habe
            //itemnumber are not unique!!
            HttpResponseMessage response;
            //mit StartsWith prüfen, wenn Sonderzeichen enthalten
            if (manufacturerPart.ItemNumber.IndexOf('#') > 0)
            {
                response = ArasIfc.GET_Response("Manufacturer Part?$filter=startswith(item_number, '" + manufacturerPart.ItemNumber.Substring(0, manufacturerPart.ItemNumber.IndexOf('#')) + "')", token);
            }
            else
            {
                response = ArasIfc.GET_Response("Manufacturer Part?$filter=item_number eq '" + manufacturerPart.ItemNumber + "'", token);
            }

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                var jsonObj = JObject.Parse(result);
                var values = (JArray)jsonObj["value"];

                

                if (values.Count == 0)
                {
                    Log.Write(manufacturerPart.ItemNumber + " " + manufacturerPart.Name + " does not exist");
                    ret = false;
                }
                else if (values.Count > 0)
                {
                    //get id and store it in object
                    string id ="";
                    string itemnumber = "";
                    foreach (var item_number in values)
                    {
                        id = (string)item_number["id"];
                        itemnumber = (string)item_number["item_number"];

                        if (itemnumber == manufacturerPart.ItemNumber)
                        {
                            //prüfen ob manufacturer passt... 
                            string manufacturer = GetManufacturerName(token, id);

                            if (manufacturer == manufacturerPart.Manufacturer)
                            {
                                manufacturerPart.Aras_ID = id;
                                Log.Write(manufacturerPart.ItemNumber + " " + manufacturerPart.Name + " already exist " + id);
                                ret = true;
                            }
                            else
                            {
                                Log.Write(manufacturerPart.ItemNumber + " " + manufacturerPart.Name + " found from differnet manufacturer " + manufacturer);
                            }
                        }
                    }

                    //just one log!!
                    if (!ret)
                    {
                        Log.Write(manufacturerPart.ItemNumber + " " + manufacturerPart.Name + " found something else ");
                    }

                }
            }
            else
            {
                Log.Write(manufacturerPart.ItemNumber + " " + manufacturerPart.Name + " does not exist - Status: " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
            }

            return ret;
        }

        private void DeleteAllManufacturerParts(string token)
        {
            parentForm.UpdateStatusLabel("deleting manufacturer parts, please wait");
            parentForm.StopProgressbar();

            //get list of all manufacturer parts
            HttpResponseMessage response = ArasIfc.GET_Response("Manufacturer Part", token);
            int cntDeleted = 0;
            int cntNotDeleted = 0;



            if (response.IsSuccessStatusCode)
            {
                Log.Write(" got data, now deleting all manufacturer parts");

                //get id and store it in object
                string result = response.Content.ReadAsStringAsync().Result;

                var jsonObj = JObject.Parse(result);
                var values = (JArray)jsonObj["value"];

                if (values.Count == 0)
                {
                    Log.Write(" does not exist");
                    parentForm.UpdateStatusLabel("done, nothing to delete");

                }
                else if (values.Count > 0)
                {
                    parentForm.StartProgressbar(0, values.Count);
                    int cnt = 0;

                    foreach (var item_number in values)
                    {
                        string id = (string)item_number["id"];
                        string name = (string)item_number["item_number"];

                        response = ArasIfc.DELETE_Response("Manufacturer Part('" + id + "')", token);
                        cnt++;
                        parentForm.UpdateProgressbar(cnt);

                        if (response.IsSuccessStatusCode)
                        {
                            Log.Write(name + " deleted");
                            cntDeleted++;

                        }
                        else
                        {
                            Log.Write("error deleting " + name + " Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
                            cntNotDeleted++;
                        }

                    }
                    parentForm.UpdateStatusLabel(cntDeleted + " deleted / " + cntNotDeleted + " not deleted");
                }
            }
            else
            {
                Log.Write("error deleting  Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
                parentForm.UpdateStatusLabel("done");
            }
        }

        #endregion


        #region PART_AML

        // to get relation from part: 
        // ID ist part ID
        //http://pdm-test.elcon-system.de/Innovator11/server/odata/Part('DE5A35EDC0A24E5188CAEB3122B6607B')/Part AML

        // to get relation details
        //http://pdm-test.elcon-system.de/Innovator11/server/odata/Part AML('1C86ACE4DFA04C69B52E203E76864FDB')

        // to get manufacturer part
        //http://pdm-test.elcon-system.de/Innovator11/server/odata/Part AML('1C86ACE4DFA04C69B52E203E76864FDB')?$expand=related_id

        // to get source
        //http://pdm-test.elcon-system.de/Innovator11/server/odata/Part AML('1C86ACE4DFA04C69B52E203E76864FDB')?$expand=source_id

        private void CreateRelationPart2ManufacturerPart(Part part, ManufacturerPart manufacturerPart, string token)
        {

            if (part != null && part.Aras_ID != null && manufacturerPart != null && manufacturerPart.Aras_ID != null)
            {

                var items = new Dictionary<string, string>();
                items.Add("source_id", part.Aras_ID);
                items.Add("related_id", manufacturerPart.Aras_ID);

                HttpResponseMessage response = ArasIfc.POST_Response(items, "Part AML", token);

                if (response.IsSuccessStatusCode)
                {
                    Log.Write("relation " + part.ItemNumber + " to " + manufacturerPart.ItemNumber + " created");
                }
                else
                {
                    Log.Write("error creating relation " + part.ItemNumber + " to " + manufacturerPart.ItemNumber + " Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
                }
            }
            else
            {
                string stemp = "";
                if (part == null)
                {
                    stemp += " part ";
                }
                if (part != null && part.Aras_ID == null)
                {
                    stemp += " part Aras ID ";
                }
                if (manufacturerPart == null)
                {
                    stemp += " manufacturerPart ";
                }
                if (manufacturerPart != null && manufacturerPart.Aras_ID == null)
                {
                    stemp += " manufacturerPart Aras ID ";
                }

                Log.Write("error creating relation " + (part!=null ? part.ItemNumber : " unknown part ") + " to " + (manufacturerPart!= null ? manufacturerPart.ItemNumber : " unknown manufacturer part ") + stemp + "  is null ");
            }
        }


        #endregion //PART_AML

        #region PART_BOM

        // to get relation from part: 
        // http://pdm-test.elcon-system.de/Innovator11/server/odata/Part?$filter=item_number eq 900453

        //http://pdm-test.elcon-system.de/Innovator11/server/odata/Part('E4939FD7D1C7435F9F9EBBF59A3B167E')


        // ID ist part ID
        //http://pdm-test.elcon-system.de/Innovator11/server/odata/Part('E4939FD7D1C7435F9F9EBBF59A3B167E')/Part BOM

        // to get relation details
        //http://pdm-test.elcon-system.de/Innovator11/server/odata/Part BOM('FD96572F01D24F37B20E844D8B2FA99B')

        // to get part
        //http://pdm-test.elcon-system.de/Innovator11/server/odata/Part BOM('FD96572F01D24F37B20E844D8B2FA99B')?$expand=related_id

        // to get source
        //http://pdm-test.elcon-system.de/Innovator11/server/odata/Part BOM('FD96572F01D24F37B20E844D8B2FA99B')?$expand=source_id

        private void CreateRelationPart2SubPart(Part part, Part subPart, int quantity, string token)
        {

            if (part != null && part.Aras_ID != null && subPart != null && subPart.Aras_ID != null)
            {

                var items = new Dictionary<string, string>();
                items.Add("source_id", part.Aras_ID);
                items.Add("related_id", subPart.Aras_ID);
                items.Add("quantity", quantity.ToString());

                HttpResponseMessage response = ArasIfc.POST_Response(items, "Part BOM", token);

                if (response.IsSuccessStatusCode)
                {
                    Log.Write("relation " + part.ItemNumber + " to " + subPart.ItemNumber + " created");
                    NoOfNewBOM++;
                }
                else
                {
                    Log.Write("error creating relation " + part.ItemNumber + " to " + subPart.ItemNumber + " Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
                    NoOfErrorBOM++;
                }
            }
            else
            {
                string stemp = "";
                if (part == null)
                {
                    stemp += " part ";
                }
                if (part != null && part.Aras_ID == null)
                {
                    stemp += " part Aras ID ";
                }
                if (subPart == null)
                {
                    stemp += " subPart ";
                }
                if (subPart != null && subPart.Aras_ID == null)
                {
                    stemp += " subPart Aras ID ";
                }

                Log.Write("error creating relation " + part.ItemNumber + " to " + subPart.ItemNumber + stemp + "  is null ");
            }
        }


        #endregion //PART_BOM



        //=================================================================================================================
        //
        //
        //           Test only
        //
        //
        //




        #region TEST_ONLY

        public string Test_CreatePart(Part part)
        {
            Log.Write("test only: creating manufacturer " + part.Name);
            string status = "";
            string data = string.Empty;

            if (ArasIfc == null)
            {
                ArasIfc = new ArasInterface();
            }


            string token = ArasIfc.GetToken();

         

            if (token.Length > 0)
            {
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    data = CreatePart(part, token);
                }).Start();
                

            }

            return data;


        }

        public void Test_CreateManufacturerPart(ManufacturerPart manufacturerpart)
        {
            Log.Write("test only: creating manufacturer " + manufacturerpart.Name);
            string status = "";

            if (ArasIfc == null)
            {
                ArasIfc = new ArasInterface();
            }


            string token = ArasIfc.GetToken();

            if (token.Length > 0)
            {
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    CreateManufacturerPart(manufacturerpart, token);
                }).Start();

            }



        }


        public string Test_GetManufacturerData(string name)
        {
            Log.Write("test only: get manufacturer data");

            string sRet = "";
            if (ArasIfc == null)
            {
                ArasIfc = new ArasInterface();
            }

            string token = ArasIfc.GetToken();

            if (token.Length > 0)
            {
                HttpResponseMessage response = ArasIfc.GET_Response("Manufacturer?$filter=name eq '" + name + "'", token);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;

                    var jsonObj = JObject.Parse(result);
                    var values = (JArray)jsonObj["value"];

                    if (values.Count == 0)
                    {
                        Log.Write(name + " does not exist");
                        sRet = name + " does not exist";
                    }
                    if (values.Count == 1)
                    {
                        sRet = name + " exist ";
                        
                        foreach (var item_number in values)
                        {
                            sRet += item_number.ToString();
                        }

                        Log.Write(name + " exist");
                        
                    }
                    if (values.Count > 1)
                    {
                        sRet = "!!!! " + name + " exists " + values.Count;
                        foreach (var item_number in values)
                        {
                            sRet += item_number.ToString();
                        }

                        Log.Write("!!!! " + name + " exists " + values.Count);
                       
                    }

                }
                else
                {
                    Log.Write(name + " does not exist - Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
                    sRet = name + " does not exist - Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result;

                }
            }


           
            return sRet;
        }

        public string Test_GetManufacturerPartData(string name)
        {
            Log.Write("test only: get manufacturer part data");

            string sRet = "";
            if (ArasIfc == null)
            {
                ArasIfc = new ArasInterface();
            }

            string token = ArasIfc.GetToken();

            if (token.Length > 0)
            {
                HttpResponseMessage response = ArasIfc.GET_Response("Manufacturer Part?$filter=item_number eq '" + name + "'", token);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;


                    var jsonObj = JObject.Parse(result);
                    var values = (JArray)jsonObj["value"];

                    if (values.Count == 0)
                    {
                        Log.Write(name + " does not exist");
                        sRet = name + " does not exist";
                    }
                    if (values.Count > 0)
                    {
                        if (values.Count > 1)
                        {
                            sRet = "!!!! " + name + " exists " + values.Count;
                        }
                        else
                        {
                            sRet = name + " exist ";
                        }

                        string id = "";
                        foreach (var item_number in values)
                        {
                            id = (string)item_number["id"];
                        }

                       

                        Log.Write("test only: got data; get detailed data " + id);
                        response = ArasIfc.GET_Response("Manufacturer Part('" + id + "')?$expand=manufacturer", token);

                        if (response.IsSuccessStatusCode)
                        {
                            result = response.Content.ReadAsStringAsync().Result;

                            jsonObj = JObject.Parse(result);

                            sRet += jsonObj.ToString();

                            Log.Write(name + " exist");
                        }
                        else
                        {
                            Log.Write(name + " error - Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
                        }

                    }
                }
                else
                {
                    Log.Write(name + " does not exist - Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
                    sRet = name + " does not exist - Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result;

                }
            }

            return sRet;
        }

        public string Test_GetPartData(string name, bool WithManufacturerPart)
        {
            Log.Write("test only: get part data");

            string sRet = "";
            if (ArasIfc == null)
            {
                ArasIfc = new ArasInterface();
            }

            string token = ArasIfc.GetToken();

            if (token.Length > 0)
            {
                HttpResponseMessage response = ArasIfc.GET_Response("Part?$filter=item_number eq '" + name + "'", token);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;

                    var jsonObj = JObject.Parse(result);
                    var values = (JArray)jsonObj["value"];

                    if (values.Count == 0)
                    {
                        Log.Write(name + " does not exist");
                        sRet = name + " does not exist";
                    }
                    if (values.Count > 0)
                    {
                        if (values.Count > 1)
                        {
                            sRet = "!!!! " + name + " exists " + values.Count;
                        }
                        else
                        {

                            sRet = name + " exist ";
                        }

                      

                        if (!WithManufacturerPart)
                        {
                            foreach (var item_number in values)
                            {
                                sRet += item_number.ToString();
                            }

                        }
                        else
                        {

                            string id = "";
                            foreach (var item_number in values)
                            {
                                id = (string)item_number["id"];
                            }

                            Log.Write("test only: got data; get detailed data " + id);
                            //response = ArasIfc.GET_Response("Part('" + id + "')/PART AML?$expand=related_id", token);
                            response = ArasIfc.GET_Response("Part('" + id + "')", token);

                            if (response.IsSuccessStatusCode)
                            {
                                result = response.Content.ReadAsStringAsync().Result;

                                jsonObj = JObject.Parse(result);
                                sRet += jsonObj.ToString();

                                Log.Write(name + " exist");
                            }
                            else
                            {
                                Log.Write(name + " error - Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
                            }
                        }
                    }                    
                }
                else
                {
                    Log.Write(name + " does not exist - Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
                    sRet = name + " does not exist - Status " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result;

                }
            }

            return sRet;
        }

        public void Test_CreateManufacturer(Manufacturer manufacturer)
        {
            Log.Write("test only: creating manufacturer " + manufacturer.Name);
            string status = "";

            if (ArasIfc == null)
            {
                ArasIfc = new ArasInterface();
            }
            

            string token = ArasIfc.GetToken();

            if (token.Length > 0)
            {

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                   CreateManufacturer(manufacturer, token);
                }).Start();

            }

          

        }

        public void Test_DeleteManufacturer(Manufacturer manufacturer)
        {
            Log.Write("test only: deleting manufacturer " + manufacturer.Name);



            if (ArasIfc == null)
            {
                ArasIfc = new ArasInterface();
            }


            string token = ArasIfc.GetToken();

            if (token.Length > 0)
            {
      
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    DeleteManufacturer(manufacturer, token);
                }).Start();

            }
        }

        public void Test_DeleteAllManufacturer(Main parent, ImportOptions options)
        {
            Log.Write("test only: deleting all manufacturer");



            parentForm = parent;

            
            if (ArasIfc == null)
            {
                ArasIfc = new ArasInterface();
            }
            importOptions = options;

            string token = ArasIfc.GetToken();

            if (token.Length > 0)
            {
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    DeleteAllManufacturer( token);
                }).Start();

            }
        }

        public void Test_DeleteManufacturerPart(ManufacturerPart manufacturer_Part)
        {
            Log.Write("test only: deleting manufacturer " + manufacturer_Part.Manufacturer);
            


            if (ArasIfc == null)
            {
                ArasIfc = new ArasInterface();
            }


            string token = ArasIfc.GetToken();

            if (token.Length > 0)
            { 
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    DeleteManufacturerPart(manufacturer_Part, token);
                }).Start();

            }

            
        }

        public void Test_UpdateManufacturerPart(ManufacturerPart manufacturerPart)
        {
            Log.Write("test only: update manufacturer part" + manufacturerPart.Name);



            if (ArasIfc == null)
            {
                ArasIfc = new ArasInterface();
            }


            string token = ArasIfc.GetToken();

            if (token.Length > 0)
            {


                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    UpdateManufacturerPart(manufacturerPart, token);
                }).Start();

            }
        }

        public void Test_UpdateManufacturer(Manufacturer manufacturer)
        {
            Log.Write("test only: update manufacturer " + manufacturer.Name);



            if (ArasIfc == null)
            {
                ArasIfc = new ArasInterface();
            }


            string token = ArasIfc.GetToken();

            if (token.Length > 0)
            {


                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    UpdateManufacturer(manufacturer, token);
                }).Start();

            }
        }

        public void Test_PromoteManufacturer(Manufacturer manufacturer)
        {
            Log.Write("test only: promote manufacturer " + manufacturer.Name);



            if (ArasIfc == null)
            {
                ArasIfc = new ArasInterface();
            }


            string token = ArasIfc.GetToken();

            if (token.Length > 0)
            {


                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    PromoteManufacturer(manufacturer, token);
                }).Start();

            }
        }


        public void Test_PromotePart(Part part)
        {
            Log.Write("test only: promote part " + part.ItemNumber);



            if (ArasIfc == null)
            {
                ArasIfc = new ArasInterface();
            }


            string token = ArasIfc.GetToken();

            if (token.Length > 0)
            {


                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    PromotePart(part, token);
                }).Start();

            }
        }


        public void Test_UpdatePart(Part part)
        {
            Log.Write("test only: update part " + part.ItemNumber);



            if (ArasIfc == null)
            {
                ArasIfc = new ArasInterface();
            }


            string token = ArasIfc.GetToken();

            if (token.Length > 0)
            {


                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    UpdatePart(part, token);
                }).Start();

            }
        }



        public void Test_DeleteAllManufacturerParts(Main parent, ImportOptions options)
        {
            Log.Write("test only: deleting all manufacturer parts");

            parentForm = parent;

            if (ArasIfc == null)
            {
                ArasIfc = new ArasInterface();
            }
            importOptions = options;

            string token = ArasIfc.GetToken();

            if (token.Length > 0)
            {
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    DeleteAllManufacturerParts(token);
                }).Start();

            }
        }

        public void Test_DeleteAllParts(Main parent, ImportOptions options)
        {
            Log.Write("test only: deleting all parts");

            parentForm = parent;

            if (ArasIfc == null)
            {
                ArasIfc = new ArasInterface();
            }
            importOptions = options;

            string token = ArasIfc.GetToken();

            if (token.Length > 0)
            {
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    DeleteAllParts(token);
                }).Start();

            }
        }

        public void Test_DeletePart(Part Part)
        {
            Log.Write("test only: deleting part " + Part.ItemNumber);



            if (ArasIfc == null)
            {
                ArasIfc = new ArasInterface();
            }


            string token = ArasIfc.GetToken();

            if (token.Length > 0)
            {
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    DeletePart(Part, token);
                }).Start();

            }


        }

        #endregion



        #region filehandling

        public void StartFileUpload (string filename)
        {
            Log.Write("start upload " + filename );
            if (ArasIfc == null)
            {
                ArasIfc = new ArasInterface();
            }

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                DoFileUpload(filename);
            }).Start();

        }


        private void DoFileUpload(string filename)
        {
            Log.Write("upload thread started");
            string token = ArasIfc.GetToken();

            if (token.Length > 0)
            {
                //get transaction id
                string transactionId= GetTransactionID(token);


                UploadFile(token,transactionId, filename);

            }
        }



        private string GetTransactionID(string token)
        {

            HttpResponseMessage response = ArasIfc.POST_Response(null, "vault.BeginTransaction", token, "vault");

            string id = "";
            if (response.IsSuccessStatusCode)
            {
                //get id and store it in object
                string result = response.Content.ReadAsStringAsync().Result;

                var jsonObj = JObject.Parse(result);
                id = (string)jsonObj["transactionId"];

                Log.Write("got transaction ID " + id);
            }
            else
            {
                Log.Write(" error getting transaction ID " + response.StatusCode + " " + response.Content.ReadAsStringAsync().Result);
            }

            return id;
        }

        private void UploadFile(string token, string transactionid, string filename)
        {
            //we need a guid
            string guid = generateNewGuid();
            guid = guid.Replace("-", "");


            //open file
            if (File.Exists(filename))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
                {

                    FileInfo fi = new FileInfo(filename);
                    long filesize = fi.Length;

                    //additional header info are needed
                    Dictionary<string, string> additionalHeaders = new Dictionary<string, string>();

                    additionalHeaders.Add("transactionid", transactionid);
                    additionalHeaders.Add("Content-Range", "bytes 0-" + (filesize - 1) + "/" + filesize);
                    additionalHeaders.Add("Content-Disposition", "attachment; filename*=utf-8''" + fi.Name);


                    //will be added later
                    //additionalHeaders.Add("Content-Type", "application/octet-stream");

                    HttpResponseMessage response = ArasIfc.POST_Response(null, "vault.UploadFile?fileID=" + guid, token, "vault", additionalHeaders, "octet-stream");


                    if (response.IsSuccessStatusCode)
                    {
                        Log.Write("file upload successfully");
                    }

                    reader.Close();
                }
            }
            else
            {
                Log.Write("file does not exist " + filename);
            }
        }

        //
        // Returns a new 32 character GUID we can use as an Aras Item id
        //

        private string generateNewGuid()
        {
            
            System.Guid guid = System.Guid.NewGuid();

            return guid.ToString();
        }

        #endregion //filehandling

    }
}

    