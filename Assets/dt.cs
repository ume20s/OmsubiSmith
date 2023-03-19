using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dt : MonoBehaviour
{
    // おむすび名配列
    public static readonly string[] Omsubi =
    {
        "",
        "手ほぐし紅ジャケ",
        "シャケバター",
        "シャケ親子（紅シャケ＆筋子）",
        "無添加梅干し",
        "山わさび醤油漬け",
        "山わさびクリームチーズ",
        "塩むすび",
        "みそらんおう",
        "生たらこ",
        "生たらこバター",
        "すじこ",
        "昆布漬辛子明太子",
        "塩ハスカップ",
        "松前漬け",
        "紅鮭切り込み",
        "切込み親子",
        "ぬかニシン",
        "サバのへしこ",
        "サバのへしこクリームチーズ",
        "なっとう",
        "梅なっとう",
        "クリームチーズなっとう",
        "山わさびなっとう",
        "らんおうなっとう",
        "鶏そぼろ",
        "しょうゆツナ",
        "ツナクリームチーズ",
        "ツナ山わさび",
        "ツナマヨ",
        "爆弾おむすび（味玉１個入）",
        "ポークたまご（しょうゆ）",
        "ポークたまご（マヨネーズ）",
        "ポークたまご（山わさび）"
    };

    // 素材とおむすびの関係配列
    public static readonly int[,] makeOmsubi =
    {
        {  7,  1, 26, 18, 17, 14,  5, 13,  4, 31,  0, 11,  9, 12, 15, 30, 8, 25, 20,   0,  0 },
        {  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  3,  0,  0,  0,  0,  0,  0,  0,  2,  0 },
        { 26,  0,  0,  0,  0,  0, 28,  0,  0,  0, 27,  0,  0,  0,  0,  0,  0,  0,  0,  0, 29 },
        { 18,  0,  0,  0,  0,  0,  0,  0,  0,  0, 19,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0 },
        { 17,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0 },
        { 14,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0 },
        {  5,  0, 28,  0,  0,  0,  0,  0,  0, 33,  6,  0,  0,  0,  0,  0,  0,  0, 23,  0,  0 },
        { 13,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0 },
        {  4,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 21,  0,  0 },
        { 31,  0,  0,  0,  0,  0, 33,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 32 },
        {  0,  0, 27, 19,  0,  0,  6,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 22,  0,  0 },
        { 11,  3,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 16,  0,  0,  0,  0,  0,  0 },
        {  9,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 10,  0 },
        { 12,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0 },
        { 15,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 16,  0,  0,  0,  0,  0,  0,  0,  0,  0 },
        { 30,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0 },
        {  8,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 24,  0,  0 },
        { 25,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0 },
        { 20,  0,  0,  0,  0,  0, 23,  0, 21,  0, 22,  0,  0,  0,  0,  0, 24,  0,  0,  0,  0 },
        {  0,  2,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 10,  0,  0,  0,  0,  0,  0,  0,  0 },
        {  0,  0, 29,  0,  0,  0,  0,  0,  0, 32,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0 }
    };

    // 客セリフ配列
    public static readonly string[,,] guestTalk =
    {
        {{"私に","を作ってください"},
            {"俺に","を作ってくれ"},
            {"そそそー、","ください"},
            {"ボクに","を作ってください"},
            {"","ください"},
            {"","を作ってくれたまえ"}},
        {{"わーい","くださーい"},
            {"俺に","を作ってくれ"},
            {"そそそー、","そそそー"},
            {"・・・","・・・"},
            {"ねぇねぇ、","作ってくれるかな"},
            {"やぁ、","をくださいな"}},
        {{"おい、","を作ってくれ"},
            {"","をくれ"},
            {"こんにちは、","をください"},
            {"あら、","がほしいわ"},
            {"やっほー、","ちょーだい"},
            {"そーですねー、","がほしいかな？"}},
        {{"美味しい","をくださいね"},
            {"うちゅ〜、","を作ってちゅ〜"},
            {"","を作ってくださるかしら"},
            {"エレレー、","ちょーだいっ"},
            {"おむすび、","ください"},
            {"いいねぇ、","をくれないか"}},
        {{"わたしに","作ってくださいな"},
            {"","を作ってー"},
            {"ボクに","を作ってほしいな"},
            {"ワハハハハ","をワハハハハ"},
            {"あーらワタクシ、","が欲しいかしら"},
            {"んー、","がいいかな"}},
        {{"そーですねー、","をください"},
            {"そーですねー、","くーださいっ"},
            {"そーですねー、","ほしいわ"},
            {"そーですねー、","チョーダイ！"},
            {"ソウデスネ、","ヲチュウモンシマス"},
            {"そーですねー、","くださいな"}},
        {{"","クダサイ"},
            {"","クダサイ"},
            {"","クダサイ"},
            {"","クダサイ"},
            {"","クダサイ"},
            {"","クダサイ"}},
        {{"ふふふふ","ふふふふ"},
            {"フヒャ、","フヒャフヒャ"},
            {"へろへろ","へろへろへろ"},
            {"ウキー！","ギギギギ"},
            {"ああああ","ああああ"},
            {"ドロォオ・・・","・・・"}}
    };
}
