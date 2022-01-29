using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] LayerMask SelectionMask;
    [SerializeField] Map map;
    [SerializeField] Tilemap tilemap;
    [SerializeField] Tilemap moveTilemap;

    [SerializeField] TileBase tile;
    [SerializeField] TileBase tile2;

    [SerializeField] UnitList unitList;

    private Vector2 mouseScreenPos;
    private Vector2 mouseWorldPos;


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
            moveTilemap.ClearAllTiles();
            mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

            StopAllCoroutines();


            RaycastHit2D hit = Physics2D.Raycast(new Vector2(mouseWorldPos.x, mouseWorldPos.y), Vector2.zero, Mathf.Infinity, SelectionMask);

            if (hit)
            {
                if (hit.collider.gameObject.CompareTag("PlayableCharacter"))
                {
                    //Tell map player is selected
                    Vector3Int playerPos = tilemap.WorldToCell(hit.transform.position);
                    //found player

                    var unit = hit.transform.GetComponent<Creature>();
                    unitList.ChangeStates(unit);
                    StartCoroutine(ShowMoveRange(unit.Move, unit.transform.position));
                }
            }
            else
            {
                MapTile mapTile = map.Tiles(new Vector2Int(tilemap.WorldToCell(mouseWorldPos).x, tilemap.WorldToCell(mouseWorldPos).y));


                if (map.selectedTile != null)
                {
                    tilemap.SetTile(new Vector3Int(map.selectedTile.x, map.selectedTile.y, 0), tile);

                }

                map.selectedTile = new Vector2Int(tilemap.WorldToCell(mouseWorldPos).x, tilemap.WorldToCell(mouseWorldPos).y);

                tilemap.SetTile(tilemap.WorldToCell(mouseWorldPos), tile2);

                if(unitList.selectedUnit != null)
                {
                    unitList.selectedUnit.transform.position = tilemap.CellToWorld(new Vector3Int( map.selectedTile.x, map.selectedTile.y, 0));
                }
            }
        }
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
                moveTilemap.SetTile(moveTilemap.WorldToCell(unitPos) + new Vector3Int(i, j, 0) + Vector3Int.left * Move, tile2);

                if (i < Move)
                {
                    moveTilemap.SetTile(moveTilemap.WorldToCell(unitPos) + new Vector3Int(-i, j, 0) + Vector3Int.right * Move, tile2);

                }
                yield return delay;
            }
        }

    }
}
