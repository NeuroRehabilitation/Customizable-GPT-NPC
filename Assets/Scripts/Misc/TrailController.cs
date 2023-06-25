using UnityEngine;

public class TrailController : MonoBehaviour
{
    public Transform navMeshAgent;
    public float slopeAngleThreshold = 30;

    private void Update()
    {
        transform.position = navMeshAgent.position;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100))
        {
            Vector3 normal = hit.normal;
            float slopeAngle = Vector3.Angle(Vector3.up, normal);
            if (slopeAngle > slopeAngleThreshold)
            {
                Quaternion slopeRotation = Quaternion.FromToRotation(Vector3.up, normal)* Quaternion.Euler(90, 0, 0);
                transform.rotation = Quaternion.Euler(0, slopeRotation.eulerAngles.y, slopeRotation.eulerAngles.z);
            }
            else
            {
                transform.rotation = Quaternion.identity;
            }
        }
    }
}
