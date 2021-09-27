using UnityEngine;

public class LevelsText : MonoBehaviour
{
    void Start()
    {
        var parent = transform.parent;
        var parentRenderer = parent.GetComponent<Renderer>();
        var renderer = GetComponent<Renderer>();
        renderer.sortingLayerID = parentRenderer.sortingLayerID;
        renderer.sortingOrder = parentRenderer.sortingOrder;
        var text = GetComponent<TextMesh>();
        text.text = "Levels";
    }
}