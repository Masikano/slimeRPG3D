using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyDetection : MonoBehaviour
{
    private GameObject _target;
    private GameObject _parent;
    private Vector2 _targetPosition;
    private Transform _targetTransform;
    private Projectile _parentProjectile;
    private Rigidbody _parentRb;

    
    //private void Start()
    //{
    //    _rb = GetComponent<Rigidbody>();
    //    _rb.velocity = Vector2.right * _projectileSpeed;
    //}
    private void Start()
    {
        _parent = transform.parent.gameObject;
        _parentProjectile = GetComponentInParent<Projectile>();
        
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.GetComponent<Enemy>())
        //    _parent.GetComponent<Rigidbody>().isKinematic = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            //Debug.Log("projectile find enemy");
            _parentProjectile.SetTargetPosition(other.gameObject.transform.position);
            //_parent.GetComponent<Rigidbody>().isKinematic = true;
            
        }

        //_parentProjectile.SetTargetPosition(_targetTransform.position);
        // transform.Translate(_targetPosition * Time.deltaTime);

        //_parent.transform.position = Vector2.MoveTowards(_parent.transform.position, _targetPosition, Time.deltaTime);
    }
}
