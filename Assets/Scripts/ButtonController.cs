using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    private Button btn;
    private int levelIndex;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnButtonClickLoadLevel);
        levelIndex = transform.GetSiblingIndex();
    }

    void Update()
    {
        
    }

    void OnButtonClickLoadLevel()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
        GameManager.instance.index = levelIndex;
    }
}
