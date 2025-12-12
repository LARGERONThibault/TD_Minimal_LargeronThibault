using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float speed;
    public Material shieldoff;
    public GameObject background;
    public AudioSource audiosource;

    //Déplace la munition
    void Update()
    {
        transform.Translate(new Vector3 (speed, 0f, 0f) * Time.deltaTime);
    }

    //Autodétruit la munition si elle ne touche rien au bout de 3 secondes au cas où.
    IEnumerator AUTODESTRUCTION()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    private void Start()
    {
        //Initie l'autodestruction.
        StartCoroutine(AUTODESTRUCTION());
        background = GameObject.Find("Background");
        audiosource = GetComponent<AudioSource>();
    }

    //Inflige des dégâts et la supprime quand il y a collision.
    void OnTriggerEnter(Collider other)
    {
        //Vérifie que l'ennemi touché existe et est un ennemi.
        Enemy myEnemy = other.GetComponent<Enemy>();
        Debug.Log(other);
        if (other != null && myEnemy != null)
        {
            //Si l'ennemi n'a pas de bouclier, lui inflige des dégâts et joue le son de feedback.
            if (other.GetComponent<Enemy>() == true && other.GetComponent<Enemy>().shield == false)
            {
                other.gameObject.GetComponent<Enemy>().hp -= damage;
                audiosource.Play();
            }
            //Si l'ennemi a son bouclier, lui retire et joue le son de feedback.
            else if (other.GetComponent<Enemy>() == true && other.GetComponent<Enemy>().shield == true)
            {
                other.gameObject.GetComponent<Enemy>().shield = false;
                other.gameObject.GetComponent<MeshRenderer>().material = shieldoff;
                audiosource.Play();
            }
            //Freeze pendant 0.2 secondes.
            StartCoroutine(Freeze(0.2f));
            //Applique du knockback sur l'ennemi.
            StartCoroutine(other.gameObject.GetComponent<Enemy>().EnemyKnockbacked());
            //Détruit l'objet.
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }

    //Quand la balle touche, elle désactive l'image de fond (sachant que la caméra affiche du blanc pur, donc flash de lumière) et freeze le jeu pour une 
    
    IEnumerator Freeze(float time)
    {
        background.GetComponent<MeshRenderer>().enabled = false;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1f;
        background.GetComponent<MeshRenderer>().enabled = true;
        Destroy(gameObject);
    }
}
