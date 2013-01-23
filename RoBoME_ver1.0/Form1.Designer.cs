﻿namespace RoBoME_ver1._0
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howToUseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.autocheck = new System.Windows.Forms.CheckBox();
            this.capturebutton = new System.Windows.Forms.Button();
            this.delaytext = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.typecombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Framelist = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.triggerlist = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.MotionTest = new System.Windows.Forms.Button();
            this.NewMotion = new System.Windows.Forms.Button();
            this.Motionlist = new System.Windows.Forms.ListBox();
            this.MotionCombo = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(624, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.actionToolStripMenuItem,
            this.saveFileToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.newToolStripMenuItem.Text = "File";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.fileToolStripMenuItem.Text = "New File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // actionToolStripMenuItem
            // 
            this.actionToolStripMenuItem.Name = "actionToolStripMenuItem";
            this.actionToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.actionToolStripMenuItem.Text = "Open File";
            this.actionToolStripMenuItem.Click += new System.EventHandler(this.actionToolStripMenuItem_Click);
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.saveFileToolStripMenuItem.Text = "Save File";
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.howToUseToolStripMenuItem,
            this.optionToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // howToUseToolStripMenuItem
            // 
            this.howToUseToolStripMenuItem.Name = "howToUseToolStripMenuItem";
            this.howToUseToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.howToUseToolStripMenuItem.Text = "Tutorials";
            this.howToUseToolStripMenuItem.Click += new System.EventHandler(this.howToUseToolStripMenuItem_Click);
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.optionToolStripMenuItem.Text = "Options";
            this.optionToolStripMenuItem.Click += new System.EventHandler(this.optionToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.Framelist);
            this.groupBox1.Location = new System.Drawing.Point(13, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 368);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Object";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.autocheck);
            this.panel1.Controls.Add(this.capturebutton);
            this.panel1.Controls.Add(this.delaytext);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.typecombo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(303, 67);
            this.panel1.TabIndex = 4;
            // 
            // autocheck
            // 
            this.autocheck.AutoSize = true;
            this.autocheck.Location = new System.Drawing.Point(226, 40);
            this.autocheck.Name = "autocheck";
            this.autocheck.Size = new System.Drawing.Size(70, 16);
            this.autocheck.TabIndex = 9;
            this.autocheck.Text = "Auto Play";
            this.autocheck.UseVisualStyleBackColor = true;
            this.autocheck.CheckedChanged += new System.EventHandler(this.autocheck_CheckedChanged);
            // 
            // capturebutton
            // 
            this.capturebutton.Location = new System.Drawing.Point(221, 7);
            this.capturebutton.Name = "capturebutton";
            this.capturebutton.Size = new System.Drawing.Size(75, 23);
            this.capturebutton.TabIndex = 7;
            this.capturebutton.Text = "Capture";
            this.capturebutton.UseMnemonic = false;
            this.capturebutton.UseVisualStyleBackColor = true;
            this.capturebutton.Click += new System.EventHandler(this.capturebutton_Click);
            // 
            // delaytext
            // 
            this.delaytext.Location = new System.Drawing.Point(80, 36);
            this.delaytext.Name = "delaytext";
            this.delaytext.Size = new System.Drawing.Size(135, 22);
            this.delaytext.TabIndex = 6;
            this.delaytext.Text = "0";
            this.delaytext.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.delaytext.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.delaytext_KeyPress);
            this.delaytext.TextChanged += new System.EventHandler(this.delaytext_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Delay (ms):";
            // 
            // typecombo
            // 
            this.typecombo.FormattingEnabled = true;
            this.typecombo.Items.AddRange(new object[] {
            "Frame",
            "Delay",
            "Sound",
            "Goto",
            "Flag"});
            this.typecombo.Location = new System.Drawing.Point(80, 10);
            this.typecombo.Name = "typecombo";
            this.typecombo.Size = new System.Drawing.Size(135, 20);
            this.typecombo.TabIndex = 0;
            this.typecombo.TextChanged += new System.EventHandler(this.typecombo_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Object Type:";
            // 
            // Framelist
            // 
            this.Framelist.AutoScroll = true;
            this.Framelist.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Framelist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Framelist.Location = new System.Drawing.Point(12, 94);
            this.Framelist.Name = "Framelist";
            this.Framelist.Size = new System.Drawing.Size(303, 270);
            this.Framelist.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.triggerlist);
            this.groupBox2.Location = new System.Drawing.Point(13, 402);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(321, 132);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Trigger";
            // 
            // triggerlist
            // 
            this.triggerlist.FormattingEnabled = true;
            this.triggerlist.ItemHeight = 12;
            this.triggerlist.Location = new System.Drawing.Point(12, 21);
            this.triggerlist.Name = "triggerlist";
            this.triggerlist.ScrollAlwaysVisible = true;
            this.triggerlist.Size = new System.Drawing.Size(303, 100);
            this.triggerlist.TabIndex = 0;
            this.triggerlist.MouseDown += new System.Windows.Forms.MouseEventHandler(this.triggerlist_MouseDown);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.MotionTest);
            this.groupBox3.Controls.Add(this.NewMotion);
            this.groupBox3.Controls.Add(this.Motionlist);
            this.groupBox3.Controls.Add(this.MotionCombo);
            this.groupBox3.Location = new System.Drawing.Point(340, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(273, 506);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Motion";
            // 
            // MotionTest
            // 
            this.MotionTest.Location = new System.Drawing.Point(142, 44);
            this.MotionTest.Name = "MotionTest";
            this.MotionTest.Size = new System.Drawing.Size(125, 23);
            this.MotionTest.TabIndex = 3;
            this.MotionTest.Text = "Motion Test";
            this.MotionTest.UseVisualStyleBackColor = true;
            this.MotionTest.Click += new System.EventHandler(this.MotionTest_Click);
            this.MotionTest.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MotionTest_KeyDown);
            // 
            // NewMotion
            // 
            this.NewMotion.Cursor = System.Windows.Forms.Cursors.Cross;
            this.NewMotion.Location = new System.Drawing.Point(7, 44);
            this.NewMotion.Name = "NewMotion";
            this.NewMotion.Size = new System.Drawing.Size(125, 23);
            this.NewMotion.TabIndex = 2;
            this.NewMotion.Text = "New Motion";
            this.NewMotion.UseVisualStyleBackColor = true;
            this.NewMotion.Click += new System.EventHandler(this.NewMotion_Click);
            // 
            // Motionlist
            // 
            this.Motionlist.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Motionlist.FormattingEnabled = true;
            this.Motionlist.IntegralHeight = false;
            this.Motionlist.ItemHeight = 24;
            this.Motionlist.Location = new System.Drawing.Point(7, 73);
            this.Motionlist.Name = "Motionlist";
            this.Motionlist.ScrollAlwaysVisible = true;
            this.Motionlist.Size = new System.Drawing.Size(261, 422);
            this.Motionlist.TabIndex = 1;
            this.Motionlist.SelectedIndexChanged += new System.EventHandler(this.Motionlist_SelectedIndexChanged);
            this.Motionlist.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Motionlist_MouseDown);
            // 
            // MotionCombo
            // 
            this.MotionCombo.FormattingEnabled = true;
            this.MotionCombo.Location = new System.Drawing.Point(7, 18);
            this.MotionCombo.Name = "MotionCombo";
            this.MotionCombo.Size = new System.Drawing.Size(260, 20);
            this.MotionCombo.TabIndex = 0;
            this.MotionCombo.SelectedIndexChanged += new System.EventHandler(this.MotionCombo_SelectedIndexChanged);
            this.MotionCombo.TextChanged += new System.EventHandler(this.MotionCombo_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(624, 542);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(640, 580);
            this.MinimumSize = new System.Drawing.Size(640, 580);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RoBoard Motion Editor-Ver1.1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem howToUseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.FlowLayoutPanel Framelist;
        private System.Windows.Forms.ListBox Motionlist;
        private System.Windows.Forms.ComboBox MotionCombo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox delaytext;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox typecombo;
        private System.Windows.Forms.ListBox triggerlist;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button NewMotion;
        private System.Windows.Forms.Button capturebutton;
        private System.Windows.Forms.Button MotionTest;
        private System.Windows.Forms.CheckBox autocheck;
    }
}

