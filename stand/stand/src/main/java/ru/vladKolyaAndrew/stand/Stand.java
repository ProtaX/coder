package ru.vladKolyaAndrew.stand;

import org.springframework.stereotype.Component;

/**
 * Created by vladislavZag on 10/03/2021.
 */

// Модель стенда
@Component
public class Stand extends Thread {
    int x = 0;
    int y = 0;
    int z = 0;

    // текущее состояние станка
    String currentCommand = "stand.stop_knife";
    String currentDircetion = "x";
    int currentIncrement = 1;

    public Stand(int x, int y, int z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Stand() {
    }

    // Обработка пришедшей команды и изменение текущего состояния станка
    public void handleCommand(KnifeCommand command) {
        if (!this.isAlive() && !this.isInterrupted()) this.start();
        if (command.method.equals("stand.stop_knife")) {
            currentCommand = command.method;
        }
        if (command.method.equals("stand.move_knife")) {
            currentCommand = command.method;
            currentDircetion = command.params.direction.toLowerCase();
            currentIncrement = command.params.isPositive()? 1 : -1 ;
        }
    }

    private void performCommand() {
        if (currentCommand.equals("stand.move_knife")) {
            if (currentDircetion.equals("x")) x += currentIncrement;
            if (currentDircetion.equals("y")) y += currentIncrement;
            if (currentDircetion.equals("z")) z += currentIncrement;
        }
    }

    // станок в отдельном потоке
    @Override
    public void run() {
        while(true){
            performCommand();
            System.out.println("x:" + x + " y:" + y + " z:" + z);
            try {
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }
}
