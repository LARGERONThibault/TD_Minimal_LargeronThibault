using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public GameObject player;
    public Transform playerposition;
    public int gamedifficulty;
    int speed; 

    //Gameover provisoire
    public void Gameover()
    {
        Application.Quit();
        Debug.Log("Prout");
    }
    //Suit le joueur.
    private void Start()
    {
        speed = player.GetComponent<Player>().speed;
    }
    void Update()
    {
      //Si le joueur et la caméra sont alignés, elle va à sa vitesse, sinon elle le rejoint plus lentement.
        if (transform.position.x < playerposition.position.x)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
      else
        {
            transform.position += Vector3.right * (speed-gamedifficulty) * Time.deltaTime;
        }

      if (playerposition.position.x - transform.position.x < -10)
        {
            Gameover();
        }
    }
}
