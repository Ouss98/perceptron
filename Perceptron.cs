using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _0Assignment2
{
    class Perceptron
    {
        /* Variables */
        private List<Vector> inputVectList; // List of input vectors
        private double[] outputArr; // Array of true outputs
        private Vector w; // Vector of weights
        
        /* Constructor */
        public Perceptron()
        {
            // Initialize weights vector w
            // Either random ( 0 < w[i] < 1 ) ...
            Random rand = new Random();
            double[] weights = new double[] { rand.NextDouble(), rand.NextDouble(), rand.NextDouble() };
            // ... or null weights
            //double[] weights = new double[] { 0, 0, 0 };
            w = new Vector(weights);
        }

        /* Reads the dataset an stores the data into 
         * a list of vector (inputs) and an array of double (outputs) */
        public void ReadData(string filename)
        {
            StreamReader sr = null;
            string tmp = null;
            char[] charSeparators = new char[] { ',' };
            string[] output = null;

            // Get the number of rows
            var inputLines = File.ReadAllLines(filename);
            int nbRows = 0;
            foreach (var line in inputLines) { nbRows++; }

            // Create size nbRows lists of vectors and array of outputs
            inputVectList = new List<Vector>(nbRows);
            outputArr = new double[nbRows];
            int iter = 0; // Initialize the number of iterations
            try
            {
                // Enclose potentially problematic code into try{}
                sr = new StreamReader(filename);
                // Reading the first line then doing nothing with it, allows us to skip it in th storing process
                sr.ReadLine();
                do
                {
                    tmp = sr.ReadLine();
                    if (tmp == null)
                    {
                        break;
                    }
                    // Process the line
                    output = tmp.Split(charSeparators);

                    // Store outputs (output[3]) into outputArr
                    outputArr[iter] = Convert.ToInt32(output[3]);
                    // Store RPM (output[1]) and Vibration (output[2]) into a size 3 Vector
                    Vector dataVect = new Vector(3);
                    dataVect[0] = 1; // 1 is for the bias
                    dataVect[1] = Convert.ToDouble(output[1]);
                    dataVect[2] = Convert.ToDouble(output[2]);
                    inputVectList.Insert(iter, dataVect);
                    iter++; // Increment iter variable each time through the loop
                    //Console.WriteLine(tmp); // Displays the dataset
                }
                while (true);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error {0}", e.Message);
            }
            finally
            {
                // Process the data
                if (sr != null)
                {
                    sr.Close(); // Finished with file, release it back into the system
                }
            }
        }

        /* Trains the data and updates the weight vector */
        public void TrainData()
        {            
            Console.WriteLine("\nInitial weights: w = ({0}, {1}, {2})", w[0], w[1], w[2]);

            // Initialize alpha and error
            double alpha = 0.1; // Learning rate
            double error;
            int iterations = 0; // Initializing iterations
            // Initialize yhat array
            double[] yhat = new double[outputArr.Length];
            Console.WriteLine("\nStarting training . . .");
            do
            {
                error = 0;
                for (int i = 0; i < inputVectList.Count; i++)
                {
                    double scalar_prod = inputVectList[i] * w; // Computes the scalar product
                    yhat[i] = (scalar_prod > 0) ? 1 : 0; // Applies activation function H
                        
                    if (outputArr[i] != yhat[i])
                    {
                        // Updates weights vector and increments error
                        w += alpha * (outputArr[i] - yhat[i]) * inputVectList[i];
                        error++;
                    }
                }
                iterations++;
            }
            while (error > 0 && iterations < 4000000); // When iterations reaches 4000000, the loop will break
            Console.WriteLine("\n. . . Training finished after {0} iterations", iterations);
            Console.WriteLine("error = {0}", error);
        }

        public int ClassifyPoint(Vector input)
        {
            double scalar_prod = input * w;
            return scalar_prod >= 0 ? 1 : 0;

        }

        /* Outputs a summary of the perceptron */
        public void Output()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n-------------------Summary of the perceptron-------------------\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Final wieghts: w = ({0}, {1}, {2})\n", w[0], w[1], w[2]);
            double x_intercept = -w[0] / w[1];
            double y_intercept = -w[0] / w[2];
            double slope = -w[1] / w[2];
            Console.WriteLine("{0, 20:0.00}{1, 20:0.00}{2, 20:0.00}", "[x-intercept]", "[y-intercept]", "[slope]");
            Console.WriteLine("{0, 20:0.00}{1, 20:0.00}{2, 20:0.00}", x_intercept, y_intercept, slope);
            Console.WriteLine("\nLine equation: RPM (Vibration) = {0:0.00} x Vibration + {1:0.00}\n", slope, y_intercept);
            //Console.ForegroundColor = ConsoleColor.Green;
            /*Console.WriteLine("{0,10} {1,10} {2,10} {3,10}", "[RPM]", "[Vibration]", "[True Output]", "[Classified Output]");
            for (int i = 0; i < inputVectList.Count; i++)
            {
                int classified = ClassifyPoint(inputVectList[i]);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("{0,10} {1,10} {2,13} {3,19}", inputVectList[i][1], inputVectList[i][2], outputArr[i], classified);
            }*/
        }
    }
}
