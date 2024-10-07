using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_SKillToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI discription;

    public void ShowTip(string _name, string _discription)
    {
        transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        skillName.text = _name;
        discription.text = _discription;

        gameObject.SetActive(true);
    }

    public void HideTip()
    {
        gameObject.SetActive(false);
    }
}
