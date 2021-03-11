package ru.vladKolyaAndrew.stand;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

import javax.swing.*;

@SpringBootApplication
public class StandApplication {

	public static void main(String[] args) {

		SpringApplication.run(StandApplication.class, args);

		System.setProperty("java.awt.headless", "false");
		SwingUtilities.invokeLater(() -> {
			new StartFrame().buildFrame();
		});
	}

}
