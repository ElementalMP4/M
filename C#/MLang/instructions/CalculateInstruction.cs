using MLang.entities;
using System.Data;

namespace MLang.instructions
{
    class CalculateInstruction : AbstractInstruction
    {
        public override void execute(FlowController controller, Registry registry, List<string> args)
        {
            string register = args[0];
            args.RemoveAt(0);
            string result = evaluate(String.Join(" ", args)).ToString();

            registry.saveVar(register, result);
        }

        private double evaluate(string expression)
        {
            var loDataTable = new DataTable();
            var loDataColumn = new DataColumn("Eval", typeof(double), expression);
            loDataTable.Columns.Add(loDataColumn);
            loDataTable.Rows.Add(0);
            return (double)(loDataTable.Rows[0]["Eval"]);
        }

        public override string getName()
        {
            return "CALC";
        }
    }
}
