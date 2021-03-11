package ru.vladKolyaAndrew.stand;

import org.springframework.stereotype.Component;

import java.io.IOException;

/**
 * Created by vladislavZag on 10/03/2021.
 */

// Модель стенда
@Component
public class Stand extends Thread {
    int x = 0;
    int y = 0;
    int z = 0;

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

    // Изменение текущей команды
    public void handleCommand(KnifeCommand command) {
        if (!this.isAlive() && !this.isInterrupted()) this.start();
        if (command.method.equals("stand.stop_knife")) {
            currentCommand = command.method;
        }
        if (command.method.equals("stand.move_knife")) {
            currentCommand = command.method;
            currentDircetion = command.params.direction.toLowerCase();
            currentIncrement = command.params.isPositive()? 3 : -3 ;


        }

        if (command.method.equals("display.set_knife_position")) {
            System.out.println(
                    "display.set_knife_position x y z: "
                            + command.params.getX()
                            + " "
                            + command.params.getY()
                            + " "
                            + command.params.getZ());
        }
    }

    // Выполнение текущей команды
    private void performCommand() {
        if (currentCommand.equals("stand.move_knife")) {
            if (currentDircetion.equals("x")) {
                StartFrame.mainPanel.lines.add(new Line(x,y,x+currentIncrement,y));
                x += currentIncrement;

            }
            if (currentDircetion.equals("y")) {
                StartFrame.mainPanel.lines.add(new Line(x,y,x,y+currentIncrement));
                y += currentIncrement;
            }
            if (currentDircetion.equals("z")) {
                Line.changeCurrentAlpha(-1 * currentIncrement);
                z = 255 - Line.currentAlpha;
            }
            System.out.println("x:" + x + " y:" + y + " z:" + z);
            try {
                Requester.sendStandData(x,y,z);
            } catch (IOException e) {
                e.printStackTrace();
            }
            //StartFrame.mainPanel.redraw();
        }
    }

    @Override
    public void run() {
        while(true){
            performCommand();

            try {
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }
}
