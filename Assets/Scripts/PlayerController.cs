using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] private Movement2D movementInput;

    
    /// <summary>
    /// number of bomb in player 
    /// </summary>
    private int bombQuantity;
    private int playerHealth = 1;
    /// <summary>
    /// speed of player
    /// </summary>
    [SerializeField]  private int playerRunSpeed;
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]  private int blastRange = 4;

    [SerializeField] private Bomb bombPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        movementInput.SetMovementSpeed(playerRunSpeed);
        movementInput.SetPlayerInput(playerInput);

        movementInput.OnClickFire1Action = PutBomb;
        movementInput.OnClickFire2Action = PutBomb;
    }

    void PutBomb()
    {
        Debug.Log("Putbomb ");

        var bomb = Instantiate(bombPrefab);
        bomb.transform.position = transform.position;
        bomb.Init(blastRange);
    }

    void RemoteBomb()
    {
        Debug.Log("RemoteBomb ");
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("GameObject2 collided with " + collision.gameObject.name + GameTag.ItemLongerBlast);

        if(collision.gameObject.tag == GameTag.ItemLongerBlast)
        {
            //Add Blast
            blastRange += 1;
        }

        if (collision.gameObject.tag == GameTag.ItemMoreBomb)
        {
            bombQuantity += 1;
        }

        if (collision.gameObject.tag == GameTag.ItemFasterRun)
        {
            playerRunSpeed += 1;
            movementInput.SetMovementSpeed(playerRunSpeed);
        }

        if (collision.gameObject.tag == GameTag.ItemRemoteBomb)
        {
            //TODO:
        }
    }
}
