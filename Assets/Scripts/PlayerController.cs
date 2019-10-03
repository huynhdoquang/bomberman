using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] private Movement2D movementInput;

    public System.Action OnRefeshSttAction;


    /// <summary>
    /// number of bomb in player 
    /// </summary>
    [SerializeField] private int bombQuantity;
    public int BombQuantity {
        get { return bombQuantity; }
        private set { bombQuantity = value; }
    }
    private int bombQuantityLeft;
    private int remoteBombQuantityLeft;
    private int playerHealth = 1;
    /// <summary>
    /// speed of player
    /// </summary>
    [SerializeField]  private int playerRunSpeed;
    public int PlayerRunSpeed
    {
        get { return playerRunSpeed; }
        private set { playerRunSpeed = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]  private int blastRange = 4;
    public int BlastRange
    {
        get { return blastRange; }
        private set { blastRange = value; }
    }

    [SerializeField] private Transform bombParent;
    [SerializeField] private Bomb bombPrefab;
    [SerializeField] private Bomb remoteBombPrefab;

    public System.Action<PlayerInput> OnPlayerDieAction;
    

    public void Init()
    {
        movementInput.SetMovementSpeed(playerRunSpeed);
        movementInput.SetPlayerInput(playerInput);

        movementInput.OnClickFire1Action = PutBomb;
        movementInput.OnClickFire2Action = RemoteBomb;

        bombQuantityLeft = bombQuantity;
        remoteBombQuantityLeft = 1;

        if (OnRefeshSttAction != null) OnRefeshSttAction.Invoke();
    }

    void PutBomb()
    {
        Debug.Log("Putbomb ");

        //Check Bombquanity

        if (bombQuantityLeft <= 0)
            return;
        
        var bomb = Instantiate(bombPrefab, new Vector3(Mathf.RoundToInt(transform.position.x),
                                  Mathf.RoundToInt(transform.position.y), transform.position.z),
                                  bombPrefab.transform.rotation);
        
        bomb.transform.parent = bombParent;
        bomb.Init(blastRange);

        bomb.ExplosiveAction = BombExplosive;

        bomb.gameObject.SetActive(true);

        bombQuantityLeft--;
    }

    void BombExplosive()
    {
        bombQuantityLeft++;
    }

    void BombExplosiveRemote()
    {
        remoteBombQuantityLeft++;
    }

    Bomb _curRemoteBomb;
    void RemoteBomb()
    {
        Debug.Log("RemoteBomb ");
        if (remoteBombQuantityLeft <= 0)
        {
            if(_curRemoteBomb != null)
                _curRemoteBomb.ExplosiveRemote();
            return;
        }

        var bomb = Instantiate(remoteBombPrefab, new Vector3(Mathf.RoundToInt(transform.position.x),
                                  Mathf.RoundToInt(transform.position.y), transform.position.z),
                                  remoteBombPrefab.transform.rotation);
        _curRemoteBomb = bomb;
        bomb.transform.parent = bombParent;
        bomb.Init(blastRange);

        bomb.ExplosiveActionRemote = BombExplosiveRemote;

        bomb.gameObject.SetActive(true);

        remoteBombQuantityLeft--;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
       // Debug.Log("GameObject2 collided with " + collision.gameObject.name + GameTag.ItemLongerBlast);

        if(collision.gameObject.tag == GameTag.ItemLongerBlast)
        {
            //Add Blast
            blastRange += 1;
            if(OnRefeshSttAction != null) OnRefeshSttAction.Invoke();
        }

        if (collision.gameObject.tag == GameTag.ItemMoreBomb)
        {
            bombQuantityLeft += 1;
            bombQuantity += 1;
            if (OnRefeshSttAction != null) OnRefeshSttAction.Invoke();
        }

        if (collision.gameObject.tag == GameTag.ItemFasterRun)
        {
            playerRunSpeed += 1;
            movementInput.SetMovementSpeed(playerRunSpeed);
            if (OnRefeshSttAction != null) OnRefeshSttAction.Invoke();
        }

        if (collision.gameObject.tag == GameTag.ItemRemoteBomb)
        {
            //TODO:
        }

        if (collision.gameObject.tag == GameTag.Blast)
        {
            // OnDie
            if (OnPlayerDieAction != null)
                OnPlayerDieAction.Invoke(playerInput);
        }

    }
}
