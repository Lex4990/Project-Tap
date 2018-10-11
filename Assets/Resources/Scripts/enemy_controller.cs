/*основной скрипт для работы всех объектов врага*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DragonBones;

public class enemy_controller : chars_behavior {

 ///////////////////////////////////////////////////////////////////////////////////////////////////
   private static readonly string[] WEAPON_RIGHT_LIST = { "weapon_1004_r", "weapon_1004b_r", "weapon_1004c_r", "weapon_1004d_r", "weapon_1004e_r" };
    private UnityArmatureComponent _armatureComp = null;
    private Slot _leftWeaponSlot = null;
    private Slot _rightWeaponSlot = null;
    private int _leftWeaponIndex = -1;
    private int _rightWeaponIndex = -1;
   ///////////////////////////////////////////////////////////////////////////////////////////////////


















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
	void Start () {
        Load_DB("skeletonpack/simple_skeleton_ske", "skeletonpack/simple_skeleton_tex", "skeleton");
        armatureComp.animation.Play("RUN");
        //transform.position = new Vector2(spawn_x, 0f);
        armatureComp.transform.position = transform.position;
        // set_weapon(0);

        // UnityFactory.factory.LoadDragonBonesData("mecha_1004d_show/mecha_1004d_show_ske");
        // UnityFactory.factory.LoadTextureAtlasData("mecha_1004d_show/mecha_1004d_show_tex");

        // Load Right Weapon Data
        UnityFactory.factory.LoadDragonBonesData("weapon_1004_show/weapon_1004_show_ske");
        UnityFactory.factory.LoadTextureAtlasData("weapon_1004_show/weapon_1004_show_tex");

        // Build Mecha Armature
        this._armatureComp = UnityFactory.factory.BuildArmatureComponent("skeleton");
        
        this._armatureComp.CloseCombineMeshs();

        // this._armatureComp.animation.Play("idle");

        // this._armatureComp.transform.localPosition = new Vector3(10.0f, 0.0f, 0.0f);

        // ПРОВЕРКА СПИСКА СЛОТОВ НЕ УДАЛЯТЬ!!!!
        List<Slot> list = new List<Slot>();
        list = this._armatureComp.armature.GetSlots();
        foreach(Slot a in list){
        Debug.LogWarning(a.name);
        }
        /////////////////////////////////////////////////
       
       // this._leftWeaponSlot = this._armatureComp.armature.GetSlot("weapon");
        this._rightWeaponSlot = this._armatureComp.armature.GetSlot("leg_left3");
        Debug.LogWarning(_armatureComp.armature.GetSlot("leg_left3"));
      
        // Set left weapon default value
        this._leftWeaponIndex = 0;
        // Set right weapon default value
        this._rightWeaponIndex = 0;


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

              if (Input.GetMouseButtonDown(0))
        {
            // var leftSide = 0.0f + Screen.width / 2.0f - Screen.width / 6.0f;
            // var rightSide = Screen.width / 2.0f + Screen.width / 6.0f;
            // var isMiddle = Input.mousePosition.x < rightSide && Input.mousePosition.x > leftSide;
            // var isTouchRight = Input.mousePosition.x > rightSide;
            // //
            // if (isMiddle)
            // {
                // this._ReplaceDisplay(0);
            // }
            // else
            // {
                this._ReplaceDisplay(-1);
            // }
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

private void _ReplaceDisplay(int type)
    {
        switch (type)
        {
            case 1:
                {
                    // Switch slot display index
                    this._leftWeaponIndex++;
                    this._leftWeaponIndex %= this._leftWeaponSlot.displayList.Count;
                    this._leftWeaponSlot.displayIndex = this._leftWeaponIndex;
                }
                break;
            case -1:
                {
                    // Replace slot display
                    this._rightWeaponIndex++;
                    this._rightWeaponIndex %= WEAPON_RIGHT_LIST.Length;
                    var weaponDisplayName = WEAPON_RIGHT_LIST[this._rightWeaponIndex];
                    //
                    UnityFactory.factory.ReplaceSlotDisplay("weapon_1004_show", "weapon", "weapon_r", weaponDisplayName, this._rightWeaponSlot);
                }
                break;
        }

    }

}
