using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokemonInfo;

[System.Serializable]
public class AttackInfo
{
    public string name;
    public Category category;
    public PokemonType attackType;
    public int power;
    public int accuracy;
    public int pp;

    //For status moves only
    public StatusEffects condition;
    public Stat stat;
    public int stage;

    public float reactionTime;

    public AnimationClip animationClip;

    //Attack with condition
    public AttackInfo(string attackName, Category acategory, PokemonType atype, int app, int apower, int aaccuracy, StatusEffects acondition, Stat astat, int astage)
    {
        name = attackName;
        category = acategory;
        attackType = atype;
        pp = app;
        power = apower;
        accuracy = aaccuracy;
        condition = acondition;
        stat = astat;
        stage = astage;
    }

    //Attack only
    public AttackInfo(string attackName, Category acategory, PokemonType atype, int app, int apower, int aaccuracy)
    {
        name = attackName;
        category = acategory;
        attackType = atype;
        pp = app;
        power = apower;
        accuracy = aaccuracy;
        condition = StatusEffects.none;
        stat = Stat.none;
        stage = 0;
    }

    //Status Only
    public AttackInfo(string attackName, PokemonType atype, int app, int aaccuracy, Stat astat, int astage)
    {
        name = attackName;
        category = Category.status;
        attackType = atype;
        pp = app;
        power = 0;
        accuracy = aaccuracy;
        condition = StatusEffects.none;
        stat = astat;
        stage = astage;
    }

    //Condition Only
    public AttackInfo(string attackName, PokemonType atype, int app, int aaccuracy, StatusEffects acondition)
    {
        name = attackName;
        category = Category.status;
        attackType = atype;
        pp = app;
        accuracy = aaccuracy;
        condition = acondition;
        stat = Stat.none;
        stage = 0;
    }
}