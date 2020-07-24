using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionObject { // 選択されたクイズのクラス
    public string questionText;
    public string answerText;
    public string[] choices;
    public int questionIndex;

    public QuestionObject (int index) {
        questionText = null;
        answerText = null;
        choices = new string[4];
        questionIndex = index;
    }

    public int[] SelectRange () {
        int[] range = new int[4] { 0, 1, 2, 3 };
        if (questionIndex <= 2) {  // indexが2以下だった場合、入れ替えをして正解を0番目に持ってくる
            for (int i = 0; i < 4; i++) {
                if (questionIndex == i) {
                    int tmp = range[0];
                    range[0] = questionIndex;
                    range[i] = tmp;
                }
            }
        } else { // 選択された問題のindexより降順に選択肢が選ばれる ex:index=34    33,32,31
            for (int i = 0; i < 4; i++) {
                range[i] = questionIndex - i;
            }
        }
        return range;
    }
}