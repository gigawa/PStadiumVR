using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PokemonInfo;

public abstract class Pokemon : MonoBehaviour {

    [System.Serializable]
    public struct stats
    {
        public int hp;
        public int attack;
        public int defense;
        public int spAtk;
        public int spDef;
        public int speed;
    };

    public string pokemonName;
    public PokemonType pokemonType;
    public int level;

    protected stats baseStats;
    public stats startingStats;
    public stats currentStats;
    public AttackInfo [] attacks;

    protected string [] possibleAttacks;
    protected string [] knownAttacks;

    protected List<PokemonType> weak; //weak against this type
    protected List<PokemonType> strong; //strong against this type
    protected List<PokemonType> noDamage; //does no damage against this

    protected List<StatusEffects> volatileEffects;
    protected List<StatusEffects> nonVolatileEffects;
    protected List<StatusEffects> battleEffects;

    public AttackList attackList;

    public Pokemon ()
    {
        
    }

    // Use this for initialization
    void Start () {
        attacks = new AttackInfo[4];
        attackList = AttackList.Instance;
        SetInfo();
        SetBaseStats();
        SetAttacks();
        startingStats = currentStats = baseStats;
        SetTypeStrength();
        SetAttackAnimations();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void SetInfo() { }
    public virtual void SetAttacks() { }
    public virtual void SetBaseStats() { }

    //This is defending pokemon.
    public void SetTypeStrength()
    {
        PokemonInfo.PokemonType type = pokemonType;

        strong = new List<PokemonType>();
        weak = new List<PokemonType>();
        noDamage = new List<PokemonType>();

        if (type == PokemonType.normal)
        {
            strong.Add(PokemonInfo.PokemonType.fighting);
            noDamage.Add(PokemonInfo.PokemonType.ghost);
        }
        else if (type == PokemonType.fighting)
        {
            strong.Add(PokemonInfo.PokemonType.flying);
            strong.Add(PokemonInfo.PokemonType.psychic);
            weak.Add(PokemonInfo.PokemonType.rock);
            weak.Add(PokemonInfo.PokemonType.bug);
        }
        else if (type == PokemonType.flying)
        {
            strong.Add(PokemonInfo.PokemonType.rock);
            strong.Add(PokemonInfo.PokemonType.electric);
            strong.Add(PokemonInfo.PokemonType.ice);
            weak.Add(PokemonInfo.PokemonType.fighting);
            weak.Add(PokemonInfo.PokemonType.bug);
            weak.Add(PokemonInfo.PokemonType.grass);
            noDamage.Add(PokemonInfo.PokemonType.ground);
        }
        else if (type == PokemonType.poison)
        {
            strong.Add(PokemonInfo.PokemonType.ground);
            strong.Add(PokemonInfo.PokemonType.bug);
            strong.Add(PokemonInfo.PokemonType.psychic);
            weak.Add(PokemonInfo.PokemonType.fighting);
            weak.Add(PokemonInfo.PokemonType.poison);
            weak.Add(PokemonInfo.PokemonType.grass);
        }
        else if (type == PokemonType.ground)
        {
            strong.Add(PokemonInfo.PokemonType.water);
            strong.Add(PokemonInfo.PokemonType.grass);
            strong.Add(PokemonInfo.PokemonType.ice);
            weak.Add(PokemonInfo.PokemonType.poison);
            weak.Add(PokemonInfo.PokemonType.rock);
            noDamage.Add(PokemonInfo.PokemonType.electric);
        }
        else if (type == PokemonType.rock)
        {
            strong.Add(PokemonInfo.PokemonType.fighting);
            strong.Add(PokemonInfo.PokemonType.ground);
            strong.Add(PokemonInfo.PokemonType.water);
            strong.Add(PokemonInfo.PokemonType.grass);
            weak.Add(PokemonInfo.PokemonType.normal);
            weak.Add(PokemonInfo.PokemonType.poison);
            weak.Add(PokemonInfo.PokemonType.flying);
            weak.Add(PokemonInfo.PokemonType.fire);
        }
        else if (type == PokemonType.bug)
        {
            strong.Add(PokemonInfo.PokemonType.flying);
            strong.Add(PokemonInfo.PokemonType.poison);
            strong.Add(PokemonInfo.PokemonType.rock);
            strong.Add(PokemonInfo.PokemonType.fire);
            weak.Add(PokemonInfo.PokemonType.fighting);
            weak.Add(PokemonInfo.PokemonType.ground);
            weak.Add(PokemonInfo.PokemonType.grass);
        }
        else if (type == PokemonType.ghost)
        {
            strong.Add(PokemonInfo.PokemonType.ghost);
            weak.Add(PokemonInfo.PokemonType.poison);
            weak.Add(PokemonInfo.PokemonType.bug);
            noDamage.Add(PokemonInfo.PokemonType.normal);
            noDamage.Add(PokemonInfo.PokemonType.fighting);
        }
        else if (type == PokemonType.fire)
        {
            strong.Add(PokemonInfo.PokemonType.ground);
            strong.Add(PokemonInfo.PokemonType.rock);
            strong.Add(PokemonInfo.PokemonType.water);
            weak.Add(PokemonInfo.PokemonType.bug);
            weak.Add(PokemonInfo.PokemonType.fire);
            weak.Add(PokemonInfo.PokemonType.grass);
        }
        else if (type == PokemonType.water)
        {
            strong.Add(PokemonInfo.PokemonType.grass);
            strong.Add(PokemonInfo.PokemonType.electric);
            weak.Add(PokemonInfo.PokemonType.fire);
            weak.Add(PokemonInfo.PokemonType.water);
            weak.Add(PokemonInfo.PokemonType.ice);
        }
        else if (type == PokemonType.grass)
        {
            strong.Add(PokemonInfo.PokemonType.flying);
            strong.Add(PokemonInfo.PokemonType.poison);
            strong.Add(PokemonInfo.PokemonType.bug);
            strong.Add(PokemonInfo.PokemonType.fire);
            strong.Add(PokemonInfo.PokemonType.ice);
            weak.Add(PokemonInfo.PokemonType.ground);
            weak.Add(PokemonInfo.PokemonType.water);
            weak.Add(PokemonInfo.PokemonType.grass);
            weak.Add(PokemonInfo.PokemonType.electric);
        }
        else if (type == PokemonType.electric)
        {
            strong.Add(PokemonInfo.PokemonType.ground);
            weak.Add(PokemonInfo.PokemonType.flying);
            weak.Add(PokemonInfo.PokemonType.electric);
        }
        else if (type == PokemonType.psychic)
        {
            strong.Add(PokemonInfo.PokemonType.bug);
            weak.Add(PokemonInfo.PokemonType.fighting);
            weak.Add(PokemonInfo.PokemonType.psychic);
            noDamage.Add(PokemonInfo.PokemonType.ghost);
        }
        else if (type == PokemonType.ice)
        {
            strong.Add(PokemonInfo.PokemonType.fighting);
            strong.Add(PokemonInfo.PokemonType.rock);
            strong.Add(PokemonInfo.PokemonType.fire);
            weak.Add(PokemonInfo.PokemonType.ice);
        }
        else if (type == PokemonType.dragon)
        {
            strong.Add(PokemonInfo.PokemonType.ice);
            strong.Add(PokemonInfo.PokemonType.dragon);
            weak.Add(PokemonInfo.PokemonType.fire);
            weak.Add(PokemonInfo.PokemonType.water);
            weak.Add(PokemonInfo.PokemonType.grass);
            weak.Add(PokemonInfo.PokemonType.electric);
        }
    }

    public void SetAttackAnimations()
    {
        for(int i = 0; i < 4; i++)
        {
            if(attacks[i].name != "")
            {
                string path = name + "/Animations/" + name + " " + attacks[i].name;
                Debug.Log(path);
                AnimationClip anim = Resources.Load<AnimationClip> (path);
                Debug.Log(anim);
                attacks[i].animationClip = anim;
            }
        }
    }

    public void Heal ()
    {
        currentStats = baseStats;
    }

    public void AdjustHP(int x)
    {
        currentStats.hp += x;
    }

    public void AdjustATK(int x)
    {
        currentStats.attack += x;
    }

    public void AdjustDEF(int x)
    {
        currentStats.defense += x;
    }

    public void AdjustSPATK(int x)
    {
        currentStats.spAtk += x;
    }

    public void AdjustSPDEF(int x)
    {
        currentStats.spDef += x;
    }

    public void AdjustSPD(int x)
    {
        currentStats.speed += x;
    }
}
