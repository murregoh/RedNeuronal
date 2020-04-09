using System;

namespace TallerRedesNeuronales
{
    public class Program
    {
        // These variables are setting up by a random function.
        internal static double actualTheta, actualW1, actualW2;

        // We decide the learning value
        internal static double learningValue = 0.2;

        // Flag to determine whether all the iterations are properly
        internal static bool allDone;

        // This variable counts how many times it needs to iterate the matriz
        internal static int count = 1;

        public static void Main(string[] args)
        {
            Console.WriteLine("Red Neuronal by Mateo Urrego \nCarnet 201620002");

            // Setting up the actualTheta, actualW1 and actualW2 with a random number
            actualTheta = Math.Round(new Random().NextDouble(), 1);
            actualW1 = Math.Round(new Random().NextDouble(), 1);
            actualW2 = Math.Round(new Random().NextDouble(), 1);

            // Manual Parameters (These parameters has been adjusted in order to get the correct results) 
            // Uncomment the next row to see it working
            //actualTheta = 0.8; actualW1 = 0.4; actualW2 = 0.5;

            Console.WriteLine("");
            ShowValues(actualTheta, actualW1, actualW2);

            // Getting the AND table
            int[][] ANDTable = GetANDTable();

            // this do-while handles the matrix iterations
            do
            {
                allDone = true;

                // Table Iteration
                for (int i = 0; i < ANDTable.Length; i++)
                {
                    double z = TransferFunc(ANDTable[i]);
                    int q = ActivationFunc(Math.Round(z, 1));
                    int error = GetError(ANDTable[i][2], q);
                    if (error != 0)
                    {
                        allDone = false;
                        AdjustValues(error, ANDTable[i][0], ANDTable[i][1]);
                    }
                }
            } while (!allDone);

            // Final result
            Console.WriteLine("Final Result.");
            Console.WriteLine(String.Format("\tIt needed {0} adjustments to the perceptron learning", count));
            Console.Write(String.Format("\tCorrect Values: "));
            ShowValues(actualTheta, actualW1, actualW2);
        }

        private static void ShowValues(double actualTheta, double actualW1, double actualW2)
        {
            Console.WriteLine(String.Format("W1: {0}, W2: {1}, Theta: {2}", actualW1, actualW2, actualTheta ));
            Console.WriteLine("");
        }

        private static int[][] GetANDTable()
        {
            return new int[][]
            {
                new int[] {0 , 0 , 0},
                new int[] {0 , 1 , 0},
                new int[] {1 , 0 , 0},
                new int[] {1 , 1 , 1}
            };
        }

        private static double TransferFunc(int[] iterarion)
        {
            return Math.Round((iterarion[0] * actualW1) + (iterarion[1] * actualW2) - Math.Abs(actualTheta), 1);
        }

        private static int ActivationFunc(double val)
        {
            if (val > 0)
                return 1;
            else
                return 0;
        }

        private static int GetError(int expected, int q)
        {
            return expected - q;
        }

        private static void AdjustValues(int error, int x1, int x2)
        {
            count++;
            double deltaTheta, deltaW1, deltaW2;

            deltaTheta = -(learningValue * error);
            deltaW1 = learningValue * error * x1;
            deltaW2 = learningValue * error * x2;

            ApplyIncrementsToValues(deltaTheta, deltaW1, deltaW2);
        }

        private static void ApplyIncrementsToValues(double theta, double w1, double w2)
        {
            double[] newValues = IncrementValues(theta, w1, w2);

            actualTheta = Math.Round(newValues[0], 1);
            actualW1 = Math.Round(newValues[1], 1);
            actualW2 = Math.Round(newValues[2], 1);

            ShowValues(actualTheta, actualW1, actualW2);
        }

        private static double[] IncrementValues(double theta, double w1, double w2)
        {
            double newTheta, newW1, newW2;

            newTheta = actualTheta + theta;
            newW1 = actualW1 + w1;
            newW2 = actualW2 + w2;
            
            return new double[] { newTheta, newW1, newW2 };
        }
    }
}
