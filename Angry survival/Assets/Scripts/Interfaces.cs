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

public interface IMovable
{
    public void Movable(bool value);
}
public interface IDroppable
{
    public void Drop(float chance);
}
