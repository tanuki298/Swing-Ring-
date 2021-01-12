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
            ms = 0;
            ms2 = 0;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player_L" || collider.gameObject.tag == "Player_R" && gameObject.tag =="LongNotes")
        {
            if (ms < miss)
            {
                Debug.Log("miss! Fast LOng");
                gameObject.SetActive(false);
            }
            else if (ms < miss + Good)
            {
                Debug.Log("Good! fast Long");
                gameObject.SetActive(false);
            }
            else if (ms < miss + Good + Great)
            {
                Debug.Log("Great! fast Long");
                gameObject.SetActive(false);
            }
            else if (ms < miss + Good + Great + Parfect)
            {
                Debug.Log("parfect! fast Long");
                gameObject.SetActive(false);
            }
            //-------------折り返し----------------------
            else if (ms < miss + Good + Great + Parfect * 2)
            {
                Debug.Log("parfect! late Long");
                gameObject.SetActive(false);
            }
            else if (ms < miss + Good + Great * 2 + Parfect * 2)
            {
                Debug.Log("Great! late Long");
                gameObject.SetActive(false);
            }
            else if (ms < miss + Good * 2 + Great * 2 + Parfect * 2)
            {
                Debug.Log("Good! late Long+");
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player_L" || collider.gameObject.tag == "Player_R" && gameObject.tag == "LongNotesBar")
        {

        }
    }
}
