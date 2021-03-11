package ru.vladKolyaAndrew.stand;

import com.sun.tools.doclets.formats.html.SourceToHTMLConverter;

import java.io.*;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.ProtocolException;
import java.net.URL;

/**
 * Created by vladislavZag on 11/03/2021.
 */

//Класс для запроса к дисплею
public class Requester {

    // урл по которому стенд будет предевать информацию дисплею
    private static String urlForDisplay = "http://localhost:8080/command";

    // json для дисплея
    private static String getJsonForDisplay(int x, int y, int z) {
        return "{\"method\": \"display.set_knife_position\",\"params\": {\"x\": "+x+",\"y\": "+y+",\"z\": "+z+"}}";
    }

    public static void sendStandData(int x, int y, int z) throws IOException {

        URL url = new URL(urlForDisplay);

        HttpURLConnection con = (HttpURLConnection)url.openConnection();
        con.setRequestMethod("POST");

        con.setRequestProperty("Content-Type", "application/json; utf-8");
        con.setRequestProperty("Accept", "application/json");
        con.setDoOutput(true);

        String jsonInputString = getJsonForDisplay(x, y, z);

        try(OutputStream os = con.getOutputStream()) {
            byte[] input = jsonInputString.getBytes("utf-8");
            os.write(input, 0, input.length);
            os.close();
        } catch (IOException e) {
            e.printStackTrace();
        }

        try(BufferedReader br = new BufferedReader(
                new InputStreamReader(con.getInputStream(), "utf-8"))) {
            StringBuilder response = new StringBuilder();
            String responseLine = null;
            while ((responseLine = br.readLine()) != null) {
                response.append(responseLine.trim());
            }
        } catch (UnsupportedEncodingException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }

    }
}
