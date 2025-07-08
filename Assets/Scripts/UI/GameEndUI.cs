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
            //�ݶ�backtoMenu
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
            //���¿�ʼ
            //��Player���õ���㣬���ó���ȵ�
            //����ʱ��
            MapRender.instance.GameStart();
            ResetThisUIAll();
        });
        replay2Btn.onClick.AddListener(() =>
        { 
            MapRender.instance.GameStart();
            ResetThisUIAll();
        });
        replay3Btn.onClick.AddListener(() =>
        {
            MapRender.instance.GameStart();
            ResetThisUIAll();
        });
    }


    /// <summary>
    /// target��������һ��UI��0Ϊ���ģ�1Ϊһ�㣬2Ϊ����
    /// </summary>
    /// <param name="target"></param>
    public void SetTargetUIActive(int target)
    {
        blackBG.gameObject.SetActive(true);
        //������ʾ������
        if(target==0)
        {
            //����ĵ�UI
            happyUI.SetActive(true);
        }
        else if (target == 1)
        {
            //����һ���UI
            calmUI.SetActive(true);
        }
        else if (target == 2)
        {
            //�������ĵ�UI
            sadUI.SetActive(true);
        }
    }
    public void ResetThisUIAll()
    {
        //Debug.Log("fucnt");
        blackBG.gameObject.SetActive(false);
        //������ͬ����UIʧ��
        happyUI.gameObject.SetActive(false);
        //Debug.Log(calmUI == null);
        calmUI.gameObject.SetActive(false);
        sadUI.gameObject.SetActive(false);
    }

}
