using UnityEngine;
using UnityEngine.EventSystems;

public class PumpPositioning : MonoBehaviour
{
    public static int secondaryNumber = 0;
    void Start()
    {
        // Try to find EventSystem3Dream(Clone)
        GameObject eventSystem = GameObject.Find("EventSystem3Dream(Clone)");

        // If EventSystem3Dream(Clone) doesn't exist, try to find EventSystem3Dream(Clone)(Clone)
        if (eventSystem == null)
        {
            eventSystem = GameObject.Find("EventSystem3Dream(Clone)(Clone)");
        }

        // If either EventSystem3Dream(Clone) or EventSystem3Dream(Clone)(Clone) exist, set the child's position
        if (eventSystem != null)
        {
            // Get the first child of the event system
            Transform firstChild = eventSystem.transform.GetChild(0);

            // Get the last child of the first child
            Transform lastChild = firstChild.GetChild(firstChild.childCount - 1 - secondaryNumber);

            // Set the position of the last child to be the same as the pump, but with a higher Y value
            lastChild.position = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z);

            secondaryNumber++;
        }
    }
}
