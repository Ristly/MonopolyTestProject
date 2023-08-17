using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyTest.Exceptions;

public class BoxSizeException : Exception
{

    public BoxSizeException()
    {
        Console.WriteLine("Box is too big for pallet");
    }

    public BoxSizeException(string message)
        : base(message)
    {
        Console.WriteLine(message);
    }
}
