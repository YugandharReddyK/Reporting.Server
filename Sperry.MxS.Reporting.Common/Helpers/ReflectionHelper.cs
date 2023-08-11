using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Helpers
{
    public static class ReflectionHelper
    {
        public  static bool comparePropetiesInfo<T1, T2>(T1 obj1, T2 obj2)
        {
            
            PropertyInfo[] properties1=typeof(T1).GetProperties();           
            PropertyInfo[] properties2 = typeof(T2).GetProperties();

            if(properties1.Length != properties2.Length)
            {
                return false;
            }
            for(int i=0; i<properties1.Length; i++)
            {
                PropertyInfo prop1 = properties1[i];
                PropertyInfo prop2 = properties2.FirstOrDefault(p => p.Name == prop1.Name);

                if (prop2 == null || prop1.PropertyType != prop2.PropertyType)
                {
                    return false;
                }

                Object value1= prop1.GetValue(obj1);
                Object value2 = prop2.GetValue(obj2);

                if(!Object.Equals(value1,value2))
                {
                    return false;
                }

            }
            return true;
           
        }

        public static bool compareConstructorInfo<T1,T2>(T1 obj1, T2 obj2)
        {
            ConstructorInfo[] ctors1=typeof(T1).GetConstructors();
            ConstructorInfo[] ctors2 = typeof(T2).GetConstructors();

            if(ctors1.Length != ctors2.Length)
            {
                return false;
            }

            for (int i = 0; i < ctors1.Length; i++)
            {
                ConstructorInfo ctor1 = ctors1[i];
                ConstructorInfo ctor2 = ctors2.FirstOrDefault(c=>c.GetParameters().Length == ctor1.GetParameters().Length);

                if (ctor2 == null)
                {
                    return false;
                }

                ParameterInfo[] parameters1 = ctor1.GetParameters();
                ParameterInfo[] parameters2 = ctor2.GetParameters();

                for(int j=0;i<parameters1.Length;j++)
                {
                    if (parameters1[j].ParameterType != parameters2[j].ParameterType)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
      
    }
}
