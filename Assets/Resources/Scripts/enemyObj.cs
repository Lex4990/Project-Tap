using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyObj{

    private int attack;
    private float attack_delay;
    private int hp;
    private int spawn_lvl;
    private string obj_name;
    public GameObject my_r_obj;

    public int GetSpawnLvl
    {
        get { return spawn_lvl; }
    }

    public enemyObj(int spawn_lvl, int attack, float attack_delay, int hp, string obj_name)
    {
        this.spawn_lvl = spawn_lvl;
        this.attack = attack;
        this.attack_delay = attack_delay;
        this.hp = hp;
        this.obj_name = obj_name;
    }

    public void Spawn(Vector2 position)
    {
        my_r_obj = (GameObject)Resources.Load("Prefs/Emenes/" + obj_name);
        my_r_obj = Object.Instantiate(my_r_obj);
        my_r_obj.transform.position = position;
        my_r_obj.GetComponent<enemy_controller>().parent_object = this;
        my_r_obj.GetComponent<enemy_controller>().attack = attack;
        my_r_obj.GetComponent<enemy_controller>().hp = hp;
        my_r_obj.GetComponent<enemy_controller>().attack_delay = attack_delay;
    }
}
