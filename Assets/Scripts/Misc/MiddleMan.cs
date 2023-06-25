using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleMan : MonoBehaviour
{
    public void playButtonSound()
    {
        GameObject.Find("StateController").GetComponent<StateController>().playButtonSound();
    }
}
