using System;
using System.Collections.Generic;

namespace SimpleBehaviourTrees
{
    class MemPriority : CompositeNode
    {
        public override TickResult Tick(AgentMemory agentMemory)
        {
            if (agentMemory.IsRunning(ID) == false)
            {
                agentMemory.SetInt(ID, "lastRunningNode", 0);
                agentMemory.StartRunning(ID);
            }

            for (int node = agentMemory.GetInt(ID, "lastRunningNode", 0); node < Children.Count; node++)
            {
                TickResult result = TickChild(Children[node], agentMemory);

                if (result == TickResult.Failure)
                {
                    continue;
                }
                else if (result == TickResult.Running)
                {
                    agentMemory.SetInt(ID, "lastRunningNode", node);
                    return result;
                }
                else
                {
                    agentMemory.StopRunning(ID);
                    return result;
                }
            }

            agentMemory.StopRunning(ID);
            return TickResult.Failure;
        }
    }
}