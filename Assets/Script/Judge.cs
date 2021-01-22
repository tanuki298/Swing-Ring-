using UnityEngine;

public class Judge : MonoBehaviour
{
    public TimeMane time;
    public Note Getnote;
    private GameObject Move;

    private int ms;　//ミリ秒
    private float ms2;
    private int s = 0;  //要素数の指定
    public float en_size; //円のサイズ

    //判定の間隔の指定
    public int Parfect;
    public int Great;
    public int Good;
    public int miss;

    //ノーツの動き
    private float StartScale;
    public float EndScale;
    private float rate;
    private int MoveTime;
    private Vector3 Start;
    private Vector3 End;

    //判定のカウント
    public int Parfect_count;
    public int Great_count;
    public int Good_count;
    public int miss_count;

    // Start is called before the first frame update
    private void Awake()
    {
        //Array.Resize(ref note, Getnote.mu.No.Length);
        transform.parent = GameObject.Find("NotesMane").transform;
        //gameObject.SetActive(false);
        Getnote = GetComponentInParent<Note>();

        Move = transform.GetChild(1).gameObject;

        Move.gameObject.transform.localScale *= en_size;

        //円の動きに必要な物
        MoveTime = Getnote.Out_Timing;
        StartScale = en_size;
        Start = new Vector3(StartScale, StartScale, StartScale);
        End = new Vector3(EndScale, EndScale, EndScale);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ms2 += Time.fixedDeltaTime;
        ms = (int)(ms2 * 1000);

        //ノーツの動き
        rate = (float)ms / MoveTime;
        Move.transform.localScale = Vector3.Lerp(Start, End, rate);

        if (ms > Getnote.Out_Timing * 2)
        {
            s++;
            Debug.Log("miss late");
            miss_count++;
            ms = 0;
            ms2 = 0;
            gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player_L" || collider.gameObject.tag == "Player_R") //要最適化
        {
            if(ms < miss)
            {
                Debug.Log("miss! Fast");
                miss_count++;
                gameObject.SetActive(false);
            }
            else if (ms < miss + Good)
            {
                Debug.Log("Good! fast");
                Good_count++;
                gameObject.SetActive(false);
            }
            else if (ms < miss + Good + Great)
            {
                Debug.Log("Great! fast");
                Great_count++;
                gameObject.SetActive(false);
            }
            else if (ms < miss + Good + Great + Parfect)
            {
                Debug.Log("parfect! fast");
                Parfect_count++;
                gameObject.SetActive(false);
            }
            //-------------折り返し----------------------
            else if (ms < miss + Good + Great + Parfect * 2)
            {
                Debug.Log("parfect! late");
                Parfect_count++;
                gameObject.SetActive(false);
            }
            else if (ms < miss + Good + Great * 2 + Parfect * 2)
            {
                Debug.Log("Great! late");
                Great_count++;
                gameObject.SetActive(false);
            }
            else if (ms < miss + Good * 2 + Great * 2 + Parfect * 2)
            {
                Debug.Log("Good! late");
                Good++;
                gameObject.SetActive(false);
            }
        }
    }
}
