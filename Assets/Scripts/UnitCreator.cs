using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCreator : MonoBehaviour
{
    [SerializeField] int numberOfUnits = 3;
    [SerializeField] Creature unitPrefab;
    [SerializeField] UnitList _unitList;
    public int yOffset;
    [SerializeField] GameObject myEnabler;

    // Start is called before the first frame update
    void Awake()
    {
        StatSheet statSheet = new StatSheet(10, 10, 5, 5, 3, 2, false);
        for (int i = 0; i < numberOfUnits; i++)
        {
            Creature _unit = Instantiate(unitPrefab, Vector3Int.right * i + Vector3Int.up * yOffset, transform.rotation);

            _unit.Init(statSheet);
            _unitList.AddUnit(_unit);
        }

        myEnabler.SetActive(true);

        Destroy(gameObject);
    }

}
