using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Creature : MonoBehaviour, ICombat
{
    Color active = Color.white;
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
    public int maxHP { get; }
    public int Strength { get; }
    public int SpiritualProwess { get; }

    UnitState myState;

    public Creature()
    {
        Strength = 5;
        HP = 10;
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
