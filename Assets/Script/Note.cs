using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Note : MonoBehaviour
{
    [Serializable]
    public class Music
    {   public string Name;
        public float Offset;

        public string[] LR;
        public int[] pos;
        public int[] ms;
        public bool[] Long;
    }
    public Music mu;

    public GameObject[] L_Notes_point_obj; //ノーツの生成位置のオブジェクトの格納する場所　左
    public GameObject[] L_LongNotes_Point_obj; //ロングノーツの生成位置のオブジェクトの格納する場よ　左
    public GameObject[] R_Notes_point_obj; //ノーツの生成位置のオブジェクトの格納する場所　右
    public GameObject[] R_LongNotes_Point_obj; //ロングノーツの生成位置のオブジェクトの格納する場よ　右
    public GameObject notes_Obj; //ノーツのオブジェクトの格納場所
    public GameObject Long_Notes_Obj; //ロングノーツのオブジェクトの格納場所
    public GameObject[] longNotesBer; //棒の部分 

    public int Out_Timing; //判定のタイミングからどれだけの時間を引くかの指定先
    public float Move_Speed;　//円の動く速度
    public int LongBerTrget; //ロングノーツの乗せる時間

    public TimeMane timeMane;  //タイムマネージャーから時間の取得
    public int ms;　//時間の格納先

    public int s = 0; //カウント
    private int L;
    public int ko; //子供の数

    public string MusicName = "savedata"; //ファイル読み込み時の名前テスト用（ここを曲名にすればOK）


    private int R;
    private List<int> Long_No = new List<int>();
    private List<int> Long_Pos = new List<int>();
    private List<string> Long_LR = new List<string>();
    // Start is called before the first frame update
    void Awake()
    {
        //ファイル読み込み
        string test = Resources.Load<TextAsset>("notesData/" + MusicName).ToString();
        mu = JsonUtility.FromJson<Music>(test);

        //ロングノーツの番号を取得
        R = Array.IndexOf(mu.Long, true);
        while (0 <= R)
        {
            Long_No.Add(R);

            if (R + 1 < mu.Long.Length)
            {
                R = Array.IndexOf(mu.Long, true, R + 1);
            }
            else
            {
                break;
            }
        }
        for(int i = 0; i < Long_No.Count; i++)
        {
            Long_Pos.Add(mu.pos[Long_No[i]]);
            Long_LR.Add(mu.LR[Long_No[i]]);
        }

        ko = mu.LR.Length;
        Debug.Log(ko);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        ms = timeMane.Mse - Out_Timing;

        //ノーツ生成　左
        if (ms >= mu.ms[s] + mu.Offset - Out_Timing && mu.LR[s] == "L") //生成時間の確認と右か左可の確認
        {
            if (mu.Long[s] == false)
            {
                Instantiate(notes_Obj, L_Notes_point_obj[mu.pos[s]].transform.position, Quaternion.identity);
                s++;
            }
            else
            {
                Instantiate(Long_Notes_Obj, L_Notes_point_obj[Long_Pos[L]].transform.position, Quaternion.identity); //ロングノーツ通常
                L++;
                if (L < Long_Pos.Count)
                {
                    switch (Long_Pos[L - 1])
                    {
                        case 0:
                            switch (Long_Pos[L])
                            {
                                case 2:
                                    Instantiate(longNotesBer[0], L_LongNotes_Point_obj[0].transform.position, Quaternion.identity);
                                    //L++;
                                    break;
                                case 6:
                                    Instantiate(longNotesBer[3], L_LongNotes_Point_obj[3].transform.position, Quaternion.identity);
                                    //L++;
                                    break;
                            }
                            break;
                        case 2:
                            switch (Long_Pos[L])
                            {
                                case 0:
                                    Instantiate(longNotesBer[0], L_LongNotes_Point_obj[0].transform.position, Quaternion.identity);
                                    //L++;
                                    break;
                                case 4:
                                    Instantiate(longNotesBer[1], L_LongNotes_Point_obj[1].transform.position, Quaternion.identity);
                                    //L++;
                                    break;
                            }
                            break;
                        case 4:
                            switch (Long_Pos[L])
                            {
                                case 2:
                                    Instantiate(longNotesBer[1], L_LongNotes_Point_obj[1].transform.position, Quaternion.identity);
                                    //L++;
                                    break;
                                case 6:
                                    Instantiate(longNotesBer[2], L_LongNotes_Point_obj[2].transform.position, Quaternion.identity);
                                    //L++;
                                    break;
                            }
                            break;
                        case 6:
                            switch (Long_Pos[L])
                            {
                                case 0:
                                    Instantiate(longNotesBer[3], L_LongNotes_Point_obj[3].transform.position, Quaternion.identity);
                                    //L++;
                                    break;
                                case 4:
                                    Instantiate(longNotesBer[2], L_LongNotes_Point_obj[2].transform.position, Quaternion.identity);
                                    //L++;
                                    break;
                            }
                            break;
                    }
                }
                s++;
            }
        }

        //ノーツ生成　右
        if (ms >= mu.ms[s] + mu.Offset - Out_Timing && mu.LR[s] == "R") //生成時間の確認と右か左可の確認
        {
            if (mu.Long[s] == false)
            {
                Instantiate(notes_Obj, R_Notes_point_obj[mu.pos[s]].transform.position, Quaternion.identity);　//通常ノーツ
                s++;
            }
            else
            {
                Instantiate(Long_Notes_Obj, R_Notes_point_obj[Long_Pos[L]].transform.position, Quaternion.identity); //ロングノーツ通常
                L++;
                switch (Long_Pos[L - 1])
                {
                    case 0:
                        switch (Long_Pos[L])
                        {
                            case 2:
                                Instantiate(longNotesBer[0], R_LongNotes_Point_obj[0].transform.position, Quaternion.identity);
                                //L++;
                                break;
                            case 6:
                                Instantiate(longNotesBer[3], R_LongNotes_Point_obj[3].transform.position, Quaternion.identity);
                                //L++;
                                break;
                        }
                        break;
                    case 2:
                        switch (Long_Pos[L])
                        {
                            case 0:
                                Instantiate(longNotesBer[0], R_LongNotes_Point_obj[0].transform.position, Quaternion.identity);
                                //L++;
                                break;
                            case 4:
                                Instantiate(longNotesBer[1], R_LongNotes_Point_obj[1].transform.position, Quaternion.identity);
                                //L++;
                                break;
                        }
                        break;
                    case 4:
                        switch (Long_Pos[L])
                        {
                            case 2:
                                Instantiate(longNotesBer[1], R_LongNotes_Point_obj[1].transform.position, Quaternion.identity);
                                //L++;
                                break;
                            case 6:
                                Instantiate(longNotesBer[2], R_LongNotes_Point_obj[2].transform.position, Quaternion.identity);
                                //L++;
                                break;
                        }
                        break;
                    case 6:
                        switch (Long_Pos[L])
                        {
                            case 0:
                                Instantiate(longNotesBer[3], R_LongNotes_Point_obj[3].transform.position, Quaternion.identity);
                                //L++;
                                break;
                            case 4:
                                Instantiate(longNotesBer[2], R_LongNotes_Point_obj[2].transform.position, Quaternion.identity);
                                //L++;
                                break;
                        }
                        break;
                }
                s++;
            }
        }
    }
}