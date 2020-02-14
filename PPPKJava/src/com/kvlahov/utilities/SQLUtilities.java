/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.kvlahov.utilities;

import com.microsoft.sqlserver.jdbc.SQLServerDataSource;
import javax.sql.DataSource;

/**
 *
 * @author lordo
 */
public class SQLUtilities {
    public static DataSource getDataSource(){
        SQLServerDataSource dataSource = new SQLServerDataSource();
        dataSource.setServerName("localhost");
        dataSource.setDatabaseName("pppk");
        dataSource.setUser("sa");
        dataSource.setPassword("SQL");
        
        return dataSource;
    }
}
