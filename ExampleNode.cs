using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBehaviourTrees
{
    // This node just counts ticks - it's purpose is to show how you can implement
    // activation/deactivation logic, node's custom data and add hidden subnodes
    class ExampleNode : Node
    {
        public override bool Activates { get { return true; } }

        Wait hiddenWaitNode;
        
        public ExampleNode()
        {
            hiddenWaitNode = new Wait(5f);
        }

        class ExampleNodeData
        {
            public int NumberOfTicks;
        }

        public override bool CheckPrecondition(AgentMemory agentMemory)
        {
            return true;
        }

        public override void Activate(AgentMemory agentMemory)
        {
            ExampleNodeData data = new ExampleNodeData();
            agentMemory.SetObject(ID, data);
        }

        public override void Deactivate(AgentMemory agentMemory)
        {
            ExampleNodeData data = agentMemory.GetObject(ID) as ExampleNodeData;
            System.Diagnostics.Trace.WriteLine("Number of ticks in five seconds: " + data.NumberOfTicks);
            agentMemory.SetObject(ID, null);
        }

        public override TickResult Tick(AgentMemory agentMemory)
        {
            ExampleNodeData data = agentMemory.GetObject(ID) as ExampleNodeData;
            if (data == null) return TickResult.Error;

            TickResult result = TickChild(hiddenWaitNode, agentMemory);
            if (result == TickResult.Running)
            {
                data.NumberOfTicks++;
                return TickResult.Running;
            }
            else
            {
                return TickResult.Success;
            }
        }
       
        public override void AssignID(int parentId, ref int idCounter, Dictionary<int, Node> nodes)
        {
            base.AssignID(parentId, ref idCounter, nodes);
            hiddenWaitNode.AssignID(ID, ref idCounter, nodes);
        }
    }
}
