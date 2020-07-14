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
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            try
            {
                tbEula.LoadFile("Help\\EULA.rtf");
            }
            catch (Exception e)
            {
                tbEula.Text = "EULA.rtf nicht gefunden!";

            }


            string sVersion = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString();
            sVersion += "." + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
            sVersion += "." + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();

            string sBuildVersion = "Build " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString();


            string sName = "BOM importer";
            lName.Text = sName + "   " + sVersion;

            lBuild.Text = sBuildVersion;
        }

        

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
