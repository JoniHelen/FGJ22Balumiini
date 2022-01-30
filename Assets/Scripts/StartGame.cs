using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartGame : MonoBehaviour
{
    public int scene = 1;

    Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Begin);
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    public void Begin()
    {
        SceneManager.LoadScene(scene);
    }
}
