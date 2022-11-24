using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryTest : StudyBase
{
    protected override void OnLog()
    {
        var map = new Dictionary<int, string>();

        // map[101] = "김민준";
        // map[201] = "윤서준";
        // map[101] = "박민준";
        // [101, 박민준], [201, 윤서준]
        map.Add(102, "이주석");
        map.Add(102, "저스틴");
        map.Add(103, "김태희");
        map.Add(104, "도민준");


        map.Add(102, "루피");
        map.Add(102, "조로");
        map.Add(102, "나루토");
        map.Add(102, "사스케");
        map.LogValues();

        // map.Add(302, "김도윤");
        // // map.Remove(101);
        // map.Add(102, "서예준");
        // // [102, 서예준], [201, 윤서준], [302, 김도윤]
        // map.LogValues();
    }
}
