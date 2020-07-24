using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private float limitTime = 30f; // 残り時間30s
    [HideInInspector] public Scrollbar timerUi;
    private float decreaseValue = 0.1f / 30f; // 10msで減らすゲージの割合

    private void Start () {
        timerUi = this.GetComponent<Scrollbar> ();
    }

    public IEnumerator TimerCorutine () {
        timerUi.size = 1;
        while (limitTime >= 0f) {
            limitTime -= 0.1f;
            timerUi.size -= decreaseValue;
            //Debug.Log ("timer:" + (int) limitTime);
            yield return new WaitForSeconds (0.1f);
        }
        yield break;
    }
}