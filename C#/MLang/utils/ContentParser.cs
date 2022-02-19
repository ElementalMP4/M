namespace MLang.utils
{
    class ContentParser
    {
        public static Dictionary<string, string> parseFileContents(List<string> lines)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (string line in lines)
            {
                List<string> lineTokens = new List<string>(line.Split(" "));
                string label = lineTokens[0];
                lineTokens.RemoveAt(0);
                string instruction = String.Join(" ", lineTokens);
                result.Add(label, instruction);
            }
            return result;
        }
    }
}
