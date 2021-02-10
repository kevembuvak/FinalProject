using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public bool Success { get; }
        public string Message { get; }

        public Result(bool success)
        {
            this.Success = success;
        }

        // Read-only yapılar (yani sadece getter fonksiyonuna sahip olan özellikler) constructor içinde set edilebilir

        public Result(bool success, string message): this(success) // otomatik olarak ilk contructor da çalışır
        {
            this.Message = message;
            
        }

        // Overload ----> ismi aynı ama imzası farklı iki metod
    }
}
