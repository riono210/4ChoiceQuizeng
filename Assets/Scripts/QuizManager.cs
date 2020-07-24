using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour {

    [HideInInspector] public TextAsset csvflie; // 答えと選択肢が対になっているcsvファイル
    [HideInInspector] public List<string[]> csvData = new List<string[]> (); // csvファイルを読み取ったリスト

    // クイズの形式クラス
    public QuestionGenerator generator;
    public QuestionObject questionObj;

    // タイマー
    public Timer timer;
    public IEnumerator timerCol = null;

    public GameObject anserButtonsObj;

    // 問題文と選択肢
    private TextMeshProUGUI[] choiceSelectsTMP = new TextMeshProUGUI[4];
    public TextMeshProUGUI questionTextTMP;

    // 正誤確認
    private AnswerChecker[] answerChecker = new AnswerChecker[4];

    private void Start () {
        LoadCSVFile ();
        ChoiceQuestion ();
        TakeSelectObject ();
    }

    // Resourceフォルダにあるcsvファイルを読み込む
    public void LoadCSVFile () {
        // csvファイルを読みこんでデータを読み込む準備
        csvflie = Resources.Load ("4ChoiseQuizeng-SourceList-test1") as TextAsset;
        StringReader reader = new StringReader (csvflie.text);

        // 一行ずつ読み込み、, 区切り
        while (reader.Peek () != -1) {
            string line = reader.ReadLine ();
            csvData.Add (line.Split (','));
        }

        // ファイルから読み込んだデータの全表示
        // foreach (var item in csvData) {
        //     foreach (var cell in item)
        //         Debug.Log ("data:" + cell);
        // }
    }

    // 選択肢を取得
    public void TakeSelectObject () {
        int childIndex = anserButtonsObj.transform.childCount; // 子供の数
        int saveIndex = 0;
        for (int i = 0; i < childIndex; i++) {
            Transform anserButton = anserButtonsObj.transform.GetChild (i);
            if (anserButton.name.StartsWith ("AnserButton")) { // 名前の先頭が"AnserButton"だったら
                // TMPと答え確認スクリプトを格納
                choiceSelectsTMP[saveIndex] = anserButton.GetChild (0).GetComponent<TextMeshProUGUI> ();
                answerChecker[saveIndex] = anserButton.GetComponent<AnswerChecker> ();
                saveIndex++;
            }
        }
    }

    // 問題の選択
    public void ChoiceQuestion () {
        // データ数でランダム
        int choiceNum = UnityEngine.Random.Range (0, csvData.Count);
        questionObj = new QuestionObject (choiceNum);

        // 選択された問題から問題形式の取得と問題文、答えのセット
        int questionFormatNum = Int32.Parse (csvData[choiceNum][0]);
        string questionText = generator.QuestionFormatSwicher (questionFormatNum, csvData[choiceNum][1]);
        questionObj.questionText = questionText;
        questionObj.answerText = csvData[choiceNum][2];
        //Debug.Log ("question:" + questionText);

        // 選択肢のセット
        int[] indexsRange = questionObj.SelectRange ();
        for (int i = 0; i < indexsRange.Length; i++) {
            questionObj.choices[i] = csvData[indexsRange[i]][2];
            //Debug.Log ("choice" + i + ":" + choiceTexts[i]);
        }
    }

    // 問題文セット
    public void SetTexts () {
        List<int> selectRand = new List<int> () { 0, 1, 2, 3 };
        int choiceLength = questionObj.choices.Length;

        // 問題文のセット
        questionTextTMP.text = questionObj.questionText;

        // 選択肢のセット  selectrandからindexをランダム選定、選ばれたものはremoveする
        for (int i = 0; i < choiceLength; i++) {
            int randomNum = UnityEngine.Random.Range (0, selectRand.Count);
            choiceSelectsTMP[i].text = questionObj.choices[selectRand[randomNum]];
            // 選択肢の識別番号を渡す
            answerChecker[i].setChoiceIndex (selectRand[randomNum]);

            //Debug.Log("choice:" + randomNum);
            selectRand.RemoveAt (randomNum);
        }
    }

    // ボタンタップ時の正誤チェック
    public void CheckChoice (int index) {
        if (index == 0) {
            Debug.Log ("正解");
        } else {
            Debug.Log ("不正解");
        }
    }

    // 問題生成
    public void QuizLoad () {
        // タイマーのリセット
        if (timerCol != null) {
            StopCoroutine (timerCol);
            timerCol = null;
        }

        // タイマー開始
        timerCol = timer.TimerCorutine ();
        StartCoroutine (timerCol);

        // 正誤チェック用クラス
        //answerChecker.SetQestion (questionObj);

        // 問題文の選択とUIへの反映
        ChoiceQuestion ();
        SetTexts ();
    }
}