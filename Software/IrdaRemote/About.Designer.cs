using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IrdaRemote
{
    partial class About
    {
        // Form Component
        private IContainer components = null;
        private PictureBox pbAbout;
        private Label lbName;
        private Label lbAuthor;
        private Label lbCopyright;
        private Label lbDescription;
        private PictureBox pbShem;

        // Clean up any resources being used.
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Initialization
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pbAbout = new System.Windows.Forms.PictureBox();
            this.lbName = new System.Windows.Forms.Label();
            this.lbAuthor = new System.Windows.Forms.Label();
            this.lbCopyright = new System.Windows.Forms.Label();
            this.lbDescription = new System.Windows.Forms.Label();
            this.pbShem = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbAbout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbShem)).BeginInit();
            this.SuspendLayout();
            // 
            // pbAbout
            //
            this.pbAbout.BackColor = Color.Transparent;
            this.pbAbout.Image = Properties.Resources.Irda_About_64px;
            this.pbAbout.Location = new System.Drawing.Point(12, 12);
            this.pbAbout.Name = "pbAbout";
            this.pbAbout.Size = new System.Drawing.Size(64, 64);
            this.pbAbout.TabIndex = 1;
            this.pbAbout.TabStop = false;
            // 
            // lbName
            // 
            this.lbName.Location = new System.Drawing.Point(90, 12);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(435, 18);
            this.lbName.TabIndex = 2;
            this.lbName.Text = "Irda Remote Control";
            // 
            // lbAuthor
            // 
            this.lbAuthor.Location = new System.Drawing.Point(90, 30);
            this.lbAuthor.Name = "lbAuthor";
            this.lbAuthor.Size = new System.Drawing.Size(435, 18);
            this.lbAuthor.TabIndex = 3;
            this.lbAuthor.Text = "Author: Novikov Vlad";
            // 
            // lbDescription
            // 
            this.lbDescription.Location = new System.Drawing.Point(90, 48);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(435, 28);
            this.lbDescription.TabIndex = 4;
            this.lbDescription.Text = "Description: control application that is currently active. Commands are received from the device connected to the Com port every 100 ms. The circuit device is shown below:";
            // 
            // pbShem
            // 
            this.pbShem.Image = Properties.Resources.Irda_Shem_514px;
            this.pbShem.BackColor = System.Drawing.Color.White;
            this.pbShem.Location = new System.Drawing.Point(12, 90);
            this.pbShem.Name = "pbShem";
            this.pbShem.Size = new System.Drawing.Size(514, 200);
            this.pbShem.TabIndex = 5;
            this.pbShem.TabStop = false;
            // 
            // lbCopyright
            // 
            this.lbCopyright.Location = new System.Drawing.Point(12, 301);
            this.lbCopyright.Name = "lbCopyright";
            this.lbCopyright.Size = new System.Drawing.Size(514, 23);
            this.lbCopyright.TabIndex = 6;
            this.lbCopyright.Text = "Copyright © NVA 2016";
            this.lbCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 336);
            this.Controls.Add(this.pbAbout);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.lbAuthor);
            this.Controls.Add(this.lbDescription);
            this.Controls.Add(this.pbShem);
            this.Controls.Add(this.lbCopyright);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            // Key Down
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(About_KeyDown);
            //
            ((System.ComponentModel.ISupportInitialize)(this.pbAbout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbShem)).EndInit();
            this.ResumeLayout(false);
        }
    }
}