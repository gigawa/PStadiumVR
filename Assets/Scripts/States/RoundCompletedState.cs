using UnityEngine;
using System.Collections;

public class RoundCompletedState : ByTheTale.StateMachine.State
{

    // Use this for initialization
    public override void Enter()
    {
        base.Enter();

        machine.ChangeState<IdleState>();
    }
}
