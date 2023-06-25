using UnityEngine.XR;
using UnityEngine;
public class RecenterPlayer : MonoBehaviour
{
    void Update()
{
    if (Input.GetKey(KeyCode.J))
    {
        GameObject XRorigin = GameObject.Find("XR Origin");
        XRorigin.transform.rotation = Quaternion.Euler(0, 17, 0);
    }
}

}
