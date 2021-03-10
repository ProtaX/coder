package ru.vladKolyaAndrew.stand;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.autoconfigure.AutoConfigureOrder;
import org.springframework.stereotype.Service;

/**
 * Created by vladislavZag on 10/03/2021.
 */

// Посредник
@Service
public class StandService {

    @Autowired
    Stand stand;

    Thread thread;

    public void changeCommand(KnifeCommand knifeCommand) {
        stand.handleCommand(knifeCommand);
    }


}
