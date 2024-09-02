/* SPDX-License-Identifier: MIT
 *
 * Copyright (C) 2019-2022 WireGuard LLC. All Rights Reserved.
 */

using DemoUI.Accessors;
using DemoUI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoUI
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
            connectButton.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            connectButton.Location = new System.Drawing.Point(9, 1008);
            connectButton.Margin = new Padding(4);
            connectButton.Name = "connectButton";
            connectButton.Size = new System.Drawing.Size(1918, 50);
            connectButton.TabIndex = 5;
            connectButton.Text = "CONNECT VPN";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += connectButton_Click;
            // 
            // logBox
            // 
            logBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            logBox.Location = new System.Drawing.Point(9, 1064);
            logBox.Margin = new Padding(2);
            logBox.Multiline = true;
            logBox.Name = "logBox";
            logBox.ReadOnly = true;
            logBox.ScrollBars = ScrollBars.Vertical;
            logBox.Size = new System.Drawing.Size(1481, 169);
            logBox.TabIndex = 4;
            logBox.TabStop = false;
            logBox.TextChanged += logBox_TextChanged;
            // 
            // aptosInfoBox
            // 
            aptosInfoBox.BackColor = System.Drawing.SystemColors.Window;
            aptosInfoBox.Location = new System.Drawing.Point(1495, 1064);
            aptosInfoBox.Name = "aptosInfoBox";
            aptosInfoBox.Size = new System.Drawing.Size(432, 169);
            aptosInfoBox.TabIndex = 6;
            aptosInfoBox.Text = "Wallet Address:\n0x1231231231231231231231231232\n\nAptos: 1.023";
            aptosInfoBox.TextChanged += aptosInfoBox_TextChanged;
            // 
            // vpnList
            // 
            vpnList.AllowUserToResizeRows = false;
            vpnList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            vpnList.AutoGenerateColumns = false;
            vpnList.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            vpnList.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            vpnList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            vpnList.Columns.AddRange(new DataGridViewColumn[] { nameDataGridViewTextBoxColumn, priceDataGridViewTextBoxColumn, countryDataGridViewTextBoxColumn, addressDataGridViewTextBoxColumn, sellerDataGridViewTextBoxColumn, objectIdDataGridViewTextBoxColumn });
            vpnList.Cursor = Cursors.Hand;
            vpnList.DataSource = vPNProviderBindingSource;
            vpnList.Location = new System.Drawing.Point(9, 12);
            vpnList.MultiSelect = false;
            vpnList.Name = "vpnList";
            vpnList.ReadOnly = true;
            vpnList.RowHeadersWidth = 62;
            vpnList.RowTemplate.DividerHeight = 1;
            vpnList.RowTemplate.Height = 33;
            vpnList.RowTemplate.Resizable = DataGridViewTriState.True;
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
            nameDataGridViewTextBoxColumn.Width = 150;
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
            countryDataGridViewTextBoxColumn.Width = 150;
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
            sellerDataGridViewTextBoxColumn.Width = 700;
            // 
            // objectIdDataGridViewTextBoxColumn
            // 
            objectIdDataGridViewTextBoxColumn.DataPropertyName = "objectId";
            objectIdDataGridViewTextBoxColumn.HeaderText = "Aptos Object ID";
            objectIdDataGridViewTextBoxColumn.MinimumWidth = 8;
            objectIdDataGridViewTextBoxColumn.Name = "objectIdDataGridViewTextBoxColumn";
            objectIdDataGridViewTextBoxColumn.ReadOnly = true;
            objectIdDataGridViewTextBoxColumn.Width = 700;
            // 
            // vPNProviderBindingSource
            // 
            vPNProviderBindingSource.DataSource = typeof(VPNProvider);
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
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(2);
            MaximizeBox = false;
            Name = "MainWindow";
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

