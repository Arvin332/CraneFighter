using UnityEngine;
using TMPro;

public class UIStatBinder : MonoBehaviour
{
    public PlayerStats player;
    public MonsterStats monster;

    [Header("Player UI")]
    public TextMeshProUGUI playerHP;
    public TextMeshProUGUI playerAT;
    public TextMeshProUGUI playerDF;
    public TextMeshProUGUI playerShield;
    public TextMeshProUGUI levelText;

    [Header("Monster UI")]
    public TextMeshProUGUI monsterHP;
    public TextMeshProUGUI monsterAT;

    void Start()
    {
        player.OnStatsChanged += UpdateUI;
        monster.OnStatsChanged += UpdateUI;
        UpdateUI();
    }

    void UpdateUI()
    {
        playerHP.text = $"HP: {player.HP}";
        playerAT.text = $"AT: {player.AT}";
        playerDF.text = $"DF: {player.DF}";
        playerShield.text = $"Shield: {player.Shield}";

        monsterHP.text = $"HP: {monster.HP}";
        monsterAT.text = $"AT: {monster.AT}";
    }

    public void UpdateLevel(int level)
{
    levelText.text = $"Level: {level}";
    UpdateUI();
}
}

