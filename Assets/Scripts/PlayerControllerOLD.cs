using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerOLD : MonoBehaviour
{
    private Animator myAnimator;
    private float attackTimer=0;
    private int muzzleIndex;
    [SerializeField] private Transform[] muzzles;

    public float AttackInterval = 2;
    public GameObject BulletPrefab;

    public AudioClip ShotClip;
    private void Awake()
    {
        myAnimator = gameObject.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer = attackTimer >= AttackInterval ? 0 : attackTimer += Time.deltaTime;
        if(attackTimer ==0)
        {
            Attack();
        }
    }

    void Attack()
    {
        Debug.Log("attack");
        GameObject.Instantiate(BulletPrefab,muzzles[GetMuzzleIndex()].position,Quaternion.identity)
        .GetComponent<Rigidbody>()
        .AddForce(Vector3.forward*-150);

        myAnimator.SetInteger("muzzle",muzzleIndex);

        AudioManager.instance.playAudio(ShotClip);
    }

    int GetMuzzleIndex()
    {
        muzzleIndex = muzzleIndex >= muzzles.Length-1 ? 0: ++muzzleIndex;
        return muzzleIndex;
    }
}
