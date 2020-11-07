using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//base interface
public interface IAttacker
{
    /// <summary>
    /// Returns the projectile speed.
    /// </summary>
     float BulletSpeed { get; set; }

    /// <summary>
    /// Returns the Attack Interval.
    /// </summary>
     float AttackInterval { get; set;}

     
     float attackTimer  { get; set; }
     

    /// <summary>
    /// Updates Attack Timer.
    /// Returns true if meets Attack Interval.
    /// <param name="deltaTime">Time.deltatime.</param>
    /// </summary>
     bool UpdateAttackTimer(float deltaTime);

    /// <summary>
    /// Shoots a bullet.
    /// </summary>
    /// <param name="position">Initialization position of the bullet.</param>
    /// <param name="direction">Movement direction of the bullet.</param>
     void Shoot(Vector3 position, float direction);

}

// concrete class
public class Attack : IAttacker
{
    public float BulletSpeed { get; set; }

    public float AttackInterval { get; set; }

    public GameObject BulletPrefab;

    public float attackTimer  { get; set; }


    /// <summary>
    /// Constructor.
    /// </summary>
    public Attack(GameObject bullet, float bulletSpeed, float attackInterval)
    {
        BulletPrefab = bullet;
        BulletSpeed = bulletSpeed;
        AttackInterval = attackInterval;
    }

    public bool UpdateAttackTimer(float deltaTime)
    {
        attackTimer = attackTimer >= AttackInterval ? 0 : attackTimer += deltaTime;

        return attackTimer == 0;
    }

    public void Shoot(Vector3 _position, float _direction)
    {
        GameObject GO = ObjectPooler.SharedInstance.GetPooledObject(0);
        GO.transform.position = _position;
        GO.GetComponent<TrailRenderer>().Clear();
        GO.SetActive(true);
        GO.GetComponent<Rigidbody>()
        .AddForce(Quaternion.Euler(0, _direction, 0)*Vector3.forward * -1 * BulletSpeed);
        
        /*GameObject.Instantiate(BulletPrefab, _position, Quaternion.identity)
        .GetComponent<Rigidbody>()
        .AddForce(Quaternion.Euler(0, _direction, 0)*Vector3.forward * -1 * BulletSpeed);*/
    }
}

//base decorator
public class AttackDecorator:IAttacker
{
    protected IAttacker _attacker;
    
    public float BulletSpeed { get; set; }

    public float AttackInterval { get; set; }

    public float attackTimer  { get; set; }

    public AttackDecorator(IAttacker attacker)
    {
        _attacker = attacker;
    }


    public virtual bool UpdateAttackTimer(float deltaTime)
    {
        return _attacker.UpdateAttackTimer(deltaTime);
    }

    public virtual void Shoot(Vector3 _position, float _direction)
    {
        _attacker.Shoot(_position,_direction);
    }

}

// Conctrete decorators
class TripleShot: AttackDecorator
{
    public TripleShot(IAttacker attacker) : base(attacker) {}



     public override void Shoot(Vector3 _position, float _direction)
    {
        base.Shoot(_position,_direction);
        base.Shoot(_position,_direction+45);
        base.Shoot(_position,_direction-45);
    }
}

class DualShot: AttackDecorator
{

    private float _dualAttackInterval = 0.3f;

    float _dualAttackTimer;
    
    public DualShot(IAttacker attacker) : base(attacker) {_dualAttackTimer = 0f;}
    
    public override bool UpdateAttackTimer(float deltaTime)
    {
        if (_dualAttackTimer == 0f)
            {
                if (base.UpdateAttackTimer(deltaTime))
                {
                    _dualAttackTimer = _dualAttackInterval;
                    return true;
                }
                return false;
            }
            else
            {
                _dualAttackTimer -= deltaTime;
                if (_dualAttackTimer <= 0f)
                {
                    _dualAttackTimer = 0f;
                    return true;
                }
                return false;
            }
    }
}

class QuickShot: AttackDecorator
{
    public QuickShot(IAttacker attacker) : base(attacker) {base._attacker.AttackInterval *= .5f;}
}

class FastBullet: AttackDecorator
{
    public FastBullet(IAttacker attacker) : base(attacker) {base._attacker.BulletSpeed *= 1.5f;}
}



