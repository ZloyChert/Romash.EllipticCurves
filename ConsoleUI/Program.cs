using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EllipticCurves;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("OK. Hello boy, try to enter module of elliptic curve");
            int.TryParse(Console.ReadLine(), out var module);
            Console.WriteLine("Nice, U r not as stupid as it seemed to be");

            Console.WriteLine("Now u can choose. Enter 1 if u r useless retard and can't enter coefficients");
            var choice = Console.ReadLine();
            EllipticCurve elliptic;
            if (choice == "1")
            {
                elliptic = new EllipticCurve(module);
                Console.WriteLine($"Let then be...A: {elliptic.Coefficients.A}; B: {elliptic.Coefficients.B}");
            }
            else
            {
                Console.WriteLine("OK. Enter A");
                int.TryParse(Console.ReadLine(), out var a);
                Console.WriteLine("Mgm. Enter B");
                int.TryParse(Console.ReadLine(), out var b);
                elliptic = new EllipticCurve(new Coefficients(a, b), module);
            }

            Console.WriteLine("Here r u'r elliptic points group");
            var points = elliptic.GetElementsOfEllipticGroup();
            for (int i = 0; i < points.Count; i++)
            {
                Console.WriteLine($"{i} - X: {points[i].X}; Y: {points[i].Y}; Order: {points[i].GetPointOrder()}");
            }

            Console.WriteLine("Select point to u'r key. The order should be max, but not nessesary");
            int.TryParse(Console.ReadLine(), out var index);

            Console.WriteLine("Enter u'r secret key");
            int.TryParse(Console.ReadLine(), out var secretKey);
            secretKey %= module;
            var publicKey = elliptic.GetElementsOfEllipticGroup()[index].Multiply(secretKey);
            Console.WriteLine($"U'r public key {publicKey.X} {publicKey.Y} under module");

            Console.WriteLine($"Enter X and Y of public other point");
            int.TryParse(Console.ReadLine(), out var pX);
            int.TryParse(Console.ReadLine(), out var pY);
            var secretPoint = new EllipticPoint(pX, pY, module, elliptic.Coefficients).Multiply(secretKey);

            Console.WriteLine($"Secret point {secretPoint.X} {secretPoint.Y} under module");


            Console.Read();
        }
    }
}
