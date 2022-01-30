using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SortOrderUpdater : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Tilemap tilemap;

    public void UpdateOrder()
    {
        spriteRenderer.sortingOrder = tilemap.WorldToCell(transform.position).y;
    }
}
