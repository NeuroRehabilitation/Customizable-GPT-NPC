using UnityEngine;

namespace Toolbox3Dream
{
    /// <summary>
    /// This abstract class allows to generate new timed triggers that are sent each timeoutSeconds and send data to the observers
    /// based on the modifications done in the abstract method SetDataTriggerValues()
    /// </summary>
    public abstract class TriggerTimed : BaseTrigger
    {
        public float timeoutSeconds = 10.0f;
        private float elapsedTime = 0.0f;

        public TriggerTimed() : base (TriggerType.TimeBased)
        {
            data.TimePeriod = timeoutSeconds;
        }

        /**************
         * UNITY
         **************/
        void Start()
        {
            if (Id.Length < 1 || Id == null)
                Debug.LogError("Should define a string Id for the triggers");
            else
                data.TriggerId = Id;

            if (timeoutSeconds <= 0.0f)
                Debug.LogError("Timeout seconds should be greater than 0");
        }

        void LateUpdate()
        {
            // Timer to know if trigger should be generated
            if (elapsedTime < timeoutSeconds)
            {
                elapsedTime += Time.deltaTime;
            }
            else if (timeoutSeconds > 0.0f)  // timeOutSeconds equals 0.0f when was set improperly by user
            {
                elapsedTime = 0.0f;
                ActivateTrigger3Dream();
            }
        }
    }
}
