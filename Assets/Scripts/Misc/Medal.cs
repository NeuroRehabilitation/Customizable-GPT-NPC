using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medal : MonoBehaviour
{
    [SerializeField] private GameObject messageDisplay;

    public void Grab()
    {
        messageDisplay.SetActive(true);
        gameObject.SetActive(false);
    }
}
