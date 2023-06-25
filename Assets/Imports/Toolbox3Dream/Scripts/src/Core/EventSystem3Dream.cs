using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Toolbox3Dream;

namespace Toolbox3Dream
{
    /// <summary>
    /// Controller of Events in Toolbox 3Dream, it is mandatory that this class is present in the scene controlled by 3DreamToolbox.
    /// This class looks for the implemented triggers in the scene and for the observer objects that are modified by trigger 3Dream.
    /// This object is checking in the Update() method if any of the triggers has fulfilled its condition and send the message (notification,
    /// event, listener, etc.) to the observer objects.
    /// IMPLEMENTATION: http://gregandaduck.blogspot.pt/2015/02/using-unitys-c-message-system-unity-46.html
    /// </summary>
    public class EventSystem3Dream : MonoBehaviour
    {
        private List<BaseTrigger> triggers3Dream;
        private List<GameObject> observerObjects3Dream;

        private void CreateTriggers()
        {
            // Find implemented triggers
            triggers3Dream = new List<BaseTrigger>();
            var triggersInScene = Object.FindObjectsOfType<BaseTrigger>();
            foreach (BaseTrigger trigger in triggersInScene)
            {
                triggers3Dream.Add(trigger);
               // Debug.Log("3Dream: TRIGGER found in GameObject " + trigger.gameObject.name);
            }

            // Find observer objects in Scene
            observerObjects3Dream = new List<GameObject>();
            var components = InterfaceHelper.FindObjects<ITriggered3Dream>(); // Get scripts that implements the interface
            foreach(ITriggered3Dream component in components)
            {
                MonoBehaviour mb = component as MonoBehaviour;          // Get the gameObject instance that is implementing the interface
                observerObjects3Dream.Add(mb.gameObject);
                //Debug.Log("3Dream: OBSERVER found in GameObject " + mb.gameObject.name);
            }

            // Print information of 3Dream setup
            /*if (triggers3Dream.Count == 0)
                Debug.Log("3Dream:: Triggers not found in Scene.");
            else
                Debug.Log("3Dream:: Found " + triggers3Dream.Count + " triggers and " + observerObjects3Dream.Count + " observers.");*/
        }


        /**************
         * UNITY
         **************/
        void Start()
        {
            CreateTriggers();
            //StartCoroutine(CheckTriggersStatus());
        }

        void OnDestroy()
        {
            //StopAllCoroutines();
        }


        void Update()
        {
            // TODO: Check performance of Notify object and implement Coroutine if needed
            CheckTriggersStatus();
        }

        /*IEnumerator*/ void CheckTriggersStatus()
        {
            foreach (BaseTrigger trigger in triggers3Dream)
            {
                if (trigger.IsActive)
                {
                    //IManageable3Dream[] objs = Object.FindObjectsOfType<IManageable3Dream>();

                    foreach (GameObject obj in observerObjects3Dream)
                    {
                        ExecuteEvents.Execute<ITriggered3Dream>(obj, null, (x, y) => x.OnTrigger3Dream(trigger.data));
                        //yield return null; // Co-routine
                    }

                    // Trigger executed, reset to false
                    trigger.IsActive = false;
                }
            }
        }
    }

}
