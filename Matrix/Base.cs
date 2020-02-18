namespace NeuralNetwork.Utils {
    public abstract class MatrixBase {
        public int rows    { get; protected set; }
        public int columns { get; protected set; }
        protected double[,] data;

        public MatrixBase(int rows, int columns, double filler = 0.0) {
            this.Reconstruct(rows, columns, filler);
        }

        protected void Reconstruct(int rows, int columns, double filler = 0.0) {
            this.rows = rows;
            this.columns = columns;
            this.data = new double[this.rows, this.columns];

            for(int x = 0; x < rows; x++) {
                for(int y = 0; y < columns; y++) {
                    this.data[x, y] = filler;
                }
            }
        }

        public abstract Matrix Add(double d);
        public abstract Matrix Add(Matrix x);
        public abstract Matrix Subtract(double d);
        public abstract Matrix Subtract(Matrix x);
        public abstract Matrix Negate();
        public abstract Matrix Scale(double s);
        public abstract Matrix Multiply(Matrix x);

        public abstract override string ToString();
        public abstract override bool Equals(object obj);
        public abstract override int GetHashCode();
    }
}
