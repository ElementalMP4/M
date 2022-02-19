using MLang.entities;

namespace MLang.instructions
{
    internal class SizeInstruction : AbstractInstruction
    {
        public override void execute(FlowController controller, Registry registry, List<string> args)
        {
            string register = args[0];
            string resultRegister = args[1];

            FileConfig config = registry.getFile(register);
            if (config == null)
            {
                Console.Error.WriteLine("No file config found at " + register);
                controller.stopExecution();
            } else
            {
                long length = File.Open(config.getPath(), FileMode.Open).Length;
                registry.saveVar(resultRegister, length.ToString());
            }
        }

        public override string getName()
        {
            return "SIZE";
        }
    }
}
