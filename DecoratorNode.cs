using System;
using System.Collections.Generic;

namespace SimpleBehaviourTrees
{
    class DecoratorNode : Node
    {
        public Node Child { get; protected set; }

        public DecoratorNode(Node child)
        {
            Child = child;
        }

        public override void AssignID(int parentId, ref int idCounter, Dictionary<int, Node> nodes)
        {
            base.AssignID(parentId, ref idCounter, nodes);

            Child.AssignID(ID, ref idCounter, nodes);
        }
    }
}