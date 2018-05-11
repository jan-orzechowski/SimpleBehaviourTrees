using System;
using System.Collections.Generic;

namespace SimpleBehaviourTrees
{
    class RepeatUntilFail : DecoratorNode
    {
        public RepeatUntilFail(Node child) : base(child) { }

        public override TickResult Tick(AgentMemory agentMemory)
        {
            TickResult result;

            do
            {
                result = TickChild(Child, agentMemory);
            }
            while (result == TickResult.Success || result == TickResult.Running);

            return result;
        }
    }
}