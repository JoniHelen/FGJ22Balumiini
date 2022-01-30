using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        Debug.Log("In Editor");
#else
        gameObject.SetActive(false);
#endif
    }
}
