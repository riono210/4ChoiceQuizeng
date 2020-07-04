using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayQuestion : MonoBehaviour {

    [HideInInspector] public TextAsset csvflie; // 答えと選択肢が対になっているcsvファイル
    [HideInInspector] public List<string[]> csvData = new List<string[]> (); // csvファイルを読み取ったリスト

    // クイズの形式クラス
    public QuestionGenerator generator;
    public QuizeManager.QuestionsObject questionClass = new QuizeManager.QuestionsObject (); // 選択された問題文と選択肢を格納したクラス

    // タイマー
    public Timer timer;
    public IEnumerator timerCol = null;

    // 問題文と選択肢
    public TextMeshProUGUI[] choiceSelectsTMP = new TextMeshProUGUI[4];
    public TextMeshProUGUI questionTextTMP;

    // 正誤確認
    public AnswerChecker[] answerChecker = new AnswerChecker[4];

    private void Start () {
        LoadCSVFile ();
        ChoiceQuestion ();
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

    // 問題の選択
    public void ChoiceQuestion () {
        // データ数でランダム
        int choiceNum = UnityEngine.Random.Range (0, csvData.Count);
        //Debug.Log ("num:" + csvData.Count + "random:" + choiceNum);

        // 選択された問題から問題形式の取得と問題文、答えのセット
        int questionFormatNum = Int32.Parse (csvData[choiceNum][0]);
        string questionText = generator.QuestionFormatSwicher (questionFormatNum, csvData[choiceNum][1]);
        questionClass.questinText = questionText;
        questionClass.answerText = csvData[choiceNum][2];
        //Debug.Log ("question:" + questionText);

        // 選択肢のセット
        string[] choiceTexts = generator.ChoicesSelector (choiceNum, csvData);
        for (int i = 0; i < choiceTexts.Length; i++) {
            questionClass.choices[i] = choiceTexts[i];
            //Debug.Log ("choice" + i + ":" + choiceTexts[i]);
        }
    }

    // 問題文セット
    public void SetTexts () {
        List<int> selectRand = new List<int> () { 0, 1, 2, 3 };
        int choiceLength = questionClass.choices.Length;

        // 問題文のセット
        questionTextTMP.text = questionClass.questinText;

        // 選択肢のセット  selectrandからindexをランダム選定、選ばれたものはremoveする
        for (int i = 0; i < choiceLength; i++) {
            int randomNum = UnityEngine.Random.Range (0, selectRand.Count);
            choiceSelectsTMP[i].text = questionClass.choices[selectRand[randomNum]];
            // 選択肢の識別番号を渡す
            answerChecker[i].setChoiceIndex(selectRand[randomNum]);

            //Debug.Log("choice:" + randomNum);
            selectRand.RemoveAt (randomNum);
        }
    }

    // ボタンタップ時の正誤チェック
    public void CheckChoice (int index) {
       if(index == 0) {
           Debug.Log("正解");
       }else{
           Debug.Log("不正解");
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
        //answerChecker.SetQestion (questionClass);

        // 問題文の選択とUIへの反映
        ChoiceQuestion ();
        SetTexts ();
    }
}