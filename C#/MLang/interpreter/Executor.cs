using MLang.entities;
using System.Reflection;

namespace MLang.interpreter
{
    class Executor
    {
        private FlowController controller;
        private Registry registry;
        private List<string> keyList;
        private Dictionary<string, string> keyDictionary;
        private List<AbstractInstruction> instructions = gatherInstructions();

        private List<string> generateKeyList(Dictionary<string, string> source)
        {
            return source.Keys.ToList();
        }

        private void run()
        {
            while (controller.keepExecuting())
            {
                string key;
                if (controller.getKey().Equals(""))
                {
                    key = keyList.First();
                } else if (controller.getNextKey().Equals(""))
                {
                    int currentKeyIndex = keyList.IndexOf(controller.getKey());
                    if (currentKeyIndex == keyList.Count - 1) key = "END";
                    else key = keyList[currentKeyIndex + 1];
                } else
                {
                    key = controller.getNextKey();
                    controller.setNextKey("");
                }
                controller.setKey(key);

                if (key.Equals("END")) controller.stopExecution();
                else
                {
                    string command = keyDictionary[key];
                    List<string> commandTokens = new List<string>(command.Split(" "));
                    string instructionName = commandTokens[0];
                    commandTokens.RemoveAt(0);
                    List<string> args = replaceVariablesWithValues(commandTokens);
                    AbstractInstruction instruction = instructions.Find(i => i.getName().Equals(instructionName));
                    if (instruction == null)
                    {
                        controller.stopExecution();
                        throw new Exception("Unknown command " + instructionName);
                    } else
                    {
                        instruction.execute(controller, registry, args);
                    }
                }
            }
        }

        public List<string> replaceVariablesWithValues(List<string> tokens)
        {
            List<string> newTokens = new List<string>();
            foreach (string token in tokens)
            {
                if (token.StartsWith("$"))
                {
                    string filteredToken = token.Replace("$", "");
                    string value = registry.getVar(filteredToken);
                    if (value == null) newTokens.Add(token);
                    else newTokens.Add(value);
                } else newTokens.Add(token);
            }
            return newTokens;
        }

        public void execute(Dictionary<string, string> source) {
            controller = new FlowController();
            keyList = generateKeyList(source);
            keyDictionary = source;
            registry = new Registry();

            run();
        }

        private static List<AbstractInstruction> gatherInstructions()
        {
            List<Type> types = new List<Type>();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                types.AddRange(assembly.GetTypes()
                    .Where(t => String.Equals(t.Namespace, "MLang.instructions", StringComparison.Ordinal))
                    .ToList());
            }
            List<AbstractInstruction> instructions = new List<AbstractInstruction>();
            foreach (Type type in types)
            {
                AbstractInstruction instruction = (AbstractInstruction) Activator.CreateInstance(type);
                instructions.Add(instruction);
            }
            return instructions;

        }
    }
}
