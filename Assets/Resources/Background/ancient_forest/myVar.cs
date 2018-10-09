using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using UnityEngine.UI;



public class myVar : MonoBehaviour {
    

    //глобальные переменные, статы
    public static int zone_lvl = 1;
    public static int player_lvl = 1;


    //
    public static bool in_move = false;
    public static bool in_slash = false;
    public static int click_count = 0;
    public Text click_text;
    private GameObject main_hero_obj;
    //Управление фоном
    //Скорость слоёв фона
    public static float speed_layer_ground = -3f;
    public static float speed_layer_1 = -2.6f;  
    public static float speed_layer_2 = -2.1f;
    public static float speed_layer_3 = -1.6f;
    public static float speed_layer_4 = -1.1f;
    public static float speed_layer_5 = -0.7f;

    //Текстуры для элементов фона
    public static Sprite[] tex_layer_ground = new Sprite[4];
    public static Sprite[] tex_layer_1 = new Sprite[4];
    public static Sprite[] tex_layer_2 = new Sprite[4];
    public static Sprite[] tex_layer_3 = new Sprite[4];
    public static Sprite[] tex_layer_4 = new Sprite[4];
    public static Sprite[] tex_layer_5 = new Sprite[4];

    //идентификаторы выбранных спрайтов в предыдущем шаге
    public static int[] previous_sprite_id = new int[6];

    //рабочии переменные
    //дебаг
    
    // Use this for initialization
    void Start ()
    {
        main_hero_obj = GameObject.Find("main_hero");
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        click_text.text = click_count.ToString();
    }
    //переключение идти/ждать
    public void tougle_run()
    {
        if (in_move == false)
        {
            
            in_move = true;
        }
        else
        {
            in_move = false;
        }
    }
    //удар
    public void make_slash()
    {
        if (myVar.in_move == false)
        {
            in_slash = true;
            click_count += 1;
        }
    }

}
