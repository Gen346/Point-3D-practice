using System;

struct Point3D
{
    public double X;
    public double Y;
    public double Z;
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter the dimensions of array A (r k):");
        string[] dimensions = Console.ReadLine().Split();
        int r = int.Parse(dimensions[0]);
        int k = int.Parse(dimensions[1]);

        Point3D[,,] arrayA = new Point3D[r, k, 3];

        Random random = new Random();
        for (int i = 0; i < r; i++)
        {
            for (int j = 0; j < k; j++)
            {
                arrayA[i, j, 0] = new Point3D { X = random.Next(101), Y = random.Next(101), Z = random.Next(101) };
                Console.WriteLine($"Array A[{i}, {j}]: ({arrayA[i, j, 0].X}, {arrayA[i, j, 0].Y}, {arrayA[i, j, 0].Z})");
            }
        }

        Console.WriteLine("Enter the modulus for vectors in array B:");
        double M = double.Parse(Console.ReadLine());

        Console.WriteLine("Enter the coordinates of vector N(a1,a2,a3):");
        string[] coordinatesN = Console.ReadLine().Split();
        double a1 = double.Parse(coordinatesN[0]);
        double a2 = double.Parse(coordinatesN[1]);
        double a3 = double.Parse(coordinatesN[2]);

        Point3D[][] arrayB = BuildArrayB(arrayA, r, k, M);
        PrintArrayB(arrayB);

        int[] colinearCounts = CountColinearVectors(arrayB, a1, a2, a3);
        PrintColinearCounts(colinearCounts);
    }

    static Point3D[][] BuildArrayB(Point3D[,,] arrayA, int r, int k, double M)
    {
        Point3D[][] arrayB = new Point3D[r][];

        for (int i = 0; i < r; i++)
        {
            arrayB[i] = new Point3D[k];
            for (int j = 0; j < k; j++)
            {
                double modulus = Math.Sqrt(arrayA[i, j, 0].X * arrayA[i, j, 0].X +
                                           arrayA[i, j, 0].Y * arrayA[i, j, 0].Y +
                                           arrayA[i, j, 0].Z * arrayA[i, j, 0].Z);
                if (modulus == M)
                {
                    arrayB[i][j] = arrayA[i, j, 0];
                }
            }
        }

        return arrayB;
    }

    static void PrintArrayB(Point3D[][] arrayB)
    {
        Console.WriteLine("Array B:");
        foreach (Point3D[] row in arrayB)
        {
            foreach (Point3D point in row)
            {
                if (point.X != 0 || point.Y != 0 || point.Z != 0)
                {
                    Console.Write($"({point.X}, {point.Y}, {point.Z}) ");
                }
                else
                {
                    Console.Write("null ");
                }
            }
            Console.WriteLine();
        }
    }

    static int[] CountColinearVectors(Point3D[][] arrayB, double a1, double a2, double a3)
    {
        int[] colinearCounts = new int[arrayB.Length];

        for (int i = 0; i < arrayB.Length; i++)
        {
            foreach (Point3D point in arrayB[i])
            {
                if (point.X != 0 || point.Y != 0 || point.Z != 0)
                {
                    double dotProduct = a1 * point.X + a2 * point.Y + a3 * point.Z;
                    double vectorMagnitude = Math.Sqrt(a1 * a1 + a2 * a2 + a3 * a3) * Math.Sqrt(point.X * point.X + point.Y * point.Y + point.Z * point.Z);
                    double cosineSimilarity = dotProduct / vectorMagnitude;
                    if (Math.Abs(cosineSimilarity - 1) < 1e-10) // коефіцієнт подібності
                    {
                        colinearCounts[i]++;
                    }
                }
            }
        }

        return colinearCounts;
    }

    static void PrintColinearCounts(int[] colinearCounts)
    {
        Console.WriteLine("Number of colinear vectors in each row of array B:");
        for (int i = 0; i < colinearCounts.Length; i++)
        {
            Console.WriteLine($"Row {i + 1}: {colinearCounts[i]}");
        }
    }
}
