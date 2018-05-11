using System;
using System.Collections.Generic;

namespace SimpleBehaviourTrees
{
    // Ticks children until first failure
    // Returns success, if all children succeeded
    class Sequence : CompositeNode
    {
        public override TickResult Tick(AgentMemory am)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                TickResult result = TickChild(Children[i], am);

                if (result != TickResult.Success)
                {
                    return result;
                }
            }

            return TickResult.Success;
        }
    }
}