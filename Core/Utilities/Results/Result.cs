using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        //this ile 2 parametreli constructer yollanırsa this ile bunuda çalıştır demek. Aşağıdakinide kapsadığı için this ile 
        public Result(bool success, string message):this(success)
        {
            //getter lar readonly dir , constructarda set edilebilir
            Message = message;
        }

        public Result(bool success)
        {
            Success = success;
        }

        public bool Success { get; }

        public string Message { get; }
    }
}
