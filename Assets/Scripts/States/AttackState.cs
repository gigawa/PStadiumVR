using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AttackState : ByTheTale.StateMachine.State
{
    BattleController battleController;

    private Queue<Tuple<Pokemon, int>> attackQueue;

    public override void Initialize()
    {
        base.Initialize();

        battleController = BattleController.Instance;
    }

    public override void Enter()
    {
        base.Enter();

        SubscribeToEvents();
        Attack();
    }

    public override void Exit()
    {
        base.Exit();

        UnsubscribeToEvents();
    }

    void SubscribeToEvents()
    {
        battleController.onAttackCompleted += FinishAttack;
    }

    void UnsubscribeToEvents()
    {
        battleController.onAttackCompleted -= FinishAttack;
    }

    void Attack()
    {
        battleController.StartCoroutine("PerformAttack");
    }

    public void FinishAttack()
    {
        var attackTuple = battleController.currentRoundInfo.attackQueue.Dequeue();
        var attackPokemon = attackTuple.Item1;
        var defendPokemon = attackTuple.Item3;
        var attack = attackPokemon.attacks[attackTuple.Item2];
        DamageCalculation.Instance.ApplyAttack(attack, attackPokemon, defendPokemon);
        
        if (battleController.currentRoundInfo.attackQueue.Count > 0)
        {
            Attack();
        }
        else
        {
            machine.ChangeState<IdleState>();
        }
    }
}
