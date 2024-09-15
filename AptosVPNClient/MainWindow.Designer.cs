/* SPDX-License-Identifier: MIT
 *
 * Copyright (C) 2019-2022 WireGuard LLC. All Rights Reserved.
 */

using AptosVPNClient.Accessors;
using AptosVPNClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AptosVPNClient
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private async void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            connectButton = new Button();
            logBox = new TextBox();
            aptosInfoBox = new RichTextBox();
            vpnList = new DataGridView();
            nameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            priceDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            countryDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            addressDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            sellerDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            objectIdDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            vPNProviderBindingSource = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)vpnList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)vPNProviderBindingSource).BeginInit();
            SuspendLayout();
            // 
            // connectButton
            // 
            connectButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            connectButton.BackColor = System.Drawing.Color.FromArgb(49, 195, 183);
            connectButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            connectButton.FlatAppearance.BorderSize = 0;
            connectButton.FlatStyle = FlatStyle.Popup;
            connectButton.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            connectButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            connectButton.Location = new System.Drawing.Point(9, 1008);
            connectButton.Margin = new Padding(4);
            connectButton.Name = "connectButton";
            connectButton.Size = new System.Drawing.Size(1918, 50);
            connectButton.TabIndex = 5;
            connectButton.Text = "CONNECT VPN";
            connectButton.UseVisualStyleBackColor = false;
            connectButton.Click += connectButton_Click;
            // 
            // logBox
            // 
            logBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            logBox.BackColor = System.Drawing.Color.FromArgb(8, 26, 30);
            logBox.BorderStyle = BorderStyle.None;
            logBox.ForeColor = System.Drawing.Color.FromArgb(49, 195, 183);
            logBox.Location = new System.Drawing.Point(9, 1064);
            logBox.Margin = new Padding(2);
            logBox.Multiline = true;
            logBox.Name = "logBox";
            logBox.ReadOnly = true;
            logBox.Size = new System.Drawing.Size(1481, 169);
            logBox.TabIndex = 4;
            logBox.TabStop = false;
            logBox.TextChanged += logBox_TextChanged;
            // 
            // aptosInfoBox
            // 
            aptosInfoBox.BackColor = System.Drawing.Color.FromArgb(8, 26, 30);
            aptosInfoBox.BorderStyle = BorderStyle.None;
            aptosInfoBox.ForeColor = System.Drawing.Color.FromArgb(49, 195, 183);
            aptosInfoBox.Location = new System.Drawing.Point(1495, 1064);
            aptosInfoBox.Name = "aptosInfoBox";
            aptosInfoBox.Size = new System.Drawing.Size(432, 169);
            aptosInfoBox.TabIndex = 6;
            aptosInfoBox.Text = "";
            aptosInfoBox.TextChanged += aptosInfoBox_TextChanged;
            // 
            // vpnList
            // 
            vpnList.AllowUserToResizeRows = false;
            vpnList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            vpnList.AutoGenerateColumns = false;
            vpnList.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            vpnList.BorderStyle = BorderStyle.None;
            vpnList.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            vpnList.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            vpnList.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(8, 26, 30);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(8, 26, 30);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            vpnList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            vpnList.ColumnHeadersHeight = 80;
            vpnList.Columns.AddRange(new DataGridViewColumn[] { nameDataGridViewTextBoxColumn, priceDataGridViewTextBoxColumn, countryDataGridViewTextBoxColumn, addressDataGridViewTextBoxColumn, sellerDataGridViewTextBoxColumn, objectIdDataGridViewTextBoxColumn });
            vpnList.Cursor = Cursors.Hand;
            vpnList.DataSource = vPNProviderBindingSource;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(8, 26, 30);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(49, 195, 183);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(49, 195, 183);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(8, 26, 30);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            vpnList.DefaultCellStyle = dataGridViewCellStyle2;
            vpnList.EnableHeadersVisualStyles = false;
            vpnList.GridColor = System.Drawing.Color.FromArgb(8, 26, 30);
            vpnList.Location = new System.Drawing.Point(13, 12);
            vpnList.MultiSelect = false;
            vpnList.Name = "vpnList";
            vpnList.ReadOnly = true;
            vpnList.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(8, 26, 30);
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(49, 195, 183);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            vpnList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            vpnList.RowHeadersVisible = false;
            vpnList.RowHeadersWidth = 62;
            vpnList.RowTemplate.DividerHeight = 1;
            vpnList.RowTemplate.Height = 33;
            vpnList.RowTemplate.Resizable = DataGridViewTriState.True;
            vpnList.ScrollBars = ScrollBars.Vertical;
            vpnList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            vpnList.Size = new System.Drawing.Size(1914, 989);
            vpnList.TabIndex = 7;
            vpnList.CellContentClick += dataGridView1_CellContentClick;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            nameDataGridViewTextBoxColumn.DataPropertyName = "name";
            nameDataGridViewTextBoxColumn.HeaderText = "Provider Name";
            nameDataGridViewTextBoxColumn.MinimumWidth = 8;
            nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            nameDataGridViewTextBoxColumn.ReadOnly = true;
            nameDataGridViewTextBoxColumn.Width = 170;
            // 
            // priceDataGridViewTextBoxColumn
            // 
            priceDataGridViewTextBoxColumn.DataPropertyName = "price";
            priceDataGridViewTextBoxColumn.HeaderText = "Price in APT/24hrs";
            priceDataGridViewTextBoxColumn.MinimumWidth = 8;
            priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
            priceDataGridViewTextBoxColumn.ReadOnly = true;
            priceDataGridViewTextBoxColumn.Width = 150;
            // 
            // countryDataGridViewTextBoxColumn
            // 
            countryDataGridViewTextBoxColumn.DataPropertyName = "country";
            countryDataGridViewTextBoxColumn.HeaderText = "Country";
            countryDataGridViewTextBoxColumn.MinimumWidth = 8;
            countryDataGridViewTextBoxColumn.Name = "countryDataGridViewTextBoxColumn";
            countryDataGridViewTextBoxColumn.ReadOnly = true;
            countryDataGridViewTextBoxColumn.Width = 120;
            // 
            // addressDataGridViewTextBoxColumn
            // 
            addressDataGridViewTextBoxColumn.DataPropertyName = "address";
            addressDataGridViewTextBoxColumn.HeaderText = "address";
            addressDataGridViewTextBoxColumn.MinimumWidth = 8;
            addressDataGridViewTextBoxColumn.Name = "addressDataGridViewTextBoxColumn";
            addressDataGridViewTextBoxColumn.ReadOnly = true;
            addressDataGridViewTextBoxColumn.Visible = false;
            addressDataGridViewTextBoxColumn.Width = 150;
            // 
            // sellerDataGridViewTextBoxColumn
            // 
            sellerDataGridViewTextBoxColumn.DataPropertyName = "seller";
            sellerDataGridViewTextBoxColumn.HeaderText = "Seller Wallet ID";
            sellerDataGridViewTextBoxColumn.MinimumWidth = 8;
            sellerDataGridViewTextBoxColumn.Name = "sellerDataGridViewTextBoxColumn";
            sellerDataGridViewTextBoxColumn.ReadOnly = true;
            sellerDataGridViewTextBoxColumn.Width = 750;
            // 
            // objectIdDataGridViewTextBoxColumn
            // 
            objectIdDataGridViewTextBoxColumn.DataPropertyName = "objectId";
            objectIdDataGridViewTextBoxColumn.HeaderText = "Aptos Object ID";
            objectIdDataGridViewTextBoxColumn.MinimumWidth = 8;
            objectIdDataGridViewTextBoxColumn.Name = "objectIdDataGridViewTextBoxColumn";
            objectIdDataGridViewTextBoxColumn.ReadOnly = true;
            objectIdDataGridViewTextBoxColumn.Width = 740;
            // 
            // vPNProviderBindingSource
            // 
            vPNProviderBindingSource.DataSource = typeof(VPNProvider);
            vPNProviderBindingSource.CurrentChanged += vPNProviderBindingSource_CurrentChanged;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            ClientSize = new System.Drawing.Size(1935, 1242);
            Controls.Add(vpnList);
            Controls.Add(aptosInfoBox);
            Controls.Add(logBox);
            Controls.Add(connectButton);
            ForeColor = System.Drawing.Color.FromArgb(49, 195, 183);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(2);
            MaximizeBox = false;
            Name = "MainWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Aptos VPN Client";
            FormClosing += MainWindow_FormClosing;
            Load += MainWindow_Load;
            ((System.ComponentModel.ISupportInitialize)vpnList).EndInit();
            ((System.ComponentModel.ISupportInitialize)vPNProviderBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.RichTextBox aptosInfoBox;
        private System.Windows.Forms.DataGridView vpnList;
        private System.Windows.Forms.BindingSource vPNProviderBindingSource;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn countryDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn addressDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn sellerDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn objectIdDataGridViewTextBoxColumn;
    }
}

