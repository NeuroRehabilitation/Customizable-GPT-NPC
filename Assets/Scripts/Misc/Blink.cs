using UnityEngine;

public class Blink : MonoBehaviour
{
    public Animation animationComponent;
    public AnimationClip animationClip;

    void Start()
    {
        animationComponent = GetComponent<Animation>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animationComponent.clip = animationClip;
            animationComponent.Play();
        }
    }
}
