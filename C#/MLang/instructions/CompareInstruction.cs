using MLang.entities;
using MLang.utils;

namespace MLang.instructions
{
    class CompareInstruction : AbstractInstruction
    {
        public override void execute(FlowController controller, Registry registry, List<string> args)
        {
            string label = args[0];
            string registerA = args[1];
            string mode = args[2];
            string registerB = args[3];

            try
            {
                switch (mode)
                {
                    case CompareMode.EQUAL:
                        if (registerA.Equals(registerB)) controller.setNextKey(label);
                        break;
                    case CompareMode.NOT_EQUAL:
                        if (!registerA.Equals(registerB)) controller.setNextKey(label);
                        break;
                    case CompareMode.GREATER_THAN:
                        if (Double.Parse(registerA) > Double.Parse(registerB)) controller.setNextKey(label);
                        break;
                    case CompareMode.LESS_THAN:
                        if (Double.Parse(registerA) < Double.Parse(registerB)) controller.setNextKey(label);
                        break;
                    default:
                        Console.Error.WriteLine("Invalid comparison mode " + mode);
                        controller.stopExecution();
                        break;
                }
            }
            catch (FormatException e)
            {
                Console.Error.WriteLine("Parameter could not be parsed to number");
                controller.stopExecution();
            }
        }

        public override string getName()
        {
            return "COMPARE";
        }
    }
}
