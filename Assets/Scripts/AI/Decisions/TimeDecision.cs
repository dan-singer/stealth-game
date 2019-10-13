using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/Time")]
public class TimeDecision : Decision
{
    public float duration = 4.0f;
    public override bool Decide(IStateController controller)
    {
        return TimePassed((StateController)controller);
    }

    private bool TimePassed(StateController controller)
    {
        return Time.time - controller.stateEnterTime >= duration;
    }
}