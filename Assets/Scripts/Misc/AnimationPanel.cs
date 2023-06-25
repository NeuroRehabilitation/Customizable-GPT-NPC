using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPanel : MonoBehaviour
{
    public Animator anim;
    public int layer;
    // Start is called before the first frame update
    public void setCurrLayer(int layer)
    {
        this.layer=layer;
    }

    public void playAnimation(string animName)
    {
        anim.CrossFade(animName, 0.1f,layer, 0.1f);
    }
}
