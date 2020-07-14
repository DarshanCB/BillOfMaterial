using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BOM_Importer_V2
{
    public partial class Part_Form : Form
    {

        private Importer importer;

        public Part_Form(Importer imp)
        {
            importer = imp;
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnGetData_Click(object sender, EventArgs e)
        {
            string data = importer.Test_GetPartData(tbItemNumber.Text, cbWithManufacturerPart.Checked);


            //to do parsen per json


            if (!data.Contains("does not exist"))
            {
                int nStart = data.IndexOf("description") + 4 + 11;
                int nEnd = data.IndexOf("\",", nStart);
                if (nStart >= 0 && nEnd >= 0 && nEnd > nStart)
                {
                    tbDescription.Text = data.Substring(nStart, nEnd - nStart);
                }
                int nTemp = data.IndexOf("modified_on");

                nStart = data.IndexOf("name", nTemp) + 4 + 4;
                nEnd = data.IndexOf("\",", nStart);
                if (nStart >= 0 && nEnd >= 0 && nEnd > nStart)
                {
                    tbName.Text = data.Substring(nStart, nEnd - nStart);
                }
                nTemp = data.IndexOf("not_lockable");

                nStart = data.IndexOf("state", nTemp) + 4 + 5;
                nEnd = data.IndexOf(",", nStart);
                if (nStart >= 0 && nEnd >= 0 && nEnd > nStart)
                {
                    string stemp = data.Substring(nStart, nEnd - nStart);
                }
                nStart = data.IndexOf("id\"") + 4 + 2;
                nEnd = data.IndexOf("\",", nStart);
                if (nStart >= 0 && nEnd >= 0 && nEnd > nStart)
                {
                    tbID.Text = data.Substring(nStart, nEnd - nStart);
                }
            }
            /*
            900612 exist {
                "created_on": "2020-02-26T10:00:26",
  "current_state@aras.name": "Preliminary",
  "description": "BIG 2862 // 230VAC, ohne WANX Slot",
  "generation": 1,
  "has_change_pending": "0",
  "id": "AF87C6399EBE4AEC9314E9994704BDE2",
  "is_current": "1",
  "is_released": "0",
  "keyed_name": "900612",
  "major_rev": "A",
  "make_buy": "Buy",
  "modified_on": "2020-02-26T10:00:26",
  "name": "BIG 2862 ",
  "new_version": "0",
  "not_lockable": "0",
  "state": "Preliminary",
  "unit": "EA",
  "item_number": "900612",
  "itemtype": "4F1AC04A2B484F3ABA4E20DB63808A88"
}

    */



            tbData.Text = data;
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            
            Part Part_value = new Part();
            Part_value.ItemNumber = tbItemNumber.Text;
            Part_value.Name = tbName.Text;
            Part_value.Description = tbDescription.Text;
            Part_value.State = cbState.Text;
            Part_value.Unit = cbUnit.Text;
            Part_value.Make_Buy = cbMakeBuy.Text;

            importer.Test_UpdatePart(Part_value);
            
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
       
            Part Part_value = new Part();
            Part_value.ItemNumber = tbItemNumber.Text;
            Part_value.Name = tbName.Text;
            Part_value.Description = tbDescription.Text;
            Part_value.State = cbState.Text;
            Part_value.Unit = cbUnit.Text;
            Part_value.Make_Buy = cbMakeBuy.Text;
            
            string data = importer.Test_CreatePart(Part_value);

            tbData.Text = data;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            Part Part_value = new Part();
            Part_value.ItemNumber = tbItemNumber.Text;
            Part_value.Name = tbName.Text;
            Part_value.Description = tbDescription.Text;

            importer.Test_DeletePart(Part_value);
        }

        private void PromoteButton_Click(object sender, EventArgs e)
        {
            Part part = new Part();
            part.State = cbState.Text;
            part.ItemNumber = tbItemNumber.Text;
            part.Aras_ID = tbID.Text;
            importer.Test_PromotePart(part);
        }
    }
}
