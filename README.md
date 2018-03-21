<h2 align="center">Управление компьютером с пульта ДУ через Com порт.</h2>

### Папки и файлы:

    Firmware   файл с прошивкой микроконтроллера.
    Hard       Электронные схемы и печатные платы для PCAD, а также файлы со схемой в формате pdf.
    Pictures   Фото устройства и скриншоты работы программы.
    Proteus    Умуляция работы устройства в прогамме Proteus.
    Software   Проект для Visual Studio на языке С#.
    Source     Проект для MPLAB IDE V8, написан на СИ и скомпилирован с помощью MPLAB C18 V3.
    README.md  Этот файл.
    
### Первый этап - устройсто на МК

Декодировать сигнал с ДУ будет микроконтроллер и чтобы поместился в разъем Com порта, например PIC12F675.

Схему и плату развел в PCAD. Программу написал на Си, под компилятор PICC в среде MPLAB.

Т.к. в наличии были только ДУ с протоколами NEC, SIRC и RC5, то в коде описываю только их.

![hard](https://github.com/nva1773/Irda-To-Uart/blob/master/Pictures/Hard.jpeg)

Кратко о алгоритме работы:
```
при подаче питания производим выдержку времени в 1 секунду, и в основном цикле выдаем в UART 4 байта ("OKOK"),
разрешаем прерывания по изменению на входе подключенному к фотоприемнику и зацикливаемся в ожидании.
В прерывании организованна state machine:
  При возникновении прерывания по спаду запускаем таймер, для отсчета времени в мили секундах, он же 
  обеспечивает таймаут, и переходим к фазе приема стартового импульса преамбулы.
  При возникновении следующего прерывания по фронту проверяем длину импульса преамбулы и если если все ОК,
  то переходим к следующей фазе и т.д. пока не приняты все биты посылки.
По окончанию приема запрещаем прерывание и разрешаем передачу в UART.
```

### Второй этап - программа для ПК

К программному модулю следующие требования – должна быть простой, маленькой (только .exe),

без библиотек (имеется ввиду сторонние .dll), при автостарте прятаться в трее,

все настойки должны быть в одном окне с сохранением параметров в реестре.

Среда разработки “Visual Studio” - выбрал из самых доступных (бесплатных), скачал c сайта Microsoft,

установил только С#, чтобы занимала меньше места. Вот что получилось:

![soft](https://github.com/nva1773/Irda-To-Uart/blob/master/Pictures/Soft.jpeg)

```
При первом запуске программа ищет записи о себе в реестре, если не нашли то запускаем окно конфигурации.
Здесь нужно выбрать Сом порт, к которому подключено устройство и открыть его нажав кнопку "Open". 
Если устройство подключено и порт открыт, то в окне сообщений появится надпись "Device OK".
Нажимаем кнопки пульта и сохраняем нужные Вам команды нажав соответствующую кнопку блока "Set Command".
Теперь закрыв окно настройки программа будет посылать активному приложению код нажатия соответствующей клавиши 
клавиатуры (Enter, Esc, Left ...) приняв ее по Сом порту.
Если в контекстном меню выбрать опцию "Show message" то во всплывающей подсказки будут отображаться сообщения
о состоянии пота, а также о принятых данных.
При вторичном запуске программы она сворачивается в трей (ни какие окна при этом не показываются),
вычитывает настойки из реестра и при удачном соединении с устройством готова к работе.
Т.е. в принципе пользователь ее присутствие заметит только если оставить включенной опцию "Show message".
```

#### Также данный проект можно посмотреть на [форуме](http://forum.easyelectronics.ru/viewtopic.php?f=16&t=25608).
