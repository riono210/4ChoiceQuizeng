using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionGenerator : MonoBehaviour {

    // 問題形式判定
    public string QuestionFormatSwicher (int formatNum, string keyword) {
        string problemStatement;  // 問題文
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

}