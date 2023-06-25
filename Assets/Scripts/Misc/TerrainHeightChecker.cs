using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainHeightChecker : MonoBehaviour
{
    public GameObject GOToSpawn;
    public GameObject GOToSpawnParent;

    public float tileX =0f; 
    public float tileZ =0f; 
    public bool spawn=false;

    public List<GameObject> placedTrees;
    public int i = 0;

    public Vector3 hitPos;
    RaycastHit hit;
    Ray landingRay;
    void Update()
    {
        if (i!= 500 && spawn == true)
        {
            float spawnPointX=Random.Range(-300.0f+(255 * tileX), 500.0f+(255 * tileX));
            float spawnPointZ=Random.Range(-200.0f+(255 * tileZ), 550.0f+(255 * tileZ));
            
            transform.position = new Vector3(spawnPointX, 115, spawnPointZ);
            
            if (Physics.Raycast(landingRay, out hit, 100f))
            {
                Debug.DrawRay(transform.position, Vector2.down * 100f, Color.blue); // try 
                 
                if (hit.collider.tag == "terrain")
                {
                     
                    hitPos = hit.point;
                    placedTrees.Add(Instantiate (GOToSpawn, hitPos, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)),GOToSpawnParent.transform)  as GameObject);
                } 
            }
            i++;
        }
        
    }

    public IEnumerator Spawn()
    {
        landingRay = new Ray(transform.position,Vector3.down);
        yield return new WaitForSeconds(4);
        spawn = true;
        
    }

}
