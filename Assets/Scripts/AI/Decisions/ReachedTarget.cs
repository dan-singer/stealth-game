using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/ReachedTarget")]
public class ReachedTarget : Decision
{
    public override bool Decide(IStateController controller)
    {
        StateController castedController = (StateController)controller;

        bool reachedTarget = Vector3.SqrMagnitude(castedController.transform.position - castedController.target.position) 
                        <= Mathf.Pow(castedController.navMeshAgent.stoppingDistance, 2.0f);

        if (reachedTarget && StateController.HitTarget != null) 
        {
            StateController.HitTarget();
        }
        return reachedTarget;
    }
}
