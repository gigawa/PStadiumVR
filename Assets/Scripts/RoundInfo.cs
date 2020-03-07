using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RoundInfo
{
    public Queue<Tuple<Pokemon, int, Pokemon>> attackQueue;

    public RoundInfo()
    {
        // attacker, attack index, defender
        attackQueue = new Queue<Tuple<Pokemon, int, Pokemon>>();
    }
}
