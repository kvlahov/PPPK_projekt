/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.kvlahov;

import com.jfoenix.controls.JFXButton;
import com.jfoenix.controls.JFXSpinner;
import com.kvlahov.dal.HibernateRepo;
import com.kvlahov.models.RouteInfo;
import com.kvlahov.models.TravelOrder;
import com.kvlahov.models.Vehicle;
import java.io.EOFException;
import java.io.File;
import java.io.IOException;
import java.math.BigDecimal;
import java.net.URL;
import java.text.SimpleDateFormat;
import java.util.ResourceBundle;
import java.util.logging.Level;
import java.util.logging.Logger;
import javafx.application.Platform;
import javafx.beans.property.SimpleObjectProperty;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.ButtonType;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import org.apache.pdfbox.pdmodel.PDDocument;
import org.apache.pdfbox.pdmodel.PDPage;
import org.apache.pdfbox.pdmodel.PDPageContentStream;
import org.apache.pdfbox.pdmodel.font.PDType1Font;

/**
 *
 * @author lordo
 */
public class ReportFXMLController implements Initializable {

    @FXML
    private TableView<TravelOrder> travelOrdersTableView;
    @FXML
    private TableColumn<TravelOrder, Integer> idTableColumn;
    @FXML
    private TableColumn<TravelOrder, String> driverTableColumn;
    @FXML
    private TableColumn<TravelOrder, String> vehicleTableColumn;
    @FXML
    private TableColumn<TravelOrder, String> typeTableColumn;
    @FXML
    private TableColumn<TravelOrder, Integer> expectedNoDaysTableColumn;
    @FXML
    private TableColumn<TravelOrder, String> reasonTableColumn;
    @FXML
    private TableColumn<TravelOrder, String> cityStartTableColumn;
    @FXML
    private TableColumn<TravelOrder, String> cityEndtTableColumn;
    @FXML
    private TableColumn<TravelOrder, BigDecimal> totalCostTableColumn;
    @FXML
    private TableColumn<TravelOrder, String> docDateTableColumn;

    @FXML
    private Button btnGenerate;
    @FXML
    private JFXSpinner spinner;

    private ObservableList<TravelOrder> travelOrders;
    private HibernateRepo repository;

    private final String FILE_PATH = "src/data/report/travelOrder.pdf";

    @Override
    public void initialize(URL url, ResourceBundle rb) {
        spinner.setVisible(true);
        new Thread(() -> {
            repository = new HibernateRepo();
            travelOrders = FXCollections.observableArrayList(repository.getAll());
            travelOrdersTableView.setItems(travelOrders);
            spinner.setVisible(false);
        }).start();
        
        initColumns();
        addListeners();

    }

    private void initColumns() {
        idTableColumn.setCellValueFactory(cell -> new SimpleObjectProperty<>(cell.getValue().getIDTravelOrder()));
        driverTableColumn.setCellValueFactory(cell -> new SimpleObjectProperty<>(cell.getValue().getDriverID().getFirstName() + " " + cell.getValue().getDriverID().getLastName()));
        vehicleTableColumn.setCellValueFactory(cell -> new SimpleObjectProperty<>(cell.getValue().getVehicleID().getModel() + " - " + cell.getValue().getVehicleID().getType()));
        typeTableColumn.setCellValueFactory(cell -> new SimpleObjectProperty<>(cell.getValue().getTravelOrderTypeID().getType()));
        expectedNoDaysTableColumn.setCellValueFactory(cell -> new SimpleObjectProperty<>(cell.getValue().getExpectedNumberOfDays()));
        reasonTableColumn.setCellValueFactory(cell -> new SimpleObjectProperty<>(cell.getValue().getReasonForTravel()));
        cityStartTableColumn.setCellValueFactory(cell -> new SimpleObjectProperty<>(cell.getValue().getCityStartId().getName()));
        cityEndtTableColumn.setCellValueFactory(cell -> new SimpleObjectProperty<>(cell.getValue().getCityEndId().getName()));
        totalCostTableColumn.setCellValueFactory(cell -> new SimpleObjectProperty<>(cell.getValue().getTotalCost()));
        SimpleDateFormat df = new SimpleDateFormat("dd.MM.yyyy");
        docDateTableColumn.setCellValueFactory(cell -> new SimpleObjectProperty<>(df.format(cell.getValue().getDocumentDate())));
    }

