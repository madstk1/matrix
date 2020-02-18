using System;
using System.Collections.Generic;

namespace NeuralNetwork.Utils {
    public class Matrix : MatrixBase {
        /// <summary>
        /// Represents a row-by-column 2D transformation matrix.
        /// This can be used for graphics, deep learning, etc.
        /// </summary>
        /// <param name="rows">The amount of rows to represent in the matrix.</param>
        /// <param name="columns">The amount of rows to represent in the matrix.</param>
        public Matrix(int rows, int columns)
            : base(rows, columns) { }

        /// <summary>
        /// Represents a row-by-column 2D transformation matrix.
        /// This can be used for graphics, deep learning, etc.
        /// </summary>
        /// <param name="rows">The amount of rows to represent in the matrix.</param>
        /// <param name="columns">The amount of rows to represent in the matrix.</param>
        /// <param name="filler">Optional. Fill the matrix with the specified double.</param>
        public Matrix(int rows, int columns, double filler)
            : base(rows, columns, filler) { }

        /// <summary>
        /// Perform matrix addition with the specified double.
        /// This action will be performed on the current matrix.
        /// </summary>
        /// <param name="d">Add the specified double to this.</param>
        /// <returns>This function returns itself.</returns>
        public override Matrix Add(double d) {
            for(int i = 0; i < this.rows; i++) {
                for(int j = 0; j < this.columns; j++) {
                    this[i, j] += d;
                }
            }
            return this;
        }

        /// <summary>
        /// Perform matrix addition with the specified matrix.
        /// This action will be performed on the current matrix.
        /// </summary>
        /// <param name="x">Add the specified matrix to this.</param>
        /// <returns>This function returns itself.</returns>
        public override Matrix Add(Matrix x) {
            if(x.rows != this.rows || x.columns != this.columns) {
                throw new ArgumentException("The number of rows and columns in the first matrix must be equal to the number of columns and rows in the second matrix.");
            }

            for(int i = 0; i < this.rows; i++) {
                for(int j = 0; j < this.columns; j++) {
                    this[i, j] += x[i, j];
                }
            }
            return this;
        }

        /// <summary>
        /// Perform matrix substraction with the specified double.
        /// This action will be performed on the current matrix.
        /// </summary>
        /// <param name="d">Subtract the specified double to this.</param>
        /// <returns>This function returns itself.</returns>
        public override Matrix Subtract(double d) {
            return this.Add(-d);
        }

        /// <summary>
        /// Perform matrix substraction with the specified matrix.
        /// This action will be performed on the current matrix.
        /// </summary>
        /// <param name="x">Subtract the specified matrix to this.</param>
        /// <returns>This function returns itself.</returns>
        public override Matrix Subtract(Matrix x) {
            if(x.rows != this.rows || x.columns != this.columns) {
                throw new ArgumentException("The number of rows and columns in the first matrix must be equal to the number of columns and rows in the second matrix.");
            }

            for(int i = 0; i < this.rows; i++) {
                for(int j = 0; j < this.columns; j++) {
                    this[i, j] -= x[i, j];
                }
            }
            return this;
        }

        /// <summary>
        /// Perform matrix scaling with the specified matrix.
        /// This action will be performed on the current matrix.
        /// </summary>
        /// <param name="s">Scalar performed on the specified matrix.</param>
        /// <returns>This function returns itself.</returns>
        public override Matrix Scale(double s) {
            for(int i = 0; i < this.rows; i++) {
                for(int j = 0; j < this.columns; j++) {
                    this[i, j] *= s;
                }
            }
            return this;
        }

        /// <summary>
        /// Negate the current matrix.
        /// This action will be performed on the current matrix.
        /// </summary>
        /// <returns>This function returns itself.</returns>
        public override Matrix Negate() {
            return this.Scale(-1);
        }

        /// <summary>
        /// Perform matrix multiplication with the specified matrix.
        /// This action will be performed on the current matrix.
        /// </summary>
        /// <param name="x">The matrix to multiply with, on the specified matrix.</param>
        /// <returns>This function returns itself.</returns>
        public override Matrix Multiply(Matrix x) {
            double[,] data = this.data;
            int prevCols = this.columns;

            if(this.columns != x.rows) {
                throw new ArgumentException("The number of columns in the first matrix must be equal to the number of rows in the second matrix.");
            }

            this.Reconstruct(this.rows, x.columns);

            for(int i = 0; i < this.rows; i++) {
                for(int j = 0; j < this.columns; j++) {
                    this[i, j] = 0;
                    for(int k = 0; k < prevCols; k++) {
                        this[i, j] += data[i, k] * x[k, j];
                    }
                }
            }
            return this;
        }

        /// <summary>
        /// Compares two matrices and check whether the data is identical.
        /// </summary>
        /// <returns>True if the data is identical, false otherwise.</returns>
        public override bool Equals(object obj) {
            if(obj == null || obj.GetType() != this.GetType()) {
                return false;
            }

            Matrix m = (Matrix) obj;
            if(m.rows != this.rows || m.columns != this.columns) {
                return false;
            }

            for(int i = 0; i < this.rows; i++) {
                for(int j = 0; j < this.columns; j++) {
                    if(m[i, j] != this[i, j]) {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Represents the matrix as a string.
        /// </summary>
        /// <returns>String representation of the matrix.</returns>
        public override string ToString() {
            string str = "";
            for(int r = 0; r < this.rows; r++) {
                str += "[";

                for(int c = 0; c < this.columns; c++) {
                    str += this[r, c];
                    if(c < this.columns - 1) {
                        str += ", ";
                    }
                }

                str += "]";
                if(r < this.rows - 1) {
                    str += "\n";
                }
            }

            return str;
        }

        /// <summary>
        /// Derives the hash-code from the data-member.
        /// </summary>
        public override int GetHashCode() {
            return this.data.GetHashCode();
        }

        /// <summary>
        /// Shortcut for matrix indexing.
        /// </summary>
        public double this[int x, int y] {
            get => this.data[x, y];
            set => this.data[x, y] = value;
        }

        /// <summary>
        /// Shortcut for matrix addition. (Digit)
        /// </summary>
        public static Matrix operator + (Matrix a, double b) {
            return a.Add(b);
        }

        /// <summary>
        /// Shortcut for matrix addition. (Matrix)
        /// </summary>
        public static Matrix operator + (Matrix a, Matrix b) {
            return a.Multiply(b);
        }

        /// <summary>
        /// Shortcut for matrix scaling. (Digit)
        /// </summary>
        public static Matrix operator * (Matrix a, double b) {
            return a.Scale(b);
        }

        /// <summary>
        /// Shortcut for matrix scaling. (Matrix)
        /// </summary>
        public static Matrix operator * (Matrix a, Matrix b) {
            return a.Multiply(b);
        }
    }
}
