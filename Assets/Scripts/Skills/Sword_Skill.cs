using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill : Skill
{
    [Header("Skill Active")]
    [SerializeField] private bool canThrowSword;
    [SerializeField] private bool haveAimDots;
    [SerializeField] private int skillMode;//1:isBouncing 2:isPierce 3:isSpin
    
    [Header("Skill Info")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private float launchForce = 20;
    [SerializeField] private float swordGravity = 3;
    [SerializeField] private float returnSpeed = 16;

    [Header("Aim Dots")]
    [SerializeField] private int numberOfDots = 10;
    [SerializeField] private float spaceBeetwenDots = .1f;
    [SerializeField] private GameObject dotPrefab;

    private GameObject[] dots;

    protected override void Start()
    {
        base.Start();

        GenereateDots();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < numberOfDots; i++) dots[i].transform.position = DotsPosition(i * spaceBeetwenDots);
        }
    }

    public override bool CanUseSkill()
    {
        if (base.CanUseSkill() && !player.sword && canThrowSword) return true;
        else return false;
    }

    public void CreateSword()
    {
        if (canThrowSword)
        {
            GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
            
            newSword.GetComponent<Sword>().SetUpSword(AimDirection() * launchForce, swordGravity, returnSpeed, skillMode);

            DotsActive(false);
            player.sword = newSword;
        }
    }

    public void CanSetDotsActive()
    {
        if (haveAimDots) DotsActive(true);
    }

    private Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return (mousePosition - playerPosition).normalized;
    }

    private void DotsActive(bool _isActive)
    {
        for (int i = 0; i < numberOfDots; i++) dots[i].SetActive(_isActive);
    }

    private void GenereateDots()
    {
        dots = new GameObject[numberOfDots];
        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, player.transform);
            dots[i].SetActive(false);
        }
    }

    private Vector2 DotsPosition(float t)
    {
        return (Vector2)player.transform.position +  AimDirection() * launchForce * t + .5f * Physics2D.gravity * swordGravity * t * t;
    }

    public float GetReturnSpeed()
    {
        return returnSpeed;
    }
}
