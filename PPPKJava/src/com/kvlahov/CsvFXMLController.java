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
import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.net.URL;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import java.util.ResourceBundle;
import java.util.stream.Collectors;
import javafx.beans.property.BooleanProperty;
import javafx.beans.property.SimpleBooleanProperty;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
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

//        chooser.setSelectedExtensionFilter(new FileChooser.ExtensionFilter("csv", "*.csv"));
        selectedFile = chooser.showOpenDialog(importDataBtn.getScene().getWindow());
        filePathLabel.setText("Selected file: " + selectedFile.getAbsolutePath());
        fileSelected.set(true);
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

                    Driver driver = new Driver();
                    driver.setIDDriver(Integer.parseInt(driverLine[0]));
                    driver.setFirstName(driverLine[1]);
                    driver.setLastName(driverLine[2]);
                    driver.setDriversLicence(driverLine[3]);

                    drivers.add(driver);
                }
                insertData(drivers);
            } else {
                List<Vehicle> vehicles = new ArrayList<>();
                while ((line = br.readLine()) != null) {
                    String[] vehicleLine = line.split(",");

                    Vehicle vehicle = new Vehicle();
                    vehicle.setIDVehicle(Integer.parseInt(vehicleLine[0]));
                    vehicle.setRegistration(vehicleLine[1]);
                    vehicle.setType(vehicleLine[2]);
                    vehicle.setModel(vehicleLine[3]);
                    vehicle.setYearManufactured(Short.parseShort(vehicleLine[1]));
                    vehicle.setInitialKilometres(Double.parseDouble(vehicleLine[1]));
                    vehicle.setIsAvailable(("1".equals(vehicleLine[1])));

                    vehicles.add(vehicle);
                }

                insertData(vehicles);
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private <T> void insertData(List<T> data) {
        if (data.isEmpty()) {
            return;
        }

        IRepo<T> repo = JDBCRepoFactory.<T>getRepo((Class<T>) data.get(0).getClass());
        Collection<T> allData = repo.getAll();

        List<T> result = data.stream().filter(d -> !allData.contains(d)).collect(Collectors.toList());
        
        repo.insertRange(result);
    }

}
