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
        for (int i = -100; i < 100; i++)
        {
            for (int j = -100; j < 100; j++)
            {
                map.AddTile(new Vector2Int(i, j), tilemap.GetTile(new Vector3Int(i, j, 0)));
            }
        }
    }
}
