using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenBorder : MonoBehaviour
{
    public Image waringBorder;
    public CanvasGroup cg;
    public bool isWarning;

    private void Start()
    {
        cg.alpha = 0;
    }
    private void Update()
    {
        if (MapRender.instance.isGameEnd)
        {
            if (Warning_ie != null)
                StopCoroutine(Warning_ie);
            cg.alpha = 0;
            isWarning = false;
            return;
        }

        //最后10s的闪烁取消
        /*if(MapRender.instance.gameTimeCounter<10&&isWarning==false)
        {
            isWarning = true;
            StartWarning();
        }*/

    }

    public float fadeSpeed;
    /*public float isFadingOut;
    public float isFadingIn;*/

    public void StartWarning()
    {
        Warning_ie = BorderWarning();
        StartCoroutine(Warning_ie);
    }
    private IEnumerator Warning_ie;
    private IEnumerator BorderWarning()
    {
        while (true)
        {
            while (cg.alpha < 1)
            {
                //fadeIn
                cg.alpha += Time.deltaTime * fadeSpeed;
                yield return null;
            }

            while (cg.alpha >0)
            {

                //fadeOut
                cg.alpha -= Time.deltaTime * fadeSpeed;
                yield return null;
            }
        }
    }

    public bool isFlashing;
    public void DoFlashOnce()
    {
        if (isWarning)
            return;
        if (isFlashing)
            return;
        isFlashing = true;
        StartCoroutine(OneFlash());
    }
    public float getHitFadeSpeed;
    private IEnumerator OneFlash()
    {
        while (cg.alpha < 1)
        {
            if (isWarning)
                yield break;
            //fadeIn
            cg.alpha += Time.deltaTime * getHitFadeSpeed;
            yield return null;
        }

        while (cg.alpha > 0)
        {
            if (isWarning)
                yield break;
            //fadeOut
            cg.alpha -= Time.deltaTime * getHitFadeSpeed;
            yield return null;
        }
        isFlashing = false;
    }
}
