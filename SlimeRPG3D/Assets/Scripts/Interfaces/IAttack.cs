

interface IAttack
{
    float BaseAttackCooldown { get; }
    float AttackCooldown { get; set; }
    int DamageValue { get; set; }
    int AttackSpeed { get; set; }  
    //void Attack();
}
