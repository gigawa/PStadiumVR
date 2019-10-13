using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokemonInfo;   

public class BattleController : MonoBehaviour {

    public GameObject playerPokemon;
    public GameObject enemyPokemon;
    public bool isPlayerTurn;
    public GameObject attackMenu;
    public bool isAttacking;
    public Pokemon playerPokemonControl;
    public Pokemon enemyPokemonControl;
    public DamageCalculation damageCalculation;

    // Use this for initialization
    void Start () {
        AssignAnimations(playerPokemon.GetComponent<Animator>(), playerPokemonControl);
        AssignAnimations(enemyPokemon.GetComponent<Animator>(), enemyPokemonControl);
        damageCalculation = DamageCalculation.Instance;
    }

    // Update is called once per frame
    void Update () {

	}

    public void AssignAnimations(Animator animator, Pokemon pokemon)
    {
        AnimatorOverrideController animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);

        animatorOverrideController["Attack0"] = pokemon.attacks[0].animationClip;
        animatorOverrideController["Attack1"] = pokemon.attacks[1].animationClip;
        animatorOverrideController["Attack2"] = pokemon.attacks[2].animationClip;
        animatorOverrideController["Attack3"] = pokemon.attacks[3].animationClip;

        animator.runtimeAnimatorController = animatorOverrideController;
    }

    public void Attack(int index, Animator animator)
    {
        animator.SetInteger("AttackIndex", index);
        animator.SetTrigger("Attack");
        StartCoroutine(WaitForAttackAnimation(animator, index));
    }

    public void PlayerAttack(int attackIndex)
    {
        if (isPlayerTurn)
        {
            Attack(attackIndex, playerPokemon.GetComponent<Animator>());
            attackMenu.SetActive(false);
        }
    }

    public void EnemyAttack()
    {
        int attackIndex = Random.Range(0, 4);
        Attack(attackIndex, enemyPokemon.GetComponent<Animator>());
    }

    public void AttackOver(int index)
    {
        if(isPlayerTurn)
        {
            isPlayerTurn = false;
            damageCalculation.classifyAttack(playerPokemonControl.attacks[index], playerPokemonControl, enemyPokemonControl);
            StartCoroutine(WaitToAttack());
        }else
        {
            isPlayerTurn = true;
            damageCalculation.classifyAttack(playerPokemonControl.attacks[index], enemyPokemonControl, playerPokemonControl);
            attackMenu.SetActive(true);
        }
        isAttacking = false;
    }

    IEnumerator WaitForAttackAnimation(Animator animator, int index)
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.5f);
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            yield return new WaitForSeconds(0.1f);
        }
        AttackOver(index);
    }

    IEnumerator WaitToAttack()
    {
        yield return new WaitForSeconds(0.5f);
        EnemyAttack();
    }
}