using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

[CreateAssetMenu(fileName = "new Map", menuName = "Scriptable Objects/Map")]
public class Map : ScriptableObject
{
    public Dictionary<Vector2Int, MapTile> tiles = new Dictionary<Vector2Int, MapTile>();

    public Vector2Int selectedTile;

    public void AddTile(Vector2Int coords, TileBase tile)
    {
        MapTile mapTile = new MapTile() { tile = tile };
        tiles.Add(coords, mapTile);
    }

    public MapTile Tiles(Vector2Int _pos)
    {
        if (tiles.ContainsKey(_pos))
            return tiles[_pos];
        return null;
    }
}
