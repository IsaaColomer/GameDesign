using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{   
    private PlayerScript other;
    public GameManager gameManager;
    [SerializeField] public float life;
    [SerializeField] public float attack;
    [SerializeField] public float defense;
    [SerializeField] public float defenseUp;
    [SerializeField] public float attackUp;
    public float level = 15f;
    private float xpReward = 0;

    public Text enemyWinsText;
    public Text enemyLifeText;
    public Text enemyLostText;
    public Text enemyPercentage;
    public Text enemyAction;
    private int lost = 0;
    private int wins = 0;

    void Start()
    {
        life = 100f;
        attack = 40f;
        defense = 15;
        level = 1;
        defenseUp = defense*1.5f;
        attackUp = attack*1.5f;
        other = GameObject.Find("Player").GetComponent<PlayerScript>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        if(gameManager.doAction)
        {
            if(gameManager.enemyAction == 0)
            {
                Attack();
                enemyAction.text = "Action: Attack";
            }
            if(gameManager.enemyAction == 1)
            {
                //Defend();
                enemyAction.text = "Action: Defend";
            }
            if(gameManager.enemyAction == 2)
            {
                SuperAttack();
                enemyAction.text = "Action: SupperAttack";
            }
            CheckLosts();
            CheckWins();
        }

        enemyWinsText.text = "Enemy wins: " + wins;
        enemyLifeText.text = "Enemy life: " + life;
        enemyLostText.text = "Enemy lost: " + lost;
        if(gameManager.timesDied == 0)
        {
            enemyPercentage.text = ("Percentage: 0%");
        }
        else
        {
            enemyPercentage.text = "Percentage: " + ((float)((float)wins/(float)gameManager.timesDied)*100).ToString("F1");
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
            CalculateReward();
            RegenerateHealth();
        }
    }
    public void RegenerateHealth()
    {
        life = 100;
    }
    private void ApplyDamage(float attack)
    {
        other.life -= (attack-other.defense);
    }
    private void SuperAttack()
    {
        ApplyDamage(attackUp);
    }
    float CalculateReward()
    {
        if(other.level-level<0)
        {
            return level/(other.level*0.5f);
        }
        else
        {
            return (other.level-level)*0.75f;
        }
    }
}
