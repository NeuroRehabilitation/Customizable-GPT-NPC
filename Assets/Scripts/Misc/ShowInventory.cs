using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInventory : MonoBehaviour
{
    public GameObject inventory;
    public GameObject lastInventory;
    public GameObject spawnPos;
    
    public void SpawnInventory()
    {
        Destroy(lastInventory);
        lastInventory = Instantiate(inventory, spawnPos.transform.position, Quaternion.identity);
    }
}
