using UnityEngine;
using System.Collections;

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
        
        RoundInfo roundInfo = new RoundInfo();
        roundInfo.playerAttackIndex = index;
        roundInfo.enemyAttackIndex = SelectEnemyAttack();
        battleController.currentRoundInfo = roundInfo;

        machine.ChangeState<AttackState>();
    }

    int SelectEnemyAttack()
    {
        return Random.Range(0, battleController.enemyPokemonControl.knownAttacks.Length);
    }
}
