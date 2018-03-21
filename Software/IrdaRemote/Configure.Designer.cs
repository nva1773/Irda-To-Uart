using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IrdaRemote
{
    partial class Configure
    {
        // Form Component
        private IContainer components = null;
        private GroupBox gbPortName;
        private ComboBox cbPortName;
        private Button btPortOpenClose;
        private TextBox tbReceive;
        private Label lbReceive;
        private Timer tmrConfigure;
        private StatusStrip statusConfigure;
        private ToolStripStatusLabel StatusLabel;
        private Button btSetEnter;
        private GroupBox gbSetCommand;
        private Button btSetPageUp;
        private Button btSetRight;
        private Button bSetLeft;
        private Button btSetDown;
        private Button btSetUp;
        private Button btSetSpace;
        private Button btSetEsc;
        private Button btSetPageDown;
        private GroupBox gbCommandList;
        private TextBox tbPageDown;
        private TextBox tbPageUp;
        private TextBox tbRight;
        private TextBox tbLeft;
        private TextBox tbDown;
        private TextBox tbUp;
        private TextBox tbPause;
        private TextBox tbPlay;
        private TextBox tbOK;
        private GroupBox gbClearCommand;
        private Button btClearEnter;
        private Button btClearUp;
        private Button btClreaEsc;
        private Button btClearPageDown;
        private Button btClearSpace;
        private Button btClearPageUp;
        private Button btClearRight;
        private Button btClearDown;
        private Button btClearLeft;

        // Clean up any resources being used.
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Required method for Designer
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gbPortName = new System.Windows.Forms.GroupBox();
            this.cbPortName = new System.Windows.Forms.ComboBox();
            this.btPortOpenClose = new System.Windows.Forms.Button();
            this.tbReceive = new System.Windows.Forms.TextBox();
            this.lbReceive = new System.Windows.Forms.Label();
            this.tmrConfigure = new System.Windows.Forms.Timer(this.components);
            this.statusConfigure = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.btSetEnter = new System.Windows.Forms.Button();
            this.gbSetCommand = new System.Windows.Forms.GroupBox();
            this.btSetEsc = new System.Windows.Forms.Button();
            this.btSetSpace = new System.Windows.Forms.Button();
            this.btSetUp = new System.Windows.Forms.Button();
            this.btSetDown = new System.Windows.Forms.Button();
            this.bSetLeft = new System.Windows.Forms.Button();
            this.btSetRight = new System.Windows.Forms.Button();
            this.btSetPageUp = new System.Windows.Forms.Button();
            this.btSetPageDown = new System.Windows.Forms.Button();
            this.gbCommandList = new System.Windows.Forms.GroupBox();
            this.tbOK = new System.Windows.Forms.TextBox();
            this.tbPlay = new System.Windows.Forms.TextBox();
            this.tbPause = new System.Windows.Forms.TextBox();
            this.tbUp = new System.Windows.Forms.TextBox();
            this.tbDown = new System.Windows.Forms.TextBox();
            this.tbLeft = new System.Windows.Forms.TextBox();
            this.tbRight = new System.Windows.Forms.TextBox();
            this.tbPageUp = new System.Windows.Forms.TextBox();
            this.tbPageDown = new System.Windows.Forms.TextBox();
            this.gbClearCommand = new System.Windows.Forms.GroupBox();
            this.btClearEnter = new System.Windows.Forms.Button();
            this.btClearUp = new System.Windows.Forms.Button();
            this.btClreaEsc = new System.Windows.Forms.Button();
            this.btClearPageDown = new System.Windows.Forms.Button();
            this.btClearSpace = new System.Windows.Forms.Button();
            this.btClearPageUp = new System.Windows.Forms.Button();
            this.btClearRight = new System.Windows.Forms.Button();
            this.btClearDown = new System.Windows.Forms.Button();
            this.btClearLeft = new System.Windows.Forms.Button();
            this.gbPortName.SuspendLayout();
            this.statusConfigure.SuspendLayout();
            this.gbSetCommand.SuspendLayout();
            this.gbCommandList.SuspendLayout();
            this.gbClearCommand.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPortName
            // 
            this.gbPortName.Controls.Add(this.cbPortName);
            this.gbPortName.Location = new System.Drawing.Point(12, 12);
            this.gbPortName.Name = "gbPortName";
            this.gbPortName.Size = new System.Drawing.Size(100, 185);
            this.gbPortName.TabIndex = 0;
            this.gbPortName.TabStop = false;
            this.gbPortName.Text = "Com Port Name";
            // 
            // cbPortName
            // 
            this.cbPortName.FormattingEnabled = true;
            this.cbPortName.Location = new System.Drawing.Point(6, 19);
            this.cbPortName.Name = "cbPortName";
            this.cbPortName.Size = new System.Drawing.Size(88, 21);
            this.cbPortName.TabIndex = 1;
            this.cbPortName.SelectedIndexChanged += new System.EventHandler(this.cbPortName_SelectedIndexChanged);
            // 
            // btPortOpenClose
            // 
            this.btPortOpenClose.Enabled = false;
            this.btPortOpenClose.Location = new System.Drawing.Point(12, 207);
            this.btPortOpenClose.Name = "btPortOpenClose";
            this.btPortOpenClose.Size = new System.Drawing.Size(100, 22);
            this.btPortOpenClose.TabIndex = 2;
            this.btPortOpenClose.Text = "Open";
            this.btPortOpenClose.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btPortOpenClose.UseVisualStyleBackColor = true;
            this.btPortOpenClose.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btPortOpenClose_MouseClick);
            // 
            // tbReceive
            // 
            this.tbReceive.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbReceive.BackColor = System.Drawing.SystemColors.Window;
            this.tbReceive.ForeColor = System.Drawing.Color.Blue;
            this.tbReceive.Location = new System.Drawing.Point(12, 265);
            this.tbReceive.Name = "tbReceive";
            this.tbReceive.ReadOnly = true;
            this.tbReceive.Size = new System.Drawing.Size(100, 20);
            this.tbReceive.TabIndex = 4;
            this.tbReceive.Text = "NO DATA";
            this.tbReceive.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbReceive.TextChanged += new System.EventHandler(this.tbReceive_TextChanged);
            // 
            // lbReceive
            // 
            this.lbReceive.Location = new System.Drawing.Point(13, 244);
            this.lbReceive.Name = "lbReceive";
            this.lbReceive.Size = new System.Drawing.Size(99, 13);
            this.lbReceive.TabIndex = 3;
            this.lbReceive.Text = "Receive Message:";
            this.lbReceive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmrConfigure
            // 
            this.tmrConfigure.Tick += new System.EventHandler(this.tmrConfigure_Tick);
            // 
            // statusConfigure
            // 
            this.statusConfigure.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.statusConfigure.Location = new System.Drawing.Point(0, 314);
            this.statusConfigure.Name = "StatusStripConfigure";
            this.statusConfigure.Size = new System.Drawing.Size(539, 22);
            this.statusConfigure.TabIndex = 8;
            this.statusConfigure.Text = "statusConfigure";
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = false;
            this.StatusLabel.ForeColor = System.Drawing.Color.Black;
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(500, 17);
            this.StatusLabel.Text = "Port Status";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btSetEnter
            // 
            this.btSetEnter.Location = new System.Drawing.Point(15, 19);
            this.btSetEnter.Name = "btSetEnter";
            this.btSetEnter.Size = new System.Drawing.Size(100, 22);
            this.btSetEnter.TabIndex = 10;
            this.btSetEnter.Text = "Enter";
            this.btSetEnter.UseVisualStyleBackColor = true;
            this.btSetEnter.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btSetCommand_MouseClick);
            // 
            // gbSetCommand
            // 
            this.gbSetCommand.Controls.Add(this.btSetEnter);
            this.gbSetCommand.Controls.Add(this.btSetEsc);
            this.gbSetCommand.Controls.Add(this.btSetSpace);
            this.gbSetCommand.Controls.Add(this.btSetUp);
            this.gbSetCommand.Controls.Add(this.btSetDown);
            this.gbSetCommand.Controls.Add(this.bSetLeft);
            this.gbSetCommand.Controls.Add(this.btSetRight);
            this.gbSetCommand.Controls.Add(this.btSetPageUp);
            this.gbSetCommand.Controls.Add(this.btSetPageDown);
            this.gbSetCommand.Location = new System.Drawing.Point(131, 13);
            this.gbSetCommand.Name = "gbSetCommand";
            this.gbSetCommand.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.gbSetCommand.Size = new System.Drawing.Size(127, 287);
            this.gbSetCommand.TabIndex = 5;
            this.gbSetCommand.TabStop = false;
            this.gbSetCommand.Text = "Set Commad";
            // 
            // btSetEsc
            // 
            this.btSetEsc.Location = new System.Drawing.Point(15, 48);
            this.btSetEsc.Name = "btSetEsc";
            this.btSetEsc.Size = new System.Drawing.Size(100, 22);
            this.btSetEsc.TabIndex = 11;
            this.btSetEsc.Text = "Esc";
            this.btSetEsc.UseVisualStyleBackColor = true;
            this.btSetEsc.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btSetCommand_MouseClick);
            // 
            // btSetSpace
            // 
            this.btSetSpace.Location = new System.Drawing.Point(15, 77);
            this.btSetSpace.Name = "btSetSpace";
            this.btSetSpace.Size = new System.Drawing.Size(100, 22);
            this.btSetSpace.TabIndex = 12;
            this.btSetSpace.Text = "Space";
            this.btSetSpace.UseVisualStyleBackColor = true;
            this.btSetSpace.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btSetCommand_MouseClick);
            // 
            // btSetUp
            // 
            this.btSetUp.Location = new System.Drawing.Point(15, 106);
            this.btSetUp.Name = "btSetUp";
            this.btSetUp.Size = new System.Drawing.Size(100, 22);
            this.btSetUp.TabIndex = 13;
            this.btSetUp.Text = "Up";
            this.btSetUp.UseVisualStyleBackColor = true;
            this.btSetUp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btSetCommand_MouseClick);
            // 
            // btSetDown
            // 
            this.btSetDown.Location = new System.Drawing.Point(15, 135);
            this.btSetDown.Name = "btSetDown";
            this.btSetDown.Size = new System.Drawing.Size(100, 22);
            this.btSetDown.TabIndex = 14;
            this.btSetDown.Text = "Down";
            this.btSetDown.UseVisualStyleBackColor = true;
            this.btSetDown.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btSetCommand_MouseClick);
            // 
            // bSetLeft
            // 
            this.bSetLeft.Location = new System.Drawing.Point(15, 164);
            this.bSetLeft.Name = "bSetLeft";
            this.bSetLeft.Size = new System.Drawing.Size(100, 22);
            this.bSetLeft.TabIndex = 15;
            this.bSetLeft.Text = "Left";
            this.bSetLeft.UseVisualStyleBackColor = true;
            this.bSetLeft.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btSetCommand_MouseClick);
            // 
            // btSetRight
            // 
            this.btSetRight.AllowDrop = true;
            this.btSetRight.Location = new System.Drawing.Point(15, 193);
            this.btSetRight.Name = "btSetRight";
            this.btSetRight.Size = new System.Drawing.Size(100, 22);
            this.btSetRight.TabIndex = 16;
            this.btSetRight.Text = "Right";
            this.btSetRight.UseVisualStyleBackColor = true;
            this.btSetRight.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btSetCommand_MouseClick);
            // 
            // btSetPageUp
            // 
            this.btSetPageUp.Location = new System.Drawing.Point(15, 222);
            this.btSetPageUp.Name = "btSetPageUp";
            this.btSetPageUp.Size = new System.Drawing.Size(100, 22);
            this.btSetPageUp.TabIndex = 17;
            this.btSetPageUp.Text = "PageUp";
            this.btSetPageUp.UseVisualStyleBackColor = true;
            this.btSetPageUp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btSetCommand_MouseClick);
            // 
            // btSetPageDown
            // 
            this.btSetPageDown.Location = new System.Drawing.Point(15, 250);
            this.btSetPageDown.Name = "btSetPageDown";
            this.btSetPageDown.Size = new System.Drawing.Size(100, 22);
            this.btSetPageDown.TabIndex = 18;
            this.btSetPageDown.Text = "PageDown";
            this.btSetPageDown.UseVisualStyleBackColor = true;
            this.btSetPageDown.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btSetCommand_MouseClick);
            // 
            // gbCommandList
            // 
            this.gbCommandList.Controls.Add(this.tbOK);
            this.gbCommandList.Controls.Add(this.tbPlay);
            this.gbCommandList.Controls.Add(this.tbPause);
            this.gbCommandList.Controls.Add(this.tbUp);
            this.gbCommandList.Controls.Add(this.tbDown);
            this.gbCommandList.Controls.Add(this.tbLeft);
            this.gbCommandList.Controls.Add(this.tbRight);
            this.gbCommandList.Controls.Add(this.tbPageUp);
            this.gbCommandList.Controls.Add(this.tbPageDown);
            this.gbCommandList.Location = new System.Drawing.Point(265, 13);
            this.gbCommandList.Name = "gbCommandList";
            this.gbCommandList.Size = new System.Drawing.Size(127, 287);
            this.gbCommandList.TabIndex = 6;
            this.gbCommandList.TabStop = false;
            // 
            // tbOK
            // 
            this.tbOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbOK.BackColor = System.Drawing.Color.White;
            this.tbOK.ForeColor = System.Drawing.Color.Blue;
            this.tbOK.Location = new System.Drawing.Point(13, 19);
            this.tbOK.Name = "tbOK";
            this.tbOK.ReadOnly = true;
            this.tbOK.Size = new System.Drawing.Size(100, 20);
            this.tbOK.TabIndex = 20;
            this.tbOK.Text = "NO DATA";
            this.tbOK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbPlay
            // 
            this.tbPlay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbPlay.BackColor = System.Drawing.Color.White;
            this.tbPlay.ForeColor = System.Drawing.Color.Blue;
            this.tbPlay.Location = new System.Drawing.Point(13, 48);
            this.tbPlay.Name = "tbPlay";
            this.tbPlay.ReadOnly = true;
            this.tbPlay.Size = new System.Drawing.Size(100, 20);
            this.tbPlay.TabIndex = 21;
            this.tbPlay.Text = "NO DATA";
            this.tbPlay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbPause
            // 
            this.tbPause.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbPause.BackColor = System.Drawing.Color.White;
            this.tbPause.ForeColor = System.Drawing.Color.Blue;
            this.tbPause.Location = new System.Drawing.Point(13, 77);
            this.tbPause.Name = "tbPause";
            this.tbPause.ReadOnly = true;
            this.tbPause.Size = new System.Drawing.Size(100, 20);
            this.tbPause.TabIndex = 22;
            this.tbPause.Text = "NO DATA";
            this.tbPause.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbUp
            // 
            this.tbUp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbUp.BackColor = System.Drawing.Color.White;
            this.tbUp.ForeColor = System.Drawing.Color.Blue;
            this.tbUp.Location = new System.Drawing.Point(13, 106);
            this.tbUp.Name = "tbUp";
            this.tbUp.ReadOnly = true;
            this.tbUp.Size = new System.Drawing.Size(100, 20);
            this.tbUp.TabIndex = 23;
            this.tbUp.Text = "NO DATA";
            this.tbUp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbDown
            // 
            this.tbDown.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbDown.BackColor = System.Drawing.Color.White;
            this.tbDown.ForeColor = System.Drawing.Color.Blue;
            this.tbDown.Location = new System.Drawing.Point(13, 135);
            this.tbDown.Name = "tbDown";
            this.tbDown.ReadOnly = true;
            this.tbDown.Size = new System.Drawing.Size(100, 20);
            this.tbDown.TabIndex = 24;
            this.tbDown.Text = "NO DATA";
            this.tbDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbLeft
            // 
            this.tbLeft.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbLeft.BackColor = System.Drawing.Color.White;
            this.tbLeft.ForeColor = System.Drawing.Color.Blue;
            this.tbLeft.Location = new System.Drawing.Point(13, 164);
            this.tbLeft.Name = "tbLeft";
            this.tbLeft.ReadOnly = true;
            this.tbLeft.Size = new System.Drawing.Size(100, 20);
            this.tbLeft.TabIndex = 25;
            this.tbLeft.Text = "NO DATA";
            this.tbLeft.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbRight
            // 
            this.tbRight.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbRight.BackColor = System.Drawing.Color.White;
            this.tbRight.ForeColor = System.Drawing.Color.Blue;
            this.tbRight.Location = new System.Drawing.Point(13, 195);
            this.tbRight.Name = "tbRight";
            this.tbRight.ReadOnly = true;
            this.tbRight.Size = new System.Drawing.Size(100, 20);
            this.tbRight.TabIndex = 26;
            this.tbRight.Text = "NO DATA";
            this.tbRight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbPageUp
            // 
            this.tbPageUp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbPageUp.BackColor = System.Drawing.Color.White;
            this.tbPageUp.ForeColor = System.Drawing.Color.Blue;
            this.tbPageUp.Location = new System.Drawing.Point(13, 222);
            this.tbPageUp.Name = "tbPageUp";
            this.tbPageUp.ReadOnly = true;
            this.tbPageUp.Size = new System.Drawing.Size(100, 20);
            this.tbPageUp.TabIndex = 27;
            this.tbPageUp.Text = "NO DATA";
            this.tbPageUp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbPageDown
            // 
            this.tbPageDown.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbPageDown.BackColor = System.Drawing.Color.White;
            this.tbPageDown.ForeColor = System.Drawing.Color.Blue;
            this.tbPageDown.Location = new System.Drawing.Point(13, 250);
            this.tbPageDown.Name = "tbPageDown";
            this.tbPageDown.ReadOnly = true;
            this.tbPageDown.Size = new System.Drawing.Size(100, 20);
            this.tbPageDown.TabIndex = 28;
            this.tbPageDown.Text = "NO DATA";
            this.tbPageDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gbClearCommand
            // 
            this.gbClearCommand.Controls.Add(this.btClearEnter);
            this.gbClearCommand.Controls.Add(this.btClearUp);
            this.gbClearCommand.Controls.Add(this.btClreaEsc);
            this.gbClearCommand.Controls.Add(this.btClearPageDown);
            this.gbClearCommand.Controls.Add(this.btClearSpace);
            this.gbClearCommand.Controls.Add(this.btClearPageUp);
            this.gbClearCommand.Controls.Add(this.btClearRight);
            this.gbClearCommand.Controls.Add(this.btClearDown);
            this.gbClearCommand.Controls.Add(this.btClearLeft);
            this.gbClearCommand.Location = new System.Drawing.Point(399, 13);
            this.gbClearCommand.Name = "gbClearCommand";
            this.gbClearCommand.Size = new System.Drawing.Size(127, 287);
            this.gbClearCommand.TabIndex = 7;
            this.gbClearCommand.TabStop = false;
            this.gbClearCommand.Text = "Clear Commad";
            // 
            // btClearEnter
            // 
            this.btClearEnter.Location = new System.Drawing.Point(13, 19);
            this.btClearEnter.Name = "btClearEnter";
            this.btClearEnter.Size = new System.Drawing.Size(100, 22);
            this.btClearEnter.TabIndex = 30;
            this.btClearEnter.Text = "Enter";
            this.btClearEnter.UseVisualStyleBackColor = true;
            this.btClearEnter.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btClearCommand_MouseClick);
            // 
            // btClearUp
            // 
            this.btClearUp.Location = new System.Drawing.Point(13, 106);
            this.btClearUp.Name = "btClearUp";
            this.btClearUp.Size = new System.Drawing.Size(100, 22);
            this.btClearUp.TabIndex = 33;
            this.btClearUp.Text = "Up";
            this.btClearUp.UseVisualStyleBackColor = true;
            this.btClearUp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btClearCommand_MouseClick);
            // 
            // btClreaEsc
            // 
            this.btClreaEsc.Location = new System.Drawing.Point(13, 48);
            this.btClreaEsc.Name = "btClreaEsc";
            this.btClreaEsc.Size = new System.Drawing.Size(100, 22);
            this.btClreaEsc.TabIndex = 31;
            this.btClreaEsc.Text = "Esc";
            this.btClreaEsc.UseVisualStyleBackColor = true;
            this.btClreaEsc.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btClearCommand_MouseClick);
            // 
            // btClearPageDown
            // 
            this.btClearPageDown.Location = new System.Drawing.Point(13, 250);
            this.btClearPageDown.Name = "btClearPageDown";
            this.btClearPageDown.Size = new System.Drawing.Size(100, 22);
            this.btClearPageDown.TabIndex = 38;
            this.btClearPageDown.Text = "PageDown";
            this.btClearPageDown.UseVisualStyleBackColor = true;
            this.btClearPageDown.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btClearCommand_MouseClick);
            // 
            // btClearSpace
            // 
            this.btClearSpace.Location = new System.Drawing.Point(13, 77);
            this.btClearSpace.Name = "btClearSpace";
            this.btClearSpace.Size = new System.Drawing.Size(100, 22);
            this.btClearSpace.TabIndex = 32;
            this.btClearSpace.Text = "Space";
            this.btClearSpace.UseVisualStyleBackColor = true;
            this.btClearSpace.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btClearCommand_MouseClick);
            // 
            // btClearPageUp
            // 
            this.btClearPageUp.Location = new System.Drawing.Point(13, 222);
            this.btClearPageUp.Name = "btClearPageUp";
            this.btClearPageUp.Size = new System.Drawing.Size(100, 22);
            this.btClearPageUp.TabIndex = 37;
            this.btClearPageUp.Text = "PageUp";
            this.btClearPageUp.UseVisualStyleBackColor = true;
            this.btClearPageUp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btClearCommand_MouseClick);
            // 
            // btClearRight
            // 
            this.btClearRight.AllowDrop = true;
            this.btClearRight.Location = new System.Drawing.Point(13, 193);
            this.btClearRight.Name = "btClearRight";
            this.btClearRight.Size = new System.Drawing.Size(100, 22);
            this.btClearRight.TabIndex = 36;
            this.btClearRight.Text = "Right";
            this.btClearRight.UseVisualStyleBackColor = true;
            this.btClearRight.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btClearCommand_MouseClick);
            // 
            // btClearDown
            // 
            this.btClearDown.Location = new System.Drawing.Point(13, 135);
            this.btClearDown.Name = "btClearDown";
            this.btClearDown.Size = new System.Drawing.Size(100, 22);
            this.btClearDown.TabIndex = 34;
            this.btClearDown.Text = "Down";
            this.btClearDown.UseVisualStyleBackColor = true;
            this.btClearDown.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btClearCommand_MouseClick);
            // 
            // btClearLeft
            // 
            this.btClearLeft.Location = new System.Drawing.Point(13, 164);
            this.btClearLeft.Name = "btClearLeft";
            this.btClearLeft.Size = new System.Drawing.Size(100, 22);
            this.btClearLeft.TabIndex = 35;
            this.btClearLeft.Text = "Left";
            this.btClearLeft.UseVisualStyleBackColor = true;
            this.btClearLeft.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btClearCommand_MouseClick);
            // 
            // Configure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 336);
            this.Controls.Add(this.gbClearCommand);
            this.Controls.Add(this.gbCommandList);
            this.Controls.Add(this.gbSetCommand);
            this.Controls.Add(this.statusConfigure);
            this.Controls.Add(this.lbReceive);
            this.Controls.Add(this.tbReceive);
            this.Controls.Add(this.btPortOpenClose);
            this.Controls.Add(this.gbPortName);
            this.Icon = Properties.Resources.Irda_Remote_64px;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Configure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Irda Remote Control";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Configure_FormClosed);
			//
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Configure_KeyDown);
			//
            this.gbPortName.ResumeLayout(false);
            this.statusConfigure.ResumeLayout(false);
            this.statusConfigure.PerformLayout();
            this.gbSetCommand.ResumeLayout(false);
            this.gbCommandList.ResumeLayout(false);
            this.gbCommandList.PerformLayout();
            this.gbClearCommand.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}