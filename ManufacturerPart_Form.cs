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
    public partial class ManufacturerPart_Form : Form
    {

        private Importer importer;

        public ManufacturerPart_Form(Importer imp)
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
            string data = importer.Test_GetManufacturerPartData(tbItemNumber.Text);

            tbData.Text = data;
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
       
            ManufacturerPart manufacturerPart_value = new ManufacturerPart();
            manufacturerPart_value.ItemNumber = tbItemNumber.Text;
            manufacturerPart_value.Name = tbName.Text;
            manufacturerPart_value.Description = tbdescription.Text;
            manufacturerPart_value.Manufacturer = tbManufacturer.Text;

            importer.Test_UpdateManufacturerPart(manufacturerPart_value);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            ManufacturerPart manufacturerPart_value = new ManufacturerPart();
            manufacturerPart_value.ItemNumber = tbItemNumber.Text;
            manufacturerPart_value.Name = tbName.Text;
            manufacturerPart_value.Description = tbdescription.Text;
            manufacturerPart_value.Manufacturer = tbManufacturer.Text;

            importer.Test_DeleteManufacturerPart(manufacturerPart_value);

           
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            ManufacturerPart manufacturerPart_value = new ManufacturerPart();
            manufacturerPart_value.ItemNumber = tbItemNumber.Text;
            manufacturerPart_value.Name = tbName.Text;
            manufacturerPart_value.Description = tbdescription.Text;
            manufacturerPart_value.Manufacturer = tbManufacturer.Text;

            importer.Test_CreateManufacturerPart(manufacturerPart_value);

        }

      
    }
}
