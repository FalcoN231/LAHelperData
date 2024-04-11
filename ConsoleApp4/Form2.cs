using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ConsoleApp4.DataLayer;

namespace ConsoleApp4
{
      public partial class Form2 : Form
      {
            private Receipt _receipt;
            private Dictionary<string, string[]> titles;

            public Form2()
            {
                  InitializeComponent();
                  _receipt = new Receipt();

                  InitializeCombo();
            }

            public Form2(ref Receipt receipt)
            {
                  InitializeComponent();
                  _receipt = receipt;

                  InitializeInfo();
                  InitializeCombo();
            }

            private void InitializeCombo()
            {
                  titles = Data.getInstance().getTitle();
                  comboBox1.Items.AddRange(titles.Keys.ToArray());
            }

            private void InitializeInfo()
            {
                  textBox2.Text = _receipt.Name();
                  textBox3.Text = _receipt.CreatePrice().ToString();
                  textBox4.Text = _receipt.Count().ToString();
                  textBox5.Text = _receipt.SellCount().ToString();
                  textBox6.Text = _receipt.Price().ToString();

                  InitializeTable();
            }

            private void InitializeTable()
            {
                  dataGridView1.Rows.Clear();

                  foreach (Ingredient item in _receipt.ListIngredient())
                        dataGridView1.Rows.Add(item.ToRow());
            }

            private void button1_Click(object sender, EventArgs e)
            {
                  string craft = titles.Keys.ElementAt(comboBox1.SelectedIndex);
                  string name = titles[craft][comboBox1.SelectedIndex];
                  int count = int.Parse(textBox1.Text);

                  _receipt.ListIngredient().Add(craft, name, count);

                  InitializeTable();
            }

            private void button2_Click(object sender, EventArgs e)
            {
                  int index = dataGridView1.CurrentCell.RowIndex;

                  _receipt.ListIngredient().Remove(index);

                  InitializeTable();
            }

            private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
            {
                  int index = comboBox1.SelectedIndex;

                  comboBox2.Items.Clear();
                  comboBox2.SelectedIndex = -1;
                  comboBox2.Items.AddRange(titles.ElementAt(index).Value);
            }

            private void button3_Click(object sender, EventArgs e)
            {

            }
      }
}
