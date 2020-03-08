using UnityEngine;
using System.Collections;
using System;

public class IdleState : ByTheTale.StateMachine.State
{
    BattleController battleController;

    // Use this for initialization
    public override void Initialize()
    {
        base.Initialize();
        battleController = BattleController.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SubscribeEvents()
    {
        battleController.attackMenu.onAttackSelected += PlayerAttackSelected;
    }

    private void UnsubscribeEvents()
    {
        battleController.attackMenu.onAttackSelected -= PlayerAttackSelected;
    }

    public override void Enter ()
    {
        base.Enter();

        battleController.attackMenu.gameObject.SetActive(true);

        SubscribeEvents();
    }

    public override void Exit()
    {
        base.Exit();

        battleController.attackMenu.gameObject.SetActive(false);

        UnsubscribeEvents();
    }
    
    void PlayerAttackSelected(int index)
    {
        Debug.Log("Attack Selected: " + index);

        QueueAttacks(index, SelectEnemyAttack());

        machine.ChangeState<AttackState>();
    }

    void QueueAttacks(int playerAttack, int enemyAttack)
    {
        RoundInfo roundInfo = new RoundInfo();

        if (battleController.playerPokemonControl.effectiveStats.speed > battleController.enemyPokemonControl.effectiveStats.speed)
        {
            roundInfo.attackQueue.Enqueue(new Tuple<Pokemon, int, Pokemon>(battleController.playerPokemonControl, playerAttack, battleController.enemyPokemonControl));
            roundInfo.attackQueue.Enqueue(new Tuple<Pokemon, int, Pokemon>(battleController.enemyPokemonControl, enemyAttack, battleController.playerPokemonControl));
        }
        else
        {
            roundInfo.attackQueue.Enqueue(new Tuple<Pokemon, int, Pokemon>(battleController.enemyPokemonControl, enemyAttack, battleController.playerPokemonControl));
            roundInfo.attackQueue.Enqueue(new Tuple<Pokemon, int, Pokemon>(battleController.playerPokemonControl, playerAttack, battleController.enemyPokemonControl));
        }

        battleController.currentRoundInfo = roundInfo;
    }

    int SelectEnemyAttack()
    {
        return UnityEngine.Random.Range(0, battleController.enemyPokemonControl.knownAttacks.Length);
    }
}
