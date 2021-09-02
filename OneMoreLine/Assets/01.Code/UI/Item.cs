using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour
{

    public enum itemType {
        Star,
        Diamond
    }

    public itemType _itemType;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            InGameManager.instance.DoAddItem(_itemType);

            Destroy(gameObject);
        }
    }
}
