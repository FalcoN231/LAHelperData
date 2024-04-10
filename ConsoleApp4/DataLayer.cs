using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections;

using Pair = System.Collections.Generic.KeyValuePair<string, ConsoleApp4.DataLayer.Element>;
using Dict = System.Collections.Generic.Dictionary<string, ConsoleApp4.DataLayer.Element>;
using Title = System.Collections.Generic.Dictionary<string, string[]>;
using BigDict = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, ConsoleApp4.DataLayer.Element>>;

namespace ConsoleApp4
{
      public class DataLayer
      {
            public class Receipt
            {
                  private double _cost;
                  private readonly string _name;
                  public int _price;
                  private readonly int _count;
                  private readonly int _sellCount;
                  private readonly int _createPrice;
                  private readonly ListIngredient _ingredients;

                  public Receipt(string name, int price, int count, int sellCount, int createPrice, ListIngredient ingredients)
                  {
                        _name = name;
                        _price = price;
                        _count = count;
                        _sellCount = sellCount;
                        _createPrice = createPrice;
                        _ingredients = ingredients;

                        Proceed();
                  }

                  public Receipt(string name, int price, int count, int sellCount, int createPrice, params (string, string, int)[] ingredients)
                  {
                        _name = name;
                        _price = price;
                        _count = count;
                        _sellCount = sellCount;
                        _createPrice = createPrice;
                        _ingredients = new ListIngredient(ingredients);

                        Proceed();
                  }

                  private void Proceed()
                  {
                        var data = Data.getInstance();

                        foreach (Ingredient item in _ingredients)
                              _cost += data.GetElement(item.Craft, item.Name).calculate(item.Count);
                  }

                  public double Profit() => _price * _count / (double)_sellCount - _cost - _createPrice - _price * 0.05 * _count / _sellCount;

                  public string Name() => _name;
                  public ListIngredient ListIngredient() => _ingredients;
                  public int Count() => _count;
                  public int SellCount() => _sellCount;
                  public int CreatePrice() => _createPrice;
                  public int Price() => _price;

                  public override string ToString() => $"{_name},{_price},{_count},{_sellCount},{_createPrice},{_ingredients}";

                  public static implicit operator string(Receipt receipt) => receipt.ToString();
                  public static implicit operator double(Receipt receipt) => receipt.Profit();
            }

            public class ListIngredient : IEnumerable
            {
                  private readonly List<Ingredient> _ingredients;

                  public ListIngredient() { _ingredients = new List<Ingredient>(); }

                  public ListIngredient((string, string, int)[] ingredients)
                  {
                        _ingredients = new List<Ingredient>();

                        foreach (var item in ingredients)
                              Add(item.Item1, item.Item2, item.Item3);
                  }

                  public void Add(string craft, string name, int count) =>
                        _ingredients.Add(new Ingredient(craft, name, count));

                  public IEnumerator GetEnumerator() => _ingredients.GetEnumerator();

                  public override string ToString() => string.Join(",", _ingredients);

                  public static implicit operator string(ListIngredient ingredients) => ingredients.ToString();
            }

            public class Ingredient
            {
                  private readonly string _craft;
                  private readonly string _name;
                  private readonly int _count;

                  public Ingredient(string craft, string name, int count)
                  {
                        _craft = craft;
                        _name = name;
                        _count = count;
                  }

                  public string Craft => _craft;
                  public string Name => _name;
                  public int Count => _count;

                  public override string ToString() => $"{_craft};{_name};{_count}";

                  public static implicit operator string(Ingredient ingredient) => ingredient.Craft;
            }

            public class Data
            {
                  private BigDict _data;
                  private readonly string[] _craft = new string[]
                  {
                        "herbalism",
                        "logging",
                        "mining",
                        "hunting",
                        "fishing",
                        "archaeology"
                  };

                  private static Data _instance;

                  private Data(List<Pair[]> values)
                  {
                        _data = new BigDict();

                        Dict temp;
                        for (int i = 0; i < values.Count; i++)
                        {
                              temp = new Dict();
                              foreach (var pair in values[i])
                                    temp.Add(pair.Key, pair.Value);

                              _data.Add(_craft[i], temp);
                        }
                  }

                  public static Data getInstance()
                  {
                        if (_instance == null)
                              _instance = new Data(FileOperations.getInstance().readData());

                        return _instance;
                  }

                  public BigDict getTable() => _data;

                  public Element GetElement(string craft, string name) => _data[craft][name];

                  ~Data()
                  {
                        var pairs = new List<Pair[]>();

                        foreach (var dict in _data.Values)
                              pairs.Add(dict.ToArray());

                        FileOperations.getInstance().writeData(pairs);
                  }
            }

            public class Element
            {
                  private int _price;
                  private int _count;

                  public Element(int price, int count)
                  {
                        _price = price;
                        _count = count;
                  }

                  public Element(string text)
                  {
                        _price = int.Parse(text.Split(',')[0]);
                        _count = int.Parse(text.Split(',')[1]);
                  }

                  public double calculate(double count) => _price * count / _count;

                  public override string ToString() => $"{_price},{_count}";

                  public static implicit operator string(Element e) => e.ToString();
            }

            public class FileOperations
            {
                  private static FileOperations _instance;

