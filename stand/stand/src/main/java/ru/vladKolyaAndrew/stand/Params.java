package ru.vladKolyaAndrew.stand;

/**
 * Created by vladislavZag on 10/03/2021.
 */
public class Params {

    // Направление х у z
    String direction;

    //координаты (нужны для тестирования)
    String x;
    String y;
    String z;

    // Прибвляем или отнимаем координату
    boolean positive;

    public Params(String direction, boolean positive) {
        this.direction = direction;
        this.positive = positive;
    }

    public Params() {
    }

    public Params(String x, String y, String z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public String getX() {
        return x;
    }

    public void setX(String x) {
        this.x = x;
    }

    public String getY() {
        return y;
    }

    public void setY(String y) {
        this.y = y;
    }

    public String getZ() {
        return z;
    }

    public void setZ(String z) {
        this.z = z;
    }

    public String getDirection() {
        return direction;
    }

    public void setDirection(String direction) {
        this.direction = direction;
    }

    public boolean isPositive() {
        return positive;
    }

    public void setPositive(boolean positive) {
        this.positive = positive;
    }
}
