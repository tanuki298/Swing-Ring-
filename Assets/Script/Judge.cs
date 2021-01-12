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
                gameObject.SetActive(false);
            }
            else if (ms < miss + Good)
            {
                Debug.Log("Good! fast");
                gameObject.SetActive(false);
            }
            else if (ms < miss + Good + Great)
            {
                Debug.Log("Great! fast");
                gameObject.SetActive(false);
            }
            else if (ms < miss + Good + Great + Parfect)
            {
                Debug.Log("parfect! fast");
                gameObject.SetActive(false);
            }
            //-------------折り返し----------------------
            else if (ms < miss + Good + Great + Parfect * 2)
            {
                Debug.Log("parfect! late");
                gameObject.SetActive(false);
            }
            else if (ms < miss + Good + Great * 2 + Parfect * 2)
            {
                Debug.Log("Great! late");
                gameObject.SetActive(false);
            }
            else if (ms < miss + Good * 2 + Great * 2 + Parfect * 2)
            {
                Debug.Log("Good! late");
                gameObject.SetActive(false);
            }
        }
    }
}
