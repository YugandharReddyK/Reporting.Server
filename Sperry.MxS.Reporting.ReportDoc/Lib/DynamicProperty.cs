﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.ReportDoc.Lib
{
    public class DynamicProperty
    {
        readonly string name;
        readonly Type type;

       
        public DynamicProperty(string name, Type type)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (type == null) throw new ArgumentNullException("type");
            this.name = name;
            this.type = type;
        }

       
        public string Name
        {
            get { return name; }
        }

       
        public Type Type
        {
            get { return type; }
        }
    }
}
