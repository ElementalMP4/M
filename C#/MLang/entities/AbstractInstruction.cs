namespace MLang.entities
{
    abstract class AbstractInstruction
    {
        public abstract void execute(FlowController controller, Registry registry, List<string> args);
        public abstract string getName();
    }
}
