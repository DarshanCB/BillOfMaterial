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
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();

            try
            {
                tbHelp.LoadFile("Help\\Help.rtf");
            }
            catch (Exception e)
            {
                tbHelp.Text = "Help.rtf nicht gefunden!";
            
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
