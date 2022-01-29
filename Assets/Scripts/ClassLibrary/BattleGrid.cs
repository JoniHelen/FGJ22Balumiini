using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleGrid
{
    public static readonly Vector3Int[] Cross = new Vector3Int[4]
    {
        Vector3Int.forward,
        Vector3Int.right,
        Vector3Int.back,
        Vector3Int.left
    };

    public static readonly Vector3Int[] Square = new Vector3Int[8]
    {
        Vector3Int.forward,
        Vector3Int.forward+Vector3Int.right,
        Vector3Int.right,
        Vector3Int.right+Vector3Int.back,
        Vector3Int.back,
        Vector3Int.back+Vector3Int.left,
        Vector3Int.left,
        Vector3Int.left+Vector3Int.forward

    };
}
