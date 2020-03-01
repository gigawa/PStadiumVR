using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokemonInfo;

namespace Assets.Scripts
{
    [Serializable]
    class Bulbasaur : Pokemon
    {
        public Bulbasaur()
        {
            
        }

        public override void SetInfo()
        {
            name = "Bulbasaur";
            pokemonType = PokemonType.grass;
            possibleAttacks = new string[] { "Tackle", "Growl", "Leech Seed", "Vine Whip", "Poison Powder", "Razor Leaf", "Growth", "Sleep Powder", "Solar Beam"};
            knownAttacks = new string[] { "Tackle", "Growl", "Leech Seed", "Vine Whip" };
        }

        public override void SetBaseStats()
        {
            baseStats.hp = 45;
            baseStats.attack = 49;
            baseStats.defense = 49;
            baseStats.spAtk = 65;
            baseStats.spDef = 65;
            baseStats.speed = 45;
        }

        public override void SetAttacks()
        {
            for(int i = 0; i < 4; i++)
            {
                AttackInfo attack;
                if (knownAttacks[i] != "")
                {
                    attack = attackList.GetAttack(knownAttacks[i]);
                }else
                {
                    attack = null;
                }
                attacks[i] = attack;
            }
        }
    }
}
