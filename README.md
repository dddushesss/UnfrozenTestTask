# UnfrozenTestTask
Павлов Алексей 
https://spb.hh.ru/resume/d701a9feff08d94e7c0039ed1f5a5751794c5a

- Есть два отряда по 4 юнита, стоят друг напротив друга.
- Бой пошаговый. Каждый юнит ходит один раз за раунд.
- Порядок хода — рандомный.
- Есть две кнопки действия: атаковать противника и пропустить ход.
- При нажатии “атаковать” можно выбрать кого атаковать.
- При ударе атакующий юнит и его цель выезжают на передний план.
- Анимация из Spine, соответственно надо разобраться с их библиотекой.

Версия Unity - 2020.3.26f1

## Архитектура

Есть точка входа GameController, в котором указываются ScriptableObject со ссылками на данные игры и объект на сцене типа MapView (в который надо добавить родительский объект для спавнеров и префаб спавнера. Там же с помощью кнопки можно добавить эти самые спавнеры).

Чтобы не имплементировать Mononbehaviour каждый раз, когда требуется UnityEvent, реализован контроллер, который реализует все UnityEvet в одном monobehaviour. В данном проекте он нужен только для подчищением за Singleton'ами. Персонажи настраиваются созданием нового ScriptableObject CharacterData. Он собирается в лист CharactersListData, откуда рандомно отдаются CharactorFactory, которая так же хранит список персонажей игрока и противника.

Далее списки персонажей и контроллер интерфейса передаётся в StateMachine, которая котролирует сам бой.

