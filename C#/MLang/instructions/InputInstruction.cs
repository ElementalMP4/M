using MLang.entities;

namespace MLang.instructions
{
    class InputInstruction : AbstractInstruction
    {
        public override void execute(FlowController controller, Registry registry, List<string> args)
        {
            string register = args[0];
            args.RemoveAt(0);
            string question = string.Join(" ", args);
            Console.Write(question);
            string input = Console.ReadLine();
            registry.saveVar(register, input);
        }

        public override string getName()
        {
            return "INPUT";
        }
    }
}
