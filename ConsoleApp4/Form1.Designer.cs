﻿namespace ConsoleApp4
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
                  this.dataGridView1 = new System.Windows.Forms.DataGridView();
                  this.NameReceipt = new System.Windows.Forms.DataGridViewTextBoxColumn();
                  this.Cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
                  this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
                  this.SellCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
                  this.CreatePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
                  this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
                  this.Ingredients = new System.Windows.Forms.DataGridViewTextBoxColumn();
                  this.Edit = new System.Windows.Forms.DataGridViewButtonColumn();
                  this.button1 = new System.Windows.Forms.Button();
                  this.button2 = new System.Windows.Forms.Button();
                  ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
                  this.SuspendLayout();
                  // 
                  // dataGridView1
                  // 
                  this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                  this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameReceipt,
            this.Cost,
            this.Count,
            this.SellCount,
            this.CreatePrice,
            this.Price,
            this.Ingredients,
            this.Edit});
                  this.dataGridView1.Location = new System.Drawing.Point(12, 12);
                  this.dataGridView1.Name = "dataGridView1";
                  this.dataGridView1.Size = new System.Drawing.Size(850, 746);
                  this.dataGridView1.TabIndex = 0;
                  this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
                  // 
                  // NameReceipt
                  // 
                  this.NameReceipt.HeaderText = "Name";
                  this.NameReceipt.Name = "NameReceipt";
                  this.NameReceipt.ReadOnly = true;
                  // 
                  // Cost
                  // 
                  this.Cost.HeaderText = "Cost";
                  this.Cost.Name = "Cost";
                  this.Cost.ReadOnly = true;
                  // 
                  // Count
                  // 
                  this.Count.HeaderText = "Count";
                  this.Count.Name = "Count";
                  this.Count.ReadOnly = true;
                  // 
                  // SellCount
                  // 
                  this.SellCount.HeaderText = "SellCount";
                  this.SellCount.Name = "SellCount";
                  this.SellCount.ReadOnly = true;
                  // 
                  // CreatePrice
                  // 
                  this.CreatePrice.HeaderText = "CreatePrice";
                  this.CreatePrice.Name = "CreatePrice";
                  this.CreatePrice.ReadOnly = true;
                  // 
                  // Price
                  // 
                  this.Price.HeaderText = "Price";
                  this.Price.Name = "Price";
                  this.Price.ReadOnly = true;
                  // 
                  // Ingredients
                  // 
                  this.Ingredients.HeaderText = "Ingredients";
                  this.Ingredients.Name = "Ingredients";
                  this.Ingredients.ReadOnly = true;
                  // 
                  // Edit
                  // 
                  this.Edit.HeaderText = "Edit";
                  this.Edit.Name = "Edit";
                  this.Edit.Text = "Edit";
                  // 
                  // button1
                  // 
                  this.button1.Location = new System.Drawing.Point(750, 764);
                  this.button1.Name = "button1";
                  this.button1.Size = new System.Drawing.Size(112, 23);
                  this.button1.TabIndex = 1;
                  this.button1.Text = "AddReceipt";
                  this.button1.UseVisualStyleBackColor = true;
                  this.button1.Click += new System.EventHandler(this.button1_Click);
                  // 
                  // button2
                  // 
                  this.button2.Location = new System.Drawing.Point(632, 764);
                  this.button2.Name = "button2";
                  this.button2.Size = new System.Drawing.Size(112, 23);
                  this.button2.TabIndex = 2;
                  this.button2.Text = "RemoveReceipt";
                  this.button2.UseVisualStyleBackColor = true;
                  this.button2.Click += new System.EventHandler(this.button2_Click);
                  // 
                  // Form1
                  // 
                  this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                  this.ClientSize = new System.Drawing.Size(874, 794);
                  this.Controls.Add(this.button2);
                  this.Controls.Add(this.button1);
                  this.Controls.Add(this.dataGridView1);
                  this.Name = "Form1";
                  this.Text = "Recepies list";
                  ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
                  this.ResumeLayout(false);

            }

            #endregion

            private System.Windows.Forms.DataGridView dataGridView1;
            private System.Windows.Forms.DataGridViewTextBoxColumn NameReceipt;
            private System.Windows.Forms.DataGridViewTextBoxColumn Cost;
            private System.Windows.Forms.DataGridViewTextBoxColumn Count;
            private System.Windows.Forms.DataGridViewTextBoxColumn SellCount;
            private System.Windows.Forms.DataGridViewTextBoxColumn CreatePrice;
            private System.Windows.Forms.DataGridViewTextBoxColumn Price;
            private System.Windows.Forms.DataGridViewTextBoxColumn Ingredients;
            private System.Windows.Forms.DataGridViewButtonColumn Edit;
            private System.Windows.Forms.Button button1;
            private System.Windows.Forms.Button button2;
      }
}