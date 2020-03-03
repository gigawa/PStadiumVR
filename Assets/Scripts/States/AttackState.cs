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
        QueueAttacks();
        battleController.StartCoroutine("PerformAttacks", attackQueue);
    }

    public override void Exit()
    {
        base.Exit();

        UnsubscribeToEvents();
    }

    void SubscribeToEvents()
    {
        battleController.onAttackCompleted += FinishAttacks;
    }

    void UnsubscribeToEvents()
    {
        battleController.onAttackCompleted -= FinishAttacks;
    }

    void QueueAttacks ()
    {
        attackQueue = new Queue<Tuple<Pokemon, int>>();
        
        if(battleController.playerPokemonControl.currentStats.speed > battleController.enemyPokemonControl.currentStats.speed)
        {
            attackQueue.Enqueue(new Tuple<Pokemon, int>(battleController.playerPokemonControl, battleController.currentRoundInfo.playerAttackIndex));
            attackQueue.Enqueue(new Tuple<Pokemon, int> (battleController.enemyPokemonControl, battleController.currentRoundInfo.enemyAttackIndex));
        }else
        {
            attackQueue.Enqueue(new Tuple<Pokemon, int>(battleController.enemyPokemonControl, battleController.currentRoundInfo.enemyAttackIndex));
            attackQueue.Enqueue(new Tuple<Pokemon, int>(battleController.playerPokemonControl, battleController.currentRoundInfo.playerAttackIndex));
        }
    }

    public void FinishAttacks()
    {
        machine.ChangeState<IdleState>();
    }
}
