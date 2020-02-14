/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.kvlahov.utilities;

import com.jfoenix.validation.base.ValidatorBase;
import com.kvlahov.Main;
import java.io.IOException;
import java.util.Optional;
import java.util.function.Consumer;
import java.util.function.Predicate;
import java.util.logging.Level;
import java.util.logging.Logger;
import java.util.regex.Pattern;
import javafx.animation.Interpolator;
import javafx.animation.KeyFrame;
import javafx.animation.Timeline;
import javafx.fxml.FXMLLoader;
import javafx.scene.Node;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.ButtonType;
import javafx.scene.control.TextField;
import javafx.scene.control.TextFormatter;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.Pane;
import javafx.stage.Stage;
import javafx.util.Duration;
import javafx.util.converter.LongStringConverter;
import javax.xml.crypto.dsig.keyinfo.KeyValue;

/**
 *
 * @author lordo
 */
public class UIHelper {
    private static final int SLIDE_ANIMATION_DURATION = 750;

    public static void switchScene(Node node, String fxml) {
        try {
            Stage stage = (Stage) node.getScene().getWindow();
            Parent root = FXMLLoader.load(Main.class.getResource(fxml));
            Scene scene = new Scene(root);
            stage.setScene(scene);
            stage.setMaximized(true);
            stage.centerOnScreen();
        } catch (IOException ex) {
            Logger.getLogger(Main.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    public static void switchComponent(Pane root, Class resourceRootClass, String componentFxml) {
        root.getChildren().clear();
        Node node = loadNode(resourceRootClass, componentFxml);
        root.getChildren().add(node);

    }

    private static Node loadNode(Class resourceRootClass, String componentFxml) {
        try {
            Node node = (Node) FXMLLoader.load(resourceRootClass.getResource(componentFxml));
            fitToParent(node);
            return node;
        } catch (IOException ex) {
            Logger.getLogger(UIHelper.class.getName()).log(Level.SEVERE, null, ex);
        }
        return null;
    }

    private static void fitToParent(Node node) {
        AnchorPane.setTopAnchor(node, 0.0);
        AnchorPane.setRightAnchor(node, 0.0);
        AnchorPane.setBottomAnchor(node, 0.0);
        AnchorPane.setLeftAnchor(node, 0.0);
    }

    public static TextFormatter<Long> getLongTextFormatter() {
        return new TextFormatter<>(
                new LongStringConverter(),
                null,
                c -> Pattern.matches("\\d*", c.getText()) ? c : null);
    }

    public static void showInfoAlert(String message) {
        Alert alert = new Alert(Alert.AlertType.WARNING, message, ButtonType.OK);
        alert.setHeaderText(null);
        alert.showAndWait();
    }

    public static void showWarningDialog(final String message, Consumer<ButtonType> consumer) {
        Alert alert = new Alert(Alert.AlertType.WARNING, message, ButtonType.YES, ButtonType.NO);
        alert.setHeaderText(null);
        Optional<ButtonType> buttonType = alert.showAndWait();
//        return buttonType;
        
        if (buttonType.isPresent() && buttonType.get() == ButtonType.YES) {
            consumer.accept(buttonType.get());
        }
    }

    public static ValidatorBase getCustomValidator(Predicate<String> predicate) {
        return new ValidatorBase() {
            @Override
            protected void eval() {
                if (srcControl.get() instanceof TextField) {
                    boolean testResult = predicate.test(((TextField) srcControl.get()).getText());
                    hasErrors.set(testResult);
                }
            }
        };
    }
}
