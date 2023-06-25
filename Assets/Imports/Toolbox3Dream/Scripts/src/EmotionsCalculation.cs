using UnityEngine;

namespace Toolbox3Dream
{
    /// <summary>
    /// Class that uses the algorithm
    /// </summary>
    public class EmotionsCalculation : MonoBehaviour
    {
        private float HRVprevious = 0f, HRV_HF = 0.0f;
        private float RSAprevious = 0f, RSA = 0.0f;
        private float HRprevious = 0f, HR = 0f;
        private float BRprevious = 0f, BR = 0f;

        public void SetDescriptorValues(float _HRV_HF, float _RSA, float _HR, float _BR)
        {
            // Store previous values to compare.
            HRVprevious = HRV_HF;
            RSAprevious = RSA;
            HRprevious = HR;
            BRprevious = BR;

            // Store current values of signals
            HRV_HF = _HRV_HF;
            RSA = _RSA;
            HR = _HR;
            BR = _BR;

            Debug.Log("::DESCRIPTORS:: HRV_HF: " + HRV_HF + " RSA: " + RSA + " HR:" + HR + " BR: " + BR);

            // TODO: Check decision tree
            // DECISION TREE ACCORDING DAMASIO's MODEL
            // The following decision tree needs to be validated through research, the conditions described 
            // are not completely directed by the model in the paper. See developer's manual for more information
            // related to this algorithm.
            if(/*Increase HR and no change in HRV_HF*/ HR > HRprevious && HRV_HF == HRVprevious)
            {
                //ANGER
                Debug.Log("New emotion ANGER");
                BaseTrigger.Emotions[EmotionalType.Anger].IntensityLevel = 1.0f;
                BaseTrigger.Emotions[EmotionalType.Happiness].IntensityLevel = 0.0f;
                BaseTrigger.Emotions[EmotionalType.Sadness].IntensityLevel = 0.0f;
                BaseTrigger.Emotions[EmotionalType.Fear].IntensityLevel = 0.0f;
            }
            else
            {
                if (/*Increase HR and decrease in HRV_HF*/ HR > HRprevious && HRV_HF < HRVprevious)
                {
                    if(/*Uncoupled with RSA*/ RSA < RSAprevious)
                    {
                        if(/*Increase Respiration Variability*/ BR > BRprevious)
                        {
                            // SADNESS
                            Debug.Log("New emotion SADNESS");
                            BaseTrigger.Emotions[EmotionalType.Anger].IntensityLevel = 0.0f;
                            BaseTrigger.Emotions[EmotionalType.Happiness].IntensityLevel = 0.0f;
                            BaseTrigger.Emotions[EmotionalType.Sadness].IntensityLevel = 1.0f;
                            BaseTrigger.Emotions[EmotionalType.Fear].IntensityLevel = 0.0f;
                        }
                        else if ( /*Increase Respiration Variability*/ BR == BRprevious )
                        {
                            // HAPPINESS
                            Debug.Log("New emotion HAPPINESS");
                            BaseTrigger.Emotions[EmotionalType.Anger].IntensityLevel = 0.0f;
                            BaseTrigger.Emotions[EmotionalType.Happiness].IntensityLevel = 1.0f;
                            BaseTrigger.Emotions[EmotionalType.Sadness].IntensityLevel = 0.0f;
                            BaseTrigger.Emotions[EmotionalType.Fear].IntensityLevel = 0.0f;
                        }
                    }
                    else if (/*Coupled with changes in respiration RSA*/ RSA > RSAprevious)
                    {
                        // FEAR
                        Debug.Log("New emotion FEAR");
                        BaseTrigger.Emotions[EmotionalType.Anger].IntensityLevel = 0.0f;
                        BaseTrigger.Emotions[EmotionalType.Happiness].IntensityLevel = 0.0f;
                        BaseTrigger.Emotions[EmotionalType.Sadness].IntensityLevel = 0.0f;
                        BaseTrigger.Emotions[EmotionalType.Fear].IntensityLevel = 1.0f;
                    }
                }
                else
                {
                    // FEAR
                    Debug.Log("Emotion not classified");
                    BaseTrigger.Emotions[EmotionalType.Anger].IntensityLevel = 0.0f;
                    BaseTrigger.Emotions[EmotionalType.Happiness].IntensityLevel = 0.0f;
                    BaseTrigger.Emotions[EmotionalType.Sadness].IntensityLevel = 0.0f;
                    BaseTrigger.Emotions[EmotionalType.Fear].IntensityLevel = 0.0f;
                }
            }
        }
    }
}
