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

    public void classifyAttack(AttackInfo attack, Pokemon attacker, Pokemon defender)
    {
        if(attack.category == Category.physical || attack.category == Category.special)
        {
            physicalAttack(attack, attacker, defender);
        }
    }

    // TODO: Check and apply type strengths
    //Calculate and apply physical attacks
    public void physicalAttack(AttackInfo attack, Pokemon attacker, Pokemon defender)
    {
        float A = attacker.level;
        float B = 1f;
        float C = attack.power;
        float D = 1f;
        float X = 1f;
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

        if(attacker.pokemonType == attack.attackType)
        {
            X = 1.5f;
        }

        float damage = (((((((2 * A) / 5) + 2) * B * D) / 50) + 2) * X * (Y / 10) * Z / 255);

        defender.currentStats.hp -= (int) damage;
        Debug.Log(damage);
    }
}