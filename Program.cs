using System;
using System.Security.Cryptography.X509Certificates;

namespace _0Assignment2
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Read CSV file */
            Console.WriteLine("\n-------------------Starting Perceptron-------------------\n");
            Console.WriteLine(". . .File is being read. . .");
            Perceptron p = new Perceptron();
            p.ReadData("C:/Users/Oussama/Documents/UCC/AM6007/Csharp/0Assignment2/data.csv");

            /* Train Data */
            p.TrainData();

            /* Output */
            p.Output();

            /* Test ClassifyPoint method */
            // Prompt input to be tested
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n-------------------Classify your point-------------------\n");
            Console.WriteLine("Please enter RPM value: ");
            double rpm = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Please enter Vibration value: ");
            double vibration = Convert.ToDouble(Console.ReadLine());

            // Store input values into a Vector
            double[] tmpArr = new double[3] { 1, rpm, vibration };
            Vector testVect = new Vector(tmpArr);

            // Classifies the input and shows the resulting output
            int testOutput = p.ClassifyPoint(testVect);
            Console.WriteLine("Test input vector = ({0}, {1}, {2})", testVect[0], testVect[1], testVect[2]);
            Console.WriteLine("Test output = {0}", testOutput);
            Console.ForegroundColor = ConsoleColor.White; // Back to white color
        }
    }
}
