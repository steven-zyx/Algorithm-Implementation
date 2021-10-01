using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDataStrcture
{
    public class DijkstraS2Stack
    {
        protected Dictionary<string, Func<double, double, double>> _operation;

        public DijkstraS2Stack()
        {
            _operation = new Dictionary<string, Func<double, double, double>>();
            _operation["+"] = (x, y) => x + y;
            _operation["-"] = (x, y) => x - y;
            _operation["*"] = (x, y) => x * y;
            _operation["/"] = (x, y) => x / y;
            _operation["sqrt"] = (x, y) => Math.Sqrt(x);
        }

        public double Evaluate(string expression)
        {
            Stack_N<string> operaters = new Stack_N<string>();
            Stack_N<double> values = new Stack_N<double>();

            foreach (string part in expression.Split(' '))
            {
                if (_operation.ContainsKey(part)) operaters.Push(part);
                else if (part == ")")
                {
                    string operater = operaters.Pop();
                    double y = 0;
                    if (operater != "sqrt")
                        y = values.Pop();
                    else
                        ;
                    values.Push(_operation[operater](values.Pop(), y));
                }
                else if (part != "(") values.Push(double.Parse(part));
            }
            return values.Pop();
        }
    }
}
