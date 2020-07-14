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
    public partial class Manufacturer_Form : Form
    {

        private Importer importer;

        public Manufacturer_Form(Importer imp)
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
            string data = importer.Test_GetManufacturerData(tbName.Text);
           
            tbData.Text = data;
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            Manufacturer manufacturer = new Manufacturer();
            manufacturer.Name = tbName.Text;
           importer.Test_CreateManufacturer(manufacturer);
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            Manufacturer manufacturer = new Manufacturer();
            manufacturer.Edit_Name = tbEditName.Text;
            manufacturer.State = cbState.Text;
            manufacturer.Name = tbName.Text;

            importer.Test_UpdateManufacturer(manufacturer);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            Manufacturer manufacturer = new Manufacturer();
            manufacturer.Name = tbName.Text;

            importer.Test_DeleteManufacturer(manufacturer);
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void BtnPromote_Click(object sender, EventArgs e)
        {
            Manufacturer manufacturer = new Manufacturer();
            manufacturer.State = cbState.Text;
            manufacturer.Name = tbName.Text;

            importer.Test_PromoteManufacturer(manufacturer);

        }
    }
}
