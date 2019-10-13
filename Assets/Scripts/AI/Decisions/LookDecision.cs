﻿using System.Collections;
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
        RaycastHit hit;
        if (Physics.SphereCast(controller.eyes.position, controller.sightRadius, controller.eyes.forward, out hit, controller.maxSight) && hit.collider.CompareTag("Player"))
        {
            PlayerVisibility pv = hit.transform.GetComponent<PlayerVisibility>();
            if (pv.IsVisible)
            {
                controller.chaseTarget = hit.transform;
                return true;
            }
        }
        return false;
    }
}
