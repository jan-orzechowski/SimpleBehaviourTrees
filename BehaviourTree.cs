using System;
using System.Collections.Generic;

namespace SimpleBehaviourTrees
{
    class BehaviourTree
    {
        public Node Root { get; protected set; }

        Dictionary<int, Node> nodes = new Dictionary<int, Node>();

        public TickResult Tick(AgentMemory agentMemory)
        {
            agentMemory.ResetActiveNodesList();
            agentMemory.ProcessTimers(agentMemory.DeltaTime);
            agentMemory.CurrentTree = this;

            TickResult result = Node.TickChild(Root, agentMemory);           
            return result;
        }

        public Node GetNodeByID(int id)
        {
            if (nodes.ContainsKey(id)) return nodes[id];
            else return null;
        }

        void AssignIDs()
        {
            int idCounter = 1;
            Root.AssignID(0, ref idCounter, nodes);
        }

        CompositeNode Subtree(CompositeNode parent, params Node[] nodes)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                parent.Add(nodes[i]);
            }

            return parent;
        }

        public void LoadTree()
        {
            Root =
            Subtree(new Priority(),
                        Subtree(new MemSequence(),
                                     new Wait(1f),
                                     new PrintMessage("We waited one second"),
                                     new Wait(1f),
                                     new PrintMessage("And another")
                            ),
                        Subtree(new Sequence(),
                                    new Inverter(new AlwaysSucceed()),
                                    new PrintMessage("This will never tick")
                            ),
                        new ExampleNode(),
                        new PrintMessage("Last node ticked")
            );

            AssignIDs();
        }
    }
}