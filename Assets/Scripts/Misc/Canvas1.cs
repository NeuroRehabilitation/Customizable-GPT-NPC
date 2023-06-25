using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        canvas1.SetActive(false);
    }

    public GameObject canvas1;

    // Call this method to set the Canvas1 object to inactive
    public void HideCanvas1()
    {
        canvas1.SetActive(true);
    }
}
