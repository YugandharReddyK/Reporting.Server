using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Utilities
{
      namespace MaxSurvey.Core.Infrastructure.Utilities
      {
            public class TypeSerializationBinder : SerializationBinder
            {
                  public override Type BindToType(string assemblyName, string typeName)
                  {
                        if (typeName.IndexOf("MaxSurvey.SharedLibraries.", StringComparison.OrdinalIgnoreCase) > -1)
                        {
                              return Type.GetType(string.Format("{0}, {1}", typeName.Replace(".SharedLibraries.", ".Core."), assemblyName.Replace(".SharedLibraries.", ".Core.")));
                        }

                        return Type.GetType($"{typeName}, {assemblyName}");
                  }
            }
      }
}
