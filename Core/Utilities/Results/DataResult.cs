using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class DataResult<Type> : Result, IDataResult<Type>
    {
        public Type Data { get; }

        public DataResult(Type data, bool success, string message): base(success, message)
        {
            this.Data = data;
        }

        public DataResult(Type data, bool success): base(success)
        {
            this.Data = data;
        }
    }
}
