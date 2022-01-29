using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Creature : MonoBehaviour
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


    UnitState myState;

    // Start is called before the first frame update
    void Start()
    {
        Move = 5;
    }

    public void ChangeState(UnitState _state)
    {
        myState = _state;
    }

    public void GetState()
    {
        StartCoroutine(ShowMoveRange());
        //IterateTiles();
    }

    private void IterateTiles()
    {
        FormSquares();
    }

    private void FormSquares()
    {
        for (int i = Move * -1; i < Move + 1; i++)
        {
            for (int j = 0; j < BattleGrid.Square.Length; j++)
            {
                var obj = Pooler.SharedInstance.PoolItem(Move * BattleGrid.Square.Length);

                if (obj == null) break;
                obj.SetActive(true);
                //get squares around player
                obj.transform.position = transform.position + BattleGrid.Square[j] * i;




            }
        }
    }



    WaitForSeconds delay = new WaitForSeconds(0.02f);
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

        Pooler.SharedInstance.ResetPool();
        tilemap.ClearAllTiles();
        for (int i = 0; i < Move + 1; i++)
        {
            for (int j = -i; j <= i; j++)
            {
                var tile = Pooler.SharedInstance.PoolItem();
                if (tile != null)
                {

                    tile.SetActive(true);
                    tile.transform.position = transform.position + new Vector3(i, j, 0) + Vector3Int.left * Move;
                    tilemap.SetTile(Vector3Int.FloorToInt(tile.transform.position), tile2);
                }

                if (i < Move)
                {

                    tile = Pooler.SharedInstance.PoolItem();
                    if (tile != null)
                    {

                        tile.SetActive(true);
                        tile.transform.position = transform.position + new Vector3(-i, j, 0) + Vector3Int.right * Move;
                        tilemap.SetTile(Vector3Int.FloorToInt(tile.transform.position), tile2);
                    }
                }
                yield return delay;
            }
        }

    }
}
