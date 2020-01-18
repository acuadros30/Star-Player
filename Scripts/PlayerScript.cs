using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text win;
    private int scoreValue = 0;

    float currentTime = 0f;
    float startTime = 12f;
    public Text Timer;

    public AudioSource musicSource;
    public AudioSource collectSource;
    public AudioClip musicClip1;
    public AudioClip musicClip2;
    public AudioClip collectClip;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        win.text = "";
        currentTime = startTime;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

        currentTime -= 1 * Time.deltaTime;
        Timer.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
        }

        if (currentTime == 0)
        {
            win.text = "You Lose!";
            Destroy(rd2d);
            musicSource.clip = musicClip2;
            musicSource.Play();
            Destroy(win, 2);
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Star")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            collectSource.clip = collectClip;
            collectSource.Play();
        }

        if (scoreValue == 3)
        {
            win.text = "You Win! Game created by Adam Cuadros";
            currentTime = startTime;
            musicSource.clip = musicClip1;
            musicSource.Play();
            Destroy(win, 2);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}
