namespace MLang.entities
{
    class FlowController
    {
        private bool continueExecution;
        private string currentKey;
        private string nextKey;

        public FlowController()
        {
            this.continueExecution = true;
            this.currentKey = "";
            this.nextKey = "";
        }

        public bool keepExecuting()
        {
            return this.continueExecution;
        }

        public void stopExecution()
        {
            this.continueExecution = false;
        }

        public string getKey()
        {
            return this.currentKey;
        }


        public void setKey(string key)
        {
            this.currentKey = key;
        }

        public string getNextKey()
        {
            return this.nextKey;
        }

        public void setNextKey(string key)
        {
            this.nextKey = key;
        }
    }
}
