/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.kvlahov.dal;

import com.kvlahov.models.Driver;
import com.kvlahov.models.Vehicle;

/**
 *
 * @author lordo
 */
public class JDBCRepoFactory<T> {
    
    public static<T> IRepo<T> getRepo(Class<T> type){
        if(type.isInstance(new Driver())) {
            return (IRepo<T>)new DriverRepo();
        } else if (type.isInstance(new Vehicle())) {
            return (IRepo<T>)new VehicleRepo();
        }
        return null;
    }
}
