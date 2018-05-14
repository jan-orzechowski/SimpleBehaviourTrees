using System;
using System.Collections.Generic;

namespace SimpleBehaviourTrees
{
    abstract class Node
    {
        public int ID { get; protected set; }
        public int ParentID { get; protected set; }
        public virtual bool Activates { get { return false; } }

        public virtual bool CheckPrecondition(AgentMemory agentMemory)
        {
            return true;
        }

        public virtual void Activate(AgentMemory agentMemory)
        {
            return;
        }

        public virtual void Deactivate(AgentMemory agentMemory)
        {
            return;
        }

        public virtual TickResult Tick(AgentMemory agentMemory)
        {
            return TickResult.Error;
        }

        public virtual void AssignID(int parentId, ref int idCounter, Dictionary<int, Node> nodes)
        {
            ParentID = parentId;
            ID = idCounter;
            idCounter++;

            nodes.Add(ID, this);
        }

        public static TickResult TickChild(Node child, AgentMemory agentMemory)
        {
            if (child.CheckPrecondition(agentMemory) == false) return TickResult.Failure;

            if (child.Activates)
            {
                TickResult result = child.Tick(agentMemory);
                return result;
            }
            else
            {
                agentMemory.ActivateNode(child.ID);
                TickResult result = child.Tick(agentMemory);
                if (result != TickResult.Running) agentMemory.DeactivateNode(child.ID);
                return result;
            }
        }
    }
}