using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision
{
    public override bool Decide(IStateController controller)
    {
        bool targetVisible = Look((StateController)controller);
        return targetVisible;
    }

    private bool Look(StateController controller)
    {
        Debug.DrawLine(controller.eyes.position, controller.eyes.position + controller.eyes.forward * controller.maxSight);
        RaycastHit hit;
        if (Physics.SphereCast(controller.eyes.position, controller.sightRadius, controller.eyes.forward, out hit, controller.maxSight) && hit.collider.CompareTag("Player"))
        {
            PlayerVisibility pv = hit.transform.GetComponent<PlayerVisibility>();
            if (pv.IsVisible)
            {
                controller.chaseTarget = hit.transform;
                controller.lastTargetSightTime = Time.time;
                return true;
            }
        }
        bool stillInMemory = Time.time - controller.lastTargetSightTime < controller.targetSightMemoryDuration;
        return stillInMemory;
    }
}
