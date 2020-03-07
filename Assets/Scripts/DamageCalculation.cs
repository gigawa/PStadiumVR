using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokemonInfo;

public class DamageCalculation
{

    private static DamageCalculation instance = null;
    private static readonly object padlock = new object();

    DamageCalculation()
    {
    }

    public static DamageCalculation Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DamageCalculation();
                }
                return instance;
            }
        }
    }

    public void ApplyAttack(AttackInfo attack, Pokemon attacker, Pokemon defender)
    {
        if(attack.category == Category.physical || attack.category == Category.special)
        {
            PhysicalAttack(attack, attacker, defender);
        }
    }

    // TODO: Check and apply type strengths
    // Calculate and apply physical attacks
    public void PhysicalAttack(AttackInfo attack, Pokemon attacker, Pokemon defender)
    {
        float A = attacker.level;
        float B = 1f;
        float C = attack.power;
        float D = 1f;
        
        float Y = 1f;
        float Z = 255f;

        if (attack.category == Category.physical)
        {
            B = attacker.currentStats.attack;
            D = defender.currentStats.defense;
        }else
        {
            B = attacker.currentStats.spAtk;
            D = defender.currentStats.spDef;
        }
        
        float X = attacker.pokemonType == attack.attackType ? 1.5f : 1f;
        X *= defender.weak.Contains(attack.attackType) ? defender.weak.FindAll(a => a == attack.attackType).Count * 2 : 1;
        X /= defender.strong.Contains(attack.attackType) ? defender.strong.FindAll(a => a == attack.attackType).Count * 2 : 1;
        X *= defender.noDamage.Contains(attack.attackType) ? 0 : 1;

        float damage = (((((((2 * A) / 5) + 2) * B * D) / 50) + 2) * X * (Y / 10) * Z / 255);

        defender.AdjustHP(-1 * (int)damage);
        Debug.Log(damage);
    }
}