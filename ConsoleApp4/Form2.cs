using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static ConsoleApp4.DataLayer;

namespace ConsoleApp4
{
      public partial class Form2 : Form
      {
            private Receipt _receipt;
            private Dictionary<string, string[]> titles;
            private readonly Form1 instance;

            private readonly int index;

            public Form2(Form1 f)
            {
                  InitializeComponent();

                  _receipt = new Receipt();
                  index = -1;
                  instance = f;

                  InitializeCombo();
            }

            public Form2(int index, Receipt receipt, Form1 f)
            {
                  InitializeComponent();

                  _receipt = receipt;
                  this.index = index;
                  instance = f;

                  InitializeInfo();
                  InitializeCombo();
            }

            private void InitializeCombo()
            {
                  comboBox1.SelectedIndex = -1;
                  comboBox1.Items.Clear();

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

                  string name = textBox2.Text;
                  int createPrice = int.Parse(textBox3.Text);
                  int count = int.Parse(textBox4.Text);
                  int sellCount = int.Parse(textBox5.Text);
                  int price = int.Parse(textBox6.Text);


                  _receipt = new Receipt(name, price, count, sellCount, createPrice, _receipt.ListIngredient());

                  if (index == -1)
                        instance.UpdateReceipt(_receipt);
                  else
                        instance.UpdateReceipt(_receipt, index);

                  Close();
            }
      }
}
