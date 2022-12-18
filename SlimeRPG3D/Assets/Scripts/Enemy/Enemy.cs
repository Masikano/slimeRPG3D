using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable, IAttack
{
    [SerializeField]
    private int _maxHealth;
    private int _health;
    [SerializeField]
    private int _damage;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private int _bounty;
    [SerializeField]
    private int _attackSpeed;
    [SerializeField]
    private float _baseAttackCooldown;
    private float _attackCooldown;
    private bool _exitCollision;
    public bool ExitCollition { get { return _exitCollision; } set { _exitCollision = value; } }
    
    public int MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    public int DamageValue { get { return _damage; } set { _damage = value; } }

    public Vector3 StartPosition { set { _startPosition = value; } }
    public int Bounty { get { return _bounty; } set { _bounty = value; } }

    

    public float BaseAttackCooldown { get => _baseAttackCooldown;}
    public float AttackCooldown { 
        get => _attackCooldown; 
        set
        {
            _attackCooldown = value;
            if (_attackCooldown < 0.7f) _attackCooldown = .7f;
        }
    }
    public int AttackSpeed
    {
        get => _attackSpeed;
        set
        {
            _attackSpeed = value;
            if (_attackSpeed < 10) _attackSpeed = 10;
        }
    }

    public void SetTargetPosition(Vector3 target) { _targetPosition = target; }
    private void Start()
    {
        //_playerPosition = PlayerController.instance.GetPosition();
        _attackCooldown = BaseAttackCooldown;
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }
    void IDamagable.Damage(int damage)
    {
        Debug.Log("damage");
        _health -= damage;
        if(_health <= 0)
        {
            //GetComponent<Collider>().enabled = false;
            transform.position = _startPosition;
            Wallet.instance.AddCoins(_bounty);
            LevelController.instance.AddFrag();
            StartCoroutine(TimeToDisable(.5f));
            //Destroy(gameObject);
        }
    }
    private IEnumerator TimeToDisable(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    public void Reset()
    {
        transform.position = _startPosition;
        _health = _maxHealth;
        gameObject.SetActive(true);
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() is IDamagable player) 
        {
            StartCoroutine(AttackReload(_attackCooldown, player));
            _exitCollision = false;
        }
    }
    private IEnumerator AttackReload(float time, IDamagable player)
    {
        while (true)
        {
            Attack(player);
            yield return new WaitForSeconds(time);
            if (_exitCollision) break;
        }
        

    }
    private void Attack(IDamagable player)
    {
        player.Damage(_damage);
    }
}
