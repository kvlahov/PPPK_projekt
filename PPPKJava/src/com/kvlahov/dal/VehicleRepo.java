/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.kvlahov.dal;

import com.kvlahov.models.Driver;
import com.kvlahov.models.Vehicle;
import com.kvlahov.utilities.SQLUtilities;
import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.sql.Types;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import javax.sql.DataSource;

/**
 *
 * @author lordo
 */
public class VehicleRepo implements IRepo<Vehicle> {

    private DataSource dataSource;

    public VehicleRepo() {
        dataSource = SQLUtilities.getDataSource();
    }

    @Override
    public Collection<Vehicle> getAll() {
        try (Connection conn = dataSource.getConnection();
                CallableStatement statement = conn.prepareCall(" {CALL getAllVehicles() }")) {
            ResultSet resultSet = statement.executeQuery();

            List<Vehicle> vehicles = new ArrayList<>();

            while (resultSet.next()) {
                Vehicle v = new Vehicle();

                v.setIDVehicle(resultSet.getInt("IDVehicle"));
                v.setModel(resultSet.getString("Model"));
                v.setType(resultSet.getString("Type"));
                v.setYearManufactured(resultSet.getShort("YearManufactured"));
                v.setInitialKilometres(resultSet.getDouble("InitialKilometres"));
                v.setIsAvailable(resultSet.getBoolean("IsAvailable"));
                v.setRegistration(resultSet.getString("Registration"));

                vehicles.add(v);
            }
            return vehicles;
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return null;
    }

    @Override
    public Vehicle getById(int id) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    @Override
    public void insert(Vehicle entity) {
        try (Connection conn = dataSource.getConnection();
                CallableStatement statement = conn.prepareCall(" {CALL insertVehicle(?,?,?,?,?,?)} ")) {

            statement.setString(1, entity.getModel());
            statement.setString(2, entity.getType());
            statement.setString(3, entity.getRegistration());
            statement.setShort(4, entity.getYearManufactured());
            statement.setDouble(5, entity.getInitialKilometres());

            statement.registerOutParameter(6, Types.INTEGER);

            statement.executeUpdate();

        } catch (SQLException e) {
            e.printStackTrace();
        }

    }

    @Override
    public void insertRange(Collection<Vehicle> entities) {
        try (Connection conn = dataSource.getConnection()) {
            conn.setAutoCommit(false);

            try (CallableStatement statement = conn.prepareCall("{ CALL insertVehicle(?,?,?,?,?,?) }")) {
                for (Vehicle entity : entities) {
                    statement.setString(1, entity.getModel());
                    statement.setString(2, entity.getType());
                    statement.setString(3, entity.getRegistration());
                    statement.setShort(4, entity.getYearManufactured());
                    statement.setDouble(5, entity.getInitialKilometres());
                    
                    statement.registerOutParameter(6, Types.INTEGER);
                    
                    statement.executeUpdate();
                }
            } catch (SQLException e) {
                conn.rollback();
            } finally {
                conn.setAutoCommit(true);
            }

        } catch (Exception ex) {
            
        }

    }

}
