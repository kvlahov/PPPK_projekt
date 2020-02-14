/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.kvlahov;

import com.kvlahov.utilities.UIHelper;
import java.net.URL;
import java.util.ResourceBundle;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.layout.Pane;

/**
 * FXML Controller class
 *
 * @author lordo
 */
public class MainFXMLController implements Initializable {

    @FXML
    private Pane root;
    
    @FXML
    public void handleImportClick(ActionEvent event){
        UIHelper.switchComponent(root, Main.class, "CsvFXML.fxml");
    }
    
    @FXML
    public void handleReportClick(ActionEvent event){
        UIHelper.switchComponent(root, Main.class, "ReportFXML.fxml");
    }
    
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        new Thread(() -> UIHelper.switchComponent(root, Main.class, "CsvFXML.fxml")).start();
    }    
    
}
