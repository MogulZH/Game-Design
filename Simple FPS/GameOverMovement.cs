using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameOverMovement : MonoBehaviour {
    private RectTransform textRT;
    void Start() {
        textRT = GetComponent<RectTransform>();
        textRT.anchoredPosition = 
        new Vector2(-Screen.width/2f + textRT.sizeDelta.x/2f, 
                     Screen.height/2f - textRT.sizeDelta.y/2f);
    }
    void Update() {
        textRT.anchoredPosition = 
        new Vector2(Mathf.Sin(Time.time)*(Screen.width/2f - textRT.sizeDelta.x/2f), 
                    Mathf.Sin(Time.time)*(Screen.height/2f - textRT.sizeDelta.y/2f));
    }
}