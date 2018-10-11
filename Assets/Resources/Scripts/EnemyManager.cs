/*Скрипт управления генерацией врагов*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {

    public int              wave_size; //Колличество врагов в очереди
    public Text             damage_text_obj;
    public List<enemyObj>   EnemyOrder = new List<enemyObj>();
    private List<int>       EnemyKilled = new List<int>();

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        SpawnEnemy();
    }

    //Случайная генерация уровня врага на основе уровня игрока и зоны
    void GenerateEnemy(int zone_lvl, int char_lvl)
    {
        int cur_enemy_lvl = ((zone_lvl + char_lvl) / 2);//Высчитывания текущего уровня врагов
        int enemy_lvl = 0;//Уровень врага для спауна
        float random_value = Random.value;
        if (random_value >= 0.00f && random_value > 0.55f) { enemy_lvl = cur_enemy_lvl; }//55% спауна врагов текущего уровня
        if (random_value >= 0.55f && random_value > 0.85f) { enemy_lvl = (cur_enemy_lvl - 1); }//30% спауна врагов предыдущего уровня
        if (random_value >= 0.85f && random_value > 0.95f) { enemy_lvl = (cur_enemy_lvl + 1); }//10% спауна врагов следующего уровня
        if (random_value >= 0.95f && random_value > 1.00f) { enemy_lvl = Random.Range(1, cur_enemy_lvl - 1); }//5% спауна врагов любого предыдущего уровня
    }

    //Возвращает случайный объект врага на основе указанного уровня
    public enemyObj GetRandomEnemyByLevel(int level)
    {
        List<enemyObj> EnemyList = GetEnemyList();
        List<enemyObj> EnemyListByLvl = new List<enemyObj>();
        int enemyIndex;

        foreach (var enemy in EnemyList)
        {
            if (enemy.GetSpawnLvl == level)
            {
                EnemyListByLvl.Add(enemy);
            }
        }
        if (EnemyListByLvl.Count != 0)
        {
            enemyIndex = Random.Range(0, EnemyListByLvl.Count);
            return EnemyListByLvl[enemyIndex];
        }
        else
        {
            return null;//не было найдено врагов указанного уровня
        }
    }

    //Возвращает список всех возможных врагов в игре
    public List<enemyObj> GetEnemyList()
    {
        List<enemyObj> EnemyList = new List<enemyObj>();
        EnemyList.Add(new enemyObj(1, 1, 2, 10, "pre_Enemy"));
        EnemyList.Add(new enemyObj(1, 1, 1, 20, "pre_Enemy"));
        EnemyList.Add(new enemyObj(2, 2, 2, 10, "pre_Enemy"));
        EnemyList.Add(new enemyObj(2, 2, 1, 20, "pre_Enemy"));
        return EnemyList;
    }

    private void SpawnEnemy()
    {
        if (EnemyOrder.Count < 5 && wave_size > 0)
        {
            for (int i = 0; i < 10; i++)
            {
                Collider2D check = Physics2D.OverlapCircle(new Vector2(16f + (2f * i), 0f), 1, LayerMask.GetMask("Enemy"));
                if (check == null)
                {
                    EnemyOrder.Add(GetRandomEnemyByLevel(1));
                    EnemyOrder[EnemyOrder.Count - 1].Spawn(new Vector2(16f + (2f * i), 0f));
                    wave_size -= 1;
                    break;
                }
            }
        }
    }

    public void ClearOrder()
    {
        for(int i = 0; i < EnemyOrder.Count; i++)
        {
            if (EnemyOrder[i].my_r_obj == null) EnemyOrder.RemoveAt(i);
        }
    }

    public void RemoveFromOrder(int position)
    {
        EnemyOrder.RemoveAt(position);
    }
}
