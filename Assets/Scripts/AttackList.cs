using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokemonInfo;

public class AttackList
{
    public List<AttackInfo> createdAttacks;

    private static AttackList instance = null;
    private static readonly object padlock = new object();

    public static AttackList Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new AttackList();
                }
                return instance;
            }
        }
    }

    AttackList()
    {
        createdAttacks = new List<AttackInfo>();
    }

    public AttackInfo GetAttack(string attackName)
    {
        AttackInfo attack = null;
        bool isCreated = false;

        foreach(AttackInfo created in createdAttacks) {
            if(attackName == created.name)
            {
                attack = created;
                isCreated = true;
                break;
            }
        }

        if(!isCreated)
        {
            attack = CreateAttack(attackName);
        }

        return attack;
    }

    private AttackInfo CreateAttack(string attackName)
    {
        AttackInfo attack = null;
        switch(attackName)
        {
            case "Tackle":
                // Physical Attack
                // Attack Name, Category, Type, PP, Power, Accuracy
                attack = new AttackInfo("Tackle", Category.physical, PokemonType.normal, 35, 40, 100);
                break;

            case "Growl":
                // Status Effect
                // Attack Name, Type, PP, Accuracy, Stat, Stage
                attack = new AttackInfo("Growl", PokemonType.normal, 40, 100, Stat.attack, -1);
                break;

            case "Leech Seed":
                // Status Condition
                // Attack Name, Type, PP, Accuracy, Condition
                attack = new AttackInfo("Leech Seed", PokemonType.grass, 10, 100, StatusEffects.leechSeed);
                break;

            case "Vine Whip":
                // Physical Attack
                // Attack Name, Category, Type, PP, Power, Accuracy
                attack = new AttackInfo("Vine Whip", Category.physical, PokemonType.grass, 25, 45, 100);
                break;

            default:
                attack = new AttackInfo("Unknown", Category.physical, PokemonType.normal, 0, 0, 0);
                break;
        }

        createdAttacks.Add(attack);
        return attack;
    }
}
