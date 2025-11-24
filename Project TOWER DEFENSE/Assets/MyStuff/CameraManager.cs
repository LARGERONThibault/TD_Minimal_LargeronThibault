using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public GameObject player;
    public Transform playerposition;
    public int gamedifficulty;
    int speed; 

    public void Gameover()
    {
        Application.Quit();
        Debug.Log("Prout");
    }
    private void Start()
    {
        speed = player.GetComponent<Player>().speed;
    }
    void Update()
    {
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
