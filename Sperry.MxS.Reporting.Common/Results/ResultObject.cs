using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Results
{
    [Serializable]
    public class ResultObject
    {
        [JsonProperty]
        private readonly List<string> _messages = new List<string>();
        public static readonly string ExceptionFormatText = "{0} : Exception: {1}";

        public ResultObject()
        {
            Success = true;
        }

        public ResultObject(bool success = true)
        {
            Success = success;
        }

        public bool Success { get; set; } = true;

        [JsonIgnore]
        public string Message
        {
            get { return string.Join(Environment.NewLine, _messages); }
            set { _messages.Add(value); }
        }


        public void AddResult(ResultObject result)
        {
            //only change the current result if the resultobject success being added is false.
            if (!result.Success)
            {
                Success = false;
            }
            _messages.AddRange(result._messages);
        }

        public ResultObject AddMessage(string description)
        {
            _messages.Add(description);
            return this;
        }

        public ResultObject AddMessageWithException(string errorMessage, Exception ex)
        {
            string message = errorMessage;
            if (ex != null)
            {
                var exceptionText = BuildExceptionMessageString(ex);
                message = string.Format(ExceptionFormatText, errorMessage, exceptionText);
            }
            return AddError(message);
        }

        public ResultObject AddException(Exception ex)
        {
            var exceptionText = BuildExceptionMessageString(ex);
            return AddError(exceptionText);
        }

        public ResultObject AddError(string errorMessage)
        {
            Success = false;
            return AddMessage(errorMessage);
        }

        private string BuildExceptionMessageString(Exception ex)
        {
            var exception = ex;
            StringBuilder result = new StringBuilder();
            if (exception != null)
            {
                result.AppendLine(exception.Message);
                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                    result.AppendLine(exception.Message);
                }

            }
            return result.ToString();
        }
    }



    public class ResultObject<T> : ResultObject
    {
        public T Data { get; set; }

        public ResultObject() : base()
        {
            Data = default(T);
        }

        public ResultObject(T data) : this()
        {
            Data = data;
        }
    }
}
