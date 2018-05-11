using System;
using System.Collections.Generic;

namespace SimpleBehaviourTrees
{
    class AlwaysFail : Node
    {
        public override TickResult Tick(AgentMemory agentMemory)
        {
            return TickResult.Failure;
        }
    }
}