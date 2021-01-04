using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Note : MonoBehaviour
{
    [Serializable]
    public class Music
    {
        public string[] LR;
        public float Offset;
        public int[] ms;
        public bool[] Long;

        public string Name;

        //public int[] No;
        public int[] pos;
    }
    public Music mu;

    public GameObject[] L_Notes_point_obj; //ノーツの生成位置のオブジェクトの格納する場所　左
    public GameObject[] L_LongNotes_Point_obj; //ロングノーツの生成位置のオブジェクトの格納する場よ　左
    public GameObject[] R_Notes_point_obj; //ノーツの生成位置のオブジェクトの格納する場所　右
    public GameObject[] R_LongNotes_Point_obj; //ロングノーツの生成位置のオブジェクトの格納する場よ　右
    public GameObject notes_Obj; //ノーツのオブジェクトの格納場所
    public GameObject[] Long_Notes_Obj; //ロングノーツのオブジェクトの格納場所

    public int Out_Timing; //判定のタイミングからどれだけの時間を引くかの指定先
    public float Move_Speed;　//円の動く速度

    public TimeMane timeMane;  //タイムマネージャーから時間の取得
    public int ms;　//時間の格納先

    public int s = 0; //カウント
    public int ko = 0; //子供の数

    public string MusicName = "savedata";　//ファイル読み込み時の名前テスト用（ここを曲名にすればOK）

    // Start is called before the first frame update
    void Start()
    {
        //ファイル読み込み
        string test = Resources.Load<TextAsset>("notesData/" + MusicName).ToString();
        mu = JsonUtility.FromJson<Music>(test);
    }

    // Update is called once per frame
    void Update()
    {
        ms = timeMane.Mse;
    }
    void FixedUpdate()
    {
        //通常ノーツ生成　左
        if (ms >= mu.ms[s] + mu.Offset - Out_Timing && mu.LR[s] == "L") //生成時間の確認と右か左可の確認
        {
            if (s >= mu.pos.Length)
            {
                s = 0;
            }
            else
            {
                Instantiate(notes_Obj, L_Notes_point_obj[mu.pos[s]].transform.position, Quaternion.identity);
                s++;
            }
        }

        //通常ノーツ生成　右
        if (ms >= mu.ms[s] + mu.Offset - Out_Timing && mu.LR[s] == "R") //生成時間の確認と右か左可の確認
        {
            if (s >= mu.pos.Length)
            {
                s = 0;
            }
            else
            {
                Instantiate(notes_Obj, R_Notes_point_obj[mu.pos[s]].transform.position, Quaternion.identity);
                s++;
            }
        }

        //ロングノーツ生成　左
        if (ms >= mu.ms[s] + mu.Offset - Out_Timing && mu.LR[s] == "L" && mu.Long[s] == true)
        {
            if (s >= mu.pos.Length)
            {
                s = 0;
            }
            else
            {
                Instantiate(Long_Notes_Obj[mu.pos[s]], L_LongNotes_Point_obj[mu.pos[s]].transform.position, Quaternion.identity);
                s++;
            }
        }

        //ロングノーツ生成　右
        if (ms >= mu.ms[s] + mu.Offset - Out_Timing && mu.LR[s] == "R" && mu.Long[s] == true)
        {
            if (s >= mu.pos.Length)
            {
                s = 0;
            }
            else
            {
                Instantiate(Long_Notes_Obj[mu.pos[s]], R_LongNotes_Point_obj[mu.pos[s]].transform.position, Quaternion.identity);
                s++;
            }
        }
    }
}