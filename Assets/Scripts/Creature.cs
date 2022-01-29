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

    public void DeprecatedShowRange()
    {
        //FormSquares();
        StopCoroutine(ShowMoveRange());
        StartCoroutine(ShowMoveRange());
        //IterateTiles();
    }

    private void IterateTiles()
    {
        FormSquares();
    }

    private void FormSquares()
    {
        tilemap.ClearAllTiles();
        tilemap.RefreshAllTiles();
        for (int i = 0; i < Move + 1; i++)
        {
            for (int j = -i; j <= i; j++)
            {
                tilemap.SetTile(tilemap.WorldToCell(transform.position) + new Vector3Int(i, j, 0) + Vector3Int.left * Move, tile2);
                if (i < Move)
                {
                    tilemap.SetTile(tilemap.WorldToCell(transform.position) + new Vector3Int(-i, j, 0) + Vector3Int.right * Move, tile2);
                }
            }
        }
    }



    WaitForSeconds delay = new WaitForSeconds(0.01f);
    IEnumerator ShowMoveRange()
    {
        //player move is 3
        /*
         * X
         *XXX
        _XXXXX
        XXXOXXX
        _XXXXX
        __XXX
        ___X
         */
        //24 tiles should be highlighted

        //Pooler.SharedInstance.ResetPool();
        tilemap.ClearAllTiles();

        for (int i = 0; i < Move + 1; i++)
        {
            for (int j = -i; j <= i; j++)
            {
                tilemap.SetTile(tilemap.WorldToCell(transform.position)+ new Vector3Int(i, j, 0) + Vector3Int.left * Move, tile2);

                if (i < Move)
                {
                    tilemap.SetTile(tilemap.WorldToCell(transform.position) + new Vector3Int(-i, j, 0) + Vector3Int.right * Move, tile2);

                }
                yield return delay;
            }
        }

    }

    public void TakeDamage(int amount)
    {
        HP = Mathf.Max(HP - amount, 0);
    }
}
