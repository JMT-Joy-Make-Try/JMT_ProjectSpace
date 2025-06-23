using JMT.Agent.NPC;

namespace JMT.Agent
{
    public interface INPCComponent
    {
        NPCAgent Agent { get; }
        void Initialize(NPCAgent agent);
    }
}