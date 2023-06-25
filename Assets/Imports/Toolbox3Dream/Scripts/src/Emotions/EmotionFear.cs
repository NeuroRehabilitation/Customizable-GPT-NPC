namespace Toolbox3Dream
{
    /// <summary>
    /// Conditions that defines the Fear emotional state from physiological sensor array.
    /// </summary>
    public class EmotionFear : BaseEmotion
    {
        public EmotionFear() : base(EmotionalType.Fear)
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
