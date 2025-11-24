using UnityEngine;
using System.Collections;
using UnityEngine.Timeline;

public class Sword : Player
{
    public int swordcooldown;
    bool cansword = true;
    public Transform playerposition;
    public MeshRenderer feedback;
    
    //Gere les fonctions liées à l'attaque
    //Gere le cooldown de l'attaque
    IEnumerator SwordCooldown()
    {
        cansword = false;
        yield return new WaitForSeconds(swordcooldown);
        cansword = true;
    }

    //Gere le feedback visuel
    IEnumerator Feedback()
    {
        feedback.enabled = true;
        yield return new WaitForSeconds(0.5f);
        feedback.enabled = false;
    }

    //Attaque ce qui rentre dans la hitbox
    private void OnTriggerEnter(Collider other)
    {
        //Si on peut attaquer, sinon ne fait rien
        if (cansword)
        {
            //Regarde que c'est bien un ennemi qu'il a détecté.
            if (other.GetComponent<Enemy>() == true)
            {
                StartCoroutine(Feedback());
                //Regarde si l'ennemi a un bouclier et s'adapte en conséquences
                if (other.GetComponent<Enemy>().shield == false)
                {
                    
                    other.GetComponent<Enemy>().hp -= might;
                    StartCoroutine(other.GetComponent<Enemy>().EnemyKnockbacked());
                }
                StartCoroutine(SwordCooldown());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerposition.position + new Vector3(1.001f,0,0);
    }
}
