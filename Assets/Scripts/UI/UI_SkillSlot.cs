using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string skillName;
    [TextArea]
    public string skillDescription;

    public bool islocked;
    public bool isActive;

    [SerializeField] private UI_SkillSlot[] parents;
    [SerializeField] private UI_SkillSlot[] siblings;
    [SerializeField] private UI_SkillSlot[] children;

    [SerializeField] private Image skillImage;

    private void Start()
    {
        skillImage = GetComponent<Image>();

        if (!islocked) skillImage.color = Color.red;
        else if (!isActive) skillImage.color = Color.grey;
        else if (isActive) skillImage.color = Color.white;

        GetComponent<Button>().onClick.AddListener(() => Click());
    }

    public void Click()
    {
        if (!islocked) UnlockSkill();
        else if (!isActive) SetActive(true);
        else if (isActive) SetActive(false);
    }

    public void UnlockSkill()//父节点都解锁时 当前节点能解锁
    {
        for (int i = 0; i < parents.Length; i++)
        {
            if (parents[i].islocked == false) return;
        }

        islocked = true;

        //父节点都激活时 解锁后的默认状态才为激活 否则能解锁 担默认为未激活
        bool flag = true;
        for (int i = 0; i < parents.Length; i++)
        {
            if (parents[i].isActive == false) flag = false;
        }
        SetActive(flag);
    }

    public void SetActive(bool _isActive)
    {
        if (_isActive)//父节点都激活时 当前节点能激活 同时设置兄弟节点为未激活
        {
            for (int i = 0; i < parents.Length; i++)
            {
                if (parents[i].isActive == false) return;
            }

            for (int i = 0; i < siblings.Length; i++)
            {
                if (siblings[i].isActive == true) siblings[i].SetActive(false);
            }

            skillImage.color = Color.white;
            isActive = true;
            UI_Manager.instance.UpdateSkillBool();
        }
        else//子节点都未激活时 当前节点能取消激活
        {
            for (int i = 0; i < children.Length; i++)
            {
                if (children[i].isActive == true) return;
            }

            skillImage.color = Color.grey;
            isActive = false;
            UI_Manager.instance.UpdateSkillBool();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UI_Manager.instance.skillTip.ShowTip(skillName, skillDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UI_Manager.instance.skillTip.HideTip();
    }
}
