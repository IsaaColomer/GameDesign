using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private EnemyScript other;    
    public GameManager gameManager;
    [SerializeField] public float life;
    [SerializeField] public float attack;
    [SerializeField] public float defense;
    [SerializeField] public float defenseUp;
    [SerializeField] public float attackUp;
    public float level = 15f;
    private float xpReward;

    public Text playerWinsText;
    public Text playerLifeText;
    public Text playerLostText;
    public Text playerPercentage;
    public Text playerAction;
    private int lost = 0;
    private int wins = 0;
    void Start()
    {
        life = 100f;
        attack = 40f;
        defense = 15;
        level = 1;
        defenseUp = defense*1.5f;
        attackUp = attack*1.6f;
        other = GameObject.Find("Enemy").GetComponent<EnemyScript>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        if(gameManager.doAction)
        {
            if(gameManager.playerAction == 0)
            {
                Attack();
                playerAction.text = "Action: Attack";
            }
            if(gameManager.playerAction == 1)
            {
                //Defend();
                playerAction.text = "Action: Defend";
            }
            if(gameManager.playerAction == 2)
            {
                SuperAttack();
                playerAction.text = "Action: SupperAttack";
            }
            CheckLosts();
            CheckWins();
            Debug.Log("Level: " + level);
        }
        playerWinsText.text = "Player wins: " + wins;
        playerLifeText.text = "Player life: " + life;
        playerLostText.text = "Player lost: " + lost;
        if(gameManager.timesDied == 0)
        {
            playerPercentage.text = ("Percentage: 0%");
        }
        else
        {
            playerPercentage.text = "Percentage: " + ((float)((float)wins/(float)gameManager.timesDied)*100).ToString("F1");
        }
    }
    private void Attack()
    {
        ApplyDamage(attack);
    }
    private void Defend()
    {
        defense = defenseUp;
        ResetDefense();
    }
    void ResetDefense()
    {
        defense = defenseUp/1.5f;
    }
    public void CheckLosts()
    {
        if(life <= 0)
        {
            gameManager.timesDied++;
            lost++;
            RegenerateHealth();
        }
    }
    public void CheckWins()
    {
        if(other.life <= 0)
        {
            wins++;
            xpReward = CalculateReward();
            level += xpReward;
            RegenerateHealth();
        }
    }
    public void RegenerateHealth()
    {
        life = 100;
    }
    private void ApplyDamage(float attack)
    {
        if((attack*level)>other.defense)
            other.life -= ((attack*(level))-other.defense);
        else
        {
            other.life -= attack-other.defense;
        }
    }
    public void SuperAttack()
    {
        ApplyDamage(attackUp);
    }

    float CalculateReward()
    {
        if(other.level<level)
        {
            return level+0.25f;
        }
        else
        {
            return (other.level-level)*0.75f;
        }
    }
}
