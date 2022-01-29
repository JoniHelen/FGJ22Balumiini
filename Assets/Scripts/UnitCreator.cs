using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCreator : MonoBehaviour
{
    [SerializeField] int numberOfUnits = 3;
    [SerializeField] Creature unitPrefab;
    [SerializeField] UnitList _unitList;

    [SerializeField] GameObject myEnabler;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < numberOfUnits; i++)
        {
            Creature _unit = Instantiate(unitPrefab, Vector3Int.right * i, transform.rotation);
            _unitList.AddUnit(_unit);
        }

        myEnabler.SetActive(true);

        Destroy(gameObject);
    }

}
