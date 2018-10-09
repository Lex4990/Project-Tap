using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class text_script : MonoBehaviour {
    public float timer = 0;
    public float timer_max = 1;
    Text me;
	// Use this for initialization
	void Start () {
        me = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        Color new_alpha = me.color;
        new_alpha.a = 1 - (timer / timer_max);
        me.color = new_alpha;
        timer += Time.deltaTime;
        if (timer >= timer_max)
        {
            Destroy(gameObject);
        }
	}
}
