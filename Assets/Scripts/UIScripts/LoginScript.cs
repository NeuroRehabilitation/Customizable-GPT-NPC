using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script used to control the login and registration process, Depending on the canvas that is active the user will be able to login, register or set the filters for the psycho messages
public class LoginScript : MonoBehaviour
{
    public GameObject LoginUI;
    public GameObject Home;
    public GameObject Tutorial;
    public int curr = 0;

    public GameObject[] Register;
    public void ShowLogin()
    {
        LoginUI.SetActive(true);
        Home.SetActive(false);
    }

    public void ShowRegister()
    {
        
        foreach (GameObject obj in Register) 
        {
            obj.SetActive(false);
        }
        if (curr >= Register.Length)
            Tutorial.SetActive(true);
        else
            Register[curr].SetActive(true);
        Home.SetActive(false);
    }

    public void filterPsycoMessages(int i)
    {
        switch (i) 
        {
            case 0:
                JSONUtils.myPlayer.psycoFilters.Add(true);
                break;
            case 1:
                JSONUtils.myPlayer.psycoFilters.Add(false);
                break;
            default:
                ShowRegister();
                break;
        }
        curr++;
        JSONUtils.UpdateJson();
        ShowRegister();
    }
}
