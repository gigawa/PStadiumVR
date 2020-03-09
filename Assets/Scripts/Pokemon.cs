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
    public PokemonType secondaryType;
    public int level;
    public int experience;
    public int currentHP;

    protected stats baseStats;
    public stats effectiveStats;
    public stats statStages;
    public stats statIVs;
    public stats statEVs;
    public AttackInfo [] attacks;

    protected string [] possibleAttacks;
    public string[] knownAttacks { get; protected set; }

    public List<PokemonType> weak; //weak against this type
    public List<PokemonType> strong; //strong against this type
    public List<PokemonType> noDamage; //does no damage against this

    protected List<StatusEffects> volatileEffects;
    protected List<StatusEffects> nonVolatileEffects;
    protected List<StatusEffects> battleEffects;

    public AttackList attackList;

    Animator animator;

    public Pokemon ()
    {
        
    }

    // Use this for initialization
    void Awake () {
        strong = new List<PokemonType>();
        weak = new List<PokemonType>();
        noDamage = new List<PokemonType>();

        animator = GetComponent<Animator>();
        attacks = new AttackInfo[4];
        attackList = AttackList.Instance;
        SetInfo();
        SetBaseStats();
        SetAttacks();
        SetIVs();
        SetEffectiveStats();
        currentHP = effectiveStats.hp;
        SetTypeStrength(pokemonType);
        if(secondaryType != PokemonType.none)
        {
            SetTypeStrength(secondaryType);
        }
        SetAttackAnimations();
        AssignAnimations();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void SetInfo() { }
    public virtual void SetAttacks() { }
    public virtual void SetBaseStats() { }

    public void SetIVs ()
    {
        statIVs.attack = Random.Range(0, 16);
        statIVs.defense = Random.Range(0, 16);
        statIVs.spAtk = Random.Range(0, 16);
        statIVs.spDef = Random.Range(0, 16);
        statIVs.speed = Random.Range(0, 16);
        statIVs.hp = Random.Range(0, 16);
    }

    public void SetEffectiveStats ()
    {
        effectiveStats.attack = (2 * baseStats.attack + (statIVs.attack * 2) + statEVs.attack) * level / 100 + 5;
        effectiveStats.defense = (2 * baseStats.defense + (statIVs.defense * 2) + statEVs.defense) * level / 100 + 5;
        effectiveStats.spAtk = (2 * baseStats.spAtk + (statIVs.spAtk * 2) + statEVs.spAtk) * level / 100 + 5;
        effectiveStats.spDef = (2 * baseStats.spDef + (statIVs.spDef * 2) + statEVs.spDef) * level / 100 + 5;
        effectiveStats.speed = (2 * baseStats.speed + (statIVs.speed * 2) + statEVs.speed) * level / 100 + 5;
        effectiveStats.hp = (2 * baseStats.hp + (2 * statIVs.hp) + statEVs.hp) * level / 100 + level + 10;
    }

    public void AssignAnimations()
    {
        AnimatorOverrideController animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animatorOverrideController.name = pokemonName;

        animatorOverrideController["Attack0"] = attacks[0].animationClip;
        animatorOverrideController["Attack1"] = attacks[1].animationClip;
        animatorOverrideController["Attack2"] = attacks[2].animationClip;
        animatorOverrideController["Attack3"] = attacks[3].animationClip;

        animator.runtimeAnimatorController = animatorOverrideController;

        Debug.Log(attacks[2].animationClip.name);
    }

    //This is defending pokemon.
    public void SetTypeStrength(PokemonType type)
    {
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
                //Debug.Log(path);
                AnimationClip anim = Resources.Load<AnimationClip> (path);
                //Debug.Log(anim);
                attacks[i].animationClip = anim;
            }
        }
    }

    public void Attack(int index)
    {
        animator.SetInteger("AttackIndex", index);
        animator.SetTrigger("Attack");
        Debug.Log(animator.runtimeAnimatorController.animationClips[index].name);
    }

    public void AdjustStatStage(Stat stat, int adjustment)
    {
        switch(stat)
        {
            case Stat.hp:
                statStages.hp += adjustment;
                break;
            case Stat.attack:
                statStages.attack += adjustment;
                break;
            case Stat.defense:
                statStages.defense += adjustment;
                break;
            case Stat.spAttack:
                statStages.spAtk += adjustment;
                break;
            case Stat.spDefense:
                statStages.spDef += adjustment;
                break;
            case Stat.speed:
                statStages.speed += adjustment;
                break;
            default:
                throw new System.NullReferenceException();
        };
    }

    #region Adjust Stats
    public void Heal ()
    {
        effectiveStats = baseStats;
    }

    public void AdjustHP(int x)
    {
        currentHP += x;
        BattleController.Instance.UpdateHPUI();
    }

    public void AdjustATK(int x)
    {
        effectiveStats.attack += x;
    }

    public void AdjustDEF(int x)
    {
        effectiveStats.defense += x;
    }

    public void AdjustSPATK(int x)
    {
        effectiveStats.spAtk += x;
    }

    public void AdjustSPDEF(int x)
    {
        effectiveStats.spDef += x;
    }

    public void AdjustSPD(int x)
    {
        effectiveStats.speed += x;
    }
    #endregion
}
