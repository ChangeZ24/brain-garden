using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{

    public Button pauseBtn;

    public GameObject blackBG;
    public GameObject pauseTex;
   /* public void Start()
    {
        pauseBtn.onClick.AddListener(()=>{
            PauseFunc();
        });
    }*/
    public bool isPasue;
    public void PauseFunc()
    {
        if (MapRender.instance.isGameEnd)
            return;

        if (isPasue ==false)
        {
            isPasue = true;
            Time.timeScale = 0;
            blackBG.SetActive(true);
            pauseTex.SetActive(true);
        }
        else
        {
            isPasue = false;
            Time.timeScale = 1;
            blackBG.SetActive(false); 
            pauseTex.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PauseFunc();
        }

    }



    /*public void SetPausePanel()
    {

    }*/
}
