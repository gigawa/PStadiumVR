using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PokemonInfo
{
    public enum PokemonType { none, normal, fighting, flying, poison, ground, rock, bug, ghost, steel, fire, water, grass, electric, psychic, ice, dragon, dark };
    public enum Category { physical, special, status, effect };
    public enum Stat { none, hp, attack, defense, spAttack, spDefense, speed };
    public enum StatusEffects { none, paralyzed, poisoned, badlyPoisoned, burned, frozen, flinch, confused, infatuation, leechSeed }
}
