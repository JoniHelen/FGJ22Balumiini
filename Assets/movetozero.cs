using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class movetozero : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject obj;

    private void Start()
    {
        obj.transform.position = tilemap.CellToWorld(Vector3Int.zero);
    }
}
