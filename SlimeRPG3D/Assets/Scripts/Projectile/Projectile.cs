using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 _targetPosition;
    [SerializeField]
    private float _projectileVelocity;
    private Rigidbody _rb;
    private Transform _targetObjectTransform;
    [SerializeField]
    private float _timeToDestroy;

    public void SetTargetObject(Transform targetTransform) { _targetObjectTransform = targetTransform; }

    public void SetProjectileVelocity(float velocity) { _projectileVelocity = velocity; } 

    
    public void SetTargetPosition(Vector3 targetPos) {  _targetPosition = targetPos; }

    private void Start()
    {
        //GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    private void FixedUpdate()
    {
        if (_targetPosition != Vector3.zero)            
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _projectileVelocity * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider collision)
    {

        if(collision.gameObject.GetComponent<Enemy>() is IDamagable target)
        {
            
            if (transform.GetComponentInParent<PlayerController>() is IAttack attack)
            {
                Debug.Log("projDamage");
                target.Damage(attack.DamageValue);
            }

            Destroy(gameObject);

        }
        else
        {
            StartCoroutine(TimeToDestroy(_timeToDestroy));
        }


    }
    private IEnumerator TimeToDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
