namespace WindowHolder
{
    partial class OptionsForm
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
            this.tcOptions = new System.Windows.Forms.TabControl();
            this.tpMain = new System.Windows.Forms.TabPage();
            this.cbAllowDragCapture = new System.Windows.Forms.CheckBox();
            this.cbConfirmOpenMultiple = new System.Windows.Forms.CheckBox();
            this.cbDoubleClick = new System.Windows.Forms.ComboBox();
            this.cbAutoHideList = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbSelectActivates = new System.Windows.Forms.CheckBox();
            this.tpDefaults = new System.Windows.Forms.TabPage();
            this.cbDefaultResizeToMaximize = new System.Windows.Forms.CheckBox();
            this.cbDefaultRememberCapturedLocations = new System.Windows.Forms.CheckBox();
            this.cbDefaultStartMinimized = new System.Windows.Forms.CheckBox();
            this.cbDefaultCloseTabIfEmpty = new System.Windows.Forms.CheckBox();
            this.nudMainWindowWait = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cbCaptureWindows = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDefaultForceSize = new System.Windows.Forms.CheckBox();
            this.cbRemoveTitleBar = new System.Windows.Forms.CheckBox();
            this.tpHotkeys = new System.Windows.Forms.TabPage();
            this.cbDisableHotkeys = new System.Windows.Forms.CheckBox();
            this.btnResetHotkeys = new System.Windows.Forms.Button();
            this.btnSet = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbHotkey = new WindowHolder.HotKeyTextBox();
            this.lvHotkeys = new System.Windows.Forms.ListView();
            this.chAction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.timerHotkey = new System.Windows.Forms.Timer(this.components);
            this.cbCaptureOnlyWithAlt = new System.Windows.Forms.CheckBox();
            this.tcOptions.SuspendLayout();
            this.tpMain.SuspendLayout();
            this.tpDefaults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMainWindowWait)).BeginInit();
            this.tpHotkeys.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcOptions
            // 
            this.tcOptions.Controls.Add(this.tpMain);
            this.tcOptions.Controls.Add(this.tpDefaults);
            this.tcOptions.Controls.Add(this.tpHotkeys);
            this.tcOptions.Location = new System.Drawing.Point(12, 12);
            this.tcOptions.Name = "tcOptions";
            this.tcOptions.SelectedIndex = 0;
            this.tcOptions.Size = new System.Drawing.Size(557, 283);
            this.tcOptions.TabIndex = 0;
            // 
            // tpMain
            // 
            this.tpMain.Controls.Add(this.cbCaptureOnlyWithAlt);
            this.tpMain.Controls.Add(this.cbAllowDragCapture);
            this.tpMain.Controls.Add(this.cbConfirmOpenMultiple);
            this.tpMain.Controls.Add(this.cbDoubleClick);
            this.tpMain.Controls.Add(this.cbAutoHideList);
            this.tpMain.Controls.Add(this.label6);
            this.tpMain.Controls.Add(this.cbSelectActivates);
            this.tpMain.Location = new System.Drawing.Point(4, 22);
            this.tpMain.Name = "tpMain";
            this.tpMain.Padding = new System.Windows.Forms.Padding(3);
            this.tpMain.Size = new System.Drawing.Size(549, 257);
            this.tpMain.TabIndex = 0;
            this.tpMain.Text = "Main";
            this.tpMain.UseVisualStyleBackColor = true;
            // 
            // cbAllowDragCapture
            // 
            this.cbAllowDragCapture.AutoSize = true;
            this.cbAllowDragCapture.Location = new System.Drawing.Point(9, 98);
            this.cbAllowDragCapture.Name = "cbAllowDragCapture";
            this.cbAllowDragCapture.Size = new System.Drawing.Size(270, 17);
            this.cbAllowDragCapture.TabIndex = 4;
            this.cbAllowDragCapture.Text = "&Allow capture of existing windows via drag-and-drop";
            this.cbAllowDragCapture.UseVisualStyleBackColor = true;
            this.cbAllowDragCapture.CheckedChanged += new System.EventHandler(this.cbAllowDragCapture_CheckedChanged);
            // 
            // cbConfirmOpenMultiple
            // 
            this.cbConfirmOpenMultiple.AutoSize = true;
            this.cbConfirmOpenMultiple.Location = new System.Drawing.Point(9, 75);
            this.cbConfirmOpenMultiple.Name = "cbConfirmOpenMultiple";
            this.cbConfirmOpenMultiple.Size = new System.Drawing.Size(261, 17);
            this.cbConfirmOpenMultiple.TabIndex = 4;
            this.cbConfirmOpenMultiple.Text = "&Confirm before opening multiple programs at once.";
            this.cbConfirmOpenMultiple.UseVisualStyleBackColor = true;
            // 
            // cbDoubleClick
            // 
            this.cbDoubleClick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDoubleClick.FormattingEnabled = true;
            this.cbDoubleClick.Location = new System.Drawing.Point(234, 50);
            this.cbDoubleClick.Name = "cbDoubleClick";
            this.cbDoubleClick.Size = new System.Drawing.Size(231, 21);
            this.cbDoubleClick.TabIndex = 3;
            // 
            // cbAutoHideList
            // 
            this.cbAutoHideList.AutoSize = true;
            this.cbAutoHideList.Location = new System.Drawing.Point(6, 29);
            this.cbAutoHideList.Name = "cbAutoHideList";
            this.cbAutoHideList.Size = new System.Drawing.Size(220, 17);
            this.cbAutoHideList.TabIndex = 1;
            this.cbAutoHideList.Text = "&Hide program list when a tab is activated.";
            this.cbAutoHideList.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(222, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Double clicking an item in the program list will:";
            // 
            // cbSelectActivates
            // 
            this.cbSelectActivates.AutoSize = true;
            this.cbSelectActivates.Location = new System.Drawing.Point(6, 6);
            this.cbSelectActivates.Name = "cbSelectActivates";
            this.cbSelectActivates.Size = new System.Drawing.Size(442, 17);
            this.cbSelectActivates.TabIndex = 0;
            this.cbSelectActivates.Text = "&Selecting a program from the list activates the most recently opened tab for tha" +
                "t program.";
            this.cbSelectActivates.UseVisualStyleBackColor = true;
            // 
            // tpDefaults
            // 
            this.tpDefaults.Controls.Add(this.cbDefaultResizeToMaximize);
            this.tpDefaults.Controls.Add(this.cbDefaultRememberCapturedLocations);
            this.tpDefaults.Controls.Add(this.cbDefaultStartMinimized);
            this.tpDefaults.Controls.Add(this.cbDefaultCloseTabIfEmpty);
            this.tpDefaults.Controls.Add(this.nudMainWindowWait);
            this.tpDefaults.Controls.Add(this.label2);
            this.tpDefaults.Controls.Add(this.cbCaptureWindows);
            this.tpDefaults.Controls.Add(this.label1);
            this.tpDefaults.Controls.Add(this.cbDefaultForceSize);
            this.tpDefaults.Controls.Add(this.cbRemoveTitleBar);
            this.tpDefaults.Location = new System.Drawing.Point(4, 22);
            this.tpDefaults.Name = "tpDefaults";
            this.tpDefaults.Size = new System.Drawing.Size(549, 257);
            this.tpDefaults.TabIndex = 1;
            this.tpDefaults.Text = "Defaults";
            this.tpDefaults.UseVisualStyleBackColor = true;
            // 
            // cbDefaultResizeToMaximize
            // 
            this.cbDefaultResizeToMaximize.AutoSize = true;
            this.cbDefaultResizeToMaximize.Location = new System.Drawing.Point(13, 175);
            this.cbDefaultResizeToMaximize.Name = "cbDefaultResizeToMaximize";
            this.cbDefaultResizeToMaximize.Size = new System.Drawing.Size(430, 17);
            this.cbDefaultResizeToMaximize.TabIndex = 9;
            this.cbDefaultResizeToMaximize.Text = "Resize windows to maximum size instead of using normal \"maximize window\" behavior" +
                "";
            this.cbDefaultResizeToMaximize.UseVisualStyleBackColor = true;
            // 
            // cbDefaultRememberCapturedLocations
            // 
            this.cbDefaultRememberCapturedLocations.AutoSize = true;
            this.cbDefaultRememberCapturedLocations.Location = new System.Drawing.Point(13, 152);
            this.cbDefaultRememberCapturedLocations.Name = "cbDefaultRememberCapturedLocations";
            this.cbDefaultRememberCapturedLocations.Size = new System.Drawing.Size(209, 17);
            this.cbDefaultRememberCapturedLocations.TabIndex = 8;
            this.cbDefaultRememberCapturedLocations.Text = "Remember captured window locations.";
            this.cbDefaultRememberCapturedLocations.UseVisualStyleBackColor = true;
            // 
            // cbDefaultStartMinimized
            // 
            this.cbDefaultStartMinimized.AutoSize = true;
            this.cbDefaultStartMinimized.Location = new System.Drawing.Point(13, 129);
            this.cbDefaultStartMinimized.Name = "cbDefaultStartMinimized";
            this.cbDefaultStartMinimized.Size = new System.Drawing.Size(256, 17);
            this.cbDefaultStartMinimized.TabIndex = 7;
            this.cbDefaultStartMinimized.Text = "Start processes minimized before capturing them.";
            this.cbDefaultStartMinimized.UseVisualStyleBackColor = true;
            // 
            // cbDefaultCloseTabIfEmpty
            // 
            this.cbDefaultCloseTabIfEmpty.AutoSize = true;
            this.cbDefaultCloseTabIfEmpty.Location = new System.Drawing.Point(13, 106);
            this.cbDefaultCloseTabIfEmpty.Name = "cbDefaultCloseTabIfEmpty";
            this.cbDefaultCloseTabIfEmpty.Size = new System.Drawing.Size(193, 17);
            this.cbDefaultCloseTabIfEmpty.TabIndex = 6;
            this.cbDefaultCloseTabIfEmpty.Text = "Close tab if no windows remain in it.";
            this.cbDefaultCloseTabIfEmpty.UseVisualStyleBackColor = true;
            // 
            // nudMainWindowWait
            // 
            this.nudMainWindowWait.Location = new System.Drawing.Point(41, 78);
            this.nudMainWindowWait.Name = "nudMainWindowWait";
            this.nudMainWindowWait.Size = new System.Drawing.Size(50, 20);
            this.nudMainWindowWait.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(93, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(231, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "seconds for the main window of a new process.";
            // 
            // cbCaptureWindows
            // 
            this.cbCaptureWindows.AutoSize = true;
            this.cbCaptureWindows.Location = new System.Drawing.Point(13, 53);
            this.cbCaptureWindows.Name = "cbCaptureWindows";
            this.cbCaptureWindows.Size = new System.Drawing.Size(280, 17);
            this.cbCaptureWindows.TabIndex = 2;
            this.cbCaptureWindows.Text = "&Capture any additional windows created by a process.";
            this.cbCaptureWindows.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Wait";
            // 
            // cbDefaultForceSize
            // 
            this.cbDefaultForceSize.AutoSize = true;
            this.cbDefaultForceSize.Location = new System.Drawing.Point(13, 7);
            this.cbDefaultForceSize.Name = "cbDefaultForceSize";
            this.cbDefaultForceSize.Size = new System.Drawing.Size(286, 17);
            this.cbDefaultForceSize.TabIndex = 0;
            this.cbDefaultForceSize.Text = "&Force captured windows to be the size of the tab page.";
            this.cbDefaultForceSize.UseVisualStyleBackColor = true;
            this.cbDefaultForceSize.CheckedChanged += new System.EventHandler(this.cbForceSize_CheckedChanged);
            // 
            // cbRemoveTitleBar
            // 
            this.cbRemoveTitleBar.AutoSize = true;
            this.cbRemoveTitleBar.Enabled = false;
            this.cbRemoveTitleBar.Location = new System.Drawing.Point(32, 30);
            this.cbRemoveTitleBar.Name = "cbRemoveTitleBar";
            this.cbRemoveTitleBar.Size = new System.Drawing.Size(185, 17);
            this.cbRemoveTitleBar.TabIndex = 1;
            this.cbRemoveTitleBar.Text = "&Remove title bar from applications";
            this.cbRemoveTitleBar.UseVisualStyleBackColor = true;
            // 
            // tpHotkeys
            // 
            this.tpHotkeys.Controls.Add(this.cbDisableHotkeys);
            this.tpHotkeys.Controls.Add(this.btnResetHotkeys);
            this.tpHotkeys.Controls.Add(this.btnSet);
            this.tpHotkeys.Controls.Add(this.label4);
            this.tpHotkeys.Controls.Add(this.label3);
            this.tpHotkeys.Controls.Add(this.tbHotkey);
            this.tpHotkeys.Controls.Add(this.lvHotkeys);
            this.tpHotkeys.Location = new System.Drawing.Point(4, 22);
            this.tpHotkeys.Name = "tpHotkeys";
            this.tpHotkeys.Size = new System.Drawing.Size(549, 257);
            this.tpHotkeys.TabIndex = 2;
            this.tpHotkeys.Text = "Hotkeys";
            this.tpHotkeys.UseVisualStyleBackColor = true;
            // 
            // cbDisableHotkeys
            // 
            this.cbDisableHotkeys.AutoSize = true;
            this.cbDisableHotkeys.Location = new System.Drawing.Point(432, 235);
            this.cbDisableHotkeys.Name = "cbDisableHotkeys";
            this.cbDisableHotkeys.Size = new System.Drawing.Size(114, 17);
            this.cbDisableHotkeys.TabIndex = 6;
            this.cbDisableHotkeys.Text = "&Disable all hotkeys";
            this.cbDisableHotkeys.UseVisualStyleBackColor = true;
            // 
            // btnResetHotkeys
            // 
            this.btnResetHotkeys.Location = new System.Drawing.Point(6, 231);
            this.btnResetHotkeys.Name = "btnResetHotkeys";
            this.btnResetHotkeys.Size = new System.Drawing.Size(103, 23);
            this.btnResetHotkeys.TabIndex = 5;
            this.btnResetHotkeys.Text = "&Reset to defaults";
            this.btnResetHotkeys.UseVisualStyleBackColor = true;
            this.btnResetHotkeys.Click += new System.EventHandler(this.btnResetHotkeys_Click);
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(284, 207);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(48, 20);
            this.btnSet.TabIndex = 3;
            this.btnSet.Text = "&Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(368, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(184, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "(Backspace to clear, Enter to accept)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "New hotkey:";
            // 
            // tbHotkey
            // 
            this.tbHotkey.ForeColor = System.Drawing.SystemColors.GrayText;
            this.tbHotkey.Location = new System.Drawing.Point(76, 207);
            this.tbHotkey.Name = "tbHotkey";
            this.tbHotkey.ReadOnly = true;
            this.tbHotkey.Size = new System.Drawing.Size(202, 20);
            this.tbHotkey.TabIndex = 2;
            this.tbHotkey.Text = "Click here and press new hotkey(s)";
            this.tbHotkey.Enter += new System.EventHandler(this.tbHotkey_Enter);
            this.tbHotkey.Leave += new System.EventHandler(this.tbHotkey_Leave);
            // 
            // lvHotkeys
            // 
            this.lvHotkeys.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chAction,
            this.chKey});
            this.lvHotkeys.FullRowSelect = true;
            this.lvHotkeys.HideSelection = false;
            this.lvHotkeys.Location = new System.Drawing.Point(0, 0);
            this.lvHotkeys.Name = "lvHotkeys";
            this.lvHotkeys.Size = new System.Drawing.Size(549, 201);
            this.lvHotkeys.TabIndex = 0;
            this.lvHotkeys.UseCompatibleStateImageBehavior = false;
            this.lvHotkeys.View = System.Windows.Forms.View.Details;
            this.lvHotkeys.SelectedIndexChanged += new System.EventHandler(this.lvHotkeys_SelectedIndexChanged);
            this.lvHotkeys.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvHotkeys_KeyDown);
            this.lvHotkeys.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvHotkeys_MouseDoubleClick);
            // 
            // chAction
            // 
            this.chAction.Text = "Action";
            this.chAction.Width = 206;
            // 
            // chKey
            // 
            this.chKey.Text = "Hotkey";
            this.chKey.Width = 221;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(413, 301);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(494, 301);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // timerHotkey
            // 
            this.timerHotkey.Interval = 50;
            this.timerHotkey.Tick += new System.EventHandler(this.timerHotkey_Tick);
            // 
            // cbCaptureOnlyWithAlt
            // 
            this.cbCaptureOnlyWithAlt.AutoSize = true;
            this.cbCaptureOnlyWithAlt.Location = new System.Drawing.Point(28, 121);
            this.cbCaptureOnlyWithAlt.Name = "cbCaptureOnlyWithAlt";
            this.cbCaptureOnlyWithAlt.Size = new System.Drawing.Size(205, 17);
            this.cbCaptureOnlyWithAlt.TabIndex = 4;
            this.cbCaptureOnlyWithAlt.Text = "Capture window only if ALT is pressed";
            this.cbCaptureOnlyWithAlt.UseVisualStyleBackColor = true;
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(581, 336);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tcOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "OptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.OptionsForm_Load);
            this.tcOptions.ResumeLayout(false);
            this.tpMain.ResumeLayout(false);
            this.tpMain.PerformLayout();
            this.tpDefaults.ResumeLayout(false);
            this.tpDefaults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMainWindowWait)).EndInit();
            this.tpHotkeys.ResumeLayout(false);
            this.tpHotkeys.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcOptions;
        private System.Windows.Forms.TabPage tpMain;
        private System.Windows.Forms.TabPage tpDefaults;
        private System.Windows.Forms.CheckBox cbRemoveTitleBar;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabPage tpHotkeys;
        private System.Windows.Forms.ListView lvHotkeys;
        private System.Windows.Forms.ColumnHeader chAction;
        private System.Windows.Forms.ColumnHeader chKey;
        private HotKeyTextBox tbHotkey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timerHotkey;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.CheckBox cbSelectActivates;
        private System.Windows.Forms.CheckBox cbAutoHideList;
        private System.Windows.Forms.Button btnResetHotkeys;
        private System.Windows.Forms.CheckBox cbDisableHotkeys;
        private System.Windows.Forms.CheckBox cbCaptureWindows;
        private System.Windows.Forms.NumericUpDown nudMainWindowWait;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDoubleClick;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbConfirmOpenMultiple;
        private System.Windows.Forms.CheckBox cbDefaultForceSize;
        private System.Windows.Forms.CheckBox cbDefaultCloseTabIfEmpty;
        private System.Windows.Forms.CheckBox cbDefaultStartMinimized;
        private System.Windows.Forms.CheckBox cbDefaultRememberCapturedLocations;
        private System.Windows.Forms.CheckBox cbDefaultResizeToMaximize;
        private System.Windows.Forms.CheckBox cbAllowDragCapture;
        private System.Windows.Forms.CheckBox cbCaptureOnlyWithAlt;
    }
}