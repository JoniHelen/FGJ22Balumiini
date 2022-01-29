using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StatSheet : IStats
{
    public int HP { get; set; }
    public int maxHP { get; }
    public int Strength { get; }
    public int SpiritualProwess { get; }
    public int PhysicalDefense { get; }
    public int SpiritualDefense { get; }
    public bool IsSpiritual { get; set; }

    public StatSheet(int hp, int maxHp, int strength, int spiritualProwess, int physicalDefense, int spiritualDefense, bool isSpiritual)
    {
        HP = hp;
        maxHP = maxHp;
        Strength = strength;
        SpiritualProwess = spiritualProwess;
        SpiritualDefense = spiritualDefense;
        PhysicalDefense = physicalDefense;
        IsSpiritual = isSpiritual;
    }
}
