using System;
using System.Collections.Generic;

namespace SimpleBehaviourTrees
{
    class AlwaysSucceed : Node
    {
        public override TickResult Tick(AgentMemory agentMemory)
        {
            return TickResult.Success;
        }
    }
}
