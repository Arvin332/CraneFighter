using System.Collections;
using TMPro;
using UnityEngine;

public enum TurnState
{
    Player,
    Monster,
    Busy
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TurnState currentTurn = TurnState.Player;

    public PlayerStats player;
    public MonsterManager monster;
    public BallSpawner spawner;
    public BallSpawner ballSpawner;
    public UIStatBinder uiStatBinder;

    [Header("UI")]
    public GameObject endScreenCanvas;
    public TextMeshProUGUI endLevelText;

    public int currentLevel = 1;


    void Start()
    {
        ballSpawner.SpawnBatch();
    }

    private void Awake()
    {
        Instance = this;
    }

    public void ResolveBall(Ball ball)
{
    if (currentTurn != TurnState.Player)
        return;

    ApplyBallEffect(ball);
}


    public void EndPlayerTurn()
{
    if (currentTurn != TurnState.Player)
        return;

    currentTurn = TurnState.Busy;
    StartCoroutine(EndPlayerTurnRoutine());
}

IEnumerator EndPlayerTurnRoutine()
{
    yield return new WaitForSeconds(0.3f);
    MonsterTurn();
}


    void MonsterTurn()
    {
        currentTurn = TurnState.Monster;

        monster.AttackPlayer(player);

        player.ResetShield();

        currentTurn = TurnState.Player;
    }

    public void OnMonsterDefeated()
{
    currentLevel++;

    monster.RandomizeMonster(currentLevel);
    uiStatBinder.UpdateLevel(currentLevel);
    AudioManager.Instance.PlaySFX(SFXType.LevelUp);
}

    public void GameOver()
{
    currentTurn = TurnState.Busy;

    Time.timeScale = 0f;

    endScreenCanvas.SetActive(true);
    endLevelText.text = $"Level Tercapai: {currentLevel}";
}



    void ApplyBallEffect(Ball ball)
    {
        switch (ball.type)
    {
        case BallType.Attack:
            monster.stats.TakeDamage(player.AT);
            break;

        case BallType.Defense:
            player.AddShield();
            break;

        case BallType.Heal:
            player.Heal(1);
            break;

        case BallType.AttackBoost:
            player.BoostAT();
            break;

        case BallType.DefenseBoost:
            player.BoostDF();
            break;

        case BallType.Overheal:
            player.Overheal();
            break;

        case BallType.Refresh:
            spawner.RefreshBatch();
            break;

        case BallType.Stun:
            monster.stats.stunned = true;
            break;
    }

}

void Update()
{
    // DEBUG
    if (Input.GetKeyDown(KeyCode.Alpha0))
    {
        player.TakeDamage(999);
    }

    if (Input.GetKeyDown(KeyCode.Alpha9))
    {
        monster.stats.TakeDamage(999);
    }
}

}
