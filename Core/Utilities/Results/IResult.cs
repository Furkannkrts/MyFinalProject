using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    //temel voidler için başlangıç
    public interface IResult
    {
        string Message { get; }
        bool Success { get; }//set yok.getirmek var sadece, okumak yok
    }
}
