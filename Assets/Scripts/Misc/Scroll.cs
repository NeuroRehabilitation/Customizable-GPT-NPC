using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{
    public Canvas Canvas;
    public ScrollRect ScrollRect;
    public float scrollValue;
    // Start is called before the first frame update
    public void scrollDown()
    {
        Canvas.ForceUpdateCanvases();
         
        ScrollRect.verticalNormalizedPosition=ScrollRect.verticalNormalizedPosition-scrollValue;
    }

    // Update is called once per frame
    public void scrollUp()
    {
        Canvas.ForceUpdateCanvases();
         
        ScrollRect.verticalNormalizedPosition=ScrollRect.verticalNormalizedPosition+scrollValue;
    }
}
