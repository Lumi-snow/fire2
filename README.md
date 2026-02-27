# fire Remake
## UMLクラス図
<iframe src="https://www.edrawmax.com/online/share.html?code=0651acbe13a011f1ab380a951ba8b83d&embed=1" frameborder="0" sandbox="allow-scripts allow-popups allow-top-navigation-by-user-activation allow-forms allow-same-origin allow-storage-access-by-user-activation allow-popups-to-escape-sandbox" allowfullscreen style="width: 768px; height: 432px; background-color: white;"></iframe>

## コーディング規約
### 命名規則
* クラス / 構造体名: パスカルケース(例: PlayerController)
* メソッド名: パスカルケース(例: Attack())
* プロパティ: パスカルケース(例: CurrentHealth)
* public変数: パスカルケース(例: MaxSpeed)
* private変数: _ + キャメルケース(例: _currentScore)
* ローカル変数: キャメルケース(例: damageAmount)
* 引数: キャメルケース(例: targetPosition)
* インターフェイス: I + パスカルケース(例: IDamageable)
* 定数: スネークケース(例: MAX_HEALTH)

## アーキテクチャ
* MVP + Clean Architecture

## 技術スタック
* DI: VContainer
* Reactive: R3
* Async: UniTask
* Asset Management: Addressables
