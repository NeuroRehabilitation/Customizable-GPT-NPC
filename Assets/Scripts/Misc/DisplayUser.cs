using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayUser : MonoBehaviour
{
    public TMP_Text userText;
    public StateController SC;
    // Start is called before the first frame update
    void Start()
    {
        SC = GameObject.Find("StateController").GetComponent<StateController>();
        userText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SC.Username != "tests")
            userText.text = SC.Username;
    }
}
