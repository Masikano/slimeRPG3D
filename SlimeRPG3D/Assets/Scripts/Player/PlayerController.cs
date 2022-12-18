using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IAttack, IDamagable
{
    

    [SerializeField]
    private int _damage;
    

    [SerializeField]
    private int _attackSpeed;
    [SerializeField]
    private int _health;
    private int _maxHealth;
    [SerializeField]
    private int _helthRegen;

    [SerializeField]
    private float _baseAttackCooldown;
    [SerializeField]
    private float _attackCooldown;
    [SerializeField]
    private float _attackSpeedCoef;
    private float _baseAttackSpeedCoef;
    private float _baseAttackSpeedCoefDelta;

    //private bool _endCorutine;
    //public bool EndCorutine { get => _endCorutine; set => _endCorutine = value; }


    public static PlayerController instance;
    private void Awake()
    {
        _attackCooldown = BaseAttackCooldown;

        instance = this;
    }




    
    public int DamageValue { get { return _damage; } set { _damage = value; } }
    public int AttackSpeed { get => _attackSpeed; set => _attackSpeed = value; }
    public int MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    public int HealthRegen { get { return _helthRegen; } set { _helthRegen = value; } }
    public float BaseAttackCooldown { get => _baseAttackCooldown; }
    public float AttackCooldown { 
        get => _attackCooldown;
        set 
        {
            _attackCooldown = value;
            if (_attackCooldown < 0.3f) _attackCooldown = .3f;
        }  
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void Start()
    {
        _attackSpeedCoef = 1;
        //_endCorutine = false;
        _baseAttackSpeedCoef = 6;
        _baseAttackSpeedCoefDelta = 1;
        StartCoroutine(HealthRegeneration());
        
    }
   

    void IDamagable.Damage(int damage)
    {
        _health -= damage;
        
        if(_health <= 0)
        {
            _health = 0;
            RestartButton.instance.gameObject.SetActive(true);
            //_endCorutine = true;
            StartCoroutine(WaitToDestroy(1));
            //gameObject.SetActive(false);
            Attacker.instance.gameObject.SetActive(false);
            
        }
        UpgradeManager.instance.CurrentHP = _health;
    }
    public void CountAttackCooldown()
    {
        //_attackSpeedCoef += 1/Mathf.Pow(_baseAttackSpeedCoef, _attackSpeed);
        //_attackCooldown = _baseAttackCooldown/_attackSpeedCoef;
        //if (_baseAttackSpeedCoef > 1) _baseAttackSpeedCoef -= _baseAttackSpeedCoefDelta;
        _attackCooldown = _baseAttackCooldown - _attackSpeed * _attackSpeedCoef;
        if(_attackCooldown <= 0.33f) { _attackCooldown = 0.33f; }
        
    }
    public void CountCurrentHealth(int currentMaxHealth)
    {
        if(_health == currentMaxHealth)
        {
            _health = MaxHealth;
        }
        else
        {
            _health = (int)Mathf.Round((float)_health / currentMaxHealth * MaxHealth);
        }
        UpgradeManager.instance.CurrentHP = _health;
    }
    private IEnumerator HealthRegeneration()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            
            
            if(_health >= _maxHealth) { _health = _maxHealth; }
            else if(_health <= 0) { _health = 0; }
            else _health += _helthRegen;
            UpgradeManager.instance.CurrentHP = _health;
            //if (_endCorutine) 
            //    break;
        }
            
            
    }
    private IEnumerator WaitToDestroy(float time)
    {
        yield return new WaitForSeconds(time);
    }
    public void ResetPlayer()
    {
        _health = MaxHealth;
        Attacker.instance.gameObject.SetActive(true);
        Attacker.instance.Reset();
        UpgradeManager.instance.CurrentHP = _health;
        //gameObject.SetActive(true);
        //_endCorutine = false;
        //StartCoroutine(HealthRegeneration());

    }



}
