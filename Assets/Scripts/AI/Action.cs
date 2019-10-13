using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents something that can be done by an agent
/// </summary>
public abstract class Action : ScriptableObject
{
    public abstract void Act(IStateController controller); 
}
