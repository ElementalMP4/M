using MLang.utils;
using MLang.interpreter;

namespace MLang
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0) Console.WriteLine("No input file supplied");
            else
            {
                string fileName = args[0];
                if (FileParser.fileExists(fileName))
                {
                    List<string> lines = FileParser.loadFileContents(fileName);
                    Dictionary<string, string> sourceMap = ContentParser.parseFileContents(lines);

                    new Executor().execute(sourceMap);
                }
                else
                {
                    Console.Error.WriteLine("File '" + fileName + "' does not exist!");
                }
            }
        }
    }
}
