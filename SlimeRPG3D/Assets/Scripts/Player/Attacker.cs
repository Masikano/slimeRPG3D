using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    public static Attacker instance;

    public GameObject projectilePrefab;
    [SerializeField]
    private float _startProjectileVelocity;
    private bool _attackIsRedy;
   
    private bool _targetEnable;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        _attackIsRedy = true;     
        
        

    }

    private void Update()
    {
        if (_targetEnable && _attackIsRedy)
        {
            Shoot();
            _attackIsRedy=false;
            StartCoroutine(StartAttack(PlayerController.instance.AttackCooldown));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.GetComponent<Enemy>())
        {
            
            _targetEnable = true;
            

            gameObject.layer = LayerMask.NameToLayer("Attacker");
            other.transform.gameObject.layer = LayerMask.NameToLayer("Target");
            

            Debug.Log("colEnt");
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        

        if (other.transform.gameObject.GetComponent<Enemy>())
        {
            Debug.Log("colEx");

            
            _targetEnable = false;
            gameObject.layer = 0;
            other.transform.gameObject.layer = 0;
        }
    }
    
    public void Shoot()
    {
        
        GameObject projectile = Instantiate(projectilePrefab, transform.parent.position, Quaternion.identity);
        projectile.transform.SetParent(transform.parent);
        
        
        projectile.GetComponent<Projectile>().SetProjectileVelocity(_startProjectileVelocity);
        
    }
    public IEnumerator StartAttack(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);
        _attackIsRedy = true;


    }
    public void Reset()
    {
        _attackIsRedy = true;
        _targetEnable = false;
        gameObject.layer = 0;
        
    }

}
