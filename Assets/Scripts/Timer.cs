using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private float limitTime = 30f;
    [HideInInspector] public Scrollbar timerUi;
    private float decreaseValue = 0.01f / 30f;

    private void Start() {
        timerUi = this.GetComponent<Scrollbar>();
    }

    public IEnumerator TimerCorutine () {
        timerUi.size = 1;
        while (limitTime >= 0f) {
            limitTime -= 0.01f;
            timerUi.size -= decreaseValue;
            //Debug.Log("timer:" + limitTime);
            yield return new WaitForSeconds(0.01f);
        }
        yield break;
    }
}