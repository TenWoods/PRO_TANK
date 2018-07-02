using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    private RectTransform rectTransform;
   
    [Header("【初始位置】")]
    [SerializeField]
    private Vector3 myValue = new Vector3(454, -160, 0);
    [Header("【目标位置】")]
    [SerializeField]
    private Vector3 value = new Vector3(200, -160, 0);

    // Use this for initialization
    void Start () {
        rectTransform = GetComponent<RectTransform>();
        Tween tween = DOTween.To(() => myValue, x => myValue = x, value, 1);
        //创建Dotween队列
        Sequence s = DOTween.Sequence();
        s.Append(tween);
    }
    //按钮事件
    public void OnStartButtonDown()
    {
        Debug.Log("开始游戏");
        SceneManager.LoadScene("SampleScene");
    }
    public void OnExitButtonDown()
    {
        Application.Quit();
        Debug.Log("退出游戏");
    }
    public void OnRestartButtonDown()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
    }
    public void OnReturnButtonDown()
    {
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.localPosition = myValue;
    }

}
