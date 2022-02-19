using MLang.entities;
using System;

namespace MLang.instructions 
{
    class PrintInstruction : AbstractInstruction
    {
        public override void execute(FlowController controller, Registry registry, List<string> args)
        {
            Console.WriteLine(string.Join(" ", args));
        }

        public override string getName()
        {
            return "PRINT";
        }
    }
}
