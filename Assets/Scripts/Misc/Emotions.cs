using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
    public class EmotionPhrase
    {
        public int emotionID;
        public List<AudioClip> phrases;
    }

    public class Emotions : MonoBehaviour
    {
        public List<EmotionPhrase> emotionPhrases;
    }
