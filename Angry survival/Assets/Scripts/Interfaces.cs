public interface IDamagable
{
    public void TakeDamage(float damage);

    public void Die();
}
public interface IEnemyBehaviour
{
    public void Initialize(EnemySO enemyData);
    public void Movement();
}

public interface IWeaponBehaviour
{
    public void Initialize(WeaponSO weaponData, BaseWeapon baseWeapon);
    public void Attack();

    public void UpgradeWeapon();
}

public interface IMovable
{
    public void Movable(bool value);
}
