using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderStrike : MonoBehaviour
{
    private bool animationOver;

    private void Update()
    {
        if (animationOver) Destroy(this.gameObject);
    }

    private void AnimationTrigger()
    {
        animationOver = true;
    }
}
