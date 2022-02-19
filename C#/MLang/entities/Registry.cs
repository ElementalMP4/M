namespace MLang.entities
{
    class Registry
    {
        public static Dictionary<string, string> variables = new Dictionary<string, string>();
        public static Dictionary<string, FileConfig> files = new Dictionary<string, FileConfig>();

        public string getVar(string variable)
        {
            if (variables.ContainsKey(variable)) return variables[variable];
            else return null;
        }

        public void saveVar(string variable, string value)
        {
            variables[variable] = value;
        }

        public FileConfig getFile(string variable)
        {
            if (files.ContainsKey(variable)) return files[variable];
            else return null;
        }

        public void saveFile(string variable, FileConfig file)
        {
            files[variable] = file;
        }
    }
}
