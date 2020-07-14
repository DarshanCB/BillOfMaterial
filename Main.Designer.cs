namespace BOM_Importer_V2
{
    partial class Main
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openBOMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.getManufacturerDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getManufacturerPartDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getPartDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.UploadFiletoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteAllManufacturerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllManufacturerPartsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllPartsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label8 = new System.Windows.Forms.Label();
            this.tbNoOfDevice = new System.Windows.Forms.TextBox();
            this.tbNoOfParts = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbNoOfManufacturerParts = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbNoOfManufacturers = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbBOMFilename = new System.Windows.Forms.TextBox();
            this.groupStatistic = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbNoOfErrorManufacturers = new System.Windows.Forms.TextBox();
            this.tbNoOfErrorDevice = new System.Windows.Forms.TextBox();
            this.tbNoOfErrorManufacturerParts = new System.Windows.Forms.TextBox();
            this.tbNoOfErrorParts = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbNoOfUpdatedManufacturers = new System.Windows.Forms.TextBox();
            this.tbNoOfUpdatedDevice = new System.Windows.Forms.TextBox();
            this.tbNoOfUpdatedManufacturerParts = new System.Windows.Forms.TextBox();
            this.tbNoOfUpdatedParts = new System.Windows.Forms.TextBox();
            this.tbNoOfNewManufacturers = new System.Windows.Forms.TextBox();
            this.tbNoOfNewDevice = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbNoOfNewManufacturerParts = new System.Windows.Forms.TextBox();
            this.tbNoOfNewParts = new System.Windows.Forms.TextBox();
            this.btnReadBOM = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbPart = new System.Windows.Forms.CheckBox();
            this.cbBOM = new System.Windows.Forms.CheckBox();
            this.cbManufacturerParts = new System.Windows.Forms.CheckBox();
            this.cbManufacturers = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupStatistic.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.actionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openBOMToolStripMenuItem,
            this.toolStripSeparator1,
            this.optionsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // openBOMToolStripMenuItem
            // 
            this.openBOMToolStripMenuItem.Name = "openBOMToolStripMenuItem";
            this.openBOMToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.openBOMToolStripMenuItem.Text = "Open BOM";
            this.openBOMToolStripMenuItem.Click += new System.EventHandler(this.OpenBOMToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(130, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.OptionsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.getManufacturerDataToolStripMenuItem,
            this.getManufacturerPartDataToolStripMenuItem,
            this.getPartDataToolStripMenuItem,
            this.toolStripSeparator4,
            this.UploadFiletoolStripMenuItem,
            this.toolStripSeparator3,
            this.deleteAllManufacturerToolStripMenuItem,
            this.deleteAllManufacturerPartsToolStripMenuItem,
            this.deleteAllPartsToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(225, 6);
            // 
            // getManufacturerDataToolStripMenuItem
            // 
            this.getManufacturerDataToolStripMenuItem.Name = "getManufacturerDataToolStripMenuItem";
            this.getManufacturerDataToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.getManufacturerDataToolStripMenuItem.Text = "Manufacturer Data";
            this.getManufacturerDataToolStripMenuItem.Click += new System.EventHandler(this.GetManufacturerDataToolStripMenuItem_Click);
            // 
            // getManufacturerPartDataToolStripMenuItem
            // 
            this.getManufacturerPartDataToolStripMenuItem.Name = "getManufacturerPartDataToolStripMenuItem";
            this.getManufacturerPartDataToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.getManufacturerPartDataToolStripMenuItem.Text = "Manufacturer Part Data";
            this.getManufacturerPartDataToolStripMenuItem.Click += new System.EventHandler(this.GetManufacturerPartDataToolStripMenuItem_Click);
            // 
            // getPartDataToolStripMenuItem
            // 
            this.getPartDataToolStripMenuItem.Name = "getPartDataToolStripMenuItem";
            this.getPartDataToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.getPartDataToolStripMenuItem.Text = "Part Data";
            this.getPartDataToolStripMenuItem.Click += new System.EventHandler(this.GetPartDataToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(225, 6);
            // 
            // UploadFiletoolStripMenuItem
            // 
            this.UploadFiletoolStripMenuItem.Name = "UploadFiletoolStripMenuItem";
            this.UploadFiletoolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.UploadFiletoolStripMenuItem.Text = "Upload File";
            this.UploadFiletoolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem2_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(225, 6);
            // 
            // deleteAllManufacturerToolStripMenuItem
            // 
            this.deleteAllManufacturerToolStripMenuItem.Name = "deleteAllManufacturerToolStripMenuItem";
            this.deleteAllManufacturerToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.deleteAllManufacturerToolStripMenuItem.Text = "Delete All Manufacturer";
            this.deleteAllManufacturerToolStripMenuItem.Click += new System.EventHandler(this.deleteAllManufacturerToolStripMenuItem_Click);
            // 
            // deleteAllManufacturerPartsToolStripMenuItem
            // 
            this.deleteAllManufacturerPartsToolStripMenuItem.Name = "deleteAllManufacturerPartsToolStripMenuItem";
            this.deleteAllManufacturerPartsToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.deleteAllManufacturerPartsToolStripMenuItem.Text = "Delete All Manufacturer Parts";
            this.deleteAllManufacturerPartsToolStripMenuItem.Click += new System.EventHandler(this.deleteAllManufacturerPartsToolStripMenuItem_Click);
            // 
            // deleteAllPartsToolStripMenuItem
            // 
            this.deleteAllPartsToolStripMenuItem.Name = "deleteAllPartsToolStripMenuItem";
            this.deleteAllPartsToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.deleteAllPartsToolStripMenuItem.Text = "Delete All Parts";
            this.deleteAllPartsToolStripMenuItem.Click += new System.EventHandler(this.deleteAllPartsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.helpToolStripMenuItem1.Text = "Help";
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.HelpToolStripMenuItem1_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 152);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "device";
            // 
            // tbNoOfDevice
            // 
            this.tbNoOfDevice.BackColor = System.Drawing.SystemColors.Control;
            this.tbNoOfDevice.Location = new System.Drawing.Point(138, 149);
            this.tbNoOfDevice.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbNoOfDevice.Name = "tbNoOfDevice";
            this.tbNoOfDevice.Size = new System.Drawing.Size(62, 20);
            this.tbNoOfDevice.TabIndex = 26;
            // 
            // tbNoOfParts
            // 
            this.tbNoOfParts.Location = new System.Drawing.Point(138, 125);
            this.tbNoOfParts.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbNoOfParts.Name = "tbNoOfParts";
            this.tbNoOfParts.ReadOnly = true;
            this.tbNoOfParts.Size = new System.Drawing.Size(62, 20);
            this.tbNoOfParts.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "parts";
            // 
            // tbNoOfManufacturerParts
            // 
            this.tbNoOfManufacturerParts.Location = new System.Drawing.Point(138, 101);
            this.tbNoOfManufacturerParts.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbNoOfManufacturerParts.Name = "tbNoOfManufacturerParts";
            this.tbNoOfManufacturerParts.ReadOnly = true;
            this.tbNoOfManufacturerParts.Size = new System.Drawing.Size(62, 20);
            this.tbNoOfManufacturerParts.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "manufacturer parts";
            // 
            // tbNoOfManufacturers
            // 
            this.tbNoOfManufacturers.Location = new System.Drawing.Point(138, 77);
            this.tbNoOfManufacturers.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbNoOfManufacturers.Name = "tbNoOfManufacturers";
            this.tbNoOfManufacturers.ReadOnly = true;
            this.tbNoOfManufacturers.Size = new System.Drawing.Size(62, 20);
            this.tbNoOfManufacturers.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "manufacturers";
            // 
            // tbBOMFilename
            // 
            this.tbBOMFilename.Location = new System.Drawing.Point(106, 18);
            this.tbBOMFilename.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbBOMFilename.Name = "tbBOMFilename";
            this.tbBOMFilename.ReadOnly = true;
            this.tbBOMFilename.Size = new System.Drawing.Size(275, 20);
            this.tbBOMFilename.TabIndex = 17;
            // 
            // groupStatistic
            // 
            this.groupStatistic.Controls.Add(this.label9);
            this.groupStatistic.Controls.Add(this.tbNoOfErrorManufacturers);
            this.groupStatistic.Controls.Add(this.tbNoOfErrorDevice);
            this.groupStatistic.Controls.Add(this.tbNoOfErrorManufacturerParts);
            this.groupStatistic.Controls.Add(this.tbNoOfErrorParts);
            this.groupStatistic.Controls.Add(this.label7);
            this.groupStatistic.Controls.Add(this.label6);
            this.groupStatistic.Controls.Add(this.label5);
            this.groupStatistic.Controls.Add(this.tbNoOfUpdatedManufacturers);
            this.groupStatistic.Controls.Add(this.tbNoOfUpdatedDevice);
            this.groupStatistic.Controls.Add(this.tbNoOfUpdatedManufacturerParts);
            this.groupStatistic.Controls.Add(this.tbNoOfUpdatedParts);
            this.groupStatistic.Controls.Add(this.tbNoOfNewManufacturers);
            this.groupStatistic.Controls.Add(this.tbNoOfNewDevice);
            this.groupStatistic.Controls.Add(this.label1);
            this.groupStatistic.Controls.Add(this.tbNoOfNewManufacturerParts);
            this.groupStatistic.Controls.Add(this.tbBOMFilename);
            this.groupStatistic.Controls.Add(this.tbNoOfNewParts);
            this.groupStatistic.Controls.Add(this.label8);
            this.groupStatistic.Controls.Add(this.tbNoOfManufacturers);
            this.groupStatistic.Controls.Add(this.tbNoOfDevice);
            this.groupStatistic.Controls.Add(this.label2);
            this.groupStatistic.Controls.Add(this.label3);
            this.groupStatistic.Controls.Add(this.tbNoOfManufacturerParts);
            this.groupStatistic.Controls.Add(this.tbNoOfParts);
            this.groupStatistic.Controls.Add(this.label4);
            this.groupStatistic.Location = new System.Drawing.Point(353, 41);
            this.groupStatistic.Name = "groupStatistic";
            this.groupStatistic.Size = new System.Drawing.Size(414, 185);
            this.groupStatistic.TabIndex = 30;
            this.groupStatistic.TabStop = false;
            this.groupStatistic.Text = "statistic";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(334, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 43;
            this.label9.Text = "error";
            // 
            // tbNoOfErrorManufacturers
            // 
            this.tbNoOfErrorManufacturers.Location = new System.Drawing.Point(337, 77);
            this.tbNoOfErrorManufacturers.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbNoOfErrorManufacturers.Name = "tbNoOfErrorManufacturers";
            this.tbNoOfErrorManufacturers.ReadOnly = true;
            this.tbNoOfErrorManufacturers.Size = new System.Drawing.Size(62, 20);
            this.tbNoOfErrorManufacturers.TabIndex = 44;
            // 
            // tbNoOfErrorDevice
            // 
            this.tbNoOfErrorDevice.BackColor = System.Drawing.SystemColors.Control;
            this.tbNoOfErrorDevice.Location = new System.Drawing.Point(337, 149);
            this.tbNoOfErrorDevice.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbNoOfErrorDevice.Name = "tbNoOfErrorDevice";
            this.tbNoOfErrorDevice.Size = new System.Drawing.Size(62, 20);
            this.tbNoOfErrorDevice.TabIndex = 47;
            // 
            // tbNoOfErrorManufacturerParts
            // 
            this.tbNoOfErrorManufacturerParts.Location = new System.Drawing.Point(337, 101);
            this.tbNoOfErrorManufacturerParts.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbNoOfErrorManufacturerParts.Name = "tbNoOfErrorManufacturerParts";
            this.tbNoOfErrorManufacturerParts.ReadOnly = true;
            this.tbNoOfErrorManufacturerParts.Size = new System.Drawing.Size(62, 20);
            this.tbNoOfErrorManufacturerParts.TabIndex = 45;
            // 
            // tbNoOfErrorParts
            // 
            this.tbNoOfErrorParts.Location = new System.Drawing.Point(337, 125);
            this.tbNoOfErrorParts.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbNoOfErrorParts.Name = "tbNoOfErrorParts";
            this.tbNoOfErrorParts.ReadOnly = true;
            this.tbNoOfErrorParts.Size = new System.Drawing.Size(62, 20);
            this.tbNoOfErrorParts.TabIndex = 46;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(267, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 36;
            this.label7.Text = "updated";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(201, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "new";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(135, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "in BOM";
            // 
            // tbNoOfUpdatedManufacturers
            // 
            this.tbNoOfUpdatedManufacturers.Location = new System.Drawing.Point(270, 77);
            this.tbNoOfUpdatedManufacturers.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbNoOfUpdatedManufacturers.Name = "tbNoOfUpdatedManufacturers";
            this.tbNoOfUpdatedManufacturers.ReadOnly = true;
            this.tbNoOfUpdatedManufacturers.Size = new System.Drawing.Size(62, 20);
            this.tbNoOfUpdatedManufacturers.TabIndex = 39;
            // 
            // tbNoOfUpdatedDevice
            // 
            this.tbNoOfUpdatedDevice.BackColor = System.Drawing.SystemColors.Control;
            this.tbNoOfUpdatedDevice.Location = new System.Drawing.Point(270, 149);
            this.tbNoOfUpdatedDevice.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbNoOfUpdatedDevice.Name = "tbNoOfUpdatedDevice";
            this.tbNoOfUpdatedDevice.Size = new System.Drawing.Size(62, 20);
            this.tbNoOfUpdatedDevice.TabIndex = 42;
            // 
            // tbNoOfUpdatedManufacturerParts
            // 
            this.tbNoOfUpdatedManufacturerParts.Location = new System.Drawing.Point(270, 101);
            this.tbNoOfUpdatedManufacturerParts.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbNoOfUpdatedManufacturerParts.Name = "tbNoOfUpdatedManufacturerParts";
            this.tbNoOfUpdatedManufacturerParts.ReadOnly = true;
            this.tbNoOfUpdatedManufacturerParts.Size = new System.Drawing.Size(62, 20);
            this.tbNoOfUpdatedManufacturerParts.TabIndex = 40;
            // 
            // tbNoOfUpdatedParts
            // 
            this.tbNoOfUpdatedParts.Location = new System.Drawing.Point(270, 125);
            this.tbNoOfUpdatedParts.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbNoOfUpdatedParts.Name = "tbNoOfUpdatedParts";
            this.tbNoOfUpdatedParts.ReadOnly = true;
            this.tbNoOfUpdatedParts.Size = new System.Drawing.Size(62, 20);
            this.tbNoOfUpdatedParts.TabIndex = 41;
            // 
            // tbNoOfNewManufacturers
            // 
            this.tbNoOfNewManufacturers.Location = new System.Drawing.Point(204, 77);
            this.tbNoOfNewManufacturers.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbNoOfNewManufacturers.Name = "tbNoOfNewManufacturers";
            this.tbNoOfNewManufacturers.ReadOnly = true;
            this.tbNoOfNewManufacturers.Size = new System.Drawing.Size(62, 20);
            this.tbNoOfNewManufacturers.TabIndex = 35;
            // 
            // tbNoOfNewDevice
            // 
            this.tbNoOfNewDevice.BackColor = System.Drawing.SystemColors.Control;
            this.tbNoOfNewDevice.Location = new System.Drawing.Point(204, 149);
            this.tbNoOfNewDevice.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbNoOfNewDevice.Name = "tbNoOfNewDevice";
            this.tbNoOfNewDevice.Size = new System.Drawing.Size(62, 20);
            this.tbNoOfNewDevice.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "BOM file";
            // 
            // tbNoOfNewManufacturerParts
            // 
            this.tbNoOfNewManufacturerParts.Location = new System.Drawing.Point(204, 101);
            this.tbNoOfNewManufacturerParts.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbNoOfNewManufacturerParts.Name = "tbNoOfNewManufacturerParts";
            this.tbNoOfNewManufacturerParts.ReadOnly = true;
            this.tbNoOfNewManufacturerParts.Size = new System.Drawing.Size(62, 20);
            this.tbNoOfNewManufacturerParts.TabIndex = 36;
            // 
            // tbNoOfNewParts
            // 
            this.tbNoOfNewParts.Location = new System.Drawing.Point(204, 125);
            this.tbNoOfNewParts.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbNoOfNewParts.Name = "tbNoOfNewParts";
            this.tbNoOfNewParts.ReadOnly = true;
            this.tbNoOfNewParts.Size = new System.Drawing.Size(62, 20);
            this.tbNoOfNewParts.TabIndex = 37;
            // 
            // btnReadBOM
            // 
            this.btnReadBOM.Location = new System.Drawing.Point(12, 41);
            this.btnReadBOM.Name = "btnReadBOM";
            this.btnReadBOM.Size = new System.Drawing.Size(108, 58);
            this.btnReadBOM.TabIndex = 31;
            this.btnReadBOM.Text = "Read BOM";
            this.btnReadBOM.UseVisualStyleBackColor = true;
            this.btnReadBOM.Click += new System.EventHandler(this.BtnReadBOM_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(12, 105);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(108, 58);
            this.btnImport.TabIndex = 32;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.BtnImport_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(383, 427);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(417, 23);
            this.progressBar.TabIndex = 33;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbPart);
            this.groupBox2.Controls.Add(this.cbBOM);
            this.groupBox2.Controls.Add(this.cbManufacturerParts);
            this.groupBox2.Controls.Add(this.cbManufacturers);
            this.groupBox2.Location = new System.Drawing.Point(179, 43);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(168, 132);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Import";
            // 
            // cbPart
            // 
            this.cbPart.AutoSize = true;
            this.cbPart.Checked = true;
            this.cbPart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPart.Location = new System.Drawing.Point(6, 75);
            this.cbPart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbPart.Name = "cbPart";
            this.cbPart.Size = new System.Drawing.Size(49, 17);
            this.cbPart.TabIndex = 29;
            this.cbPart.Text = "parts";
            this.cbPart.UseVisualStyleBackColor = true;
            // 
            // cbBOM
            // 
            this.cbBOM.AutoSize = true;
            this.cbBOM.Checked = true;
            this.cbBOM.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbBOM.Location = new System.Drawing.Point(6, 97);
            this.cbBOM.Name = "cbBOM";
            this.cbBOM.Size = new System.Drawing.Size(50, 17);
            this.cbBOM.TabIndex = 3;
            this.cbBOM.Text = "BOM";
            this.cbBOM.UseVisualStyleBackColor = true;
            // 
            // cbManufacturerParts
            // 
            this.cbManufacturerParts.AutoSize = true;
            this.cbManufacturerParts.Checked = true;
            this.cbManufacturerParts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbManufacturerParts.Location = new System.Drawing.Point(6, 53);
            this.cbManufacturerParts.Name = "cbManufacturerParts";
            this.cbManufacturerParts.Size = new System.Drawing.Size(114, 17);
            this.cbManufacturerParts.TabIndex = 1;
            this.cbManufacturerParts.Text = "manufacturer parts";
            this.cbManufacturerParts.UseVisualStyleBackColor = true;
            // 
            // cbManufacturers
            // 
            this.cbManufacturers.AutoSize = true;
            this.cbManufacturers.Checked = true;
            this.cbManufacturers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbManufacturers.Location = new System.Drawing.Point(6, 30);
            this.cbManufacturers.Name = "cbManufacturers";
            this.cbManufacturers.Size = new System.Drawing.Size(93, 17);
            this.cbManufacturers.TabIndex = 0;
            this.cbManufacturers.Text = "manufacturers";
            this.cbManufacturers.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnReadBOM);
            this.Controls.Add(this.groupStatistic);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "BOM Importer";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupStatistic.ResumeLayout(false);
            this.groupStatistic.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openBOMToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbNoOfDevice;
        private System.Windows.Forms.TextBox tbNoOfParts;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbNoOfManufacturerParts;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbNoOfManufacturers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbBOMFilename;
        private System.Windows.Forms.GroupBox groupStatistic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReadBOM;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbPart;
        private System.Windows.Forms.CheckBox cbBOM;
        private System.Windows.Forms.CheckBox cbManufacturerParts;
        private System.Windows.Forms.CheckBox cbManufacturers;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem getManufacturerDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getManufacturerPartDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getPartDataToolStripMenuItem;
        private System.Windows.Forms.TextBox tbNoOfUpdatedManufacturers;
        private System.Windows.Forms.TextBox tbNoOfUpdatedDevice;
        private System.Windows.Forms.TextBox tbNoOfUpdatedManufacturerParts;
        private System.Windows.Forms.TextBox tbNoOfUpdatedParts;
        private System.Windows.Forms.TextBox tbNoOfNewManufacturers;
        private System.Windows.Forms.TextBox tbNoOfNewDevice;
        private System.Windows.Forms.TextBox tbNoOfNewManufacturerParts;
        private System.Windows.Forms.TextBox tbNoOfNewParts;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem deleteAllManufacturerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAllManufacturerPartsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAllPartsToolStripMenuItem;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbNoOfErrorManufacturers;
        private System.Windows.Forms.TextBox tbNoOfErrorDevice;
        private System.Windows.Forms.TextBox tbNoOfErrorManufacturerParts;
        private System.Windows.Forms.TextBox tbNoOfErrorParts;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem UploadFiletoolStripMenuItem;
    }
}

