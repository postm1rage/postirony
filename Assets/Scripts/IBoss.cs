using UnityEngine;

public interface IBoss
{
    int MaxHealth { get; }
    int CurrentHealth { get; }
    void TakeDamage(int damage);
}


