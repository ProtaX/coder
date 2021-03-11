package ru.vladKolyaAndrew.stand;

import java.awt.*;
import java.io.Serializable;

/**
 * Created by vladislavZag on 11/03/2021.
 */
public class Line {
    int x;
    int y;
    int x2;
    int y2;
    Color color;
    public static int currentAlpha = 30;

    public Line(int x, int y, int x2, int y2) {
        color= new Color(1,1,1, currentAlpha);
        this.x=x;
        this.y=y;
        this.x2=x2;
        this.y2=y2;
    }

    public static void changeCurrentAlpha(int delta) {
        int newValue = currentAlpha + delta;
        if (newValue<0) currentAlpha = 0;
        else
            if (newValue>255) currentAlpha = 255;
        else
            currentAlpha = newValue;

    }

    public void draw(Graphics2D g) {
        g.setStroke(new BasicStroke(3));
        g.setColor(color);
        g.drawLine(x,y,x2,y2);
    }
}
