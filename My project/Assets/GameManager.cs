using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public PlayerScript player;
    public GameObject playerObject;
    public GameObject enemyObject;
    public EnemyScript enemy;
    [SerializeField] public int playerAction = -1;
    [SerializeField] public int enemyAction = -1;
    public float timeBtwnActions = 1f;
    public bool doAction;
    public int totalActions;
    public Text TotalActionTexts;
    public Text TimesDiedText;
    public int timesDied = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        enemyObject = GameObject.Find("Enemy");
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        enemy = GameObject.Find("Enemy").GetComponent<EnemyScript>();
        doAction = false;
    }

    // Update is called once per frame
    void Update()
    {
        TotalActionTexts.text = "Total actions: " + totalActions;
        TimesDiedText.text = "Times died: " + timesDied;
        if(timeBtwnActions >= 0)
        {
            timeBtwnActions -= Time.deltaTime;
            doAction = false;
        }
        else
        {
//          ------- Start -------
            totalActions++;
            doAction = true;
            playerAction = Random.Range(0,2);
            enemyAction = Random.Range(0,2);

            timeBtwnActions = 1f;
        }

    }
}
