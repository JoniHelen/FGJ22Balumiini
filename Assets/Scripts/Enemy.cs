using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{

    [SerializeField] LayerMask SelectionMask;
    string mySide;

    [SerializeField] Map map;
    [SerializeField] Tilemap tilemap;
    [SerializeField] Tilemap moveTilemap;

    [SerializeField] TileBase tile;
    [SerializeField] TileBase tile2;
    [SerializeField] TileBase moveTile;

    [SerializeField] UnitList unitList;
    [SerializeField] UnitList playerList;
    /*
      sends input to inputhandler
     go through enemy list one at a time.
    check move range
    option 1: attack player in range
    option 2: escape ( move to negative X
    Wait
    next enemy unit...

    ...
    End phase
     
     */

    private void OnEnable()
    {
        //start phase
        //OrderEnemies();
    }

    public void OrderEnemies()
    {
        for (int i = 0; i < unitList.Count; i++)
        {
            //get next unit
            Creature _unit = unitList.Next(i);
            unitList.ToggleSelection(_unit);
            Select();
        }
    }

    private void OnDisable()
    {
        //unitList.ReturnToIdle();
    }


    private void Awake()
    {
        mySide = gameObject.tag;
    }

    public void Select()
    {
        Creature unit = unitList.selectedUnit;
        //Tell map player is selected
        Vector3Int enemyPos = tilemap.WorldToCell(unit.transform.position);

        int Move = unit.Move;
        Vector3Int playerCell;
        Vector3Int tilePos;
        bool isFound = false;
        for (int i = 0; i < Move + 1; i++)
        {
            if (isFound) break;
            for (int j = -i; j <= i; j++)
            {
                if (isFound) break;
                //get tile within move range
                if (i < Move)
                {
                    tilePos = enemyPos + new Vector3Int(-i, j, 0) + Vector3Int.right * Move;
                }
                else
                tilePos = enemyPos + new Vector3Int(i, j, 0) + Vector3Int.left * Move;
                // check if any player pos is same
                for (int p = 0; p < playerList.Count; p++)
                {
                    var player = playerList.Next(p);
                    playerCell = tilemap.WorldToCell(player.transform.position);
                    if (tilePos == playerCell)
                    {
                        //player found, go to it.
                        Vector3Int target = playerCell - enemyPos;
                        Debug.Log($"enemy {enemyPos} => {target}");
                        isFound = true;
                        int x = Mathf.Abs(target.x);
                        int y = Mathf.Abs(target.y);
                        if (y > x)
                        {
                            y -= 1;
                        }
                        else
                        {
                            x -= 1;
                        }

                        target.x = (target.x > 0) ? x : x * -1;
                        target.y = (target.y > 0) ? y : y * -1;
                        unit.transform.position = tilemap.CellToWorld(enemyPos+ target);
                        unitList.Wait();
                        /*
                         (-3, 1)
                        ABS (2, 1)
                         target => (-2, 1)
                         */

                        break;
                    }
                }

            }
        }
        unitList.Wait();

        //found player



        //MoveUnit();

    }

    //private void MoveUnit()
    //{
    //    if (unitList.selectedUnit != null && moveTilemap.HasTile(
    //        new Vector3Int(moveTilemap.WorldToCell(mouseWorldPos).x,
    //        moveTilemap.WorldToCell(mouseWorldPos).y, 0)))
    //    {
    //        unitList.selectedUnit.transform.position = tilemap.CellToWorld(
    //            new Vector3Int(map.selectedTile.x,
    //            map.selectedTile.y, 0));
    //        //prompt actions
    //        unitList.Wait();
    //    }
    //    else
    //    {
    //        unitList.selectedUnit = null;
    //        unitList.ToggleSelection(unitList.selectedUnit);
    //    }

    //    moveTilemap.ClearAllTiles();
    //}

    private void SelectTile(Vector2Int selectedCoords)
    {
        if (HasPreviousSelectedTile())
        {
            //make white
            tilemap.SetTile(new Vector3Int(map.selectedTile.x, map.selectedTile.y, 0), tile);
        }

        if (IsInMap(selectedCoords))
        {
            map.selectedTile = selectedCoords;
            //make red
            tilemap.SetTile(new Vector3Int(selectedCoords.x, selectedCoords.y, 0), tile2);
        }
    }

    private bool IsInMap(Vector2Int selectedCoords)
    {
        return map.tiles.ContainsKey(selectedCoords);
    }

    private bool HasPreviousSelectedTile()
    {
        return map.selectedTile != null;
    }

}
