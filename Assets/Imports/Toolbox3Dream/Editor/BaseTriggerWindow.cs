using System.Collections;
using System.Collections.Generic;
#if(UNITY_EDITOR)
using UnityEditor;
#endif

/// TODO: Not implemented yet!!!!!!!!!!

namespace Toolbox3Dream
{
    [CustomEditor(typeof(BaseTrigger))]
    public class BaseTriggerWindow : Editor
    {
        public BaseTrigger mTrigger;

        void OnEnable()
        {
            mTrigger = (BaseTrigger) target;

            foreach (KeyValuePair<EmotionalType, BaseEmotion> em in BaseTrigger.Emotions)
            {
                em.Value.IntensityLevel = 0.0f;
            }
        }

        public override void OnInspectorGUI()
        {
            // Show the custom GUI controls.
            EditorGUILayout.IntSlider("Anger", 0, 0, 100);
            //EditorGUILayout.FloatSlider(damageProp, 0, 100, new GUIContent("Damage"));

        }
    }
}

