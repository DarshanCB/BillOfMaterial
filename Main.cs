using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;



namespace BOM_Importer_V2
{


    public partial class Main : Form
    {

        private int Progressbar_Max = 500;

        public Main()
        {
            InitializeComponent();

            btnImport.Enabled = false;
            progressBar.Visible = false;
            toolStripStatusLabel1.Text = "please open a BOM to start...";

            Log.Write("===== BOM Importer - Application started =====", false);
            Log.Write("Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }

        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //to do Use a option object and handover to form and get it back

            OptionsForm options = new OptionsForm();
            options.Show();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.Show();
        }

        private void HelpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HelpForm help = new HelpForm();
            help.Show();
        }

        private void OpenBOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenBOMFile();
        }


        #region READ_BOM

        private void OpenBOMFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "excel files (*.xlsx)|*.xlsx|csv files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    tbBOMFilename.Text = openFileDialog.FileName;

                    new Thread(() =>
                    {
                        Thread.CurrentThread.IsBackground = true;
                        AnalyseBOMFile(openFileDialog.FileName);
                    }).Start();

                    
                }
            }
            toolStripStatusLabel1.Text = "BOM file selected, ready for import";
        }

        private void AnalyseBOMFile(string filename)
        {

            Log.Write("BOM file selected " + filename);
            if (Path.GetExtension(filename) == ".csv")
            {
                AnalyseBOMFile_csv(filename);
            }
            else
            {
                AnalyseBOMFile_excel(filename);
            }

        }


        private void AnalyseBOMFile_excel(string filename)
        {
            toolStripStatusLabel1.Text = "excel not yet supported";
        }

        private BOM Bom2Import = new BOM();

        private void AnalyseBOMFile_csv(string filename)
        {
            toolStripStatusLabel1.Text = "analyzing BOM file ...";

            StartProgressbar(0, Progressbar_Max);
            int i = 0;
            using (StreamReader sr = new StreamReader(filename))
            {
                string currentLine;

                while ((currentLine = sr.ReadLine()) != null)
                {
                    //skip first line ?
                    if (!Properties.Settings.Default.SkipFirstLine || i > 0)
                    {
                        string[] values = null;

                        values = currentLine.Split(';');

                        Bom2Import.AnalyzeLine(values);
                    }
                    i++;
                    if (i > Progressbar_Max) i = Progressbar_Max;

                    UpdateProgressbar(i);
                }
            }
            StopProgressbar();

            UpdateStatistic();

        }

        private void BtnReadBOM_Click(object sender, EventArgs e)
        {
            OpenBOMFile();

            btnImport.Enabled = true;
        }

        #endregion


        #region IMPORT_BOM

        private Importer importer = new Importer();

        private void BtnImport_Click(object sender, EventArgs e)
        {
            
            toolStripStatusLabel1.Text = "importing BOM ...";

            ImportOptions options = new ImportOptions();
            options.WithManufacturers = cbManufacturers.Checked;
            options.WithManufacturerParts = cbManufacturerParts.Checked;
            options.WithParts = cbPart.Checked;
            options.WithBOM = cbBOM.Checked;

            importer.Start(this, Bom2Import, options);
        }

        #endregion

        #region ---------- Main Progressbar ----------
        public void StartProgressbar(int min, int max)
        {

            if (this.progressBar.InvokeRequired)
            {
                this.progressBar.Invoke((MethodInvoker)delegate ()
                {
                    this.progressBar.Visible = true;
                    this.progressBar.Minimum = min;
                    this.progressBar.Maximum = max;
                    this.progressBar.Value = min;
                }
                );
            }
            else
            {
                this.progressBar.Visible = true;
                this.progressBar.Minimum = min;
                this.progressBar.Maximum = max;
                this.progressBar.Value = min;
            }
        }

        public void StopProgressbar()
        {
            if (this.progressBar.InvokeRequired)
            {
                this.progressBar.Invoke((MethodInvoker)delegate ()
                {
                    this.progressBar.Visible = false;

                }
                );
            }
            else
            {
                this.progressBar.Visible = false;

            }
        }

        public void UpdateProgressbar(int val)
        {
            if (this.progressBar.InvokeRequired)
            {
                this.progressBar.Invoke((MethodInvoker)delegate ()
                {
                    this.progressBar.Value = val;

                }
                );
            }
            else
            {
                this.progressBar.Value = val;

            }
        }

        public void UpdateStatusLabel(string text)
        {
            this.toolStripStatusLabel1.Text = text;
        }


        public void UpdateStatistic()
        {

            if (this.tbNoOfManufacturers.InvokeRequired)
            {
                this.tbNoOfManufacturers.Invoke((MethodInvoker)delegate ()
                {
                    this.tbNoOfManufacturers.Text = Bom2Import.GetNumberOfManufacturers().ToString();
                }
                );
            }
            else
            {
                this.tbNoOfManufacturers.Text = Bom2Import.GetNumberOfManufacturers().ToString() ;
            }

            if (this.tbNoOfManufacturerParts.InvokeRequired)
            {
                this.tbNoOfManufacturerParts.Invoke((MethodInvoker)delegate ()
                {
                    this.tbNoOfManufacturerParts.Text = Bom2Import.GetNumberOfManufacturerParts().ToString() ;
                }
                );
            }
            else
            {
                this.tbNoOfManufacturerParts.Text = Bom2Import.GetNumberOfManufacturerParts().ToString() ;
            }

            if (this.tbNoOfParts.InvokeRequired)
            {
                this.tbNoOfParts.Invoke((MethodInvoker)delegate ()
                {
                    this.tbNoOfParts.Text = Bom2Import.GetNumberOfParts().ToString();
                }
                );
            }
            else
            {
                this.tbNoOfParts.Text = Bom2Import.GetNumberOfParts().ToString();
            }

            if (this.tbNoOfDevice.InvokeRequired)
            {
                this.tbNoOfDevice.Invoke((MethodInvoker)delegate ()
                {
                    this.tbNoOfDevice.Text = Bom2Import.GetNumberOfPartsWithSubParts().ToString();
                }
                );
            }
            else
            {
                this.tbNoOfDevice.Text = Bom2Import.GetNumberOfPartsWithSubParts().ToString();
            }

            if (this.tbNoOfNewManufacturers.InvokeRequired)
            {
                this.tbNoOfNewManufacturers.Invoke((MethodInvoker)delegate ()
                {
                    this.tbNoOfNewManufacturers.Text = importer.NoOfNewManufacturer.ToString();
                }
                );
            }
            else
            {
                this.tbNoOfNewManufacturers.Text = importer.NoOfNewManufacturer.ToString();
            }

            if (this.tbNoOfNewManufacturerParts.InvokeRequired)
            {
                this.tbNoOfNewManufacturerParts.Invoke((MethodInvoker)delegate ()
                {
                    this.tbNoOfNewManufacturerParts.Text = importer.NoOfNewManufacturerParts.ToString();
                }
                );
            }
            else
            {
                this.tbNoOfNewManufacturerParts.Text = importer.NoOfNewManufacturerParts.ToString();
            }

            if (this.tbNoOfNewParts.InvokeRequired)
            {
                this.tbNoOfNewParts.Invoke((MethodInvoker)delegate ()
                {
                    this.tbNoOfNewParts.Text = importer.NoOfNewParts.ToString();
                }
                );
            }
            else
            {
                this.tbNoOfNewParts.Text = importer.NoOfNewParts.ToString();
            }

            //+++
            if (this.tbNoOfUpdatedManufacturers.InvokeRequired)
            {
                this.tbNoOfUpdatedManufacturers.Invoke((MethodInvoker)delegate ()
                {
                    this.tbNoOfUpdatedManufacturers.Text = importer.NoOfUpdatedManufacturer.ToString();
                }
                );
            }
            else
            {
                this.tbNoOfUpdatedManufacturers.Text = importer.NoOfUpdatedManufacturer.ToString();
            }

            if (this.tbNoOfUpdatedManufacturerParts.InvokeRequired)
            {
                this.tbNoOfUpdatedManufacturerParts.Invoke((MethodInvoker)delegate ()
                {
                    this.tbNoOfUpdatedManufacturerParts.Text = importer.NoOfUpdatedManufacturerParts.ToString();
                }
                );
            }
            else
            {
                this.tbNoOfUpdatedManufacturerParts.Text = importer.NoOfUpdatedManufacturerParts.ToString();
            }

            if (this.tbNoOfUpdatedParts.InvokeRequired)
            {
                this.tbNoOfUpdatedParts.Invoke((MethodInvoker)delegate ()
                {
                    this.tbNoOfUpdatedParts.Text = importer.NoOfUpdatedParts.ToString();
                }
                );
            }
            else
            {
                this.tbNoOfUpdatedParts.Text = importer.NoOfUpdatedParts.ToString();
            }

            if (this.tbNoOfErrorManufacturers.InvokeRequired)
            {
                this.tbNoOfErrorManufacturers.Invoke((MethodInvoker)delegate ()
                {
                    this.tbNoOfErrorManufacturers.Text = importer.NoOfErrorManufacturer.ToString();
                }
                );
            }
            else
            {
                this.tbNoOfErrorManufacturers.Text = importer.NoOfErrorManufacturer.ToString();
            }

            if (this.tbNoOfErrorManufacturerParts.InvokeRequired)
            {
                this.tbNoOfErrorManufacturerParts.Invoke((MethodInvoker)delegate ()
                {
                    this.tbNoOfErrorManufacturerParts.Text = importer.NoOfErrorManufacturerParts.ToString();
                }
                );
            }
            else
            {
                this.tbNoOfErrorManufacturerParts.Text = importer.NoOfErrorManufacturerParts.ToString();
            }

            if (this.tbNoOfErrorParts.InvokeRequired)
            {
                this.tbNoOfErrorParts.Invoke((MethodInvoker)delegate ()
                {
                    this.tbNoOfErrorParts.Text = importer.NoOfErrorParts.ToString();
                }
                );
            }
            else
            {
                this.tbNoOfErrorParts.Text = importer.NoOfErrorParts.ToString();
            }

            if (this.tbNoOfNewDevice.InvokeRequired)
            {
                this.tbNoOfNewDevice.Invoke((MethodInvoker)delegate ()
                {
                    this.tbNoOfNewDevice.Text = importer.NoOfNewBOM.ToString();
                }
                );
            }
            else
            {
                this.tbNoOfNewDevice.Text = importer.NoOfNewBOM.ToString();
            }

            if (this.tbNoOfErrorDevice.InvokeRequired)
            {
                this.tbNoOfErrorDevice.Invoke((MethodInvoker)delegate ()
                {
                    this.tbNoOfErrorDevice.Text = importer.NoOfErrorBOM.ToString();
                }
                );
            }
            else
            {
                this.tbNoOfErrorDevice.Text = importer.NoOfErrorBOM.ToString();
            }

        }

        #endregion




        private void GetManufacturerDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manufacturer_Form manufacturerForm = new Manufacturer_Form(importer);

            manufacturerForm.Show();
        }

        private void GetManufacturerPartDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManufacturerPart_Form manufacturerPartForm = new ManufacturerPart_Form(importer);

            manufacturerPartForm.Show();
        }

        private void GetPartDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Part_Form PartForm = new Part_Form(importer);

            PartForm.Show();
        }

        





        private void deleteAllManufacturerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("not implemented yet");
            DialogResult result1 = MessageBox.Show("Do really want to delete ALL manufacturer?", "Attention", MessageBoxButtons.YesNo);

            if (result1 == DialogResult.Yes)
            {
                ImportOptions options = new ImportOptions();
                options.WithManufacturers = cbManufacturers.Checked;
                options.WithManufacturerParts = cbManufacturerParts.Checked;
                options.WithParts = cbPart.Checked;
                options.WithBOM = cbBOM.Checked;

                importer.Test_DeleteAllManufacturer(this, options);
            }
        }

        private void deleteAllManufacturerPartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("not implemented yet");
            DialogResult result1 = MessageBox.Show("Do really want to delete ALL manufacturer parts?", "Attention", MessageBoxButtons.YesNo);

            if (result1 == DialogResult.Yes)
            {
                ImportOptions options = new ImportOptions();
                options.WithManufacturers = cbManufacturers.Checked;
                options.WithManufacturerParts = cbManufacturerParts.Checked;
                options.WithParts = cbPart.Checked;
                options.WithBOM = cbBOM.Checked;

                importer.Test_DeleteAllManufacturerParts(this, options);
            }
        }

        private void deleteAllPartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("not implemented yet");
            DialogResult result1 = MessageBox.Show("Do really want to delete ALL parts?", "Attention", MessageBoxButtons.YesNo);

            if (result1 == DialogResult.Yes)
            {
                ImportOptions options = new ImportOptions();
                options.WithManufacturers = cbManufacturers.Checked;
                options.WithManufacturerParts = cbManufacturerParts.Checked;
                options.WithParts = cbPart.Checked;
                options.WithBOM = cbBOM.Checked;

                importer.Test_DeleteAllParts(this, options);
            }
        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            UploadFile_Form UploadFileForm = new UploadFile_Form(importer);

            UploadFileForm.Show();
        }
    }
}
