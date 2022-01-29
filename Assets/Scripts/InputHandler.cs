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

    private Vector2 mouseScreenPos;
    private Vector2 mouseWorldPos;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
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
            Debug.Log("Registered Click at: " + mouseScreenPos + " , " + mouseWorldPos);

            RaycastHit2D hit = Physics2D.Raycast(new Vector2(mouseWorldPos.x, mouseWorldPos.y), Vector2.zero, Mathf.Infinity, SelectionMask);

            if (hit)
            {
                if (hit.collider.gameObject.CompareTag("PlayableCharacter"))
                {
                    Debug.Log("Ray hit player at: " + hit.point);
                }
            }
            else
            {
                MapTile mapTile = map.tiles[new Vector2Int(tilemap.WorldToCell(mouseWorldPos).x, tilemap.WorldToCell(mouseWorldPos).y)];

                Debug.Log("Selected tile at: " + tilemap.WorldToCell(mouseWorldPos));

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