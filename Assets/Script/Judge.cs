using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Judge : MonoBehaviour
{
    public TimeMane time;
    public Note Getnote;
    public GameObject Move;

    private int ms;　//ミリ秒
    private float ms2;
    private int s = 0;  //要素数の指定
    public float MoveSpeed; //動く速度
    public float en_size; //円のサイズ

    //判定の間隔の指定
    public int Parfect;
    public int Great;
    public int Good;
    public int miss;

    //ノーツの発生時間と判定の間隔を足したもの
    private int Parfect2;
    private int Great2;
    private int Good2;
    private int miss2;

    private int Parfect3;
    private int Great3;
    private int Good3;
    private int miss3;


    // Start is called before the first frame update

    private void Awake()
    {
        //Array.Resize(ref note, Getnote.mu.No.Length);
        transform.parent = GameObject.Find("NotesMane").transform;
        //gameObject.SetActive(false);
        Getnote = GetComponentInParent<Note>();

        Move = transform.GetChild(1).gameObject;

        Move.gameObject.transform.localScale *= en_size;

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        ms2 += Time.fixedDeltaTime;
        ms = (int)(ms2 * 1000);
        s = Getnote.ko;
        //Debug.Log(ms);

        Parfect2 = Getnote.mu.ms[s] + Parfect;
        Great2 = Getnote.mu.ms[s] + Great;
        Good2 = Getnote.mu.ms[s] + Good;
        miss2 = Getnote.mu.ms[s] + miss;

        Parfect3 = Getnote.mu.ms[s] - Parfect;
        Great3 = Getnote.mu.ms[s] - Great;
        Good3 = Getnote.mu.ms[s] - Good;
        miss3 = Getnote.mu.ms[s] - miss;


        //s = Getnote.mu.number;
        //Getnote.Getms(note);
        //note_Timer = Getnote.mu.ms[Getnote.mu.No[1]];

    }
    void FixedUpdate()
    {
        //if (Good3 > Getnote.mu.ms[s] || Getnote.mu.ms[s] > Good2)
        //{
        //    Debug.Log("Parfect " + ms);
        //}
        //else if (Great3 > Getnote.mu.ms[s] || Getnote.mu.ms[s] > Great2)
        //{
        //    Debug.Log("Great " + ms);
        //}
        //else if (Parfect3 < Getnote.mu.ms[s] || Getnote.mu.ms[s] < Parfect2)
        //{
        //    Debug.Log("Good " + ms);
        //}else
        //{
        //    Debug.Log("obre " + ms);
        //}

        Move.transform.localScale -= new Vector3(MoveSpeed, MoveSpeed, MoveSpeed) ;

        if (ms > Getnote.Out_Timing * 2)
        {
            s++;
            Debug.Log("miss ");
            ms = 0;
            ms2 = 0;
            gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        //if (collider.gameObject.tag == "Player_L" || collider.gameObject.tag == "Player_R")
        //{
            if (Parfect3 < Getnote.mu.ms[s] || Getnote.mu.ms[s] < Parfect2)
            {
                Debug.Log("Parfect");
                s++;
                gameObject.SetActive(false);
            }
            else if (Great3 < Getnote.mu.ms[s] || Getnote.mu.ms[s] < Great2)
            {
                Debug.Log("Great");
                s++;
                gameObject.SetActive(false);
            }
            else if (Good3 < Getnote.mu.ms[s] || Getnote.mu.ms[s] < Good2)
            {
                Debug.Log("Good");
                s++;
                gameObject.SetActive(false);
            }
        //}
    }
}
