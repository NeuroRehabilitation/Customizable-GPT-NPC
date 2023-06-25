
using UnityEngine;

public class RaycastRotationDetector : MonoBehaviour
{
    private void Update()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 normal = hit.normal;
            Quaternion rotation = Quaternion.FromToRotation(transform.up, normal);
           
            Debug.Log("Angle right: " + rotation.eulerAngles);
        }
    }
}
