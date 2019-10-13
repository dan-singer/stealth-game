using UnityEngine;

/// <summary>
/// Represents a decision that can be made by an agent.
/// </summary>
public abstract class Decision : ScriptableObject
{
    public abstract bool Decide(IStateController controller);
    
}