using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Unit List", menuName ="Scriptable Objects/Unit List")]
public class UnitList : ScriptableObject
{
    [SerializeField] List<Creature> units = new List<Creature>();

    private void OnEnable()
    {
        units.Clear();
    }

    public void AddUnit(Creature _creature)
    {
        units.Add(_creature);
    }

    public void ChangeStates(Creature _selectedUnit)
    {
        foreach (var _unit in units)
        {
            _unit.ChangeState(_unit == _selectedUnit ? Creature.UnitState.Selected : Creature.UnitState.Idle);
        }
    }
}
