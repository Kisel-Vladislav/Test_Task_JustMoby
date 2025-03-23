


https://github.com/user-attachments/assets/02754941-59ef-4682-a166-21491c34a945





Максимальное время на выполнение - 1 неделя.
Ваша работа не будет использоваться нами в коммерческих целях, она не оплачивается.

Для выполнения задания используйте Unity 2022.3, последнюю из LTS.

# Описание:
⭐Нижняя часть экрана - горизонтальный скролл из 20 прямоугольников (далее - кубики)  разных цветов.

⭐Любой из кубиков можно перенести в правую верхнюю часть и поставить друг на друга в виде башни с условиями:

⭐первый кубик можно поставить в любое место правой верхней части экрана,

⭐остальные кубики ставятся в башню только если их перенесли поверх уже выставленных, в этом случае они анимацией подпрыгивают и ставятся сверху башни,

⭐если игрок перенес кубик промазав мимо башни, кубик исчезает или взрывается, желательно анимированно,

⭐кубики ставятся поверх предыдущих в башню с случайным смещением по горизонтали, но не более 50% длины грани,

⭐ограничением по высоте является высота экрана устройства, если последний кубик вышел за ее границу, то больше установить становится нельзя,

⭐кубики внизу бесконечные и не пропадают после перетаскивания наверх,

⭐физика не используется, только анимации.

⭐В любой момент кубик из башни можно выкинуть в дыру, перенеся его на ее изображение в левой верхней части экрана. В этом случае, те что были выше башне плавно опускаются вниз анимацией. Желательно учесть ее овальные границы при оценке попал ли игрок в нее, перетаскивая кубик.

⭐Под каждой частью экрана свой фон.

⭐Каждое действие (установка кубика, выкидывание кубика, пропадание кубика, ограничение по высоте) должно комментироваться надписью над нижней частью экрана.

⭐Конфигурация игры должна содержать минимально: количество кубиков внизу и их цвета.



# Требования:
⭐Код должен предусматривать масштабирование для будущих обновлений (к примеру нужно будет переносить новый кубик только на кубик такого же цвета в башне).

⭐Предусмотрите возможность локализации (можно без самой системы).

⭐Нужно учесть что источником конфигурации игры могут стать разные источники данных (в игре может быть 1 реализация - из ScriptableObject).

⭐Drag & drop кубиков должен работать корректно с учетом скролла (не должно быть ситуаций, когда при начале перетаскивания скроллируется нижняя полоса).

⭐Должна использоваться как минимум 1 анимация (Unity Animation или DoTween).

⭐Прогресс сохраняется между сессиями.

⭐Используйте минимально необходимый набор графики, чтобы оценить ее визуально.

⭐Необходимо чтобы мини-игра выглядела хорошо на основных соотношениях сторон мобильных устройств: 19.5x9, 16x9, 4x3.

Приоритеты оценки тестового задания:
Самый большой приоритет при оценке вашего тестового это п.1-4 требований.
Использование DI контейнеров (Zenject и т.п.), UniRx является плюсом.
Необработанные крайние состояния, краши и ошибки при исполнении игры - большой минус.

Результат желательно прислать в виде ссылки на репозиторий Git.
Если нет опыта работы с Git, то в виде ссылки на архив лежащий в каком-либо облачном хранилище.
