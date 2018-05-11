using System;
using System.Collections.Generic;

namespace SimpleBehaviourTrees
{
    class CompositeNode : Node
    {
        public List<Node> Children { get; protected set; }

        public CompositeNode(params Node[] children)
        {
            Children = new List<Node>(children);
        }

        public Node Add(Node node)
        {
            Children.Add(node);
            return node;
        }

        public override void AssignID(int parentId, ref int idCounter, Dictionary<int, Node> nodes)
        {
            base.AssignID(parentId, ref idCounter, nodes);

            foreach (Node child in Children)
            {
                child.AssignID(ID, ref idCounter, nodes);
            }
        }
    }
}