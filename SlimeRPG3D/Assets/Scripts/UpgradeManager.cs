using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    
    public enum UpgradeValue
    {
        Damage = 10,
        AttackSpeed = 1,
        MaxHealth = 20,
        HelthRegen = 1,
    }
    public enum UpgradeCost
    {
        Damage = 10,
        AttackSpeed = 50,
        MaxHealth = 20,
        HelthRegen = 50,
    }
    public UpgradeController damage;
    public UpgradeController attackspeed;
    public UpgradeController maxHealth;
    public UpgradeController helthRegen;
    public TMPro.TextMeshProUGUI helthText;

    private int _currentHP;
    private int _maxHP;

    private int _prevCurrentHP;
    private int _prevMaxHP;

    public int CurrentHP { get { return _currentHP; } set { _currentHP = value; } }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        PlayerController.instance.DamageValue = damage.Value;
        PlayerController.instance.AttackSpeed = attackspeed.Value;
        PlayerController.instance.MaxHealth = maxHealth.Value;
        PlayerController.instance.HealthRegen = helthRegen.Value;
        _maxHP = _prevMaxHP = _currentHP = _prevCurrentHP = PlayerController.instance.MaxHealth;
        helthText.text = _maxHP.ToString() + "/" + _maxHP.ToString();
    }

    private void Update()
    {
        if(_maxHP != _prevMaxHP || _currentHP != _prevCurrentHP)
        {
            helthText.text = _currentHP.ToString() + "/" + _maxHP.ToString();
            _prevCurrentHP = _currentHP;
            _prevMaxHP = _maxHP;
        }
    }
    public void DamageUpgrade()
    {
        PlayerController.instance.DamageValue = damage.Buy((int)UpgradeValue.Damage,(int)UpgradeCost.Damage);        
    }
    public void AttackSpeedUpgrade()
    {
        attackspeed.Buy((int)UpgradeValue.AttackSpeed, (int)UpgradeCost.AttackSpeed);
        PlayerController.instance.AttackSpeed++;
        PlayerController.instance.CountAttackCooldown();
    }
    public void MaxHealthUpgrade()
    {
        int currentMaxHealth = PlayerController.instance.MaxHealth;
        PlayerController.instance.MaxHealth = _maxHP = maxHealth.Buy((int)UpgradeValue.MaxHealth, (int)UpgradeCost.MaxHealth);
        PlayerController.instance.CountCurrentHealth(currentMaxHealth);
        
    }
    public void HealthRegenUpgrade()
    {
        PlayerController.instance.HealthRegen = helthRegen.Buy((int)UpgradeValue.HelthRegen, (int)UpgradeCost.HelthRegen);
    }
}
