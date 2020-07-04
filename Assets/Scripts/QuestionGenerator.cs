using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionGenerator : MonoBehaviour {

    // 問題形式判定
    public string QuestionFormatSwicher (int formatNum, string keyword) {
        string problemStatement;
        switch (formatNum) {
            case 0:
                problemStatement = "「" + keyword + "」を日本語でいうと何？";
                break;

            default:
                problemStatement = "問題形式エラーやで";
                break;
        }

        return problemStatement;
    }

    // 選択肢の選択
    public string[] ChoicesSelector (int index, List<string[]> csvdata) {
        string[] choiceTexts = new string[4];
        int otherIndex = index;

        // 正解を0番目に格納
        choiceTexts[0] = csvdata[index][2];

        otherIndex = 3;

        // indexで2番目以下の時
        if (index - 2 <= 0) {
            int removeIndex = 1;
            for (int i = 0; i < 4; i++) {

                if (i != index) {
                    choiceTexts[removeIndex] = csvdata[i][2];
                    removeIndex++;
                }
            }

        } else {
            // 正解以外の別の選択肢を格納
            for (int i = 1; i < 4; i++) {
                choiceTexts[i] = csvdata[index - i][2];
            }
        }

        return choiceTexts;
    }
}