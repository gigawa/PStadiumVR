using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokemonInfo;
using System;
using UnityEngine.UI;

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

    public Slider enemyHPBar;
    public Slider playerHPBar;
    public Text enemyNameText;
    public Text playerNameText;
    public Text enemyLvlText;
    public Text playerLvlText;
    public Text playerHPText;

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

    public override void Start()
    {
        base.Start();
        attackMenu.SetAttackText(playerPokemonControl.knownAttacks);
        SetUI();
    }

    public void SetUI ()
    {
        playerHPBar.maxValue = playerPokemonControl.effectiveStats.hp;
        playerHPBar.value = playerPokemonControl.currentHP;
        playerNameText.text = playerPokemonControl.pokemonName;
        playerLvlText.text = "Lvl " + playerPokemonControl.level;
        playerHPText.text = playerPokemonControl.currentHP + " / " + playerPokemonControl.effectiveStats.hp;

        enemyHPBar.maxValue = enemyPokemonControl.effectiveStats.hp;
        enemyHPBar.value = enemyPokemonControl.currentHP;
        enemyNameText.text = enemyPokemonControl.pokemonName;
        enemyLvlText.text = "Lvl " + enemyPokemonControl.level;
    }
    public void UpdateHPUI()
    {
        playerHPBar.value = playerPokemonControl.currentHP;
        playerHPText.text = playerPokemonControl.currentHP + " / " + playerPokemonControl.effectiveStats.hp;
        enemyHPBar.value = enemyPokemonControl.currentHP;
    }

    public override void AddStates()
    {
        AddState<IdleState>();
        AddState<AttackState>();
        AddState<RoundCompletedState>();

        SetInitialState<IdleState>();
        Debug.Log(initialState);
    }

    public void Attack(Queue<Tuple<Pokemon, int, Pokemon>> attackQueue)
    {
        var attack = attackQueue.Peek();
        attack.Item1.Attack(attack.Item2);
    }

    IEnumerator PerformAttack()
    {
        Attack(currentRoundInfo.attackQueue);
        yield return new WaitForSeconds(1f);

        onAttackCompleted();
    }
}