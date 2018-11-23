using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour {

    public GameObject ItemPrefab;
    public Image ItemParent;

    private int hour;
    private int minute;
    private int second;
    private int millisecond;

    List<Score> scoreList = new List<Score>();

	// Use this for initialization
	void Start () {
        StreamReader sr = new StreamReader(Application.dataPath + "/Resources/RankingList.txt");
        string nextLine;
        while((nextLine = sr.ReadLine()) != null)
        {
            scoreList.Add(JsonUtility.FromJson<Score>(nextLine));
        }
        sr.Close();
          
        for(int i = 0; i < scoreList.Count; i++)
        {
            GameObject item = Instantiate(ItemPrefab, ItemParent.transform.position, Quaternion.identity) as GameObject;
            item.transform.parent = ItemParent.transform;
            Text[] Children = item.GetComponentsInChildren<Text>();
            //item.transform.Find("Number").GetComponent<Text>().text = (i + 1).ToString();
            //item.transform.Find("Name").GetComponent<Text>().text = scoreList[i].name;
            //item.transform.Find("Score").GetComponent<Text>().text = scoreList[i].score.ToString();

            hour = scoreList[i].score /1000 / 3600;
            minute = (scoreList[i].score / 1000 - hour * 3600) / 60;
            second = scoreList[i].score / 1000 - hour * 3600 - minute * 60;
            millisecond = (scoreList[i].score - scoreList[i].score / 1000 * 1000);

            Children[0].text = (i + 1).ToString();
            Children[1].text = scoreList[i].name;
            Children[2].text = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}", hour, minute, second, millisecond);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
