using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_inicialize : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        LoadNewLevel(0);
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void LoadNewLevel(int level_id)
    {
        switch (level_id)
        {
            default:
                myVar.tex_layer_ground[0] = Resources.Load<Sprite>("background/ancient_forest/ground");
                myVar.tex_layer_ground[1] = Resources.Load<Sprite>("background/ancient_forest/ground");
                myVar.tex_layer_ground[2] = Resources.Load<Sprite>("background/ancient_forest/ground");
                myVar.tex_layer_ground[3] = Resources.Load<Sprite>("background/ancient_forest/ground");

                myVar.tex_layer_1[0] = Resources.Load<Sprite>("background/ancient_forest/1_bush_1");
                myVar.tex_layer_1[1] = Resources.Load<Sprite>("background/ancient_forest/1_bush_2");
                myVar.tex_layer_1[2] = Resources.Load<Sprite>("background/ancient_forest/1_bush_3");
                myVar.tex_layer_1[3] = Resources.Load<Sprite>("background/ancient_forest/1_bush_4");

                myVar.tex_layer_2[0] = Resources.Load<Sprite>("background/ancient_forest/2_tree_1");
                myVar.tex_layer_2[1] = Resources.Load<Sprite>("background/ancient_forest/2_tree_2");
                myVar.tex_layer_2[2] = Resources.Load<Sprite>("background/ancient_forest/2_tree_3");
                myVar.tex_layer_2[3] = Resources.Load<Sprite>("background/ancient_forest/2_tree_4");

                myVar.tex_layer_3[0] = Resources.Load<Sprite>("background/ancient_forest/3_tree_1");
                myVar.tex_layer_3[1] = Resources.Load<Sprite>("background/ancient_forest/3_tree_2");
                myVar.tex_layer_3[2] = Resources.Load<Sprite>("background/ancient_forest/3_tree_3");
                myVar.tex_layer_3[3] = Resources.Load<Sprite>("background/ancient_forest/3_tree_4");

                myVar.tex_layer_4[0] = Resources.Load<Sprite>("background/ancient_forest/4_tree_1");
                myVar.tex_layer_4[1] = Resources.Load<Sprite>("background/ancient_forest/4_tree_2");
                myVar.tex_layer_4[2] = Resources.Load<Sprite>("background/ancient_forest/4_tree_3");
                myVar.tex_layer_4[3] = Resources.Load<Sprite>("background/ancient_forest/4_tree_4");
                break;

        }
    }
}
