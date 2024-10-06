using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StatInfo : MonoBehaviour
{
    [SerializeField] private RectTransform self;
    [SerializeField] private TextMeshProUGUI info;

    public void ShowInfo(string stat)
    {
        transform.position = self.anchoredPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        info.text = Factory.GetDescription(stat);

        gameObject.SetActive(true);
    }

    public void HideInfo() => gameObject.SetActive(false);
}
