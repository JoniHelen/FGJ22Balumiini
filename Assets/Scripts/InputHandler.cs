using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine;

public class InputHandler : MonoBehaviour
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

    private Vector2 mouseScreenPos;
    private Vector2 mouseWorldPos;

    private void Awake()
    {
        mySide = gameObject.tag;
    }

    private void OnDisable()
    {
        Debug.Log("Playerit palatkaa idleen");
        unitList.ReturnToIdle();
    }

    public void UpdateMouse(InputAction.CallbackContext ctx)
    {
        mouseScreenPos = CorrectByRect(Camera.main, ctx.ReadValue<Vector2>());
    }

    public Vector2 CorrectByRect(Camera camera, Vector2 pointer)
    {
        var size = camera.rect.size;
        var offset = camera.pixelRect.position;
        return new Vector2(pointer.x * size.x + offset.x, pointer.y * size.y + offset.y);
    }

    public void Select(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            StopAllCoroutines();
            ClickInteraction();
        }
    }

    private void ClickInteraction()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(mouseWorldPos.x, mouseWorldPos.y), Vector2.zero, Mathf.Infinity, SelectionMask);

        if (hit)
        {
            if (ClickedPlayer(ref hit))
            {
                //Tell map player is selected
                SelectClickedPlayer(hit);
            }
        }
        else
        {
            ClickedMap();
        }
    }

    private void ClickedMap()
    {
        MapTile mapTile = map.Tiles(new Vector2Int(tilemap.WorldToCell(mouseWorldPos).x, tilemap.WorldToCell(mouseWorldPos).y));

        SelectTile(new Vector2Int(tilemap.WorldToCell(mouseWorldPos).x, tilemap.WorldToCell(mouseWorldPos).y));

        MoveUnit();
    }

    private void SelectClickedPlayer(RaycastHit2D hit)
    {
        Vector3Int playerPos = tilemap.WorldToCell(hit.transform.position);
        //found player

        SelectTile(new Vector2Int(playerPos.x, playerPos.y));

        var unit = hit.transform.GetComponent<Creature>();
        unitList.ToggleSelection(unit);
        StartCoroutine(ShowMoveRange(unit.Move, unit.transform.position));
    }

    private bool ClickedPlayer(ref RaycastHit2D hit)
    {
        return hit.collider.gameObject.CompareTag(mySide);
    }

    private void MoveUnit()
    {
        if (unitList.selectedUnit != null && moveTilemap.HasTile(new Vector3Int(moveTilemap.WorldToCell(mouseWorldPos).x, moveTilemap.WorldToCell(mouseWorldPos).y, 0)))
        {
            unitList.selectedUnit.transform.position = tilemap.CellToWorld(new Vector3Int(map.selectedTile.x, map.selectedTile.y, 0));
            //prompt actions
            unitList.Wait();
        }
        else
        {
            unitList.selectedUnit = null;
            unitList.ToggleSelection(unitList.selectedUnit);
        }

        moveTilemap.ClearAllTiles();
    }

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

    public void Drag(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {

        }
    }




    WaitForSeconds delay = new WaitForSeconds(0.01f);
    IEnumerator ShowMoveRange(int Move, Vector3 unitPos)
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
        moveTilemap.ClearAllTiles();

        for (int i = 0; i < Move + 1; i++)
        {
            for (int j = -i; j <= i; j++)
            {
                Vector3Int tilePos = moveTilemap.WorldToCell(unitPos) + new Vector3Int(i, j, 0) + Vector3Int.left * Move;
                if (tilemap.HasTile(tilePos))
                moveTilemap.SetTile(tilePos, moveTile);

                if (i < Move)
                {
                    tilePos = moveTilemap.WorldToCell(unitPos) + new Vector3Int(-i, j, 0) + Vector3Int.right * Move;
                    if(tilemap.HasTile(tilePos))
                        moveTilemap.SetTile(tilePos, moveTile);

                }
                yield return delay;
            }
        }

    }
}
