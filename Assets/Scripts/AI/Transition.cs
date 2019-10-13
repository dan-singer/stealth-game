using UnityEngine;

[System.Serializable]
/// <summary>
/// Represents an arrow on a behavior tree. A decision is evaulated, and takes one of two branching states.
/// </summary>
public class Transition
{
    public Decision decision;
    public State trueState;
    public State falseState;
}