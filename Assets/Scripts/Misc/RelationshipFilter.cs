using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationshipFilter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (JSONUtils.myPlayer.psycoFilters[0]==true)
                gameObject.SetActive(false);
        }
        catch (System.Exception)
        {
           // print("can't filter, no login yet");
        }
        
    }
}
