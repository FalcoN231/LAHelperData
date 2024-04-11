using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static ConsoleApp4.DataLayer;

namespace ConsoleApp4
{
      public partial class Form1 : Form
      {
            private int rows = 0;
            private List<Receipt> receiptList;

            public Form1()
            {
                  InitializeComponent();

                  receiptList = FileOperations.getInstance().readReceipt();

                  Initialize();
            }

            public void Initialize()
            {
                  dataGridView1.Rows.Clear();

                  foreach (var item in receiptList)
                  {
                        dataGridView1.Rows.Add(item.ToRow());
                        rows++;
                  }
            }

            private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {
                  var senderGrid = (DataGridView)sender;

                  if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                      e.RowIndex >= 0 && e.RowIndex < rows)
                  {
                        var item = receiptList[e.RowIndex];
                        new Form2(e.RowIndex, item, this).ShowDialog();
                  }
            }

            private void button1_Click(object sender, EventArgs e)
            {
                  new Form2(this).ShowDialog();
            }

            private void button2_Click(object sender, EventArgs e)
            {
                  int index = dataGridView1.CurrentCell.RowIndex;

                  receiptList.RemoveAt(index);

                  Initialize();
            }

            public void UpdateReceipt(Receipt receipt, int index = -1)
            {
                  if (index == -1)
                        receiptList.Add(receipt);
                  else
                        receiptList[index] = receipt;

                  Initialize();
            }
      }
}
