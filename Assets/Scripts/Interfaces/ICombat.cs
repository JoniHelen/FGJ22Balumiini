using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombat
{
    int HP { get; set; }
    int maxHP { get; }
    int Strength { get; }
    int SpiritualProwess { get; }

    void TakeDamage(int amount);
}
