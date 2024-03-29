﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlastDir
{
    top,
    down,
    left, 
    right
}

public class Bomb : MonoBehaviour
{
    [SerializeField] private Blast blastPrefab;

    [SerializeField] private int timeToExplosive;
    [SerializeField] private int blastExplosive;

    [Tooltip("Time each blast")]
    [SerializeField] private float blastTime = 0.5f;

    [SerializeField] private LayerMask LayerMask;
 
    bool onAlive;
    bool isExplosived;
    private float timeBomb;
    public System.Action ExplosiveAction;
    public System.Action ExplosiveActionRemote;

    private int numbOfBalst;

    public void Init(int blastExplosive, int timeToExplosive = 3)
    {
        this.timeToExplosive = timeToExplosive;
        this.blastExplosive = blastExplosive;
        numbOfBalst = 0;
        onAlive = true;
    }

    void Update()
    {
        if (onAlive)
        {
            timeBomb += Time.deltaTime;
            if (timeBomb >= timeToExplosive)
            {
                if(!isExplosived) Explosive();
                isExplosived = true;
            }

            if (numbOfBalst >= blastExplosive*4 +1)
            {
                //
                onAlive = false;
                Destroy(gameObject);
            }
        }
    }

    public void ExplosiveRemote()
    {
        Explosive();
    }

    void Explosive()
    {
        if (ExplosiveAction != null)
            ExplosiveAction.Invoke();

        if (ExplosiveActionRemote != null)
            ExplosiveActionRemote.Invoke();


        for (int i = 0; i <= blastExplosive; i++)
        {
            if (i == 0) {
                var b = Instantiate(blastPrefab);
                b.transform.position = transform.position;
                b.Init();
                b.gameObject.SetActive(true);
                numbOfBalst++;

                continue;
            }
            StartCoroutine(SpwanBlast(i));
        }
    }


    IEnumerator SpwanBlast(int i)
    {
        yield return new WaitForSeconds(blastTime*i);
        foreach (BlastDir e in System.Enum.GetValues(typeof(BlastDir)))
        {
            if(OnCheckBrick(e,i))
                OnSpawnBlast(e, i);
            numbOfBalst++;
        }
    }

    void OnSpawnBlast(BlastDir blastDir, int dis)
    {
        float addOnX = transform.position.x;
        float addOnY = transform.position.y;

        switch (blastDir)
        {
            case BlastDir.top:
                addOnX = transform.position.x;
                addOnY = transform.position.y + dis;
                break;
            case BlastDir.down:
                addOnX = transform.position.x;
                addOnY = transform.position.y - dis;
                break;
            case BlastDir.left:
                addOnX = transform.position.x - dis;
                addOnY = transform.position.y;
                break;
            case BlastDir.right:
                addOnX = transform.position.x + dis;
                addOnY = transform.position.y;
                break;
            default:
                break;
        }
        var b = Instantiate(blastPrefab);
        b.transform.position = new Vector3(addOnX, addOnY, transform.position.z);
        b.gameObject.SetActive(true);
        b.Init();

        Debug.Log("sinh bom " + blastDir);
    }

    private Dictionary<BlastDir, bool> _isGetBrickDict;
    bool OnCheckBrick(BlastDir blastDir, int dis)
    {
        Vector3 direction = Vector3.zero;

        switch (blastDir)
        {
            case BlastDir.top:
                direction = Vector3.up;
                break;
            case BlastDir.down:
                direction = Vector3.down;
                break;
            case BlastDir.left:
                direction = Vector3.left;
                break;
            case BlastDir.right:
                direction = Vector3.right;
                break;
            default:
                break;
        }

        //
        if (_isGetBrickDict == null)
            _isGetBrickDict = new Dictionary<BlastDir, bool>();

        if (_isGetBrickDict.ContainsKey(blastDir))
            if (_isGetBrickDict[blastDir]) return false;

        RaycastHit2D hit;
        hit = Physics2D.Raycast(new Vector3(transform.position.x -0.5f, transform.position.y - 0.5f, transform.position.z), direction,
          dis, LayerMask);

        Debug.DrawRay(new Vector3(transform.position.x - 0.5f, transform.position.y - 0.5f, transform.position.z), direction * dis, Color.green);

        if (hit.collider)
        {
            Debug.Log("hit brick hit ");

            if (hit.collider.tag == GameLayer.Brick)
            {
                if (!_isGetBrickDict.ContainsKey(blastDir))
                    _isGetBrickDict.Add(blastDir, true);
                Debug.Log("hit NORMAL brick hit");
                return true;
            }else
                Debug.Log("hit UNBRAEKE brick hit");
        }

        if (hit.collider != null) return false;
        else  return true;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("GameObject2 collided with " + collision.gameObject.name);
        if (collision.gameObject.tag == "blast")
        {
            Debug.Log("Explosived thought blast");
            if (!isExplosived) Explosive();
            isExplosived = true;
        }
    }
    
}
