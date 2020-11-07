using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int muzzleIndex =0;
    Animator myAnimator;
    bool isThisAClone =false;

    public GameObject bulletPrefab;
    public Transform[] Muzzles;
    public AudioClip ShotClip;
    
    public IAttacker attacker;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        attacker = new Attack(bulletPrefab,150,2);
 
    }

    void Update()
    {
        if(attacker.UpdateAttackTimer(Time.deltaTime))
        {
            attacker.Shoot(Muzzles[GetMuzzleIndex()].position,0);
            OnShoot();
        }
    }

    int GetMuzzleIndex()
    {
        muzzleIndex = muzzleIndex >= Muzzles.Length-1 ? 0: ++muzzleIndex;
        return muzzleIndex;
    }

    void OnShoot()
    {
        
        myAnimator.SetInteger("muzzle",muzzleIndex);

        AudioManager.instance.playAudio(ShotClip);
    }
}
