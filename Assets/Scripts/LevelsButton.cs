using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

public class LevelsButton : MonoBehaviour, IPointerClickHandler,
                                  IPointerDownHandler, IPointerEnterHandler,
                                  IPointerUpHandler, IPointerExitHandler
{
    public List<Sprite> buttons = new List<Sprite>();
    private SpriteRenderer render;
    public GameObject buttonText;
    public GameObject popup;
    private int levelIndex;

    void Start()
    {
        Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        addEventSystem();
        popup.SetActive(false);
        render = GetComponent<SpriteRenderer>();
        levelIndex = transform.GetSiblingIndex();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        popup.SetActive(true);
        Destroy(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Sprite buttonDown = buttons[1];
        render.sprite = buttonDown;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Sprite buttonUp = buttons[0];
        render.sprite = buttonUp;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }


    public void OnPointerExit(PointerEventData eventData)
    {
    }

    void addEventSystem()
    {
        GameObject eventSystem = null;
        GameObject tempObj = GameObject.Find("EventSystem");
        if (tempObj == null)
        {
            eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }
        else
        {
            if ((tempObj.GetComponent<EventSystem>()) == null)
            {
                tempObj.AddComponent<EventSystem>();
            }

            if ((tempObj.GetComponent<StandaloneInputModule>()) == null)
            {
                tempObj.AddComponent<StandaloneInputModule>();
            }
        }
    }

}