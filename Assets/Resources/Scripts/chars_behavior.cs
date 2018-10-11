//Базовое поведение всех объектов врагов и игрока
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DragonBones;
using System.Linq;

public class chars_behavior : MonoBehaviour {

    //параметры персонажа

   





    public int hp = 0;
    public int attack = 0;
    public float attack_delay = 0.0f;

    public float attack_pos_x = 0.6f;
    public float attack_pos_y = 0.0f;
    public float attack_range = 1.0f;

    protected UnityArmatureComponent armatureComp;
    protected GameObject gcontroller;
    protected UnitySlot weapon_slot;

    //Свойства

    protected void Load_DB(string DBD_path, string TAD_path, string armature_name)
    {
        UnityFactory.factory.LoadDragonBonesData(DBD_path);
        UnityFactory.factory.LoadTextureAtlasData(TAD_path);
        armatureComp = UnityFactory.factory.BuildArmatureComponent(armature_name);
        gcontroller = GameObject.Find("global_controller");
        weapon_slot = armatureComp.armature.GetSlot("weapon") as UnitySlot;
    }

    virtual public void Get_Damage(int damage)
    {
        hp -= damage;
    }

    //Атака
    virtual public void melee_attack(int damage)
    {
        int flip_value = 1;
        if (armatureComp.armature.flipX == true)
        {
            flip_value = 1;
        }
        else
        {
            flip_value = -1;
        }
        Collider2D[] enemies_to_damage = Physics2D.OverlapCircleAll((Vector2)gameObject.transform.position + new Vector2(attack_pos_x * flip_value, attack_pos_y), attack_range);
        for (int i = 0; i < enemies_to_damage.Length; i++)
        {
            if (enemies_to_damage[i] != gameObject.GetComponent<Collider2D>() && enemies_to_damage[i].GetComponent<chars_behavior>())//предотвращение от нанесения урона самому себе
            {
                enemies_to_damage[i].GetComponent<chars_behavior>().Get_Damage(damage);
            }
        }
    }

    //изменение оружия
    virtual public void set_weapon(int weapon_index)
    {
        Texture weapon_texture = null;
        GameObject weapon_object = new GameObject();
        MeshRenderer weapon_comp;

        weapon_texture = Resources.Load<Texture>("branch");
        //weapon_comp = weapon_object.AddComponent<SpriteRenderer>();
        //weapon_comp.sprite = weapon_texture;
        weapon_comp = weapon_object.AddComponent<MeshRenderer>();
        weapon_comp.material.SetTexture("weapon", weapon_texture);
        //weapon_slot.display = weapon_texture;
    }



    


}
