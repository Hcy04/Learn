using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalAnimationTriggers : MonoBehaviour
{
    private Crystal crystal => GetComponentInParent<Crystal>();

    private void AnimationTrigger()
    {
        crystal.triggerCalled = true;
    }
}
