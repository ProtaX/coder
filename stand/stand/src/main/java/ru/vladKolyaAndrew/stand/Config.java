package ru.vladKolyaAndrew.stand;

import com.googlecode.jsonrpc4j.spring.JsonServiceExporter;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.servlet.handler.BeanNameUrlHandlerMapping;

/**
 * Created by vladislavZag on 11/03/2021.
 */

@Configuration
public class Config {

    @Bean(name = "standServiceRPC")
    public StandServiceRPCInterface userService() {
        return new StandServiceRPC();
    }

    @Bean(name = "/StandServiceRPC.json")
    public JsonServiceExporter mapping() {
        JsonServiceExporter a = new JsonServiceExporter();
        a.setServiceInterface(StandServiceRPCInterface.class);
        a.setService(userService());
        return a;
    };

}
