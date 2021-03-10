package ru.vladKolyaAndrew.stand;

/**
 * Created by vladislavZag on 10/03/2021.
 */

// Класс джейсона команды
public class KnifeCommand {

    String method;
    Params params;

    public KnifeCommand(String method, Params params) {
        this.method = method;
        this.params = params;
    }

    public KnifeCommand() {
    }

    public String getMethod() {
        return method;
    }

    public void setMethod(String method) {
        this.method = method;
    }

    public Params getParams() {
        return params;
    }

    public void setParams(Params params) {
        this.params = params;
    }
}
