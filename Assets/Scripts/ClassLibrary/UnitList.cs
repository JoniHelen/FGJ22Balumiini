using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Unit List", menuName ="Scriptable Objects/Unit List")]
public class UnitList : ScriptableObject
{
    [SerializeField] List<Creature> units = new List<Creature>();

    public Creature selectedUnit;


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
            if (_unit == _selectedUnit && _selectedUnit.MyState != Creature.UnitState.Wait)
            {
                _unit.ChangeState(Creature.UnitState.Selected);
                selectedUnit = _unit;
            }
            else if (_unit.MyState == Creature.UnitState.Selected)
            {
                _unit.ChangeState(Creature.UnitState.Idle);
                if (_unit == selectedUnit) selectedUnit = null;
            }
        }
    }

    public void Wait()
    {
        selectedUnit.ChangeState(Creature.UnitState.Wait);
        selectedUnit = null;
    }

    void SelectUnit()
    {

    }
}
