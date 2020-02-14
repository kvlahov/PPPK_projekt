/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.kvlahov.dal;

import com.kvlahov.models.TravelOrder;
import java.util.Collection;
import java.util.List;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.TypedQuery;

/**
 *
 * @author lordo
 */
public class HibernateRepo implements IRepo<TravelOrder>{

    private EntityManager entityManager;

    public HibernateRepo() {
        EntityManagerFactory emf = javax.persistence.Persistence.createEntityManagerFactory("PPPKJavaPU");
        entityManager = emf.createEntityManager();
    }

    @Override
    public Collection<TravelOrder> getAll() {
        TypedQuery<TravelOrder> namedQuery = entityManager.createNamedQuery("TravelOrder.findAll", TravelOrder.class);

        return namedQuery.getResultList();
    }

    @Override
    public TravelOrder getById(int id) {
         TypedQuery<TravelOrder> namedQuery = entityManager.createNamedQuery("TravelOrder.findByIDTravelOrder", TravelOrder.class);
        namedQuery.setParameter(0, id);
        
        return namedQuery.getSingleResult();
    }

    @Override
    public void insert(TravelOrder entity) {
        
    }

    @Override
    public void insertRange(Collection<TravelOrder> entities) {
    }

}
