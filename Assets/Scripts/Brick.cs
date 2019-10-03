using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private Transform itemParent;
    [SerializeField] private PickupItem[] itemList;
    const int percentDropItem = 30;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Brick collided with " + collision.gameObject.name + GameTag.ItemLongerBlast);
        
        if (collision.gameObject.tag == GameTag.Blast)
        {
            // Spawn Item follow %
            if (Random.Range(0, 100) < percentDropItem) //Random.Range(0,100) < 50
            {
                var item = Instantiate(itemList[Random.Range(0, itemList.Length)]);
                item.transform.position = transform.position;
                item.transform.parent = itemParent;
                item.gameObject.SetActive(true);
            }
            // Destroy
            Destroy(gameObject);
        }
    }
}
