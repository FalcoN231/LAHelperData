using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                  Initialize();
            }

            private void Initialize()
            {
                  receiptList = new Test().read();

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
                        new Form2(ref item).ShowDialog();
                  }
            }

            private void button1_Click(object sender, EventArgs e)
            {
                  new Form2().ShowDialog();
            }

            private void button2_Click(object sender, EventArgs e)
            {
                  int index = dataGridView1.CurrentCell.RowIndex;

                  receiptList.RemoveAt(index);
            }
      }
}
