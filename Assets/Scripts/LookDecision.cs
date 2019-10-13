using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    private bool Look(StateController controller)
    {
        Debug.DrawRay(controller.eyes.position, controller.transform.forward * 2.0f, Color.green);
        RaycastHit hit;
        if (Physics.SphereCast(controller.eyes.position, 2.0f, controller.eyes.forward, out hit, 5.0f) && hit.collider.CompareTag("Player"))
        {
            controller.chaseTarget = hit.transform;
            return true;
        }
        return false;
    }
}
