﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace UnitTest.Json
{
    public class Data
    {
        public string Name;

        public int NumberInt;

        public double NumberDouble;

        public Guid Guid;
    }

    public class DataNullable
    {
        public string Name;

        public int? NumberInt;

        public double? NumberDouble;

        public Guid? Guid;

        public string Type;
    }

    public class DataWithList
    {
        public string Name;

        public List<DataWithListItem> List;
    }

    public class DataWithListItem
    {
        public string Name;
    }

    public class DataWithListItem2 : DataWithListItem
    {
        public string Type;
    }

    public class DataWithDictionary
    {
        public Dictionary<string, int> List;
    }

    public class DataWithDictionary2
    {
        public Dictionary<string, DataWithListItem> List;
    }

    public class DataWithListNested
    {
        public List<Dictionary<string, int>> List;

        public List<List<int>> List2;
    }

    public class DataWithObject
    {
        public object Value;
    }

    public class DataListNull
    {
        public string Name;

        public List<object> List;

        public Dictionary<string, int> List2;
    }

    public class DataListNestedNull
    {
        public List<List<object>> List;

        public Dictionary<string, Dictionary<string, int>> List2;
    }

    public class UnitTest : UnitTestBase
    {
        public void Test01()
        {
            Data data = new Json.Data();
            data.Name = "Name";
            data.NumberInt = 88;
            data.NumberDouble = 34.223;
            Guid guid = Guid.NewGuid();
            string json = Server.Json.Util.Serialize(data);
            var data2 = Server.Json.Util.Deserialize<Data>(json);
            Util.Assert(data.Name == data2.Name);
            Util.Assert(data.NumberInt == data2.NumberInt);
            Util.Assert(data.NumberDouble == data2.NumberDouble);
            Util.Assert(data.Guid == data2.Guid);
        }

        public void Test02()
        {
            DataNullable data = new Json.DataNullable();
            data.Name = "Name";
            data.NumberInt = 88;
            data.NumberDouble = 34.223;
            Guid guid = Guid.NewGuid();
            string json = Server.Json.Util.Serialize(data);
            var data2 = Server.Json.Util.Deserialize<DataNullable>(json);
            Util.Assert(data.Name == data2.Name);
            Util.Assert(data.NumberInt == data2.NumberInt);
            Util.Assert(data.NumberDouble == data2.NumberDouble);
            Util.Assert(data.Guid == data2.Guid);
        }

        public void Test03()
        {
            DataWithList data = new Json.DataWithList();
            data.Name = "L";
            data.List = null;
            string json = Server.Json.Util.Serialize(data);
            DataWithList data2 = Server.Json.Util.Deserialize<DataWithList>(json);
        }

        public void Test04()
        {
            DataWithList data = new Json.DataWithList();
            data.Name = "L";
            data.List = new List<Json.DataWithListItem>();
            data.List.Add(new Json.DataWithListItem() { Name = "X1" });
            data.List.Add(new Json.DataWithListItem2() { Name = "X2" });
            data.List.Add(null);
            string json = Server.Json.Util.Serialize(data);
            var data2 = Server.Json.Util.Deserialize<DataWithList>(json);
            Util.Assert(data.List[0].Name == data2.List[0].Name);
            Util.Assert(data.List[1].GetType() == typeof(DataWithListItem2));
        }

        public void Test05()
        {
            DataWithDictionary data = new Json.DataWithDictionary();
            data.List = new Dictionary<string, int>();
            data.List["F"] = 33;
            data.List["G"] = 44;
            string json = Server.Json.Util.Serialize(data);
            var data2 = Server.Json.Util.Deserialize<DataWithDictionary>(json);
            Util.Assert(data.List["F"] == 33);
            Util.Assert(data.List["G"] == 44);
        }

        public void Test06()
        {
            var data = new Json.DataWithDictionary2();
            data.List = new Dictionary<string, Json.DataWithListItem>();
            data.List["F"] = new DataWithListItem() { Name = "FF" };
            data.List["G"] = new DataWithListItem2() { Name = "GG" };
            data.List["H"] = null;
            string json = Server.Json.Util.Serialize(data);
            var data2 = Server.Json.Util.Deserialize<DataWithDictionary2>(json);
            Util.Assert(data.List["F"].Name == "FF");
            Util.Assert(data.List["G"].Name == "GG");
            Util.Assert(data.List["G"].GetType() == typeof(DataWithListItem2));
            Util.Assert(data.List["H"] == null);
        }

        public void Test07()
        {
            var data = new Json.DataWithListNested();
            data.List = new List<Dictionary<string, int>>();
            data.List.Add(new Dictionary<string, int>());
            data.List[0]["X"] = 99;
            data.List2 = new List<List<int>>();
            data.List2.Add(new List<int>());
            data.List2[0].Add(88);
            string json = Server.Json.Util.Serialize(data);
            var data2 = Server.Json.Util.Deserialize<DataWithListNested>(json);
        }

        public void Test08()
        {
            var data = new DataWithObject();
            data.Value = "H";
            string json = Server.Json.Util.Serialize(data);
            var data2 = Server.Json.Util.Deserialize<DataWithObject>(json);
            Util.Assert((string)data2.Value == "H");
        }

        public void Test09()
        {
            var data = new DataWithObject();
            data.Value = 2.23;
            string json = Server.Json.Util.Serialize(data);
            var data2 = Server.Json.Util.Deserialize<DataWithObject>(json);
            Util.Assert((double)data2.Value == 2.23);
        }

        public void Test10()
        {
            var data = new DataWithObject();
            data.Value = (double)2;
            string json = Server.Json.Util.Serialize(data);
            var data2 = Server.Json.Util.Deserialize<DataWithObject>(json);
            Util.Assert((double)data2.Value == 2);
        }

        public void Test11()
        {
            var data = new DataWithObject();
            data.Value = (int)2;
            try
            {
                string json = Server.Json.Util.Serialize(data);
            }
            catch (Server.Json.ExceptionJson exception)
            {
                Util.Assert(exception.Message == "Type only string or double!");
            }
        }

        public void Test12()
        {
            var data = new DataWithObject();
            data.Value = new DataWithListItem();
            try
            {
                string json = Server.Json.Util.Serialize(data);
            }
            catch (Server.Json.ExceptionJson exception)
            {
                Util.Assert(exception.Message == "Object has no Type field!");
            }
        }

        public void Test13()
        {
            var data = new DataWithObject();
            data.Value = new DataWithListItem2() { Name = "M" };
            string json = Server.Json.Util.Serialize(data);
            var data2 = Server.Json.Util.Deserialize<DataWithObject>(json);
            Util.Assert(data2.Value.GetType() == typeof(DataWithListItem2));
            Util.Assert(((DataWithListItem2)data2.Value).Name == "M");
        }

        public void Test14()
        {
            var data = new DataListNull();
            data.Name = "H";
            string json = Server.Json.Util.Serialize(data);
            var data2 = Server.Json.Util.Deserialize<DataListNull>(json);
            Util.Assert(!json.Contains("List"));
            Util.Assert(data.List != null);
            Util.Assert(data.List2 != null);
            Util.Assert(data2.List != null);
            Util.Assert(data2.List2 != null);
            Util.Assert(data.Name == "H");
            Util.Assert(data2.Name == "H");
        }

        public void Test15()
        {
            var data = new DataListNestedNull();
            data.List = new List<List<object>>();
            data.List.Add(new List<object>());
            data.List.Add(null);
            string json = Server.Json.Util.Serialize(data);
            var data2 = Server.Json.Util.Deserialize<DataListNestedNull>(json);
            Util.Assert(data2.List[1] != null);
        }

        public void Test16()
        {
            var data = new DataListNestedNull();
            data.List2 = new Dictionary<string, Dictionary<string, int>>();
            data.List2.Add("X", new Dictionary<string, int>());
            data.List2.Add("Y", null);
            string json = Server.Json.Util.Serialize(data);
            var data2 = Server.Json.Util.Deserialize<DataListNestedNull>(json);
            Util.Assert(data2.List2["Y"] != null);
        }

        public void Test17()
        {
            var data = new DataListNestedNull();
            data.List2 = new Dictionary<string, Dictionary<string, int>>();
            string json = Server.Json.Util.Serialize(data);
            var data2 = Server.Json.Util.Deserialize<DataListNestedNull>(json);
            Util.Assert(data2.List2.Count == 0);
        }

        public void Test18()
        {
            var data = new DataDictionaryKey();
            data.List = new Dictionary<object, object>();
            data.List["A"] = "X";
            data.List["D"] = null;
            string json = Server.Json.Util.Serialize(data);
            var data2 = Server.Json.Util.Deserialize<DataDictionaryKey>(json);
            Util.Assert((string)data2.List["A"] == "X");
            Util.Assert(data2.List["D"] == null);
        }

        public void Test19()
        {
            var data = new DataDictionaryKey();
            data.List = new Dictionary<object, object>();
            data.List["A"] = "X";
            data.List[8] = null;
            try
            {
                string json = Server.Json.Util.Serialize(data);
            }
            catch (Server.Json.ExceptionJson exception)
            {
                Util.Assert(exception.Message == "Dictionary key needs to be of type string!");
            }
        }

        public class DataDictionaryKey
        {
            public Dictionary<object, object> List;
        }
    }
}
