using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SortOrderUpdater : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    public void UpdateOrder(Tilemap tilemap)
    {
        spriteRenderer.sortingOrder = (-tilemap.WorldToCell(transform.position).y) + (-tilemap.WorldToCell(transform.position).x);
    }
}
