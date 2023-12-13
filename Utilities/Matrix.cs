namespace Utilities
{
    public class Matrix<T>
    {
        private T[,] _matrix;

        public int Count
        {
            get { return _matrix.Length; }
        }

        public Matrix(int rowDimension, int columnDimension )
        {
            _matrix = new T[rowDimension, columnDimension];
        }

        public T this[int rowIndex, int colIndex] 
        {
            get { return _matrix[rowIndex, colIndex]; }
            set { _matrix[rowIndex, colIndex] = value; }
        }

        public List<MatrixLocation<T>> GetNeighbors(int row, int col)
        {
            List<MatrixLocation<T>> neighbors = new List<MatrixLocation<T>>();
            // Neighbor above
            if (row > 0)
            {
                neighbors.Add(new MatrixLocation<T> { Value = _matrix[row - 1, col], Row = row - 1, Column = col });
            }
            // Neighbor below
            if (row < _matrix.GetLength(0) - 1)
            {
                neighbors.Add(new MatrixLocation<T> { Value = _matrix[row + 1, col], Row = row + 1, Column = col });
            }
            // Neighbor left
            if (col > 0)
            {
                neighbors.Add(new MatrixLocation<T> { Value = _matrix[row, col - 1], Row = row, Column = col - 1 });
            }
            // Neighbor right
            if (col < _matrix.GetLength(1) - 1)
            {
                neighbors.Add(new MatrixLocation<T> { Value = _matrix[row, col + 1], Row = row, Column = col + 1 });
            }

            return neighbors;
        }

        public List<MatrixLocation<T>> GetNeighborsWithDiagonals(int row, int col)
        {
            List<MatrixLocation<T>> neighbors = new List<MatrixLocation<T>>();
            // Neighbors above
            if (row > 0)
            {
                //Directly Above
                neighbors.Add(new MatrixLocation<T> { Value = _matrix[row - 1, col], Row = row - 1, Column = col });
                //Above Left
                if(col > 0)
                {
                    neighbors.Add(new MatrixLocation<T> { Value = _matrix[row - 1, col - 1], Row = row - 1, Column = col - 1 });
                }
                //Above Right
                if(col < _matrix.GetLength(1) - 1)
                {
                    neighbors.Add(new MatrixLocation<T> { Value = _matrix[row - 1, col + 1], Row = row - 1, Column = col + 1 });
                }
            }
            // Neighbors below
            if (row < _matrix.GetLength(0) - 1)
            {
                //Directly Below
                neighbors.Add(new MatrixLocation<T> { Value = _matrix[row + 1, col], Row = row + 1, Column = col });
                //Below Left
                if (col > 0)
                {
                    neighbors.Add(new MatrixLocation<T> { Value = _matrix[row + 1, col - 1], Row = row + 1, Column = col - 1 });
                }
                //Below Right
                if (col < _matrix.GetLength(1) - 1)
                {
                    neighbors.Add(new MatrixLocation<T> { Value = _matrix[row + 1, col + 1], Row = row + 1, Column = col + 1 });
                }

            }
            // Neighbor left
            if (col > 0)
            {
                neighbors.Add(new MatrixLocation<T> { Value = _matrix[row, col - 1], Row = row, Column = col - 1 });
            }
            // Neighbor right
            if (col < _matrix.GetLength(1) - 1)
            {
                neighbors.Add(new MatrixLocation<T> { Value = _matrix[row, col + 1], Row = row, Column = col + 1 });
            }

            return neighbors;
        }

        public MatrixLocation<T> GetNeighborAbove(int row, int col)
        {
            if (row > 0)
            {
                return new MatrixLocation<T> { Value = _matrix[row - 1, col], Row = row - 1, Column = col };
            }
            return new MatrixLocation<T>();
        }

        public MatrixLocation<T> GetNeighborBelow(int row, int col)
        {
            if (row < _matrix.GetLength(0) - 1)
            {
                return new MatrixLocation<T> { Value = _matrix[row + 1, col], Row = row + 1, Column = col };
            }
            return new MatrixLocation<T>();
        }

        public MatrixLocation<T> GetNeighborLeft(int row, int col)
        {
            if (col > 0)
            {
                return new MatrixLocation<T> { Value = _matrix[row, col - 1], Row = row, Column = col - 1 };
            }
            return new MatrixLocation<T>();
        }
        public MatrixLocation<T> GetNeighborRight(int row, int col)
        {
            if (col < _matrix.GetLength(1) - 1)
            {
                return new MatrixLocation<T> { Value = _matrix[row, col + 1], Row = row, Column = col + 1 };
            }
            return new MatrixLocation<T>();
        }

    }

    public class MatrixLocation<T>
    {
        public T Value { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
