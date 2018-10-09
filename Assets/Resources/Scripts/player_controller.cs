/*Основной скрипт для объекта игрока*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;



public class player_controller : chars_behavior {

	// Use this for initialization
	void Start () {
        //загрузка модельки
        Load_DB("db_files/knight_hero_ske", "db_files/knight_hero_tex", "Armature");
        //Load_DB("db_files/simple_skeleton_ske", "db_files/simple_skeleton_tex");
        armatureComp.armature.flipX = true;
        armatureComp.transform.position = new Vector2(4.5f, 0);
        armatureComp.animation.Play("IDLE");
        hp = 100;
    }

	// Update is called once per frame
	void Update () {
        transform.position = armatureComp.transform.position;
		if (myVar.in_move == true && myVar.in_slash == false)
        {
            if (armatureComp.animation.lastAnimationName != "RUN")
            armatureComp.animation.Play("RUN");
        }
        else if (myVar.in_slash == false)
        {
            if (armatureComp.animation.lastAnimationName != "IDLE")
                armatureComp.animation.Play("IDLE");
        }
        if (myVar.in_slash == true && armatureComp.animation.lastAnimationName != "slash_test")
        {
            armatureComp.animation.Play("slash_test", 1);
        }
        if (armatureComp.animation.isCompleted == true && armatureComp.animation.lastAnimationName == "slash_test")
        {
            //GetComponent<melee_attack>().attack(myVar.click_count);
            melee_attack(myVar.click_count);
            armatureComp.animation.Play("IDLE");
            myVar.in_slash = false;
            myVar.click_count = 0;
        }
	}
}
