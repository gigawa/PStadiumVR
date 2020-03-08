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
        }else if(attack.category == Category.status && attack.stat != Stat.none)
        {
            StatusMove(attack, attacker, defender);
        }

        if(attack.condition != StatusEffects.none)
        {

        }
    }

    public void StatusMove(AttackInfo attack, Pokemon attacker, Pokemon defender)
    {
        if(attack.affectsSelf)
        {
            attacker.AdjustStatStage(attack.stat, attack.stage);
        }else
        {
            defender.AdjustStatStage(attack.stat, attack.stage);
        }
        
    }

    // TODO: Check and apply type strengths
    // Calculate and apply physical attacks
    public void PhysicalAttack(AttackInfo attack, Pokemon attacker, Pokemon defender)
    {
        if(defender.noDamage.Contains(attack.attackType))
        {
            Debug.Log("No Damage");
        } else
        {
            float A = attacker.level;
            float B = 1f;
            float C = attack.power;
            float D = 1f;

            float Y = 1f;
            float Z = 255f;

            if (attack.category == Category.physical)
            {
                int attackMod = attacker.statStages.attack > 0 ? (2 + attacker.statStages.attack) / 2 : 2 / (2 + (-1 * attacker.statStages.attack));
                int defenseMod = attacker.statStages.defense > 0 ? (2 + defender.statStages.defense) / 2 : 2 / (2 + (-1 * defender.statStages.defense));
                B = attacker.effectiveStats.attack * attackMod;
                D = defender.effectiveStats.defense * defenseMod;
            }
            else
            {
                int attackMod = attacker.statStages.spAtk > 0 ? (2 + attacker.statStages.spAtk) / 2 : 2 / (2 + (-1 * attacker.statStages.spAtk));
                int defenseMod = attacker.statStages.spDef > 0 ? (2 + defender.statStages.spDef) / 2 : 2 / (2 + (-1 * defender.statStages.spDef));
                B = attacker.effectiveStats.spAtk * attackMod;
                D = defender.effectiveStats.spDef * defenseMod;
            }

            float X = attacker.pokemonType == attack.attackType ? 1.5f : 1f;
            X *= defender.weak.Contains(attack.attackType) ? defender.weak.FindAll(a => a == attack.attackType).Count * 2 : 1;
            X /= defender.strong.Contains(attack.attackType) ? defender.strong.FindAll(a => a == attack.attackType).Count * 2 : 1;

            float damage = (((((((2 * A) / 5) + 2) * B * D) / 50) + 2) * X * (Y / 10) * Z / 255);

            defender.AdjustHP(-1 * (int) (damage > 1 ? damage : 1));
            Debug.Log(damage);
        }
    }
}