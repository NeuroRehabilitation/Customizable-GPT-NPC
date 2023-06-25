using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class ColorPallette : MonoBehaviour
{
    public StateController stateController;

    void Start()
     {
        stateController = GameObject.Find("StateController").GetComponent<StateController>();
        Color myColor = new Color();

        // Get image from this parent gameobject and change to the respective color defined in the StateController
        if (transform.gameObject.GetComponent<Image>())
        {
            ColorUtility.TryParseHtmlString(stateController.ImageHexValue, out myColor);
            transform.gameObject.GetComponent<Image>().color = myColor;
        }
        // loops for each child of the gameobject until there's no more children left
        WithForeachLoop(transform);
     }
 
    // Does the same as above, but for each other element, like text, title, etc.
     void WithForeachLoop(Transform thisTransform)
     {
         Color myColor = new Color();
         
         foreach (Transform child in thisTransform)
         {
            if (child.gameObject.tag=="PalletteText")
            {
                ColorUtility.TryParseHtmlString(stateController.TextHexValue, out myColor);
                child.gameObject.GetComponent<TextMeshProUGUI>().color = myColor;
            }
                
            else if (child.gameObject.tag=="PalletteTitle")
                {
                    ColorUtility.TryParseHtmlString(stateController.TitleHexValue, out myColor);
                    child.gameObject.GetComponent<TextMeshProUGUI>().color = myColor;
                }

            if (child.gameObject.tag=="PalletteButton")
            {
                ColorUtility.TryParseHtmlString(stateController.ButtonImageHexValue, out myColor);
                child.gameObject.GetComponent<Image>().color = myColor;
            }
            else if (child.gameObject.tag=="PalletteImage")
            {
                ColorUtility.TryParseHtmlString(stateController.ImageHexValue, out myColor);
                child.gameObject.GetComponent<Image>().color = myColor;
            }
            WithForeachLoop(child);
            
         }
             
     }
}
