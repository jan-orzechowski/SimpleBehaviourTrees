using System;
using System.Collections.Generic;

namespace SimpleBehaviourTrees
{
    // Ticks children until first failure/error
    // Memorises the last running node and starts ticking from it
    class MemSequence : CompositeNode
    {
        public override TickResult Tick(AgentMemory am)
        {
            if (am.IsRunning(ID) == false)
            {
                am.SetInt(ID, "lastRunningNode", 0);
                am.StartRunning(ID);
            }

            for (int node = am.GetInt(ID, "lastRunningNode", 0); node < Children.Count; node++)
            {
                TickResult result = TickChild(Children[node], am);

                if (result == TickResult.Success)
                {
                    // Sprawdzamy następny
                    continue;
                }
                else if (result == TickResult.Running)
                {
                    am.SetInt(ID, "lastRunningNode", node);
                    return result;
                }
                else
                {
                    // Porażka lub błąd - zaczynamy od nowa
                    am.StopRunning(ID);
                    return result;
                }
            }

            am.StopRunning(ID);
            return TickResult.Success;
        }
    }
}