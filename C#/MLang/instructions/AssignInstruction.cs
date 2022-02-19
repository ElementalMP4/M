using MLang.entities;

namespace MLang.instructions
{
    internal class AssignInstruction : AbstractInstruction
    {
        public override void execute(FlowController controller, Registry registry, List<string> args)
        {
            string register = args[0];
            string filePath = args[1];
            string fileMode = args[2];

            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine("File " + filePath + " could not be found");
                controller.stopExecution();
            }
            else if (!(fileMode.Equals("READ") || fileMode.Equals("WRITE")))
            {
                Console.Error.WriteLine("Invalid file mode provided. Use READ or WRITE");
                controller.stopExecution();
            } else
            {
                registry.saveFile(register, new FileConfig().setPath(filePath).setMode(fileMode));
            }
        }

        public override string getName()
        {
            return "ASSIGN";
        }
    }
}