    private void addListeners() {
        travelOrdersTableView.getSelectionModel().selectedItemProperty().addListener((obs, oldSelection, newSelection) -> {
            btnGenerate.setVisible(newSelection != null);
        });
    }

    @FXML
    public void handleGenerateClick(ActionEvent event) {
        TravelOrder to = travelOrdersTableView.getSelectionModel().getSelectedItem();
        new Thread(() -> generateReport(to)).start();
        spinner.setVisible(true);
    }

    private void generateReport(TravelOrder to) {
        try (PDDocument doc = new PDDocument()) {

            PDPage myPage = new PDPage();
            doc.addPage(myPage);

            try (PDPageContentStream cont = new PDPageContentStream(doc, myPage)) {

                cont.beginText();

                cont.newLineAtOffset(25, 700);
                cont.setFont(PDType1Font.TIMES_BOLD, 20);
                cont.showText("TRAVEL ORDER");
                cont.newLine();
                cont.newLine();
                cont.newLine();
                cont.newLine();

                cont.setFont(PDType1Font.TIMES_ROMAN, 12);
                cont.setLeading(14.5f);
                
                cont.showText("ID: " + to.getIDTravelOrder());
                cont.newLine();
                
                SimpleDateFormat df = new SimpleDateFormat("dd.MM.yyyy");
                cont.showText("Document date: " + df.format(to.getDocumentDate()));
                cont.newLine();
                cont.showText("Type: " + to.getTravelOrderTypeID().getType());
                cont.newLine();

                cont.showText("Driver: " + to.getDriverID().getFirstName() + " " + to.getDriverID().getLastName());
                cont.newLine();
                Vehicle v = to.getVehicleID();
                String vehicle = v.getModel() + " - " + v.getType() + " Registration: " + v.getRegistration();
                cont.showText("Vehicle: " + vehicle);
                cont.newLine();

                cont.showText("City start: " + to.getCityStartId().getName());
                cont.newLine();

                cont.showText("City end: " + to.getCityEndId().getName());
                cont.newLine();
                cont.showText("Expected no of days: " + to.getExpectedNumberOfDays());
                cont.newLine();
                cont.showText("Reason for travel: " + to.getReasonForTravel());
                cont.newLine();
                cont.showText("Total cost: " + to.getTotalCost());
                cont.newLine();
                cont.newLine();
                cont.newLine();

                cont.showText("ROUTE INFO");
                cont.newLine();
                cont.showText("------------------------------------------");

                for (RouteInfo routeInfo : to.getRouteInfoCollection()) {
                    cont.showText("Date start: " + df.format(routeInfo.getDateTimeStart()));
                    cont.newLine();
                    cont.showText("Date end: " + df.format(routeInfo.getDateTimeEnd()));
                    cont.newLine();

                    cont.showText("Average speed: " + routeInfo.getAverageSpeed());
                    cont.newLine();
                    cont.showText("Distance In Km: " + routeInfo.getDistanceInKm());
                    cont.newLine();
                    cont.showText("Fuel Expense: " + routeInfo.getFuelExpense());
                    cont.newLine();
                    cont.showText("Latitude Start: " + routeInfo.getLatitudeStart());
                    cont.newLine();
                    cont.showText("Longitude Start: " + routeInfo.getLongitudeStart());
                    cont.newLine();
                    cont.showText("Latitude Start: " + routeInfo.getLatitudeStart());
                    cont.newLine();
                    cont.showText("Longitude End: " + routeInfo.getLongitudeEnd());
                    cont.newLine();

                    cont.showText("------------------------------------------");
                    cont.newLine();
                }

                cont.endText();
            }

            doc.save(FILE_PATH);

            Platform.runLater(() -> {
                spinner.setVisible(false);
                Alert alert = new Alert(Alert.AlertType.INFORMATION, "Succesfully created pdf ", ButtonType.OK);
                alert.setHeaderText(null);

                alert.showAndWait();
            });
        } catch (IOException ex) {
            Platform.runLater(() -> {
                spinner.setVisible(false);
                Alert alert = new Alert(Alert.AlertType.ERROR, ex.toString(), ButtonType.OK);
                alert.setHeaderText(null);

                alert.showAndWait();
            });
        }
    }

}
