﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ContinuousIntegration
{
    public static class ConnectionManager
    {
        public static string ConnectionString
        {
            get
            {
                return @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Debug;Integrated Security=True";
            }
        }

        public static string FolderName
        {
            get
            {
                return new Uri(new Uri(Assembly.GetEntryAssembly().Location), ".").LocalPath;
            }
        }

        public static string SchemaFileName
        {
            get
            {
                return FolderName + @"Sql\schema.sql";
            }
        }

        public static string DatabaseFileName
        {
            get
            {
                return new Uri(new Uri(FolderName), @"..\..\..\..\WebApplication\Database.cs").LocalPath;
            }
        }
    }
}