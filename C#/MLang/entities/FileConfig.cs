namespace MLang.entities
{
    public class FileConfig
    {
        private string path;
        private string mode;

        public FileConfig setPath(string path)
        {
            this.path = path;
            return this;
        }

        public FileConfig setMode(string mode)
        {
            this.mode = mode;
            return this;
        }

        public string getPath()
        {
            return path;
        }

        public string getMode()
        {
            return mode;
        }
    }
}
