using Toolbox3Dream;
using UnityEngine;

/// <summary>
/// Example of trigger that sends a random emotion and its respective intensity level when triggered.
/// </summary>
public class TriggerRandomEmotion : TriggerTimed
{
    protected override void SetDataTriggerValues(DataTrigger data)
    {
        float[] angerLevel = new float[] { Emotions[EmotionalType.Anger].IntensityLevel,
            Emotions[EmotionalType.Happiness].IntensityLevel,
            Emotions[EmotionalType.Sadness].IntensityLevel,
            Emotions[EmotionalType.Fear].IntensityLevel,
        };

        int index = Random.Range(0,angerLevel.Length);

        data.EmotionType = (EmotionalType) (index + 1);
        data.InterpolationValue = angerLevel[index];
    }
}
