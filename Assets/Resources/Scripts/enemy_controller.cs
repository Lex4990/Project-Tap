/*основной скрипт для работы всех объектов врага*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DragonBones;

public class enemy_controller : chars_behavior {
    //Характеристики персонажа
    //public float attack_delay = 2;//время между ударами
    //public int hp = 10;//здоровье
    //public int attack = 3;//атака

    public string           action = "walking";//walking, waiting, hit
    public float            spawn_x = 16.0f;
    
    
    public float            waiting_timer = 0f;
    public float            speed = 0.025f;
    public enemyObj         parent_object;
    public int              MyOrder;
    
    private Text            damage_text_obj;
    private GameObject      death_particles;
    
	// Use this for initialization
	void                   Start () {
        Load_DB("db_files/simple_skeleton_ske", "db_files/simple_skeleton_tex", "skeleton");
        armatureComp.animation.Play("RUN");
        //transform.position = new Vector2(spawn_x, 0f);
        armatureComp.transform.position = transform.position;
        set_weapon(0);
    }

    // Update is called once per frame
    private void            FixedUpdate()
    {
        waiting_timer += (1 * Time.deltaTime);
    }

    void                   Update ()
    {
        armatureComp.transform.position = transform.position;
        GetMyOrder();
        switch (action)//выполенение последовательности действий
        {
            case "walking":
                if (armatureComp.animation.lastAnimationName != "RUN")
                    armatureComp.animation.Play("RUN");
                transform.position = (Vector2)transform.position + new Vector2(-speed, 0f);
                if (transform.position.x <= 6 + (MyOrder * 2))
                {
                    action = "waiting";
                    transform.position = new Vector2(6 + (MyOrder * 2), transform.position.y);
                    waiting_timer = 0;
                }
                break;
            case "waiting":
                if (armatureComp.animation.lastAnimationName != "IDLE")
                    armatureComp.animation.Play("IDLE");
                if (waiting_timer >= attack_delay && MyOrder == 0)
                    action = "hit";
                break;
            case "hit":
                if (armatureComp.animation.lastAnimationName != "slash_test")
                { 
                    armatureComp.animation.Play("slash_test", 1);
                }
                if (armatureComp.animation.isCompleted == true && armatureComp.animation.lastAnimationName == "slash_test")
                {
                    melee_attack(attack);
                    action = "waiting";
                    waiting_timer = 0;
                }
                break;
        }
        //уничтожение объекта при обнулении здоровья
        if (hp <= 0 && armatureComp.animation.lastAnimationName != "DEATH")
        {
            action = "death";
            armatureComp.animation.Play("DEATH", 1);
        }
        if (armatureComp.animation.isCompleted == true && armatureComp.animation.lastAnimationName == "DEATH")
        {
            death_particles = (GameObject)Resources.Load("Objects/fx/pts_monster_death");
            death_particles = Object.Instantiate(death_particles);
            death_particles.transform.position = this.transform.position;
            Destroy(armatureComp.gameObject);
            gcontroller.GetComponent<EnemyManager>().RemoveFromOrder(MyOrder);
            Destroy(gameObject);
        }
    }
    
    override public void    Get_Damage(int damage)
    {
        base.Get_Damage(damage);
        damage_text_obj = gcontroller.GetComponent<EnemyManager>().damage_text_obj;
        Text damage_count = Instantiate(damage_text_obj);
        Rigidbody2D rg_damage_count = damage_count.GetComponent<Rigidbody2D>();
        BoxCollider2D bc_me = gameObject.GetComponent<BoxCollider2D>();
        damage_count.text = damage.ToString();
        damage_count.transform.SetParent(GameObject.Find("Canvas").transform, false);
        damage_count.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + (bc_me.size.y / 2));
        rg_damage_count.AddForce(new Vector2(Random.Range(-100, 100), 600));
    }

    private void GetMyOrder()
    {
        List<enemyObj> EnemyOrderRef = gcontroller.GetComponent<EnemyManager>().EnemyOrder;
        for(int i = 0; i < EnemyOrderRef.Count; i++)
        {
            if(Object.Equals(parent_object, EnemyOrderRef[i]))
            {
                if(MyOrder != i)
                {
                    action = "walking";
                    MyOrder = i;
                }
            }
        }
    }

    private void OnDrawGizmos()//рисует область атаки
    {
        int flip_value = 1;
        if (armatureComp.armature.flipX == true)
            flip_value = 1;
        else
            flip_value = -1;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)gameObject.transform.position + new Vector2(attack_pos_x * flip_value, attack_pos_y), attack_range);
    }
}
