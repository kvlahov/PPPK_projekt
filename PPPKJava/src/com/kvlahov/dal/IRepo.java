/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.kvlahov.dal;

import java.util.Collection;

public interface IRepo<T> {
    Collection<T> getAll();
    T getById(int id);
    void insert(T entity);
    void insertRange(Collection<T> entities);
}
