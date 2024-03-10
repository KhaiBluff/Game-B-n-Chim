using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefs 
{
    //dữ liệu còn lưu lại cho người dùng
    public static int bestScore_1{
        //lấy đc số điểm cao nhất của người chơi khi lưu vào bestcore và dl k tồn tại nó lưu là 0
        get=>PlayerPrefs.GetInt(GameConst.BEST_SCORE_1,0);
        set
        {
            //biến đã đc lưu trong bộ nhớ
            int curScore=PlayerPrefs.GetInt(GameConst.BEST_SCORE_1);
            //điểm số mới lớn hơn điểm số cao nhất cũ thì sẽ đc lưu lại
            if(value > curScore)
            {
                PlayerPrefs.SetInt(GameConst.BEST_SCORE_1, value);

            }

        } 

    }

    //dữ liệu còn lưu lại cho người dùng
    public static int bestScore_2
    {
        //lấy đc số điểm cao nhất của người chơi khi lưu vào bestcore và dl k tồn tại nó lưu là 0
        get => PlayerPrefs.GetInt(GameConst.BEST_SCORE_2, 0);
        set
        {
            //biến đã đc lưu trong bộ nhớ
            int curScore = PlayerPrefs.GetInt(GameConst.BEST_SCORE_2);
            //điểm số mới lớn hơn điểm số cao nhất cũ thì sẽ đc lưu lại
            if (value > curScore)
            {
                PlayerPrefs.SetInt(GameConst.BEST_SCORE_2, value);

            }

        }

    }

    //dữ liệu còn lưu lại cho người dùng
    public static int bestScore_3
    {
        //lấy đc số điểm cao nhất của người chơi khi lưu vào bestcore và dl k tồn tại nó lưu là 0
        get => PlayerPrefs.GetInt(GameConst.BEST_SCORE_3, 0);
        set
        {
            //biến đã đc lưu trong bộ nhớ
            int curScore = PlayerPrefs.GetInt(GameConst.BEST_SCORE_3);
            //điểm số mới lớn hơn điểm số cao nhất cũ thì sẽ đc lưu lại
            if (value > curScore)
            {
                PlayerPrefs.SetInt(GameConst.BEST_SCORE_3, value);

            }

        }

    }
}
