public interface IDamagable
{
    public void TakeDamage(float damage);

    public void Die();
}
public interface IEnemyBehaviour
{

}

public interface IMove
{
    public void Movement(float speed);
}

public interface IDroppable
{
    public void Drop(float chance);
}
