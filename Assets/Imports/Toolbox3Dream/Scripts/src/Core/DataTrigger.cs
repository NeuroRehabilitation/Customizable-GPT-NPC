namespace Toolbox3Dream
{
    /// <summary>
    /// Class that contains information sent from the Trigger3Dream class to the observers of these triggers. In this class is strongly recommended to
    /// implement the InterpolationValue property to make sure that the observer object can modulate its own components with this normalized value.
    /// For instance, if it is desired to send the intensity of the maximum emotion detected, normalize it dividing by the maximum possible value.
    /// </summary>
    public class DataTrigger
    {
        // Name of the trigger event executed
        private string triggerId;
        public string TriggerId
        {
            get { return triggerId; }
            set { triggerId = value; }
        }

        // Type of trigger
        private TriggerType type;
        public TriggerType Type
        {
            get { return type; }
            set { type = value; }
        }


        // Emotion of maximum signals
        private EmotionalType emotionType;
        public EmotionalType EmotionType
        {
            get { return emotionType; }
            set { emotionType = value; }
        }

        // Field useful to use the trigger as a controller of an animation or analog input in the object.
        // Value from 0 to 1.
        private float interpolationValue;
        public float InterpolationValue
        {
            get { return interpolationValue; }
            set
            {
                if (value > 1.0f)
                    interpolationValue = 1.0f;
                else if (value < 0.0f)
                    interpolationValue = 0.0f;
                else
                    interpolationValue = value;
            }
        }

        private float time;
        public float TimePeriod
        {
            get { return time; }
            set { time = value; }
        }

        /***************
         * CONSTRUCTOR
         ***************/
        public DataTrigger()
        {
            Type = TriggerType.Null;
            InterpolationValue = 0.0f;
        }

        public override string ToString()
        {
            string str = "name: " + TriggerId +
                "\tInterpolationValue: " + InterpolationValue.ToString() +
                "\tType: " + Type.ToString() +
                "\tEmotion: " + EmotionType.ToString() +
                "\n";
                
            return str;
        }

    }
}
