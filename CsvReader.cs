using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CsvReader
{
  public class CsvReader
  {

    public List<T> Read<T>(string filename) where T : struct
    {
      var splitter = new StringSplitter(6);
      var result = new List<T>();

      using (StreamReader sr = File.OpenText(filename))
      {
        var headers = splitter.Split(sr.ReadLine(), ',');
        var genericType = typeof (T);
        FieldInfo[] propertyIndex = new FieldInfo[6];
        for (int i = 0; i < headers.Length; i++)
        {
          propertyIndex[i] = genericType.GetField(headers[i]);
        }

        string s = String.Empty;
        string[] data;
        FieldInfo activeProp;
        
        while ((s = sr.ReadLine()) != null)
        {
          ValueType record = new T();
          //we're just testing read speeds
          data = splitter.Split(s, ',');
          for (int i = 0; i < headers.Length; i++)
          {
            activeProp = propertyIndex[i];
            activeProp.SetValue(record, _Convert(data[i], activeProp.FieldType));
          }
          result.Add((T)record);
        }
      }
      return result;
    }

    private object _Convert(string value, Type type)
    {
      if (type == typeof (string)) return value;
      if (type == typeof (int)) return CustomParseInt(value);
      return null;
    }

    private int res = 0;
    private int CustomParseInt(string s)
    {
      var res = 0;
      foreach (var c in s) {
        res = 10 * res + (c - '0');
      }
      return res;
    }
  }
}