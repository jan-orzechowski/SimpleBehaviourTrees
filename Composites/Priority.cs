using System;
using System.Collections.Generic;

namespace SimpleBehaviourTrees
{
    // Checks children until first success
    // If no children succeeded, returns failure
    class Priority : CompositeNode
    {
        public override TickResult Tick (AgentMemory agentMemory)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                TickResult result = TickChild(Children[i], agentMemory);

                if (result == TickResult.Failure)
                {
                    continue;
                }
                else
                {
                    return result;
                }
            }

            return TickResult.Failure;
        }
    }
}