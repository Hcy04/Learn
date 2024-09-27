using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.MPE;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody2D rb;
    #endregion

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {

    }
}
