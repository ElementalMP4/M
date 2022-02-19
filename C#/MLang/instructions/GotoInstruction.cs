using MLang.entities;

namespace MLang.instructions
{
    class GotoInstruction : AbstractInstruction
    {
        public override void execute(FlowController controller, Registry registry, List<string> args)
        {
            controller.setNextKey(args[0]);
        }

        public override string getName()
        {
            return "GOTO";
        }
    }
}
