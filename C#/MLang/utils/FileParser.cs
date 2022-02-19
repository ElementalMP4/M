namespace MLang.utils
{
	class FileParser
	{
		public static List<string> loadFileContents(string fileName)
		{
			return new List<string>(File.ReadAllLines(fileName));
		}

		public static bool fileExists(string filename)
        {
			return File.Exists(filename);
        }
	}
}
