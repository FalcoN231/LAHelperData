using System;
using System.Windows.Forms;
using static ConsoleApp4.DataLayer;

namespace ConsoleApp4
{
      internal class Program
      {
            [STAThread]
            static void Main(string[] args)
            {
                  //new Test().write();

                  //var temp = Data.getInstance().getTable();

                  //foreach (var item in temp.Values)
                  //      foreach (var key in item.Keys)
                  //            Console.WriteLine($"{key}={item[key]}");


                  //Receipt receipt = new Receipt("potion", 33, 3, 1, 30, ("herbalism", "rotten_wildflower", 48), ("herbalism", "fresh_wildflower", 24), ("herbalism", "lush_wildflower", 6));
                  //new Test().test(receipt);

                  Data.getInstance().save();
                  var test = new Test();

                  var temp = test.read();

                  foreach (var item in temp)
                  {
                        foreach (var item2 in item)
                              Console.WriteLine($"{item2.Key}={item2.Value}");
                  }

                  //Application.EnableVisualStyles();
                  //Application.Run(new Form1());

                  Console.ReadKey();
            }
      }
}
