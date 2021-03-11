package ru.vladKolyaAndrew.stand;

import javax.swing.*;
import java.awt.*;
import java.awt.image.BufferedImage;
import java.util.ArrayList;

/**
 * Created by vladislavZag on 11/03/2021.
 */
public class DrawPanel extends JPanel implements Runnable {
    private BufferedImage image;
    private Graphics2D g;

    public static ArrayList<Line> lines;

    private Thread thread;

    public void start() {
        thread = new Thread(this);
        thread.start();
    }

    public static int WIDTH = 700;
    public static int HEIGHT = 450;

    private int FPS;
    private double millisToFPS;
    private long timerFPS;
    private int sleepTime;

    public DrawPanel() {
        super();
        setPreferredSize(new Dimension(WIDTH, HEIGHT));
        setFocusable(true);
        requestFocus();
    }


    public void redraw() {


    }

    @Override
    public void run() {
        FPS = 35;
        millisToFPS = 1000 / FPS;
        sleepTime = 0;

        image = new BufferedImage(WIDTH, HEIGHT, BufferedImage.TYPE_INT_RGB);
        g = (Graphics2D) image.getGraphics();
        g.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);

        image = new BufferedImage(WIDTH, HEIGHT, BufferedImage.TYPE_INT_RGB);
        g = (Graphics2D) image.getGraphics();
        g.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);
        lines = new ArrayList<>();

        while (true) {
            timerFPS = System.nanoTime();


            g.setColor(new Color(255, 242, 199));
            g.fillRect(0,0,DrawPanel.WIDTH,DrawPanel.HEIGHT);
            for (Line l : lines) {
                l.draw(g);
            }
            Graphics g2 = this.getGraphics();
            g2.drawImage(image, 0, 0, null);
            g2.dispose();


            timerFPS = (System.nanoTime() - timerFPS) / 1000000;
            if (millisToFPS > timerFPS) {
                sleepTime = (int) (millisToFPS - timerFPS);
            } else sleepTime = 0;

            try {
                thread.sleep(sleepTime);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
            timerFPS = 0;
            sleepTime = 1;
        }
    }
}
