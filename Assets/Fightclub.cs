using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fightclub : MonoBehaviour
{
    [SerializeField] Creature a;
    [SerializeField] Creature b;

    void Start()
    {
        Combat.Fight(a, b);
    }
}
