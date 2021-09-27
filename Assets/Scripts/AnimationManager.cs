using DigitalRuby.Tween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationManager : MonoBehaviour
{
    public GameObject star;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        Rotate();
    }

    void Update()
    {
        
    }

    private void Rotate()
    {
        System.Action<ITween<float>> circleRotate = (t) =>
        {
            star.transform.rotation = Quaternion.identity;
            star.transform.Rotate(Camera.main.transform.forward, t.CurrentValue);
        };

        float startAngle = star.transform.rotation.eulerAngles.z;
        float endAngle = startAngle + 1440.0f;

        star.gameObject.Tween("RotateCircle", startAngle, endAngle, 4.0f, TweenScaleFunctions.CubicEaseInOut, circleRotate);

        StartCoroutine(LoadLevelAfterDelay(7f));
    }

    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
