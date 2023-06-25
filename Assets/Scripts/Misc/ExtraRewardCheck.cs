using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraRewardCheck : MonoBehaviour
{
    StateController SC;
    public int scene;
    void Start()
    {
        SC = GameObject.Find("StateController").GetComponent<StateController>();
    }

    // checks if the user has completed the module and if so, activates the reward
    void Update()
    {
        if (SC.finishedModules[scene]==true)
            transform.GetChild(0).gameObject.SetActive(true);
    }
}
