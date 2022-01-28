using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;

    [SerializeField] Map map;
    // Start is called before the first frame update
    void Start()
    {
        
        map.AddTile(Vector2Int.zero, tilemap.GetTile(Vector3Int.zero));
    }
}
