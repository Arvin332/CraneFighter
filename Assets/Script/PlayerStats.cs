using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    public int HP = 10;
    public int MaxHP = 10;
    public int AT = 1;
    public int DF = 1;
    public int Shield = 0;
    public event Action OnStatsChanged;

    public void TakeDamage(int dmg)
    {
        int blocked = Mathf.Min(Shield, dmg);
        Shield -= blocked;
        dmg -= blocked;

        HP -= dmg;
        HP = Mathf.Max(0, HP);
        OnStatsChanged?.Invoke();
        AudioManager.Instance.PlaySFX(SFXType.Hit);

        if (HP == 0)
    {
        GameManager.Instance.GameOver();
        AudioManager.Instance.PlaySFX(SFXType.GameOver);
    }
    }

    public void Heal(int value)
    {
        HP = Mathf.Min(MaxHP, HP + value);
        OnStatsChanged?.Invoke();
    }

    public void AddShield()
    {
        Shield += DF;
        OnStatsChanged?.Invoke();
    }

    public void ResetShield()
    {
        Shield = 0;
        OnStatsChanged?.Invoke();
    }

    public void BoostAT() 
    {
        AT++;
        OnStatsChanged?.Invoke();
    }
    public void BoostDF()
    {
        DF++;
        OnStatsChanged?.Invoke();
    }

    public void Overheal()
    {
        MaxHP++;
        HP = MaxHP;
        OnStatsChanged?.Invoke();
    }
}

