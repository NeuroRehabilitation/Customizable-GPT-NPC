using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas3 : MonoBehaviour
{
    public string canvas2Name = "Canvas3";
    private GameObject canvas2;

    private void Start()
    {
        canvas2 = GameObject.Find(canvas2Name);
        if (canvas2 != null)
        {
            canvas2.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand") && canvas2 != null)
        {
            canvas2.SetActive(true);
        }
    }
}
