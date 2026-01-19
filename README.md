# fire2

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
