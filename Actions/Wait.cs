using System;
using System.Collections.Generic;

namespace SimpleBehaviourTrees
{
    class Wait : Node
    {
        float waitingTime;

        public Wait(float waitingTime)
        {
            this.waitingTime = waitingTime;
        }

        public override TickResult Tick(AgentMemory agentMemory)
        {
            if (agentMemory.IsRunning(ID) == false)
            {
                agentMemory.SetFloat(ID, "timer", waitingTime);
                agentMemory.StartRunning(ID);
            }

            float timer = agentMemory.GetFloat(ID, "timer", 0f);
            timer -= agentMemory.DeltaTime;
            agentMemory.SetFloat(ID, "timer", timer);

            if (timer <= 0)
            {
                agentMemory.StopRunning(ID);
                return TickResult.Success;
            }
            else
            {
                return TickResult.Running;
            }
        }

        public void ChangeWaitingTime(float newTime)
        {
            waitingTime = newTime;
        }
    }
}