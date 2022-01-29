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
    [SerializeField] TileBase tile;
    [SerializeField] TileBase tile2;

    private Vector2 mouseScreenPos;
    private Vector2 mouseWorldPos;

    [SerializeField] UnitList _unitList;

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


            Debug.Log("Registered Click at: " + mouseScreenPos + " , " + mouseWorldPos);

            RaycastHit2D hit = Physics2D.Raycast(new Vector2(mouseWorldPos.x, mouseWorldPos.y), Vector2.zero, Mathf.Infinity, SelectionMask);

            if (hit)
            {
                if (hit.collider.gameObject.CompareTag("PlayableCharacter"))
                {
                    Debug.Log("Ray hit player at: " + hit.transform.position);
                    //Tell map player is selected
                    Vector3Int playerPos = tilemap.WorldToCell(hit.transform.position);
                    //found player

                    Debug.Log($"{map.Tiles(new Vector2Int(playerPos.x, playerPos.y))} {playerPos}");
                    var unit = hit.transform.GetComponent<Creature>();
                    Debug.Log("");
                }
            }
            else
            {
                MapTile mapTile = map.Tiles(new Vector2Int(tilemap.WorldToCell(mouseWorldPos).x, tilemap.WorldToCell(mouseWorldPos).y));

                Debug.Log("Selected tile at: " + tilemap.WorldToCell(mouseWorldPos));

                if (map.selectedTile != null)
                {
                    tilemap.SetTile(new Vector3Int(map.selectedTile.x, map.selectedTile.y, 0), tile2);
                }

                map.selectedTile = new Vector2Int(tilemap.WorldToCell(mouseWorldPos).x, tilemap.WorldToCell(mouseWorldPos).y);

                tilemap.SetTile(tilemap.WorldToCell(mouseWorldPos), tile);
            }
        }
    }

    public void Drag(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {

        }
    }
}
