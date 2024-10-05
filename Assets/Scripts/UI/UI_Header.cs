using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Header : MonoBehaviour
{
    [SerializeField] private Transform body;

    private void Start()
    {
        for (int i = 0; i < body.transform.childCount; i++)
        {
            if (i == 0) body.transform.GetChild(i).gameObject.SetActive(true);
            else body.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void SwitchTo(GameObject _menu)
    {
        for (int i = 0; i < body.transform.childCount; i++) body.transform.GetChild(i).gameObject.SetActive(false);

        if (_menu != null) _menu.SetActive(true);
    }
}
