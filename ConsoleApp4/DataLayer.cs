using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;


using Pair = System.Collections.Generic.KeyValuePair<string, ConsoleApp4.DataLayer.Element>;
using Title = System.Collections.Generic.Dictionary<string, string[]>;
using BigDict = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, ConsoleApp4.DataLayer.Element>>;

namespace ConsoleApp4
{
      public class DataLayer
      {
            [Serializable]
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

                  public Receipt()
                  {
                        _ingredients = new ListIngredient();
                  }

                  public void Proceed()
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

                  public string[] ToRow() => new string[] { _name, _cost.ToString(), _count.ToString(), _sellCount.ToString(), _createPrice.ToString(), _price.ToString(), _ingredients, "Edit" };

                  public override string ToString() => $"{_name},{_price},{_count},{_sellCount},{_createPrice},{_ingredients}";

                  public static implicit operator string(Receipt receipt) => receipt.ToString();
                  public static implicit operator double(Receipt receipt) => receipt.Profit();
            }

            [Serializable]
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

                  public void Remove(int index) => _ingredients.RemoveAt(index);

                  public IEnumerator GetEnumerator() => _ingredients.GetEnumerator();

                  public override string ToString() => string.Join(",", _ingredients);

                  public static implicit operator string(ListIngredient ingredients) => ingredients.ToString();
            }

            [Serializable]
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

                  public string[] ToRow() => new string[] { _craft, _name, _count.ToString() };

                  public override string ToString() => $"{_craft};{_name};{_count}";

                  public static implicit operator string(Ingredient ingredient) => ingredient.Craft;
            }

            public class Data
            {
                  private readonly BigDict _data;

                  private static Data _instance;

                  private Data(BigDict data)
                  {
                        _data = data;
                  }

                  public static Data getInstance()
                  {
                        if (_instance == null)
                              _instance = new Data(FileOperations.getInstance().readData());

                        return _instance;
                  }

                  public Title getTitle()
                  {
                        var result = new Title();

                        foreach (var key in _data.Keys)
                              result.Add(key, _data[key].Keys.ToArray());

                        return result;
                  }

                  public Element GetElement(string craft, string name) => _data[craft][name];

                  public void save() => FileOperations.getInstance().write(_data);
            }

            [Serializable]
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

                  private readonly BinaryFormatter _formatter;

                  private readonly string _fileData = "LAHelper/data.txt";
                  private readonly string _fileReceipt = "LAHelper/receipt.txt";
                  private readonly string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                  private FileOperations() { _formatter = new BinaryFormatter(); }

                  public static FileOperations getInstance()
                  {
                        if (_instance == null)
                              _instance = new FileOperations();
                        return _instance;
                  }

                  public BigDict readData()
                  {
                        BigDict data;

                        using (FileStream fs = new FileStream(Path.Combine(docPath, _fileData), FileMode.OpenOrCreate))
                        {
                              data = (BigDict)_formatter.Deserialize(fs);
                        }

                        return data;
                  }

                  public void write(BigDict data)
                  {
                        using (FileStream sw = new FileStream(Path.Combine(docPath, _fileData), FileMode.OpenOrCreate))
                        {
                              _formatter.Serialize(sw, data);
                        }
                  }

                  public void write(List<Receipt> list)
                  {
                        using (FileStream sw = new FileStream(Path.Combine(docPath, _fileReceipt), FileMode.OpenOrCreate))
                        {
                              _formatter.Serialize(sw, list);
                        }
                  }

                  public List<Receipt> readReceipt()
                  {
                        List<Receipt> list;

                        if (!File.Exists(Path.Combine(docPath, _fileReceipt))) return new List<Receipt>();

                        using (FileStream fs = new FileStream(Path.Combine(docPath, _fileReceipt), FileMode.OpenOrCreate))
                        {
                              list = (List<Receipt>)_formatter.Deserialize(fs);
                        }

                        return list;
                  }
            }

            public class Test
            {
                  private readonly string _test = "LAHelper/dataOld.txt";
                  private readonly string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                  public Test() { }

                  public void write(List<Pair[]> data)
                  {
                        BinaryFormatter formatter = new BinaryFormatter();
                        using (FileStream sw = new FileStream(Path.Combine(docPath, _test), FileMode.OpenOrCreate))
                        {
                              formatter.Serialize(sw, data.Count);

                              foreach (Pair[] pair in data)
                                    formatter.Serialize(sw, pair);
                        }
                  }

                  public List<Pair[]> read()
                  {
                        var result = new List<Pair[]>();

                        using (StreamReader sr = new StreamReader(Path.Combine(docPath, _test)))
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
            }
      }
}
