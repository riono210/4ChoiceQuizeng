using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnswerChecker : MonoBehaviour {

    public int ChoiceIndex;    // 選択肢の識別番号
    public QuizManager displayQuestion;


    // ボタンクリックすると呼ばれる　押されたことを通知して正解かをチェックしたい
    public void ClieckBtn() {
        displayQuestion.CheckChoice(ChoiceIndex);
    }

    // 選択肢の識別番号をセット
    public void setChoiceIndex(int index) {
        ChoiceIndex = index;
    }
    

}