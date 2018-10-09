using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee_attack : MonoBehaviour {

    public Transform attack_pos;
    public float attack_range;
    public LayerMask enemy_layer;
    
    public void attack(int damage)
    {
        Collider2D[] enemies_to_damage = Physics2D.OverlapCircleAll(attack_pos.position, attack_range, enemy_layer);
        
        for(int i = 0; i < enemies_to_damage.Length; i++)
        {
            if (enemies_to_damage[i] != gameObject.GetComponent<Collider2D>() && enemies_to_damage[i].GetComponent<chars_behavior>())//предотвращение от нанесения урона самому себе
            {
                enemies_to_damage[i].GetComponent<chars_behavior>().Get_Damage(damage);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere((Vector2)gameObject.transform.position + new Vector2(attack_pos_x * flip_value, attack_pos_y), attack_range);
    }
}
