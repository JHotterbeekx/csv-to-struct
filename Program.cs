using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReader {
  class Program {
    static void Main(string[] args)
    {
      for (int i = 0; i < 10; i++)
      {
        var sw = new Stopwatch();
        sw.Start();

        var data = new CsvReader().Read<testData>(@"C:/temp/MOCK_DATA.csv");
        data = null;

        sw.Stop();
        Console.WriteLine($"{sw.ElapsedMilliseconds} ms");
      }
      Console.ReadLine();
    }
  }

  struct testData
  {
    public int id;
    public string first_name;
    public string last_name;
    public string email;
    public string gender;
    public string ip_address;
  }
}
