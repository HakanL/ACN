﻿namespace StreamingAcn
{
    partial class StreamingAcnSnoop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StreamingAcnSnoop));
            this.dataTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.portGrid = new System.Windows.Forms.DataGridView();
            this.receiveTab = new System.Windows.Forms.TabPage();
            this.channelArea = new System.Windows.Forms.FlowLayoutPanel();
            this.sendTab = new System.Windows.Forms.TabPage();
            this.sendChannelArea = new System.Windows.Forms.FlowLayoutPanel();
            this.levelGroup = new System.Windows.Forms.GroupBox();
            this.levelBar = new System.Windows.Forms.TrackBar();
            this.levelZero = new System.Windows.Forms.Button();
            this.levelFull = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.receiveSelect = new System.Windows.Forms.ToolStripButton();
            this.sendSelect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.networkCardSelect = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.dataTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portGrid)).BeginInit();
            this.receiveTab.SuspendLayout();
            this.sendTab.SuspendLayout();
            this.levelGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.levelBar)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataTabs
            // 
            this.dataTabs.Controls.Add(this.tabPage1);
            this.dataTabs.Controls.Add(this.receiveTab);
            this.dataTabs.Controls.Add(this.sendTab);
            this.dataTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataTabs.Location = new System.Drawing.Point(0, 38);
            this.dataTabs.Name = "dataTabs";
            this.dataTabs.SelectedIndex = 0;
            this.dataTabs.Size = new System.Drawing.Size(747, 396);
            this.dataTabs.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.portGrid);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(739, 370);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Ports";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // portGrid
            // 
            this.portGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.portGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.portGrid.Location = new System.Drawing.Point(3, 3);
            this.portGrid.Name = "portGrid";
            this.portGrid.Size = new System.Drawing.Size(733, 364);
            this.portGrid.TabIndex = 0;
            this.portGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.portGrid_CellBeginEdit);
            // 
            // receiveTab
            // 
            this.receiveTab.Controls.Add(this.channelArea);
            this.receiveTab.Location = new System.Drawing.Point(4, 22);
            this.receiveTab.Name = "receiveTab";
            this.receiveTab.Padding = new System.Windows.Forms.Padding(3);
            this.receiveTab.Size = new System.Drawing.Size(739, 370);
            this.receiveTab.TabIndex = 0;
            this.receiveTab.Text = "Receive";
            this.receiveTab.UseVisualStyleBackColor = true;
            // 
            // channelArea
            // 
            this.channelArea.AutoScroll = true;
            this.channelArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.channelArea.Location = new System.Drawing.Point(3, 3);
            this.channelArea.Name = "channelArea";
            this.channelArea.Size = new System.Drawing.Size(733, 364);
            this.channelArea.TabIndex = 0;
            // 
            // sendTab
            // 
            this.sendTab.Controls.Add(this.sendChannelArea);
            this.sendTab.Controls.Add(this.levelGroup);
            this.sendTab.Location = new System.Drawing.Point(4, 22);
            this.sendTab.Name = "sendTab";
            this.sendTab.Padding = new System.Windows.Forms.Padding(3);
            this.sendTab.Size = new System.Drawing.Size(739, 370);
            this.sendTab.TabIndex = 1;
            this.sendTab.Text = "Send";
            this.sendTab.UseVisualStyleBackColor = true;
            // 
            // sendChannelArea
            // 
            this.sendChannelArea.AutoScroll = true;
            this.sendChannelArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sendChannelArea.Location = new System.Drawing.Point(52, 3);
            this.sendChannelArea.Name = "sendChannelArea";
            this.sendChannelArea.Size = new System.Drawing.Size(684, 364);
            this.sendChannelArea.TabIndex = 1;
            // 
            // levelGroup
            // 
            this.levelGroup.Controls.Add(this.levelBar);
            this.levelGroup.Controls.Add(this.levelZero);
            this.levelGroup.Controls.Add(this.levelFull);
            this.levelGroup.Dock = System.Windows.Forms.DockStyle.Left;
            this.levelGroup.Enabled = false;
            this.levelGroup.Location = new System.Drawing.Point(3, 3);
            this.levelGroup.Name = "levelGroup";
            this.levelGroup.Size = new System.Drawing.Size(49, 364);
            this.levelGroup.TabIndex = 0;
            this.levelGroup.TabStop = false;
            this.levelGroup.Text = "Level";
            // 
            // levelBar
            // 
            this.levelBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.levelBar.Location = new System.Drawing.Point(3, 53);
            this.levelBar.Maximum = 255;
            this.levelBar.Name = "levelBar";
            this.levelBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.levelBar.Size = new System.Drawing.Size(43, 271);
            this.levelBar.TabIndex = 2;
            this.levelBar.TickFrequency = 16;
            this.levelBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.levelBar.Scroll += new System.EventHandler(this.levelBar_Scroll);
            // 
            // levelZero
            // 
            this.levelZero.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.levelZero.Location = new System.Drawing.Point(3, 324);
            this.levelZero.Name = "levelZero";
            this.levelZero.Size = new System.Drawing.Size(43, 37);
            this.levelZero.TabIndex = 1;
            this.levelZero.Text = "Zero";
            this.levelZero.UseVisualStyleBackColor = true;
            this.levelZero.Click += new System.EventHandler(this.levelZero_Click);
            // 
            // levelFull
            // 
            this.levelFull.Dock = System.Windows.Forms.DockStyle.Top;
            this.levelFull.Location = new System.Drawing.Point(3, 16);
            this.levelFull.Name = "levelFull";
            this.levelFull.Size = new System.Drawing.Size(43, 37);
            this.levelFull.TabIndex = 0;
            this.levelFull.Text = "Full";
            this.levelFull.UseVisualStyleBackColor = true;
            this.levelFull.Click += new System.EventHandler(this.levelFull_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.receiveSelect,
            this.sendSelect,
            this.toolStripSeparator1,
            this.networkCardSelect,
            this.toolStripLabel1,
            this.toolStripTextBox1,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(747, 38);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // receiveSelect
            // 
            this.receiveSelect.Checked = true;
            this.receiveSelect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.receiveSelect.Image = ((System.Drawing.Image)(resources.GetObject("receiveSelect.Image")));
            this.receiveSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.receiveSelect.Name = "receiveSelect";
            this.receiveSelect.Size = new System.Drawing.Size(51, 35);
            this.receiveSelect.Text = "Receive";
            this.receiveSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.receiveSelect.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // sendSelect
            // 
            this.sendSelect.Image = ((System.Drawing.Image)(resources.GetObject("sendSelect.Image")));
            this.sendSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sendSelect.Name = "sendSelect";
            this.sendSelect.Size = new System.Drawing.Size(37, 35);
            this.sendSelect.Text = "Send";
            this.sendSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.sendSelect.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // networkCardSelect
            // 
            this.networkCardSelect.DropDownWidth = 600;
            this.networkCardSelect.Name = "networkCardSelect";
            this.networkCardSelect.Size = new System.Drawing.Size(250, 38);
            this.networkCardSelect.SelectedIndexChanged += new System.EventHandler(this.networkCardSelect_SelectedIndexChanged);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(55, 35);
            this.toolStripLabel1.Text = "Universe:";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(50, 38);
            this.toolStripTextBox1.Text = "1";
            this.toolStripTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox1_KeyDown);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(65, 35);
            this.toolStripButton1.Text = "Read Only";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // StreamingAcnSnoop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 434);
            this.Controls.Add(this.dataTabs);
            this.Controls.Add(this.toolStrip1);
            this.Name = "StreamingAcnSnoop";
            this.Text = "sACN Snoop";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StreamingAcnSnoop_FormClosing);
            this.dataTabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.portGrid)).EndInit();
            this.receiveTab.ResumeLayout(false);
            this.sendTab.ResumeLayout(false);
            this.levelGroup.ResumeLayout(false);
            this.levelGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.levelBar)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl dataTabs;
        private System.Windows.Forms.TabPage receiveTab;
        private System.Windows.Forms.TabPage sendTab;
        private System.Windows.Forms.FlowLayoutPanel channelArea;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox networkCardSelect;
        private System.Windows.Forms.FlowLayoutPanel sendChannelArea;
        private System.Windows.Forms.GroupBox levelGroup;
        private System.Windows.Forms.TrackBar levelBar;
        private System.Windows.Forms.Button levelZero;
        private System.Windows.Forms.Button levelFull;
        private System.Windows.Forms.ToolStripButton receiveSelect;
        private System.Windows.Forms.ToolStripButton sendSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView portGrid;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}

