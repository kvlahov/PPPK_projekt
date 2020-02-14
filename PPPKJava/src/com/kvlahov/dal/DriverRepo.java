/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.kvlahov.dal;

import com.kvlahov.models.Driver;
import com.kvlahov.utilities.SQLUtilities;
import com.microsoft.sqlserver.jdbc.SQLServerDataSource;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;

import java.util.Collection;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.sql.DataSource;

/**
 *
 * @author lordo
 */
public class DriverRepo implements IRepo<Driver> {

    private DataSource dataSource;

    public DriverRepo() {
        dataSource = SQLUtilities.getDataSource();
    }

    @Override
    public Collection<Driver> getAll() {
        final String query = "SELECT * FROM Driver";
        try (Connection conn = dataSource.getConnection();
                Statement statement = conn.createStatement()) {
            ResultSet resultSet = statement.executeQuery(query);

            List<Driver> drivers = new ArrayList<>();

            while (resultSet.next()) {
                Driver d = new Driver();

                d.setIDDriver(resultSet.getInt("IDDriver"));
                d.setFirstName(resultSet.getString("FirstName"));
                d.setLastName(resultSet.getString("LastName"));
                d.setDriversLicence(resultSet.getString("DriversLicence"));

                drivers.add(d);
            }
            return drivers;
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return null;
    }

    @Override
    public Driver getById(int id) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    @Override
    public void insert(Driver entity) {
        final String query = "INSERT INTO Driver(FirstName, LastName, DriversLicence) values (?,?,?)";
        try (Connection conn = dataSource.getConnection();
                PreparedStatement stmt = conn.prepareStatement(query);) {

            stmt.setString(0, entity.getFirstName());
            stmt.setString(1, entity.getLastName());
            stmt.setString(2, entity.getDriversLicence());
            stmt.executeUpdate(query);
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void insertRange(Collection<Driver> entities) {
        try (Connection connection = dataSource.getConnection()) {
            connection.setAutoCommit(false);

            final String query = "INSERT INTO Driver(FirstName, LastName, DriversLicence) values (?,?,?)";
            try (PreparedStatement stmt = connection.prepareStatement(query)) {
                for (Driver entity : entities) {
                    stmt.setString(1, entity.getFirstName());
                    stmt.setString(2, entity.getLastName());
                    stmt.setString(3, entity.getDriversLicence());
                    
                    stmt.executeUpdate();
                }
            } catch (SQLException e) {
                connection.rollback();
            } finally {
                connection.setAutoCommit(true);
            }

        } catch (SQLException ex) {

        }

    }
}
