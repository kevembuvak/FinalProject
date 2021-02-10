using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public interface IDataResult<Type>: IResult
    {
        // IResulttan gelen özellikler (success, message) var, üstüne Data var
        Type Data { get; }
    }
}
