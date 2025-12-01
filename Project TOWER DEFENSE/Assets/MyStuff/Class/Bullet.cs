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

    //Autodétruit la munition si elle ne touche rien au cas où.
    IEnumerator AUTODESTRUCTION()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(AUTODESTRUCTION());
        background = GameObject.Find("Background");
        audiosource = GetComponent<AudioSource>();
    }

    //Inflige des dégâts et la supprime quand il y a collision.
    void OnTriggerEnter(Collider other)
    {
        Enemy myEnemy = other.GetComponent<Enemy>();
        Debug.Log(other);
        if (other != null && myEnemy != null)
        {
            if (other.GetComponent<Enemy>() == true && other.GetComponent<Enemy>().shield == false)
            {
                other.gameObject.GetComponent<Enemy>().hp -= damage;
                audiosource.Play();
            }
            else if (other.GetComponent<Enemy>() == true && other.GetComponent<Enemy>().shield == true)
            {
                other.gameObject.GetComponent<Enemy>().shield = false;
                other.gameObject.GetComponent<MeshRenderer>().material = shieldoff;
                audiosource.Play();
            }
            StartCoroutine(Freeze(0.2f));
            StartCoroutine(other.gameObject.GetComponent<Enemy>().EnemyKnockbacked());
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }

    //Effet feedback freeze
    
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
