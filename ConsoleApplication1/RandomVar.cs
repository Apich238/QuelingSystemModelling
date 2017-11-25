using System;

namespace ConsoleApplication1
{
    public abstract class RandomVar<T>
    {
        public RandomVar(RandomValueGenerator trans)
        {
            generator = trans;
        }
        protected static Random rand = new Random();
        protected RandomValueGenerator generator;
        //public RandomValueGenerator Generator { get { return generator; } }
        public T Measure()
        {
            return generator();
        }
        public delegate T RandomValueGenerator();
    }
    public class ContinuousRandomVar : RandomVar<double>
    {
        public ContinuousRandomVar(RandomValueGenerator tr) : base(tr) { }
        #region predefined distributions
        public static ContinuousRandomVar R(double a = 0, double b = 1)
        {
            return a + (b - a) * new ContinuousRandomVar(() => rand.NextDouble());
        }
        private static ContinuousRandomVar R_STD = new ContinuousRandomVar(() =>
        {
            var v = R(-1, 1);
            double a, b, c;
            do
            {
                a = v.Measure();
                b = v.Measure();
                c = a * a + b * b;
            }
            while (c == 0 || c > 1);
            return a * Math.Sqrt(-2 * Math.Log(c) / c);
        });
        public static ContinuousRandomVar STD() { return R_STD; }
        public static ContinuousRandomVar NORM(double M, double S) { return M + S * R_STD; }
        public static ContinuousRandomVar CAUCHY(double X0 = 0, double Gamma = 1)
        {
            return X0 + Gamma * new ContinuousRandomVar(() => Math.Tan(Math.PI * (rand.NextDouble() - 0.5)));
        }
        #endregion
        #region productions
        // a*b
        public static ContinuousRandomVar operator *(double a, ContinuousRandomVar b)
        {
            return new ContinuousRandomVar(() => a * b.generator());
        }
        public static ContinuousRandomVar operator *(ContinuousRandomVar a, double b)
        {
            return b * a;
        }
        public static ContinuousRandomVar operator *(ContinuousRandomVar a, ContinuousRandomVar b)
        {
            return new ContinuousRandomVar(() => a.generator() * b.generator());
        }
        //a+b
        public static ContinuousRandomVar operator +(double a, ContinuousRandomVar b)
        {
            return new ContinuousRandomVar(() => a + b.generator());
        }
        public static ContinuousRandomVar operator +(ContinuousRandomVar a, double b)
        {
            return b + a;
        }
        public static ContinuousRandomVar operator +(ContinuousRandomVar a, ContinuousRandomVar b)
        {
            return new ContinuousRandomVar(() => a.generator() + b.generator());
        }
        //a-b
        public static ContinuousRandomVar operator -(double a, ContinuousRandomVar b)
        {
            return new ContinuousRandomVar(() => a - b.generator());
        }
        public static ContinuousRandomVar operator -(ContinuousRandomVar a, double b)
        {
            return new ContinuousRandomVar(() => a.generator() - b);
        }
        public static ContinuousRandomVar operator -(ContinuousRandomVar a, ContinuousRandomVar b)
        {
            return new ContinuousRandomVar(() => a.generator() - b.generator());
        }
        //a/b
        public static ContinuousRandomVar operator /(double a, ContinuousRandomVar b)
        {
            return new ContinuousRandomVar(() => a / b.generator());
        }
        public static ContinuousRandomVar operator /(ContinuousRandomVar a, double b)
        {
            return new ContinuousRandomVar(() => a.generator() / b);
        }
        public static ContinuousRandomVar operator /(ContinuousRandomVar a, ContinuousRandomVar b)
        {
            return new ContinuousRandomVar(() => a.generator() / b.generator());
        }
        #endregion
    }
}