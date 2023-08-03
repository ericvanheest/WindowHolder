namespace WindowHolder
{
    partial class WindowHolderMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lvPrograms = new System.Windows.Forms.ListView();
            this.chProgram = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmPrograms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miProgramProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.miProgramRename = new System.Windows.Forms.ToolStripMenuItem();
            this.miProgramDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.cmTabs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmClose = new System.Windows.Forms.ToolStripMenuItem();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.miProgramNew = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmPrograms.SuspendLayout();
            this.cmTabs.SuspendLayout();
            this.msMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvPrograms
            // 
            this.lvPrograms.AllowDrop = true;
            this.lvPrograms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chProgram});
            this.lvPrograms.ContextMenuStrip = this.cmPrograms;
            this.lvPrograms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPrograms.FullRowSelect = true;
            this.lvPrograms.LabelEdit = true;
            this.lvPrograms.Location = new System.Drawing.Point(0, 0);
            this.lvPrograms.Name = "lvPrograms";
            this.lvPrograms.Size = new System.Drawing.Size(192, 483);
            this.lvPrograms.TabIndex = 0;
            this.lvPrograms.UseCompatibleStateImageBehavior = false;
            this.lvPrograms.View = System.Windows.Forms.View.Details;
            this.lvPrograms.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lvPrograms_AfterLabelEdit);
            this.lvPrograms.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvPrograms_DragDrop);
            this.lvPrograms.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvPrograms_DragEnter);
            this.lvPrograms.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvPrograms_KeyDown);
            this.lvPrograms.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvPrograms_MouseDoubleClick);
            // 
            // chProgram
            // 
            this.chProgram.Text = "Program";
            this.chProgram.Width = 174;
            // 
            // cmPrograms
            // 
            this.cmPrograms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miProgramNew,
            this.miProgramProperties,
            this.miProgramRename,
            this.miProgramDelete});
            this.cmPrograms.Name = "cmPrograms";
            this.cmPrograms.Size = new System.Drawing.Size(124, 92);
            this.cmPrograms.Opening += new System.ComponentModel.CancelEventHandler(this.cmPrograms_Opening);
            // 
            // miProgramProperties
            // 
            this.miProgramProperties.Name = "miProgramProperties";
            this.miProgramProperties.Size = new System.Drawing.Size(152, 22);
            this.miProgramProperties.Text = "&Properties";
            this.miProgramProperties.Click += new System.EventHandler(this.miProgramProperties_Click);
            // 
            // miProgramRename
            // 
            this.miProgramRename.Name = "miProgramRename";
            this.miProgramRename.Size = new System.Drawing.Size(152, 22);
            this.miProgramRename.Text = "&Rename";
            this.miProgramRename.Click += new System.EventHandler(this.miProgramRename_Click);
            // 
            // miProgramDelete
            // 
            this.miProgramDelete.Name = "miProgramDelete";
            this.miProgramDelete.Size = new System.Drawing.Size(152, 22);
            this.miProgramDelete.Text = "&Delete";
            this.miProgramDelete.Click += new System.EventHandler(this.miProgramDelete_Click);
            // 
            // tcMain
            // 
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(694, 483);
            this.tcMain.TabIndex = 1;
            this.tcMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tcMain_MouseUp);
            // 
            // cmTabs
            // 
            this.cmTabs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmClose});
            this.cmTabs.Name = "cmTabs";
            this.cmTabs.Size = new System.Drawing.Size(101, 26);
            // 
            // cmClose
            // 
            this.cmClose.Name = "cmClose";
            this.cmClose.Size = new System.Drawing.Size(100, 22);
            this.cmClose.Text = "&Close";
            this.cmClose.Click += new System.EventHandler(this.cmClose_Click);
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuHelp});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(890, 24);
            this.msMain.TabIndex = 2;
            this.msMain.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(35, 20);
            this.menuFile.Text = "&File";
            // 
            // menuFileExit
            // 
            this.menuFileExit.Name = "menuFileExit";
            this.menuFileExit.Size = new System.Drawing.Size(92, 22);
            this.menuFileExit.Text = "&Exit";
            this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHelpAbout});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(40, 20);
            this.menuHelp.Text = "&Help";
            // 
            // menuHelpAbout
            // 
            this.menuHelpAbout.Name = "menuHelpAbout";
            this.menuHelpAbout.Size = new System.Drawing.Size(103, 22);
            this.menuHelpAbout.Text = "&About";
            this.menuHelpAbout.Click += new System.EventHandler(this.menuHelpAbout_Click);
            // 
            // miProgramNew
            // 
            this.miProgramNew.Name = "miProgramNew";
            this.miProgramNew.Size = new System.Drawing.Size(152, 22);
            this.miProgramNew.Text = "&New";
            this.miProgramNew.Click += new System.EventHandler(this.miProgramNew_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvPrograms);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tcMain);
            this.splitContainer1.Size = new System.Drawing.Size(890, 483);
            this.splitContainer1.SplitterDistance = 192;
            this.splitContainer1.TabIndex = 3;
            // 
            // WindowHolderMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 507);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.msMain);
            this.MainMenuStrip = this.msMain;
            this.Name = "WindowHolderMain";
            this.Text = "Window Holder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WindowHolderMain_FormClosing);
            this.Load += new System.EventHandler(this.WindowHolderMain_Load);
            this.cmPrograms.ResumeLayout(false);
            this.cmTabs.ResumeLayout(false);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvPrograms;
        private System.Windows.Forms.ColumnHeader chProgram;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.ContextMenuStrip cmTabs;
        private System.Windows.Forms.ToolStripMenuItem cmClose;
        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem menuFileExit;
        private System.Windows.Forms.ContextMenuStrip cmPrograms;
        private System.Windows.Forms.ToolStripMenuItem miProgramProperties;
        private System.Windows.Forms.ToolStripMenuItem miProgramRename;
        private System.Windows.Forms.ToolStripMenuItem miProgramDelete;
        private System.Windows.Forms.ToolStripMenuItem miProgramNew;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

