using UnityEngine;
using System;
using System.Collections;
using Unity.VisualScripting;

public class MonsterStats : MonoBehaviour
{
    public int HP = 4;
    public int AT = 1;
    public event Action OnStatsChanged;
    public Animator animator;

    public bool stunned = false;

    public void TakeDamage(int dmg)
    {
        HP -= dmg;
        HP = Mathf.Max(0, HP);
        OnStatsChanged?.Invoke();
        if (HP > 0)
        {
            animator.SetTrigger("MonsterHurt");
            StartCoroutine(HoldAnimation(1f));
        }

        if (HP == 0)
    {
        GameManager.Instance.OnMonsterDefeated();
        animator.SetTrigger("MonsterDead");
        StartCoroutine(HoldAnimation(2f));
    }

    }

    IEnumerator HoldAnimation(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}


