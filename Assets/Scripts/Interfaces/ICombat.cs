using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombat : IStats
{
    void TakeDamage(int amount);
}
