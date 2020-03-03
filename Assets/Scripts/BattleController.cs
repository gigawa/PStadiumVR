using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokemonInfo;
using System;

public class BattleController : ByTheTale.StateMachine.MachineBehaviour {

    public GameObject playerPokemon;
    public GameObject enemyPokemon;
    public bool isPlayerTurn;
    public AttackMenu attackMenu;
    public bool isAttacking;
    public Pokemon playerPokemonControl;
    public Pokemon enemyPokemonControl;
    public DamageCalculation damageCalculation;
    public RoundInfo currentRoundInfo;

    public delegate void AttackCompleted();
    public event AttackCompleted onAttackCompleted;

    public static BattleController Instance { get; private set; }

    public override void Initialize()
    {
        base.Initialize();
        Instance = this;
        damageCalculation = DamageCalculation.Instance;
    }

    public void Awake()
    {
        Instance = this;
        damageCalculation = DamageCalculation.Instance;
    }

    public override void AddStates()
    {
        AddState<IdleState>();
        AddState<AttackState>();
        AddState<RoundCompletedState>();

        SetInitialState<IdleState>();
        Debug.Log(initialState);
    }

    public void Attack(Queue<Tuple<Pokemon, int>> attackQueue)
    {
        var attack = attackQueue.Dequeue();
        attack.Item1.Attack(attack.Item2);
    }

    //IEnumerator WaitForAttackAnimation(Animator animator, int index)
    //{
    //    isAttacking = true;
    //    yield return new WaitForSeconds(0.5f);
    //    while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
    //    {
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //    AttackOver(index);
    //}

    //IEnumerator WaitToAttack()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //}

    IEnumerator PerformAttacks(Queue<Tuple<Pokemon, int>> attackQueue)
    {
        while (attackQueue.Count > 0)
        {
            Attack(attackQueue);
            yield return new WaitForSeconds(1f);
        }

        onAttackCompleted();
    }
}