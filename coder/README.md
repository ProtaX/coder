# coder
Программатор принимает команды из графического интерфейса, запоминает параметры и программный код, передает управляющие коды объекту управления.
## Чтение программы
Программатор читает программный код и должен читать следующие команды:
* Переместить нож на `VALUE` по оси `AXIS`
```
SET <AXIS> <VALUE>
```
* Цикл по `I` от `START` до `END`
```
FOR <I> FROM <START> TO <END>
  ...
END
```