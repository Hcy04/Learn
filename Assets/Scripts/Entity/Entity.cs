using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.MPE;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    [HideInInspector] public Animator anim;
    #endregion

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {

    }
}
