using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FadeScreen : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FadeIn() => anim.Play("fadeOut");
    public void FadeOut() => anim.Play("fadeIn");
}
