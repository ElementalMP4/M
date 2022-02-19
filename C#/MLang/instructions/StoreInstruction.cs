using MLang.entities;

namespace MLang.instructions
{
    class StoreInstruction : AbstractInstruction
    {
        public override void execute(FlowController controller, Registry registry, List<string> args)
        {
            string register = args[0];
            args.RemoveAt(0);
            registry.saveVar(register, String.Join(" ", args));
        }

        public override string getName()
        {
            return "STORE";
        }
    }
}
