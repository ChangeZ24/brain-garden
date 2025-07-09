using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameEndUI : MonoBehaviour
{
    public Button nextBtn;
    public Button replay1Btn;

    public Button menu2Btn;
    public Button replay2Btn;

    public Button menu3Btn;
    public Button replay3Btn;

    public Image blackBG;

    public GameObject happyUI;
    public GameObject calmUI;
    public GameObject sadUI;

    public AudioClip happyAC;
    public AudioClip calmAC;
    public AudioClip sadAC;
    public AudioSource musicSource;


    public void Start()
    {
        nextBtn.onClick.AddListener(() =>
        {
            //暂定backtoMenu
            SceneManager.LoadScene(0);
        });

        menu2Btn.onClick.AddListener(() =>
        {
            //backtoMenu
            SceneManager.LoadScene(0);
        });

        menu3Btn.onClick.AddListener(() =>
        {
            //backtoMenu
            SceneManager.LoadScene(0);
        });


        replay1Btn.onClick.AddListener(() =>
        {
            //重新开始
            //将Player重置到起点，重置朝向等等
            //重置时间
            /* MapRender.instance.GameStart();
             ResetThisUIAll();*/
            SceneManager.LoadScene(2);
            Time.timeScale = 1;
        });
        replay2Btn.onClick.AddListener(() =>
        {
            /*MapRender.instance.GameStart();
            ResetThisUIAll();*/
            SceneManager.LoadScene(2);
            Time.timeScale = 1;
        });
        replay3Btn.onClick.AddListener(() =>
        {
            /*MapRender.instance.GameStart();
            ResetThisUIAll();*/
            SceneManager.LoadScene(2);
            Time.timeScale = 1;
        });
    }


    /// <summary>
    /// target代表激活哪一个UI，0为开心，1为一般，2为伤心
    /// </summary>
    /// <param name="target"></param>
    public void SetTargetUIActive(int target)
    {
        blackBG.gameObject.SetActive(true);
        scoreUI.gameObject.SetActive(true);
        scoreUI.text = (MapRender.instance.playerAreaRate * 100).ToString("F1") + "%";
        //启用显示并激活
        if (target==0)
        {
            //激活开心的UI
            happyUI.SetActive(true);
            SoundManager.instance.HappyAudio();
        }
        else if (target == 1)
        {
            //激活一般的UI
            calmUI.SetActive(true);
            SoundManager.instance.CalmAudio();
        }
        else if (target == 2)
        {
            //激活伤心的UI
            sadUI.SetActive(true);
            SoundManager.instance.SadAudio();
        }
    }
    public void ResetThisUIAll()
    {
        //Debug.Log("fucnt");
        blackBG.gameObject.SetActive(false);
        //三个不同结算UI失活
        happyUI.gameObject.SetActive(false);
        //Debug.Log(calmUI == null);
        calmUI.gameObject.SetActive(false);
        sadUI.gameObject.SetActive(false);
        scoreUI.gameObject.SetActive(false);
    }
    public Text scoreUI;

}
