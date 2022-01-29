using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Phase", menuName ="Scriptable Objects/Phase")]
public class Phase : ScriptableObject
{
    public enum Phases
    {
        Player,
        Enemy,
        End
    }

    public Phases current { get; private set; }

    public void Change()
    {
        current = (current == Phases.Player) ? Phases.Enemy : Phases.Player;
    }

    public void End()
    {
        current = Phases.End;
    }
}
