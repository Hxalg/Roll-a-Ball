using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;

public class PlayerController : MonoBehaviour {

    public float speed;

    public Text countText;

    public Text winText;

    public Text timeText;

    private Rigidbody rb;

    private int count;

    private int hour;
    private int minute;
    private int second;
    private int millisecond;

    public string timeScore;

    private float timeSpend;

    private bool isSpend;

    public Button endGame;

    List<Score> scoreList = new List<Score>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        hour = 0;
        minute = 0;
        second = 0;
        millisecond = 0;
        timeSpend = 0.0f;
        isSpend = true;
        timeScore = "";
        SetCountText();
        winText.text = "";

        StreamReader sr = new StreamReader(Application.dataPath + "/Resources/RankingList.txt");
        string nextLine;
        while ((nextLine = sr.ReadLine()) != null)
        {
            scoreList.Add(JsonUtility.FromJson<Score>(nextLine));
        }
        sr.Close();
    }

    //void Update()
    //{
        //timeSpend += Time.deltaTime;
        //GlobalSetting.timeSpend = timeSpend;
        //hour = (int)timeSpend / 3600;
        //minute = ((int)timeSpend - hour * 3600) / 60;
        //second = (int)timeSpend - hour * 3600 - minute * 60;
        //millisecond = (int)((timeSpend - (int)timeSpend) * 1000);
    //}

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

        if (isSpend)
        {
            timeSpend += Time.deltaTime;
            //GlobalSetting.timeSpend = timeSpend;
            hour = (int)timeSpend / 3600;
            minute = ((int)timeSpend - hour * 3600) / 60;
            second = (int)timeSpend - hour * 3600 - minute * 60;
            millisecond = (int)((timeSpend - (int)timeSpend) * 1000);

            SetCountText();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count:" + count.ToString();
        timeText.text = "Score:" + string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}", hour, minute, second, millisecond);
        timeScore = hour + ":" + minute + ":" + second + "." + millisecond;
        if (count >= 12)
        {
            string Name = "Admin";
            int numScore;
            numScore = (int)(timeSpend * 1000);

            isSpend = false;
            winText.text = "Your Score: " + timeScore;
            endGame.gameObject.SetActive(true);

            scoreList.Add(new Score(Name, numScore));

            scoreList.Sort();
            scoreList.Reverse();
            StreamWriter sw = new StreamWriter(Application.dataPath + "/Resources/RankingList.txt");
            if (scoreList.Count > 10)
                for (int i = 10; i < scoreList.Count; i++)
                    scoreList.RemoveAt(i);
            for(int i = 0; i < scoreList.Count; i++)
            {
                sw.WriteLine(JsonUtility.ToJson(scoreList[i]));
                Debug.Log(scoreList[i].ToString());
            }
            sw.Close();
        }
    }

}
