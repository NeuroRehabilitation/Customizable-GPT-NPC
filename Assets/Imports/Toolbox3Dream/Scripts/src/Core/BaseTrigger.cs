using System;
using System.Collections.Generic;
using UnityEngine;

namespace Toolbox3Dream
{
    /// <summary>
    /// This abstract class allows to create triggers (Messages with IEventSystem) that are sent to objects in the scene (observer). The id should be unique among all the triggers to 
    /// allow be detected by the observer. Each trigger has an instance of DataTrigger that allows to send specific information to the GameObjects and allows it to execute certain
    /// actions based on specific triggers or values that varies from 0 to 1 depending the conditions set in the trigger.
    /// For instance, a inherited class could generate a trigger named "MaximumEmotion" activated each 10 seconds. And send the normalized value (0-1) of the stronger Emotion, besides
    /// the EmotionType associated to that intensity. All contained in the DataTrigger object.
    /// </summary>

    public abstract class BaseTrigger : MonoBehaviour
    {
        // Unique name of the trigger
        [SerializeField]
        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        // Variable that states if a trigger is active or inactive
        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        // Emotions that are used to check if the trigger activates
        private static Dictionary<EmotionalType, BaseEmotion> emotions;
        public static Dictionary<EmotionalType, BaseEmotion> Emotions       // DEBUG: Modified from protected to public to allow modification from Unity
        {
            get { return emotions; }
        }

        // If the trigger is generated to be activated by Time, by collision, etc.
        private TriggerType type;
        public TriggerType Type
        {
            get { return type; }
            set { type = value; }
        }

        // Data to send to notified objects
        [SerializeField]
        public DataTrigger data;

        // Actions done when trigger conditions are accomplished
        protected void ActivateTrigger3Dream()
        {
            SetDataTriggerValues(data);     // Read data with the conditions of the specific trigger implementation
            float valueOfTriggerData = data.InterpolationValue;
            if (valueOfTriggerData < 0.0f || valueOfTriggerData > 1.0f)
                Debug.LogWarning("Interpolation data for trigger is outside [0,1] bounds. It is strongly recommended to check the conditions" +
                    "to assure that this value could be used to modulate a Lerp or transition value." + "Value: " + valueOfTriggerData);
            // Variable that notifies to observers of this trigger, this is set to false by EventSystem3Dream immediately notification to observers is 
            IsActive = true;
        }


        // Conditions to determine new value to send to observers
        // This function must use Emotion property to create conditions of triggering
        protected abstract void SetDataTriggerValues(DataTrigger data);


        /**************
         * CONSTRUCTOR
         **************/
        // Constructor to generate only one instance of each emotion and store it in the dictionary
        static BaseTrigger()
        {
            emotions = new Dictionary<EmotionalType, BaseEmotion>();

            // Emotional Signals that define triggers
            BaseEmotion[] emotionArray = {
                new EmotionAnger(),
                new EmotionHappiness(),
                new EmotionSadness(),
                new EmotionFear(),
                // INFO: Add new emotions here if needed, in order to generate a singleton for each Emotion.
            };

            foreach (BaseEmotion _emotion in emotionArray)
            {
                Emotions.Add(_emotion.Type, _emotion);
            }
        }

        // Inherited constructor
        protected BaseTrigger(TriggerType _type)
        {
            // New instance of data sent to observers
            data = new DataTrigger();

            // Initial state of trigger is desactivated, the trigger is activated by "EventSystem3Dream" script
            IsActive = false;
            Type = _type;
            
            // Update type of trigger of the data container
            data.Type = Type;
        }
    }
}
