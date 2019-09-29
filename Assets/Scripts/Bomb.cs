using System.Collections;
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

    bool onAlive;
    bool isExplosived;
    private float timeBomb;
    public System.Action ExplosiveAction;

    private int numbOfBalst;

    public void Init(int blastExplosive, int timeToExplosive = 4)
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

    void Explosive()
    {
        if (ExplosiveAction != null)
            ExplosiveAction.Invoke();


        for (int i = 0; i <= blastExplosive; i++)
        {
            if (i == 0) {
                var b = Instantiate(blastPrefab);
                b.transform.position = transform.position;
                b.Init();
                numbOfBalst++;
            }
            StartCoroutine(SpwanBlast(i));
        }
    }


    IEnumerator SpwanBlast(int i)
    {
        yield return new WaitForSeconds(blastTime*i);
        foreach (BlastDir e in System.Enum.GetValues(typeof(BlastDir)))
        {
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
        b.Init();
    }
}
