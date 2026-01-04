using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MonsterManager : MonoBehaviour
{
    public Image monsterImage;
    public Sprite[] monsterSprites;
    public MonsterStats stats;
    public PlayerStats player;
    public BallSpawner ballSpawner;
    public event Action OnStatsChanged;

    [Header("Base Stats")]
    public int baseHP = 5;
    public int baseAT = 1;

    [Header("Scaling")]
    public int hpPerLevel = 2;
    public int atPerXLevel = 3;

    [Header("Animator")]
    public Animator monsterAnimator;

    void Start()
    {
        RandomizeMonster(1);
    }

    public void RandomizeMonster(int level)
    {
       stats.HP = baseHP + (level - 1) * hpPerLevel;
       stats.AT = baseAT + (level - 1) / atPerXLevel;


        if (monsterSprites.Length == 0) return;

        monsterImage.sprite = monsterSprites[
            UnityEngine.Random.Range(0, monsterSprites.Length)
        ];

        OnStatsChanged?.Invoke();
    }

    public void AttackPlayer(PlayerStats player)
    {
        if (stats.stunned)
        {
            stats.stunned = false;
            AudioManager.Instance.PlaySFX(SFXType.Stun);
            return;
        }

        if (ballSpawner.RefreshBall)
        {
            ballSpawner.SpawnBatch();
            ballSpawner.RefreshBall = false;
        }
        monsterAnimator.SetTrigger("MonsterAttack");
        StartCoroutine(HoldAnimation(1f));

        player.TakeDamage(stats.AT);
    }

    IEnumerator HoldAnimation(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
