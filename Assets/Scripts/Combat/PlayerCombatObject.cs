using UnityEngine;

public abstract class PlayerCombatObject : MonoBehaviour
{
    [SerializeField] private int health = 3;
    public bool IsBlocking { get; set; }
    public int Health => health;

    public void TakeDamage(int amount)
    {
        health -= amount;
    }
}