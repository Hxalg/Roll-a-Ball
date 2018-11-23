using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AllCount : MonoBehaviour {

    public Texture fadeImage;
    public float fadeSpeed = 1.5f;

    private float GUIColorAlpha;

    // Use this for initialization
    void Start()
    {
        GUIColorAlpha = GUI.color.a;
        GUIColorAlpha = 0.0f;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FadeToBlack()
    {
        GUIColorAlpha = Mathf.Lerp(GUIColorAlpha, 1.0f, fadeSpeed * Time.deltaTime);
    }

    void OnGUI()
    {
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, Mathf.Clamp01(GUIColorAlpha));
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeImage);
    }

    public void OnCountGame(string sceneName)
    {
        FadeToBlack();
        SceneManager.LoadScene(sceneName);
    }


}
