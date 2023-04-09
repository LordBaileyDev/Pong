using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    GameManager manager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        manager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal_Red"))
        {
            manager.AddPoint(1);
        }
        else if (collision.CompareTag("Goal_Blue"))
        {
            manager.AddPoint(0);
        }

        manager.ballAmount--;

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            collision.collider.GetComponent<Animator>().Play("WallHit");
        }
        else if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Animator>().Play("PadelHit");
        }
    }
}