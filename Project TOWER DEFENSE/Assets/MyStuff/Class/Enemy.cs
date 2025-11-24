using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    public int hp;
    public float speed;
    public int knockback;
    public int knocklenght;
    public bool shield;
    bool canmove = true;
    public int attackcooldown;
    
    //Système de mouvement des ennemis.
    public void Move()
    {
        if (canmove)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
    //Gère le knockback des ennemis.
    public IEnumerator EnemyKnockbacked()
    {
        for (int i = 0; i < knocklenght; i++)
        {
            canmove = false;
            transform.position += Vector3.right * (knockback) * Time.deltaTime;
            yield return null;
            canmove = true;
        }
    }

    //Cooldown des attaques ennemies.
    public IEnumerator Cooldown()
    {
        canmove = false;
        yield return new WaitForSeconds(attackcooldown);
        canmove = true;
    }

    void Update()
    {
        Move();
        if (hp == 0)
        {
            DestroyImmediate(gameObject);
        }
    }
}
