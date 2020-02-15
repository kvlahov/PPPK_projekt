/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.kvlahov;

import com.jfoenix.controls.JFXButton;
import com.jfoenix.controls.JFXComboBox;
import com.kvlahov.dal.DriverRepo;
import com.kvlahov.dal.HibernateRepo;
import com.kvlahov.dal.IRepo;
import com.kvlahov.dal.JDBCRepoFactory;
import com.kvlahov.models.Driver;
import com.kvlahov.models.Vehicle;
import com.sun.org.apache.xerces.internal.impl.dv.DatatypeException;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.net.URL;
import java.text.ParseException;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import java.util.ResourceBundle;
import java.util.logging.Level;
import java.util.logging.Logger;
import java.util.regex.Pattern;
import java.util.stream.Collectors;
import java.util.stream.Stream;
import javafx.application.Platform;
import javafx.beans.property.BooleanProperty;
import javafx.beans.property.SimpleBooleanProperty;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Alert;
import javafx.scene.control.ButtonType;
import javafx.scene.control.Label;
import javafx.stage.FileChooser;

/**
 * FXML Controller class
 *
 * @author lordo
 */
public class CsvFXMLController implements Initializable {

    @FXML
    private Label filePathLabel;
    @FXML
    private JFXButton importDataBtn;
    @FXML
    private JFXComboBox<String> dataTypeComboBox;

    private BooleanProperty fileSelected;

    private File selectedFile;

    @FXML
    public void handleChooseFileClick(ActionEvent event) {
        FileChooser chooser = new FileChooser();

        chooser.getExtensionFilters().add(new FileChooser.ExtensionFilter("Csv", "*.csv"));
        chooser.setInitialDirectory(new File("src/data/csv"));
        selectedFile = chooser.showOpenDialog(importDataBtn.getScene().getWindow());
        if(selectedFile != null) {
            filePathLabel.setText("Selected file: " + selectedFile.getAbsolutePath());
            fileSelected.set(true);
        }
    }

    @FXML
    public void handleImportDataClick(ActionEvent event) {
        new Thread(() -> importCsv(selectedFile)).start();
    }

    @Override
    public void initialize(URL url, ResourceBundle rb) {
        fileSelected = new SimpleBooleanProperty(false);
        importDataBtn.visibleProperty().bind(fileSelected);

        ObservableList<String> dataTypes = FXCollections.observableArrayList("Driver", "Vehicle");
        dataTypeComboBox.setItems(dataTypes);
        dataTypeComboBox.getSelectionModel().select(0);
    }

    private void importCsv(File selectedFile) {
        try (BufferedReader br = new BufferedReader(new FileReader(selectedFile))) {
            String line;
            String selectedDataType = dataTypeComboBox.getSelectionModel().getSelectedItem();

            if (selectedDataType.equalsIgnoreCase("Driver")) {
                List<Driver> drivers = new ArrayList<>();
                while ((line = br.readLine()) != null) {
                    String[] driverLine = line.split(",");

                    if (driverLine.length != 3) {
                        throw new Exception("Number of parameters doesn't match required datatype: Driver");
                    }

                    if (driverLine.length == 3) {
                        Driver driver = new Driver();
                        driver.setFirstName(driverLine[0]);
                        driver.setLastName(driverLine[1]);
                        driver.setDriversLicence(driverLine[2]);

                        drivers.add(driver);

                    }
                }
                insertData(drivers);
            } else {
                List<Vehicle> vehicles = new ArrayList<>();
                while ((line = br.readLine()) != null) {
                    String[] dirtyLine = line.split(",");

                    List<String> vehicleLine = Stream.of(dirtyLine)
                            .map(s -> ltrim(s.replaceAll("\\R", "").trim()))
                            .collect(Collectors.toList());

                    if (vehicleLine.size() != 5) {
                        throw new Exception("Number of parameters doesn't match required datatype: Vehicle");
                    }

                    if (vehicleLine.size() == 5) {
                        Vehicle vehicle = new Vehicle();
                        vehicle.setRegistration(vehicleLine.get(0));
                        vehicle.setType(vehicleLine.get(1));
                        vehicle.setModel(vehicleLine.get(2));
                        vehicle.setYearManufactured(Short.parseShort(vehicleLine.get(3)));
                        vehicle.setInitialKilometres(Double.parseDouble(vehicleLine.get(4)));

                        vehicles.add(vehicle);
                    }
                }

                insertData(vehicles);
            }
        } catch (IOException e) {
            e.printStackTrace();
        } catch (Exception ex) {
            Platform.runLater(() -> {
                Alert a = new Alert(Alert.AlertType.ERROR, "Error while importing data:\n" + ex.getMessage(), ButtonType.OK);
                a.setHeaderText(null);

                a.showAndWait();
            });
        }
    }

    private <T> void insertData(List<T> data) {
        if (data.isEmpty()) {
            return;
        }

        IRepo<T> repo = JDBCRepoFactory.<T>getRepo((Class<T>) data.get(0).getClass());
        Collection<T> allData = repo.getAll();

        List<T> result = data.stream()
                .filter(d -> !allData.contains(d))
                .distinct()
                .collect(Collectors.toList());

        repo.insertRange(result);

        Platform.runLater(() -> {
            Alert a = new Alert(Alert.AlertType.INFORMATION, "Imported " + result.size() + " records", ButtonType.OK);
            a.setHeaderText(null);

            a.showAndWait();
        });
    }

    private final static Pattern LTRIM = Pattern.compile("^\\s+");

    public static String ltrim(String s) {
        return LTRIM.matcher(s).replaceAll("");
    }

}
