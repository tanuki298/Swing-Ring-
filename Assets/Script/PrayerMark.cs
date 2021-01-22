using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayerMark : MonoBehaviour
{
    Rigidbody2D rb;

    public float L_X_Default;
    public float R_X_Default;
    public float Y_Default;
    public float Speed;

    public AudioClip SE;
    public new AudioSource audio;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        Y_Default = gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float L_Stick_Hori = Input.GetAxis("L_Stick_Hori");
        float R_Stick_Hori = Input.GetAxis("R_Stick_Hori");
        float L_Stick_Vert = Input.GetAxis("L_Stick_Vert");
        float R_Stick_Vert = Input.GetAxis("R_Stick_Vert");

        if (this.gameObject.tag == "Player_L")
        {
            this.transform.position = new Vector3(L_Stick_Hori * Speed + L_X_Default, L_Stick_Vert * Speed + Y_Default, 0);
        }

        if (this.gameObject.tag == "Player_R")
        {
            this.transform.position = new Vector3(R_Stick_Hori * Speed + R_X_Default, R_Stick_Vert * Speed + Y_Default, 0);
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Notes")
        {
            audio.PlayOneShot(SE);
        }
    }
}