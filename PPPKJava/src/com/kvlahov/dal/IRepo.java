/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.kvlahov.dal;

import com.kvlahov.models.Driver;
import java.util.Collection;

/**
 *
 * @author lordo
 */
public interface IRepo<T> {
    Collection<T> getAll();
    T getById(int id);
    void insert(T entity);
    void insertRange(Collection<T> entities);
}
