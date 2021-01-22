using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LongNotes : MonoBehaviour
{
    public Note GetNote;
    public Judge GetJudge;
    private GameObject Move;

    private int ms;
    private float ms2;

    //ロングの棒用時間
    private int Long_ms;
    private float Long_ms2;
    private int Long_trget;

    //判定の間隔の指定
    public int Parfect;
    public int Great;
    public int Good;
    public int miss;

    //ノーツの動き
    private float StartScale;
    public float EndScale;
    private float rate;
    public int MoveTime;
    private Vector3 Start;
    private Vector3 End;

    //判定のカウント
    public int Parfect_count;
    public int Great_count;
    public int Good_count;
    public int miss_count;

    // Start is called before the first frame update
    void Awake()
    {
        transform.parent = GameObject.Find("NotesMane").transform;

        GetNote = GetComponentInParent<Note>();

        if(gameObject.tag == "LongNotes")
        { 
            Move = transform.GetChild(1).gameObject;
            Move.gameObject.transform.localScale *= GetJudge.en_size;
        }

        Long_trget = GetNote.LongBerTrget; //乗せる時間を取得

        //円の動きに必よなもの
        MoveTime = GetNote.Out_Timing;
        StartScale = GetJudge.en_size;
        Start = new Vector3(StartScale, StartScale, StartScale);
        End = new Vector3(EndScale, EndScale, EndScale);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //時間計算
        ms2 += Time.fixedDeltaTime;
        ms = (int)(ms2 * 1000);

        //ノーツの動き
        if (gameObject.tag == "LongNotes")
        {
            rate = (float)ms / MoveTime;
            Move.transform.localScale = Vector3.Lerp(Start, End, rate);
        }

        //一定時間経過で消える
        if (ms > GetNote.Out_Timing * 2)
        {
            Debug.Log("miss late LOng");
            miss_count++;
            ms = 0;
            ms2 = 0;
            Long_ms = 0;
            Long_ms2 = 0;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (gameObject.tag == "LongNotes")
        {
            if (collider.gameObject.tag == "Player_L" || collider.gameObject.tag == "Player_R")
            {
                if (ms < miss)
                {
                    Debug.Log("miss! Fast LOng");
                    miss_count++;
                    gameObject.SetActive(false);
                }
                else if (ms < miss + Good)
                {
                    Debug.Log("Good! fast Long");
                    Good_count++;
                    gameObject.SetActive(false);
                }
                else if (ms < miss + Good + Great)
                {
                    Debug.Log("Great! fast Long");
                    Great_count++;
                    gameObject.SetActive(false);
                }
                else if (ms < miss + Good + Great + Parfect)
                {
                    Debug.Log("parfect! fast Long");
                    Parfect_count++;
                    gameObject.SetActive(false);
                }
                //-------------折り返し----------------------
                else if (ms < miss + Good + Great + Parfect * 2)
                {
                    Debug.Log("parfect! late Long");
                    Parfect_count++;
                    gameObject.SetActive(false);
                }
                else if (ms < miss + Good + Great * 2 + Parfect * 2)
                {
                    Debug.Log("Great! late Long");
                    Great_count++;
                    gameObject.SetActive(false);
                }
                else if (ms < miss + Good * 2 + Great * 2 + Parfect * 2)
                {
                    Debug.Log("Good! late Long+");
                    Good_count++;
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        //if (gameObject.tag == "LongNotesBar")
        //{
            if (collider.gameObject.tag == "Player_L" || collider.gameObject.tag == "Player_R")
            {
                Long_ms2 += Time.fixedDeltaTime;
                Long_ms = (int)(ms2 * 1000);
                if (Long_ms >= Long_trget)
                {
                    Debug.Log("Ber_OK");
                    gameObject.SetActive(false);
                }
            }
        //}
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        
    }
}
