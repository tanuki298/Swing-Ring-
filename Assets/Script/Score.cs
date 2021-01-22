using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int Perfect;
    public int Great;
    public int Good;
    public int Miss;
    
    private int Perfect_N;
    private int Great_N;
    private int Good_N;
    private int Miss_N;

    private int Perfect_L;
    private int Great_L;
    private int Good_L;
    private int Miss_L;

    private int MaxScore;
    private int NowScore;
    public int ko;

    public Text score;
    private float per;
    public Judge GetJudge;
    public LongNotes GetLong;
    public Note GetNote;
    // Start is called before the first frame update
    void Awake()
    {
        ko = GetNote.ko;
        Debug.Log(ko + "Sco");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Perfect_N = GetJudge.Parfect_count * Perfect;
        Great_N = GetJudge.Great_count * Great;
        Good_N = GetJudge.Good_count * Good;
        Miss_N = GetJudge.miss_count * Miss;

        Perfect_L = GetLong.Parfect_count * Perfect;
        Great_L = GetLong.Great_count * Great;
        Good_L = GetLong.Good_count * Good;
        Miss_L = GetLong.miss_count * Miss;

        NowScore = Perfect_N + Great_N + Good_N + Miss_N + Perfect_L + Great_L + Good_L + Miss_L;
        MaxScore = ko * Perfect;

        per = NowScore / MaxScore;

        score.text = per.ToString() + "%";
    }
}
