package ru.vladKolyaAndrew.stand;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

/**
 * Created by vladislavZag on 10/03/2021.
 */

// Контроллер
@RestController
public class StandController {

    @Autowired
    StandService standService;
    
    // курл для терминала
    // curl --data
    // '{"method": "stand.move_knife","params": {"direction": "x","positive": true}}'
    // -H "Content-Type: application/json" -X POST http://localhost:8080/command
    @PostMapping(value = "/command", consumes = MediaType.APPLICATION_JSON_VALUE)
    public void move(
            @RequestBody KnifeCommand knifeCommand
    ) {
        standService.changeCommand(knifeCommand);
    }
}
