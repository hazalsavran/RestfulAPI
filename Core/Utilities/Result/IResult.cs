using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Result
{
    // temel voidler için başlangıç
    public interface IResult
    {

        bool Succes { get; } // get demek sadece okunabilir readonly özelliktir ( sadece return), succes propertisi başarılımı başarısızmı bool tipinde onu dönecek
        string Message { get; } //kullanıcıyı bilgilendirir 
    }
}
