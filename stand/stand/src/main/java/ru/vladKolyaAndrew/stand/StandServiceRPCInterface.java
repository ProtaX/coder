package ru.vladKolyaAndrew.stand;

import com.googlecode.jsonrpc4j.JsonRpcParam;

/**
 * Created by vladislavZag on 11/03/2021.
 */
public interface StandServiceRPCInterface {

    void move_knife(@JsonRpcParam(value="direction")String direction);
    void stop_knife();
}
