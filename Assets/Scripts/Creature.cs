using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Creature : MonoBehaviour, ICombat
{
    public Color active = Color.white;
    Color wait = Color.grey;

    SpriteRenderer renderer;
    // define the state of being selected
    public enum UnitState
    {
        Idle,
        Selected,
        Wait
    }

    public int Move { get; set; }
    public int HP { get; set; }
    public int maxHP { get; private set; }
    public int Strength { get; private set; }
    public int SpiritualProwess { get; private set; }
    public int PhysicalDefense { get; private set; }
    public int SpiritualDefense { get; private set; }
    public bool IsSpiritual { get; set; }


    UnitState myState;

    public void Init(StatSheet stats)
    {
        HP = stats.HP;
        maxHP = stats.maxHP;
        Strength = stats.Strength;
        SpiritualProwess = stats.SpiritualProwess;
        PhysicalDefense = stats.PhysicalDefense;
        SpiritualDefense = stats.SpiritualDefense;
        IsSpiritual = stats.IsSpiritual;
    }

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
        Move = 5;
    }

    public void ChangeState(UnitState _state)
    {
        myState = _state;
        if(renderer != null)
            renderer.color = (myState == UnitState.Wait) ? wait : active;
    }

    public UnitState MyState
    {
        get => myState;
    }

    
    public void TakeDamage(int amount)
    {
        HP = Mathf.Max(HP - amount, 0);
    }
}
