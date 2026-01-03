using UnityEngine;
using System;

public class MonsterStats : MonoBehaviour
{
    public int HP = 4;
    public int AT = 1;
    public event Action OnStatsChanged;

    public bool stunned = false;

    public void TakeDamage(int dmg)
    {
        HP -= dmg;
        HP = Mathf.Max(0, HP);
        OnStatsChanged?.Invoke();

        if (HP == 0)
    {
        GameManager.Instance.OnMonsterDefeated();
    }

    }
}


