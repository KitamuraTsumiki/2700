# 2700
2017.3.1f1


```uml
@startuml
#AAAAAA:開始フェイズ;
note right
  Lane3の少し離れた位置にトラックが止まっている
  目の前(Lane1)にもトラックが止まっている
  トラックにはコンテナが乗っている
　コンテナにはアームがついている
  (任意のタイミングでフェイズをスキップできるようにする)
end note
:コンテナアームが上がる;
:状況と作業についてのナレーション;
:目標とする停止線UIを表示;
#AAAAAA:停止誘導フェイズへ;
@enduml
```

```uml
@startuml
#AAAAAA:停止誘導フェイズ;
 :プレイヤーが次のトラックに気づくよう誘導UI;
 :トラックがLane3の向こうからLane1に入ってくる;
if (プレイヤーが指定範囲から出そう) 
  : YES}
  :範囲に移動するように指示UI;
else
  : NO}
endif
if (プレイヤーが指定範囲から出た)
  : YES}
  #AAAAAA:正しい誘導位置の講習フェイズへ;
  detach
else
  : NO}
endif
:トラックが正しい位置で停止する;
:正しい位置に停止できていることをプレイヤーが確認する;
#AAAAAA:事故発生フェイズ;
@enduml
```

```uml
@startuml
#AAAAAA:事故発生フェイズ;
note right
  プレイヤーが誘導完了を確認しているとき
  Lane2に別のトラックが侵入開始している
end note
if (プレイヤーがLane2のトラックを確認する)
  :NO}
  :接触直前にLane2ドライバークラクション;
  :ドライバーが急ブレーキを踏む;
  :積載部がプレイヤーを巻き込むように横滑りする;
else
  :YES}
  :プレイヤーに触れない程度の位置でトラック停止;
  #AAAAAA:講習フェイズへ;
  detach
endif

if (プレイヤーとトラックが接触する)
  :YES}
note right
　トラックの急ブレーキ時
  接触判定を少し大きめにとる
end note
  :衝撃・転倒・暗転;
  #AAAAAA:リプレイフェイズへ;
detach
else
  :NO}
  :Lane2トラック停止;
  :Lane2運転手、プレイヤーを怒鳴る;
  #AAAAAA:講習フェイズへ;
detach
endif
@enduml
```
