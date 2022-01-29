using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{

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
        Move = 3;
    }

    public void GetState()
    {
        Debug.Log($"{myState}, move: {Move}");
    }

}
