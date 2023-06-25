using Toolbox3Dream;
using System.Linq;

/// <summary>
/// Example of trigger that detect the minimum intensity level among a list of emotions and send the data when triggered.
/// </summary>
public class TriggerMaximumEmotion : TriggerTimed
{
    protected override void SetDataTriggerValues(DataTrigger data)
    {
        float[] emotionsLevel = new float[] { Emotions[EmotionalType.Anger].IntensityLevel,
            Emotions[EmotionalType.Happiness].IntensityLevel,
            Emotions[EmotionalType.Sadness].IntensityLevel,
            Emotions[EmotionalType.Fear].IntensityLevel,
        };

        float maxValue = emotionsLevel.Max();
        int maxIndex = emotionsLevel.ToList().IndexOf(maxValue);

        data.EmotionType = (EmotionalType) (maxIndex+1);  // Translate number to enum omitting first literal of enum
        data.InterpolationValue = maxValue;
    }
}
