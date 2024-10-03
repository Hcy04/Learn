using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigget : MonoBehaviour
{
    Item item => GetComponentInParent<Item>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer  == 11) item.PickUpItem();
    }
}
