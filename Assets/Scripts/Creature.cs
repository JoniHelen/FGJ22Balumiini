using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Creature : MonoBehaviour, ICombat
{
    [SerializeField] Map map;
    [SerializeField] Tilemap tilemap;
    [SerializeField] TileBase tile;
    [SerializeField] TileBase tile2;
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
        Move = 5;
    }

    public void ChangeState(UnitState _state)
    {
        myState = _state;
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
