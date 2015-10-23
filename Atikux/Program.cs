using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Atikux.classes;

// Adjetivo Quechua -> Algo que es posible
namespace Atikux
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0) {
                // Proceed
                SunatServices sunatService = new SunatServices();
                sunatService.sendDocument(args[0]);
            }
            else {
                Console.WriteLine("Debe especificar el nombre de un archivo ZIP");
                Console.WriteLine("Ejemplo: Atikux.exe 20392654861-01-F001-1.ZIP");
            }
        }
    }
}
