namespace Utilities
{
    public class InputLoader
    {
        public static string[] LoadFile(string inputFilePath)
        {
            if (!File.Exists(inputFilePath))
            {
                throw new FileNotFoundException(inputFilePath);
            }
            var input = File.ReadAllLines(inputFilePath);
            return input;
        }

    }
}