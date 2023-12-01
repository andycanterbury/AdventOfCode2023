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


        public List<T> GetNeighbors(int row, int col)
        {
            List<T> neighbors = new List<T>();
            // Neighbor above
            if (row > 0)
            {
                neighbors.Add(_matrix[row - 1, col]);
            }
            // Neighbor below
            if (row < _matrix.GetLength(0) - 1)
            {
                neighbors.Add(_matrix[row + 1, col]);
            }
            // Neighbor left
            if (col > 0)
            {
                neighbors.Add(_matrix[row, col - 1]);
            }
            // Neighbor right
            if (col < _matrix.GetLength(1) - 1)
            {
                neighbors.Add(_matrix[row, col + 1]);
            }

            return neighbors;
        }

    }
}
