using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_StatSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UI_Manager.instance.statInfo.ShowInfo(text.text);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UI_Manager.instance.statInfo.HideInfo();
    }
}
