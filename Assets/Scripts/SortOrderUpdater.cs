using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortOrderUpdater : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    public void UpdateOrder()
    {
        spriteRenderer.sortingOrder = -(int)transform.position.y;
    }
}
