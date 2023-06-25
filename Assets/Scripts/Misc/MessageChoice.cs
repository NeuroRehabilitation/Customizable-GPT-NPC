using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageChoice : MonoBehaviour
{
    public GameObject RecordMSGMenu;
    public GameObject WriteMSGMenu;
    public GameObject ChoiceMenu;

    public void ChooseRecord()
    {
        ChoiceMenu.SetActive(false);
        RecordMSGMenu.SetActive(true);
    }

    public void ChooseWrite()
    {
        ChoiceMenu.SetActive(false);
        WriteMSGMenu.SetActive(true);
    }

    public void Back()
    {
        ChoiceMenu.SetActive(true);
        WriteMSGMenu.SetActive(false);
        RecordMSGMenu.SetActive(false);
    }
}
