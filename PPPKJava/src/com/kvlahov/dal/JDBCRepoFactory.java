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
        if(type.isInstance(Driver.class)) {
            return (IRepo<T>)new DriverRepo();
        } else if (type.isInstance(Vehicle.class)) {
            return (IRepo<T>)new VehicleRepo();
        }
        return null;
    }
}
