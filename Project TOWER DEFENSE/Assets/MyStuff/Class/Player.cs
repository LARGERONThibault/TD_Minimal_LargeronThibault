using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int speed;
    public int knockback;
    public int knocklenght;
    public int might;
    public float reloadtime;
    bool canmove = true;
    bool canshoot = true;
    public GameObject bullet;

    //Fonction pour faire se déplacer le joueur sur la droite s'il peut bouger.
    public void Move()
    {
        if (canmove == true)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    //Gere le knockback du joueur en le faisant reculer pendant X frame et en l'empechant d'avancer durant cette durée.
    IEnumerator Knockbacked()
    {
        canmove = false;
        for (int i = 0; i < knocklenght; i++)
        {
            transform.position += Vector3.left * (knockback) * Time.deltaTime;
            yield return null;
        }
        canmove = true;
    }

    //Si le joueur rencontre un ennemi, il est knockbacked.
    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(Knockbacked());
        StartCoroutine(collision.gameObject.GetComponent<Enemy>().Cooldown());
    }


    //Coroutine qui gère le temps de rechargement des projectiles.
    IEnumerator Reload()
    {
        canshoot = false;
        yield return new WaitForSeconds(reloadtime);
        canshoot = true;
    }

    void Update()
    {
        //Déplace le joueur automatiquement.
        Move();

        //TIR COMME UN GROS COWBOY YEEHAW
        if (Input.GetKeyDown(KeyCode.Q) && canshoot)
        {
            float playerx = transform.position.x;
            Vector3 spawnpoint = new Vector3(playerx+1.1f, 0, 0);
            Instantiate(bullet, spawnpoint, Quaternion.identity);
            StartCoroutine(Reload());
        }
    }
}
