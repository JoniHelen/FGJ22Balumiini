using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class Creature : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    // define the state of being selected
    enum State
    {
        Idle,
        Selected,
        Wait
    }

    public int Move { get; set; }


    State myState;

    // Start is called before the first frame update
    void Start()
    {
        Move = 5;
    }

    public void GetState()
    {
        StartCoroutine(ShowMoveRange());
        //IterateTiles();
    }

    private void IterateTiles()
    {
        FormSquares();
    }

    private void FormSquares()
    {
        for (int i = Move * -1; i < Move + 1; i++)
        {
            for (int j = 0; j < BattleGrid.Square.Length; j++)
            {
                var obj = Pooler.SharedInstance.PoolItem(Move * BattleGrid.Square.Length);

                if (obj == null) break;
                obj.SetActive(true);
                //get squares around player
                obj.transform.position = transform.position + BattleGrid.Square[j] * i;




            }
        }
    }



    WaitForSeconds delay = new WaitForSeconds(0.02f);
    IEnumerator ShowMoveRange()
    {
        //player move is 3
        /*
         * X
         *XXX
        _XXXXX
        XXXOXXX
        _XXXXX
        __XXX
        ___X
         */
        //24 tiles should be highlighted

        Pooler.SharedInstance.ResetPool();

        for (int i = 0; i < Move + 1; i++)
        {
            for (int j = -i; j <= i; j++)
            {
                var tile = Pooler.SharedInstance.PoolItem();
                if (tile != null)
                {

                    tile.SetActive(true);
                    tile.transform.position = transform.position + new Vector3(i, 0, j) + Vector3Int.left * Move;
                    debugText.text = $"{i} {j}";
                }

                if (i < Move)
                {

                    tile = Pooler.SharedInstance.PoolItem();
                    if (tile != null)
                    {

                        tile.SetActive(true);
                        tile.transform.position = transform.position + new Vector3(-i, 0, j) + Vector3Int.right * Move;
                        debugText.text += $", {-i} {j}";
                    }
                }
                yield return delay;
            }
        }

    }
}
