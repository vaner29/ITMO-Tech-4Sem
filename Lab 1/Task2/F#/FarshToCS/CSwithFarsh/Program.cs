using Microsoft.FSharp.Collections;

namespace FarshToCS
{
    class Program
    {
        static void Main(string[] args)
        {
            FarshLibary.Shape mySquare = FarshLibary.Shape.NewSquare(5);
            Console.WriteLine($"Is this a square: {mySquare.IsSquare}");
            Console.WriteLine(FarshLibary.area(mySquare));
        }
    } 
}