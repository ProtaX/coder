package ru.vladKolyaAndrew.stand;

import javax.swing.*;
import java.awt.*;

/**
 * Created by vladislavZag on 11/03/2021.
 */
public class StartFrame {
    static JFrame startFrame;
    static DrawPanel mainPanel;

    void buildFrame(){
        mainPanel=new DrawPanel();
        startFrame=new JFrame("Stand");
        startFrame.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
        startFrame.getContentPane().add(BorderLayout.CENTER,mainPanel);
        //mainPanel.setSize(500,500);
        startFrame.pack();
        mainPanel.start();
        startFrame.setVisible(true);
    }
}
