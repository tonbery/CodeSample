
using UnityEngine;

public class BattleCheats : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))  DamageAllEnemies();
        if(Input.GetKeyDown(KeyCode.DownArrow))  DamageFirstEnemy();
    }

    private void DamageFirstEnemy()
    {
        var battleManager = FindObjectOfType<BattleManager>();
        battleManager.EnemyCharacters[0].TakeDamage(1);
    }

    void DamageAllEnemies()
    {
        var battleManager = FindObjectOfType<BattleManager>();
        foreach (var enemy in battleManager.EnemyCharacters)
        {
            enemy.TakeDamage(1);
        }
    }
}
