# stand
Объект управления - строгальный станок. Нож станка может перемещаться в плоскости XY, а также выдвигаться вдоль оси Z.
## Управление станком с программатора
Управление осуществляется через управляющие коды. Код содержит информацию, в каком направлении необходимо двигаться ножу и на какое расстояние.
Направление движения может быть вдоль одной из осей XYZ:
```
{
  "jsonrpc": "2.0",
  "method": "stand.move_knife",
  "params": {
    "direction": <string>,
    "value": <number>
  }
}
```
  Где направление может быть "x", "y", "z".

## Получение координат ножа
Если координаты ножа меняются, то модуль отправляет данные об этом в графсический интерфейс:
```
{
  "jsonrpc": "2.0",
  "method": "display.set_knife_position",
  "params": {
    "x": <number>,
    "y": <number>,
    "z": <number>
  }
}
```
Где x, y, z - новые координаты ножа.