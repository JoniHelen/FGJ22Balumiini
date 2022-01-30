using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{

    [SerializeField] LayerMask SelectionMask;
    string mySide;

    [SerializeField] Map map;
    [SerializeField] Tilemap tilemap;
    [SerializeField] Tilemap moveTilemap;


    [SerializeField] UnitList unitList;
    [SerializeField] UnitList playerList;

    [SerializeField] Phase phase;

    [SerializeField] AudioSource source;
    /*
      sends input to inputhandler
     go through enemy list one at a time.
    check move range
    option 1: attack player in range
    option 2: escape ( move to negative X
    Wait
    next enemy unit...

    ...
    End phase
     
     */
    private void OnEnable()
    {
        StopAllCoroutines();
        if (phase.current == Phase.Phases.Enemy)
            StartCoroutine(CommandUnits());
    }
    WaitForSeconds decisionTime = new WaitForSeconds(1f);

    public IEnumerator CommandUnits()
    {
        yield return decisionTime;
        for (int i = 0; i < unitList.Count; i++)
        {
            //get next unit
            Creature _unit = unitList.Next(i);
            unitList.ToggleSelection(_unit);
            Select();
            yield return decisionTime;
        }
        unitList.ReturnToIdle();
        phase.Change();

    }


    private void Awake()
    {
        mySide = gameObject.tag;
        source = GetComponent<AudioSource>();
    }

    public void Select()
    {
        Creature unit = unitList.selectedUnit;

        if (unit != null)
        {
            //Tell map player is selected
            Vector3Int enemyPos = tilemap.WorldToCell(unit.transform.position);
            int Move = unit.Move;
            Vector3Int tilePos;
            for (int i = 0; i < Move + 1; i++)
            {
                if (IsUnitWaiting(unit)) break;
                for (int j = -i; j <= i; j++)
                {
                    if (IsUnitWaiting(unit)) break;
                    //get tile within move range

                    if (i < Move)
                    {
                        tilePos = enemyPos + new Vector3Int(-i, j, 0) + Vector3Int.right * Move;
                        FindPlayersInRange(unit, enemyPos, tilePos);
                    }

                    tilePos = enemyPos + new Vector3Int(i, j, 0) + Vector3Int.left * Move;

                    // check if any player pos is same
                    FindPlayersInRange(unit, enemyPos, tilePos);
                }
            }
            unitList.Wait();
        }
    }

    private static bool IsUnitWaiting(Creature unit)
    {
        return unit.MyState == Creature.UnitState.Wait;
    }

    Creature player;
    private void FindPlayersInRange(Creature unit, Vector3Int enemyPos, Vector3Int tilePos)
    {
        Vector3Int playerCell;
        for (int p = 0; p < playerList.Count; p++)
        {
            player = playerList.Next(p);
            playerCell = tilemap.WorldToCell(player.transform.position);

            if (IsPlayerOnThisTile(playerCell, tilePos))
            {
                //player found, go to it.
                MoveToPlayer(unit, enemyPos, playerCell);
                unitList.Wait();

                break;
            }
        }
    }

    private void MoveToPlayer(Creature unit, Vector3Int enemyPos, Vector3Int playerCell)
    {
        Vector3Int target = playerCell - enemyPos;
        target = DefinePathTo(target);
        if (IsFreeSpace(enemyPos, target))
        {
            unit.transform.position = tilemap.CellToWorld(enemyPos + target);
            SortOrderUpdater updater = unit.GetComponent<SortOrderUpdater>();
            updater.UpdateOrder(tilemap);
            // combat
            Battle.Initiate.CombatRound(unit, player);
            source.Play();
        }

    }

    private static Vector3Int DefinePathTo(Vector3Int target)
    {
        int x = Mathf.Abs(target.x);
        int y = Mathf.Abs(target.y);
        if (y > x)
            y -= 1;
        else
            x -= 1;
        target.x = (target.x > 0) ? x : x * -1;
        target.y = (target.y > 0) ? y : y * -1;
        Debug.Log("TARGET: " + target);
        return target;
    }

    private static bool IsPlayerOnThisTile(Vector3Int playerCell, Vector3Int tilePos)
    {
        //Debug.Log($"{playerCell} VS {tilePos}");
        return tilePos == playerCell;
    }


    private bool IsFreeSpace(Vector3Int enemyPos, Vector3Int target)
    {
        for (int i = 0; i < unitList.Count; i++)
        {
            Creature c = unitList.Next(i);

            if (enemyPos + target == tilemap.WorldToCell(c.transform.position))
            {
                return false;
            }
        }

        for (int i = 0; i < playerList.Count; i++)
        {
            Creature c = playerList.Next(i);

            if (enemyPos + target == tilemap.WorldToCell(c.transform.position))
            {
                return false;
            }
        }

        return true;
    }
}
