using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sperry.MxS.Core.Common.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Extensions
{
      public static class ResultObjectExtenstions
      {
            //Created extension method to handle adding model binding errors from the http requests. Decided to use extension method so as not to force a reference to the Microsoft.AspNetCore.Mvc.ModelBinding in the shared libs.
            //and this is only required on the server side.
            public static void AddModelBindingErrors(this ResultObject resultObject, ModelStateDictionary modelState)
            {
                  var errors = modelState.Values.SelectMany(v => v.Errors);
                  resultObject.AddError("Model Binding Errors");
                  foreach (var modelError in errors)
                  {
                        resultObject.AddMessageWithException(modelError.ErrorMessage, modelError.Exception);
                  }
            }
      }
}
