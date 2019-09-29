using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    LongerBlast,
    MorrBomb,
    FasterRun,
    RemoteBomb
}

public class PickupItem : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Item collided with " + collision.gameObject.name + GameTag.ItemLongerBlast);

        if (collision.gameObject.tag == GameTag.Player
            || collision.gameObject.tag == GameTag.Blast)
        {
            Destroy(gameObject);
        }
        
    }
}
