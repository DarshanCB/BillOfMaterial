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
    public partial class UploadFile_Form : Form
    {

        private Importer importer;

        public UploadFile_Form(Importer imp)
        {
            importer = imp;
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    tbFilename.Text = openFileDialog.FileName;

                }
            }
            
        }

        private void BtnUpload_Click(object sender, EventArgs e)
        {
            if (tbFilename.Text.Length > 0)
            {
                importer.StartFileUpload(tbFilename.Text);
            }
        }
    }
}
