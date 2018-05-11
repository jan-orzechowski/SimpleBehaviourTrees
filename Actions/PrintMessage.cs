using System;
using System.Collections.Generic;

namespace SimpleBehaviourTrees
{
    class PrintMessage : Node
    {
        string message;

        public PrintMessage(string message)
        {
            this.message = message;
        }

        public override TickResult Tick(AgentMemory am)
        {
            System.Diagnostics.Trace.WriteLine(message);
            return TickResult.Success;
        }
    }
}
