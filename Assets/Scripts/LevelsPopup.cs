using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.UI;
using Unity.VisualScripting;

public class LevelsPopup : MonoBehaviour//, IScrollHandler
{
    public static LevelsPopup instance;

    private SpriteRenderer render;
    public GameObject levelPanel;

    void Awake()
    {
        instance = GetComponent<LevelsPopup>();
    }

    void Start()
    {
        Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        //addEventSystem();
        render = GetComponent<SpriteRenderer>();
        setLevelInfo();
    }

    void Update()
    {
        
    }

    public void setLevelInfo()
    {
        for (int i = 0; i < levelPanel.transform.childCount; i++)
        {
            int lvl = GameManager.instance.levels[i].level_number;
            int moves = GameManager.instance.levels[i].move_count;
            levelPanel.transform.GetChild(i).GetComponentsInChildren<Text>()[0].text = "LEVEL: "+lvl+"          MOVES: "+moves;
            levelPanel.transform.GetChild(i).gameObject.AddComponent<ButtonController>();
        }
    }

    //public void OnScroll(PointerEventData eventData)
    //{
    //    Debug.Log("Scrolled!");

    //    throw new System.NotImplementedException();
    //}

    //void addEventSystem()
    //{
    //    GameObject eventSystem = null;
    //    GameObject tempObj = GameObject.Find("EventSystem");
    //    if (tempObj == null)
    //    {
    //        eventSystem = new GameObject("EventSystem");
    //        eventSystem.AddComponent<EventSystem>();
    //        eventSystem.AddComponent<StandaloneInputModule>();
    //    }
    //    else
    //    {
    //        if ((tempObj.GetComponent<EventSystem>()) == null)
    //        {
    //            tempObj.AddComponent<EventSystem>();
    //        }

    //        if ((tempObj.GetComponent<StandaloneInputModule>()) == null)
    //        {
    //            tempObj.AddComponent<StandaloneInputModule>();
    //        }
    //    }
    //}
}
