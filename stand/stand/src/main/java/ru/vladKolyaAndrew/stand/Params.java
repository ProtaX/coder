package ru.vladKolyaAndrew.stand;

/**
 * Created by vladislavZag on 10/03/2021.
 */
public class Params {

    // Направление х у z
    String direction;

    // Прибвляем или отнимаем координату
    boolean positive;

    public Params(String direction, boolean positive) {
        this.direction = direction;
        this.positive = positive;
    }

    public Params() {
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
