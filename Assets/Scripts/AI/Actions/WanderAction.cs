using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Wander")]
public class WanderAction : Action
{
    public override void Act(IStateController controller)
    {
        Wander((StateController)controller);
    }

    private void Wander(StateController controller)
    {
        if (controller.wanderTarget == Vector3.zero) {
            controller.wanderTarget = controller.transform.position;
            float angle = Random.Range(0, Mathf.PI * 2);
            float radius = Random.Range(controller.wanderRadius/2.0f, controller.wanderRadius);
            Vector3 offset = new Vector3( Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            controller.wanderTarget += offset;
            controller.navMeshAgent.destination = controller.wanderTarget;
            controller.navMeshAgent.isStopped = false;
        }

        if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending) {
            controller.wanderTarget = Vector3.zero;
        }
    }
}
