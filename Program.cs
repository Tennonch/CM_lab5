//using System;

//class Program
//{
//    static double Lagrange(double x, double[] nodes, int k)
//    {
//        double result = 1.0;
//        for (int j = 0; j < nodes.Length; j++)
//        {
//            if (j != k)
//            {
//                result *= (x - nodes[j]) / (nodes[k] - nodes[j]);
//            }
//        }
//        return result;
//    }

//    static double Integrate(Func<double, double> f, double a, double b, int n)
//    {
//        double h = (b - a) / n;
//        double sum = 0.5 * (f(a) + f(b));
//        for (int i = 1; i < n; i++)
//        {
//            sum += f(a + i * h);
//        }
//        return sum * h;
//    }

//    static void Main()
//    {
//        double[] nodes = { 1, 2, 3, 4, 5 };
//        double[] weights = new double[nodes.Length];

//        for (int k = 0; k < nodes.Length; k++)
//        {
//            int index = k; // Для коректної передачі в лямбда-функцію
//            weights[k] = Integrate(
//                x => Lagrange(x, nodes, index),
//                1,
//                5,
//                1000
//            );
//        }

//        Console.WriteLine("Weights:");
//        for (int i = 0; i < weights.Length; i++)
//        {
//            Console.WriteLine($"w_{i} = {weights[i]:F2}");
//        }

//        Func<double, double> f = x => 2 * Math.Pow(x, 8) + 3 * Math.Pow(x, 7) + 5 * Math.Pow(x, 5) - 2;

//        double approx = 0.0;
//        for (int k = 0; k < nodes.Length; k++)
//        {
//            approx += weights[k] * f(nodes[k]);
//        }

//        Console.WriteLine($"\nIntegral Approximation: {approx:F2}");
//    }
//}

using System;

class Program
{
    // Функція f(x)
    static double F(double x)
    {
        return 2 * Math.Pow(x, 8) + 3 * Math.Pow(x, 7) + 5 * Math.Pow(x, 5) - 2;
    }

    // Метод Рімана (правило середньої точки)
    static double Riemann(double a, double b, int n)
    {
        double h = (b - a) / n;
        double sum = 0.0;
        for (int i = 0; i < n; i++)
        {
            double x = a + i * h + h / 2; // Середня точка
            sum += F(x);
        }
        return h * sum;
    }

    // Правило Сімпсона
    static double Simpson(double a, double b, int n)
    {
        if (n % 2 != 0)
        {
            throw new ArgumentException("n must be even");
        }

        double h = (b - a) / n;
        double sum = F(a) + F(b);

        for (int i = 1; i < n; i++)
        {
            double x = a + i * h;
            if (i % 2 == 0)
            {
                sum += 2 * F(x); // Для парних індексів
            }
            else
            {
                sum += 4 * F(x); // Для непарних індексів
            }
        }

        return h * sum / 3;
    }

    static void Main()
    {
        double a = 1.0, b = 5.0;
        int n = 10;

        // Метод Рімана
        double riemannResult = Riemann(a, b, n);
        Console.WriteLine($"Riemann Sum: {riemannResult:F2}");

        // Правило Сімпсона
        double simpsonResult = Simpson(a, b, n);
        Console.WriteLine($"Simpson’s Rule: {simpsonResult:F2}");
    }
}
