using System;
using System.Linq;

namespace NeuralNetwork.Utils {
    public static class MatrixExtensions {
        public static double Multiply(this double[] x, double[] y) {
            if(x.Length != y.Length) {
                throw new ArgumentException("Vector lengths don't match. Can't multiply.");
            }

            return x.Select((v, k) => v * y[k]).Sum();
        }
    }
}
