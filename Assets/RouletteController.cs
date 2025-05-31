using UnityEngine;

// ポップアップ追加分
//  UI Toolkit を使うときに必要な宣言
// ボタン、ラベル、テキストフィールド、ポップアップウィンドウ、スライダーなどを提供している
using UnityEngine.UIElements;


// このファイルの「guid」は「69b19b05715069b42b8364b4e2563282」
// （「Roulette\Assets\RouletteController.cs.meta」より）

// シーンを保存することで「シーン名.unity」が作成され、そこに現状のオブジェクト情報やアッタッチ状況が記録される
// 今回は「Roulette\Assets\RouletteScene.unity」に記録

// この RouletteController は、ゲームオブジェクト「roulette_0」にアタッチしている
// 「roulette_0」は「RouletteScene.unity」にてID「1019421225」と割り当てられており、

// ID「1019421225」をたどっていくと、「MonoBehaviour:」にて
// m_Script: {fileID: 11500000, guid: 69b19b05715069b42b8364b4e2563282, type: 3} とアタッチされていると記録がある

// こんな感じで内部的にはID管理らしい



public class RouletteController : MonoBehaviour
{
    // 【教本通り】------------------------------------------------------------------
    // ルーレットの回転速度（初期値：0）
    //float rotSpeed = 0;

    //void Start()
    //{
    //    // FPS（1秒間に何フレーム使用するか）
    //    // 1秒間に60フレーム分、つまり1秒間に60回update()メソッドを呼ぶ
    //    // しかし、確定ではない、処理の重さや機材環境次第では予定通り全て発火しない可能性もあり
    //    Application.targetFrameRate = 60;

    //    // Unityは毎フレーム画面を描画して処理を進める
    //    // Unityが毎秒何回フレームを更新しようとするかを指定する（FPS と呼ぶ）
    //    // 更新頻度（フレームレート）を60回／秒に固定（毎秒60フレームを目指す（1フレーム ≒ 0.016秒））
    //    // この設定は「目標」であって「保証」ではない、処理が重すぎるとfpsは落ちる（=60出せない）
    //    // 30：遅い、240：速い
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    // マウスが左クリックされた瞬間による発火
    //    // 0：左クリック
    //    // 1：右クリック
    //    // 2：中ボタンクリック
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        // 回転速度を10にセットして、ルーレットの回転を開始（1フレーム、10度）
    //        // 「f」は省略可能、this.rotSpeed = 10
    //        this.rotSpeed = 10f;
    //    }

    //    // オブジェクトを回転方向（Z軸）に rotSpeed度だけ回す
    //    // (0, 0, this.rotSpeed)、(X軸, Y軸, Z軸)、(縦回転, 横回転, 時計回り)
    //    // 「-（マイナス）」を付けることで回転方向が変わる
    //    transform.Rotate(0, 0, this.rotSpeed);

    //    // 回転速度を毎フレーム96%に減らす（＝4%ずつ減速）
    //    this.rotSpeed *= 0.96f;
    //}
    // 【以上、教本通り】------------------------------------------------------------------




    // 【アレンジ】------------------------------------------------------------------

    float rotSpeed = 0;

    // ルーレットの停止と開始を管理するフラグ
    bool isSpinning = false;

    // ポップアップの表示状態を管理するフラグ
    // 今回は「popup.style.display = DisplayStyle.None;」で確認でき、
    // 不要であるが、複雑になる場合はあると便利なので作成
    bool hasStopped = false;

    // UIDocument：UI Toolkit で作成したUIレイアウトをシーン上に表示するためのコンポーネント
    // UIDocument は UI Toolkit 専用のコンポーネント

    // UIDocument コンポーネントを受け取るための変数
    // Unityエディタのインスペクターで割り当てる
    // Unityエディタで UIDocument を持つオブジェクトを、ドラッグ＆ドロップで割り当てることで、
    // C#側から拡張子「.uxml」の UI にアクセスできる
    public UIDocument uiDocument;

    // 側から拡張子「.uxml」のファイルの中で、
    // ID が "popup" の UI 要素を参照するための変数
    private VisualElement popup;


    void Start()
    {
        // 初めに「UIDocumentがあるか確認」する
        // UIDocument が null の状態で、その下の「uiDocument.rootVisualElement」にアクセスすると、
        // そこで 「NullReferenceException」 が発生してゲームが止まる

        // 受け取れるUIDocumentがあるか確認、ないなら発火しない
        if (uiDocument == null)
        {
            return;
        }

        // UIDocumentに割り当てた情報を取得
        // rootVisualElement：UXMLファイルのルート（最上位）要素にアクセスするためのプロパティ
        //  UIツリーを自動的に探索して該当の要素を見つけるためパス指定は不要
        var root = uiDocument.rootVisualElement;

        // UXMLの中で name="popup" もしくは id="popup" と指定された要素を検索し、
        // popup という変数に代入
        popup = root.Q<VisualElement>("popup");

        // popupがあるか確認、ないなら発火しない
        if (popup == null)
        {
            return;
        }

        // ポップアップフラグを更新
        hasStopped = false;

        // UI要素を非表示にする命令
        // popup というUI要素（VisualElement）を画面から非表示にする
        popup.style.display = DisplayStyle.None;
    }


    void Update()
    {
        // Start()でUIDocumentとpopupの確認をしているので、Update()では不要

        // deltaTime を使う場合、fps に関係なく一定速度で回転する

        if (Input.GetMouseButtonDown(0))
        {
            if (isSpinning)
            {
                // 回転中にクリック、停止
                isSpinning = false;
                this.rotSpeed = 0;

                // ポップアップフラグを更新
                hasStopped = true;

                // UI要素を非表示にする命令
                // popupという要素を表示する
                popup.style.display = DisplayStyle.Flex;
            }
            else
            {
                // 停止中にクリック、開始
                isSpinning = true;

                // deltaTimeを使用するため、ここでは
                // 1秒間で達成したい角度を入れる
                // 今回は1秒で360度とする
                this.rotSpeed = 360;

                // ポップアップフラグを更新
                hasStopped = false;

                // UI要素を非表示にする命令
                // popup というUI要素（VisualElement）を画面から非表示にする
                popup.style.display = DisplayStyle.None;
            }
        }


        if (isSpinning)
        {
            // Time.deltaTime：前のフレームからの経過時間（秒）
            transform.Rotate(0, 0, this.rotSpeed * Time.deltaTime);

            // 毎秒10%減らす、1秒後には0.9倍にする
            // Mathf.Pow：べき乗
            this.rotSpeed *= Mathf.Pow(0.9f, Time.deltaTime);

            // スピードが1fを切ったら0にして完全に止める
            // もしFPSを使用するなら「this.rotSpeed < 0.01f」くらいがいい
            if (this.rotSpeed < 50f)
            {
                this.rotSpeed = 0;
                isSpinning = false;
                hasStopped = true;
                popup.style.display = DisplayStyle.Flex;
            }
        }

    }
}
