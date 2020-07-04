using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizeManager : SingletonMonoBehaviour<QuizeManager> {

    public NotificationObject<bool> isClick;


    // 選択されたクイズのクラス
    public class QuestionsObject {
        public string questinText;
        public string answerText;
        public string[] choices;

        public QuestionsObject () {
            questinText = null;
            answerText = null;
            choices = new string[4];
        }
    }

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    // 初期化メソッド
    protected override void Awake () {
        base.Awake();

        isClick = new NotificationObject<bool>(false);
    }
}