                  private readonly string _fileData = "LAHelper/data.txt";
                  private readonly string _fileTitle = "LAHelper/title.txt";
                  private readonly string _fileReceipt = "LAHelper/receipt.txt";
                  private readonly string[] _craft = new string[]
                  {
                        "herbalism",
                        "logging",
                        "mining",
                        "hunting",
                        "fishing",
                        "archaeology"
                  };
                  private readonly string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


                  private FileOperations() { }

                  public static FileOperations getInstance()
                  {
                        if (_instance == null)
                              _instance = new FileOperations();
                        return _instance;
                  }

                  public List<Pair[]> readData()
                  {
                        var result = new List<Pair[]>();

                        using (StreamReader sr = new StreamReader(Path.Combine(docPath, _fileData)))
                        {
                              sr.ReadLine();

                              List<Pair> pairs = new List<Pair>();
                              string line;
                              while (sr.Peek() != -1)
                              {
                                    line = sr.ReadLine();

                                    if (_craft.Contains(line))
                                    {
                                          result.Add(pairs.ToArray());
                                          pairs.Clear();
                                          continue;
                                    }
                                    var (key, value) = (line.Split('=')[0], new Element(line.Split('=')[1]));
                                    pairs.Add(new Pair(key, value));
                              }
                              result.Add(pairs.ToArray());
                        }

                        return result;
                  }

                  public void writeData(List<Pair[]> pairs)
                  {
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, _fileData), false))
                        {
                              for (int i = 0; i < pairs.Count; i++)
                              {
                                    outputFile.WriteLine(_craft[i]);

                                    foreach (var pair in pairs.ElementAt(i))
                                          outputFile.WriteLine($"{pair.Key}={pair.Value}");
                              }
                        }
                  }

                  public Title readTitle()
                  {
                        var title = new Title();

                        using (StreamReader sr = new StreamReader(Path.Combine(docPath, _fileTitle), false))
                        {

                              List<string> temp = new List<string>();
                              string key = sr.ReadLine();

                              string line;
                              while (sr.Peek() != -1)
                              {
                                    line = sr.ReadLine();
                                    if (_craft.Contains(line))
                                    {
                                          title.Add(key, temp.ToArray());
                                          temp.Clear();
                                          key = line;
                                          continue;
                                    }

                                    temp.Add(line);
                              }

                              title.Add(key, temp.ToArray());
                        }

                        return title;
                  }

                  public List<Receipt> readReceipt()
                  {
                        var list = new List<Receipt>();

                        using (StreamReader sr = new StreamReader(Path.Combine(docPath, _fileReceipt), false))
                        {

                        }

                        return list;
                  }
            }

            public class Test
            {
                  private readonly string _fileData = "LAHelper/data.txt";
                  private readonly string _fileDat = "LAHelper/receipt.txt";
                  private readonly string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                  public Test() { }

                  public void write()
                  {
                        var temp = FileOperations.getInstance().readTitle();

                        using (StreamWriter sw = new StreamWriter(Path.Combine(docPath, _fileData), false))
                        {
                              foreach (var item in temp)
                              {
                                    sw.WriteLine(item.Key);
                                    foreach (var item2 in item.Value)
                                    {
                                          Console.Write($"{item2}=");
                                          string name = Console.ReadLine();
                                          Console.Write("count=");
                                          sw.WriteLine($"{item2}={name},{Console.ReadLine()}");
                                    }
                              }
                        }
                  }

                  public void test(Receipt receipt)
                  {
                        var temp = FileOperations.getInstance().readTitle();

                        using (StreamWriter sw = new StreamWriter(Path.Combine(docPath, _fileDat), false))
                        {
                              for (int i = 0; i < 3; i++)
                              {
                                    sw.WriteLine($"name={receipt.Name()}");
                                    sw.WriteLine($"ingredients={receipt.ListIngredient()}");
                                    sw.WriteLine($"createprice={receipt.CreatePrice()}");
                                    sw.WriteLine($"count={receipt.Count()}");
                                    sw.WriteLine($"count={receipt.SellCount()}");
                                    sw.WriteLine($"price={receipt.Price()}");
                              }
                        }
                  }

                  public List<Receipt> read()
                  {
                        var list = new List<Receipt>();

                        using (StreamReader sr = new StreamReader(Path.Combine(docPath, _fileDat), false))
                        {
                              while (!sr.EndOfStream)
                              {
                                    string name = sr.ReadLine().Split('=')[1];
                                    string ingredients = sr.ReadLine().Split('=')[1];
                                    int createPrice = int.Parse(sr.ReadLine().Split('=')[1]);
                                    int count = int.Parse(sr.ReadLine().Split('=')[1]);
                                    int sellCount = int.Parse(sr.ReadLine().Split('=')[1]);
                                    int price = int.Parse(sr.ReadLine().Split('=')[1]);

                                    ListIngredient listIngredient = new ListIngredient();
                                    foreach (string i in ingredients.Split(','))
                                    {
                                          var par = i.Split(';');
                                          listIngredient.Add(par[0], par[1], int.Parse(par[2]));
                                    }

                                    list.Add(new Receipt(name, price, count, sellCount, createPrice, listIngredient));
                              }
                        }

                        return list;
                  }
            }
      }
}
