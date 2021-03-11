package ru.vladKolyaAndrew.stand;

import org.springframework.beans.factory.annotation.Autowired;

/**
 * Created by vladislavZag on 11/03/2021.
 */
public class StandServiceRPC implements StandServiceRPCInterface{

    @Autowired
    Stand stand;

    @Override
    public void move_knife(String direction) {
        stand.goCommand(direction);
    }

    @Override
    public void stop_knife() {
        stand.stopCommand();
    }
}
