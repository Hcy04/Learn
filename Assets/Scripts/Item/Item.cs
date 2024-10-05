using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private Rigidbody2D rb;

    public void SetUpItem(ItemData _itemData, Vector2 _velocity)
    {
        itemData = _itemData;
        rb.velocity = _velocity;

        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = "Item - " + itemData.itemName;
    }

    public void PickUpItem()
    {
        if (Inventory.instance.ManageItem(itemData, true)) Destroy(gameObject);
        else rb.velocity = new Vector2(Random.Range(-5, 6), Random.Range(10, 15));
    }
}
