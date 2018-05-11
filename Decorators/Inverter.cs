using System;
using System.Collections.Generic;

namespace SimpleBehaviourTrees
{
    class Inverter : DecoratorNode
    {
        public Inverter(Node child) : base(child) { }

        public override TickResult Tick(AgentMemory agentMemory)
        {
            TickResult result = TickChild(Child, agentMemory);

            if (result == TickResult.Failure)
            {
                return TickResult.Success;
            }
            else if (result == TickResult.Success)
            {
                return TickResult.Failure;
            }
            else
            {
                return result;
            }
        }
    }
}