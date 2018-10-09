using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_background : MonoBehaviour {

    //myVar global;
    private BoxCollider2D box_colider;
    private float box_width;
    private int box_quantity;
    private float speed = 0.2f;
    private SpriteRenderer sprite_renderer;
    
    public enum Layer
    {
        ground = 0,
        bc1 = 1,
        bc2 = 2,
        bc3 = 3,
        bc4 = 4,
        bc5 = 5
    }
    public Layer Layers;

    void Awake()
    {
        
    }

    void Start () {
        box_colider = GetComponent < BoxCollider2D>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        speed = myVar.speed_layer_ground;
        box_width = box_colider.size.x;
        update_sprite();

    }
	
	void FixedUpdate () {
        switch ((int)Layers)
        {
            case 0:
                speed = myVar.speed_layer_ground;
                box_quantity = 16;
                break;
            case 1:
                speed = myVar.speed_layer_1;
                box_quantity = 5;
                break;
            case 2:
                speed = myVar.speed_layer_2;
                box_quantity = 5;
                break;
            case 3:
                speed = myVar.speed_layer_3;
                box_quantity = 5;
                break;
            case 4:
                speed = myVar.speed_layer_4;
                box_quantity = 5;
                break;
            case 5:
                speed = myVar.speed_layer_5;
                box_quantity = 5;
                break;
        }
        //rb.velocity = new Vector2(speed, 0);
        if (myVar.in_move == true)
        {
            transform.position = (Vector2)transform.position + new Vector2(speed * Time.deltaTime, 0);
        }
        if (transform.position.x < -box_width)
        {
            make_repeat();
            update_sprite();
        } 
	}

    private void make_repeat()
    {
        Vector2 offser = new Vector2(box_width * box_quantity, 0);
        transform.position = (Vector2)transform.position + offser;
    }

    private void update_sprite()
    {
        int rnd;
        switch ((int) Layers)
        {
            case 0:
                rnd = get_sprite_id(myVar.tex_layer_ground.Length - 1);
                sprite_renderer.sprite = myVar.tex_layer_ground[rnd];
                break;
            case 1:
                rnd = get_sprite_id(myVar.tex_layer_1.Length - 1);
                sprite_renderer.sprite = myVar.tex_layer_1[rnd];
                break;
            case 2:
                rnd = get_sprite_id(myVar.tex_layer_2.Length - 1);
                sprite_renderer.sprite = myVar.tex_layer_2[rnd];
                break;
            case 3:
                rnd = get_sprite_id(myVar.tex_layer_3.Length - 1);
                sprite_renderer.sprite = myVar.tex_layer_3[rnd];
                break;
            case 4:
                rnd = get_sprite_id(myVar.tex_layer_4.Length - 1);
                sprite_renderer.sprite = myVar.tex_layer_4[rnd];
                break;
            case 5:
                rnd = get_sprite_id(myVar.tex_layer_5.Length - 1);
                sprite_renderer.sprite = myVar.tex_layer_5[rnd];
                break;
        }
    }

    private int get_sprite_id(int max_int)
    {
        int rnd;
        rnd = Random.Range(0, max_int);
        if (rnd == myVar.previous_sprite_id[(int)Layers])
        {
            if (myVar.previous_sprite_id[(int)Layers] != max_int)
            {
                rnd = myVar.previous_sprite_id[(int)Layers] + 1;
            }
            else
            {
                rnd = 0;
            }
        }
        myVar.previous_sprite_id[(int)Layers] = rnd;
        return rnd;
    }
}
