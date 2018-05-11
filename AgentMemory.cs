using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleBehaviourTrees
{
    class AgentMemory
    {
        public BehaviourTree CurrentTree;

        public float DeltaTime;

        Dictionary<int, Dictionary<string, float>> floats;
        Dictionary<int, Dictionary<string, int>> ints;
        Dictionary<int, object> objects;
        Dictionary<int, float> timers;

        HashSet<int> runningNodes;

        List<int> nodesActivePreviousTick = new List<int>();
        List<int> nodesActiveThisTick = new List<int>();

        public AgentMemory()
        {
            floats = new Dictionary<int, Dictionary<string, float>>();
            ints = new Dictionary<int, Dictionary<string, int>>();
            objects = new Dictionary<int, object>();
            timers = new Dictionary<int, float>();
            runningNodes = new HashSet<int>();
        }

        public void ResetActiveNodesList()
        {
            nodesActivePreviousTick = nodesActiveThisTick;
            nodesActiveThisTick = new List<int>();
        }

        public void ActivateNode(int nodeToActivateID)
        {
            nodesActiveThisTick.Add(nodeToActivateID);

            if (nodesActiveThisTick.Count > nodesActivePreviousTick.Count)
            {
                CurrentTree.GetNodeByID(nodeToActivateID).Activate(this);
                return;
            }
            
            int currentIndex = (nodesActiveThisTick.Count - 1);
            if (nodesActiveThisTick[currentIndex] != nodesActivePreviousTick[currentIndex])
            {
                for (int previousTickNodeIndex = (nodesActivePreviousTick.Count - 1);
                    previousTickNodeIndex >= currentIndex;
                    previousTickNodeIndex--)
                {
                    int nodeToDeactivateID = nodesActivePreviousTick[previousTickNodeIndex];

                    nodesActivePreviousTick.RemoveAt(previousTickNodeIndex);

                    CurrentTree.GetNodeByID(nodeToDeactivateID).Deactivate(this);
                }

                CurrentTree.GetNodeByID(nodeToActivateID).Activate(this);
            }
        }

        public string PrintNodesActiveThisTick()
        {
            if (nodesActiveThisTick == null || nodesActiveThisTick.Count == 0)
            {
                return "No active nodes";
            }
            else
            {
                string result = "Active nodes: ";
                result += nodesActiveThisTick[0].ToString();
                for (int i = 1; i < nodesActiveThisTick.Count; i++)
                {
                    result += (", " + nodesActiveThisTick[i]);
                }
                return result;
            }
        }

        public void DeactivateNode(int nodeToDeactivateID)
        {
            if (nodesActiveThisTick.Contains(nodeToDeactivateID) == false ||
                nodesActiveThisTick[nodesActiveThisTick.Count - 1] != nodeToDeactivateID)
            {          
                return;
            }

            nodesActiveThisTick.RemoveAt(nodesActiveThisTick.Count - 1);
            CurrentTree.GetNodeByID(nodeToDeactivateID).Deactivate(this);
        }

        public float GetFloat(int id, string key, float notFoundValue = 0f)
        {
            if (floats.ContainsKey(id) && floats[id].ContainsKey(key))
            {
                return floats[id][key];
            }
            else
            {
                return notFoundValue;
            }
        }

        public void SetFloat(int id, string key, float value)
        {
            if (floats.ContainsKey(id) == false)
            {
                floats.Add(id, new Dictionary<string, float>());
            }
            floats[id][key] = value;
        }

        public int GetInt(int id, string key, int notFoundValue = 0)
        {
            if (ints.ContainsKey(id) && ints[id].ContainsKey(key))
            {
                return ints[id][key];
            }
            else
            {
                return notFoundValue;
            }
        }

        public void SetInt(int id, string key, int value)
        {
            if (ints.ContainsKey(id) == false)
            {
                ints.Add(id, new Dictionary<string, int>());
            }
            ints[id][key] = value;
        }

        public object GetObject(int id)
        {
            if (objects.ContainsKey(id)) return objects[id];
            else return null;
        }

        public void SetObject(int id, object obj)
        {
            objects[id] = obj;
        }

        public void StartRunning(int id)
        {
            if (runningNodes.Contains(id) == false) runningNodes.Add(id);
        }

        public void StopRunning(int id)
        {
            if (runningNodes.Contains(id)) runningNodes.Remove(id);
        }

        public bool IsRunning(int id)
        {
            return (runningNodes.Contains(id));
        }

        public void SetTimer(int id, float timeBeforeNextActivation)
        {
            if (timers.ContainsKey(id) == false)
            {
                timers.Add(id, timeBeforeNextActivation);
            }
            else
            {
                timers[id] = timeBeforeNextActivation;
            }
        }

        public bool HasTimerElapsed(int id)
        {
            return (timers.ContainsKey(id) == false);
        }

        public void ProcessTimers(float deltaTime)
        {
            foreach (int id in timers.Keys.ToList())
            {
                timers[id] -= deltaTime;
                if (timers[id] <= 0)
                {
                    timers.Remove(id);
                }
            }
        }
    }
}