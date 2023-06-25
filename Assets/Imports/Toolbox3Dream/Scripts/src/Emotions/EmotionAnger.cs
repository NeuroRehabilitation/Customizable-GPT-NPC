namespace Toolbox3Dream
{
    /// <summary>
    /// Conditions that defines the Anger emotional state from physiological sensor array.
    /// </summary>
    public class EmotionAnger : BaseEmotion
    {
        public EmotionAnger() : base(EmotionalType.Anger)
        {
            //Descriptors.Add(new DescriptorRespirationVariability());
        }

        protected override float UpdateEmotionalIntensity()
        {
            // Linear interpolation to calculate emotion intensity
            return (Descriptors[0].CurrentValue - Descriptors[0].MinValue) / (Descriptors[0].MaxValue - Descriptors[0].MinValue);
        }
    }

}
