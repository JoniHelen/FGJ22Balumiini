using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit List", menuName = "Scriptable Objects/Unit List")]
public class UnitList : ScriptableObject
{
    [SerializeField] List<Creature> units = new List<Creature>();

    public Creature selectedUnit;


    private void OnEnable()
    {
        units.Clear();
    }

    public int Count
    {
        get => units.Count;
    }

    public Creature Next(int index)
    {
        if (index < Count)
        {
            return units[index];
        }
        return null;
    }

    public void AddUnit(Creature _creature)
    {
        units.Add(_creature);
    }

    public void ToggleSelection(Creature _selectedUnit)
    {
        foreach (var _unit in units)
        {
            FindAndToggle(_selectedUnit, _unit);
        }
    }

    private void FindAndToggle(Creature _selectedUnit, Creature _unit)
    {
        if (AreSelectedUnitAndListUnitTheSame(_selectedUnit, _unit))
        {
            SelectThis(_unit);
        }
        else if (IsThisUnitIsSelected(_unit))
        {
            ReturnToIdle(_unit);
        }
    }

    private void ReturnToIdle(Creature _unit)
    {
        _unit.ChangeState(Creature.UnitState.Idle);
        if (_unit == selectedUnit) selectedUnit = null;
    }

    private static bool IsThisUnitIsSelected(Creature _unit)
    {
        return _unit.MyState == Creature.UnitState.Selected;
    }

    private void SelectThis(Creature _unit)
    {
        if (_unit != null)
        {

            _unit.ChangeState(Creature.UnitState.Selected);
            selectedUnit = _unit;
        }
    }

    private static bool AreSelectedUnitAndListUnitTheSame(Creature _selectedUnit, Creature _unit)
    {
        return _unit == _selectedUnit && _selectedUnit.MyState != Creature.UnitState.Wait;
    }

    public void Wait()
    {
        if (selectedUnit != null)
        {

            selectedUnit.ChangeState(Creature.UnitState.Wait);
            selectedUnit = null;
        }
    }

    public void ReturnToIdle()
    {
        foreach (var _unit in units)
        {
            _unit.ChangeState(Creature.UnitState.Idle);
        }
    }

    public bool IsEveryoneWaiting()
    {
        foreach (var unit in units)
        {
            if (unit.MyState != Creature.UnitState.Wait) return false;
        }
        return true;
    }
}
