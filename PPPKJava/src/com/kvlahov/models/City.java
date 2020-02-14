/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.kvlahov.models;

import java.io.Serializable;
import java.util.Collection;
import javax.persistence.Basic;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.OneToMany;
import javax.persistence.Table;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlTransient;

/**
 *
 * @author lordo
 */
@Entity
@Table(name = "City")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "City.findAll", query = "SELECT c FROM City c")
    , @NamedQuery(name = "City.findByIDCity", query = "SELECT c FROM City c WHERE c.iDCity = :iDCity")
    , @NamedQuery(name = "City.findByName", query = "SELECT c FROM City c WHERE c.name = :name")})
public class City implements Serializable {

    private static final long serialVersionUID = 1L;
    @Id
    @Basic(optional = false)
    @Column(name = "IDCity")
    private Integer iDCity;
    @Column(name = "Name")
    private String name;
    @OneToMany(mappedBy = "cityStartId")
    private Collection<TravelOrder> travelOrderCollection;
    @OneToMany(mappedBy = "cityEndId")
    private Collection<TravelOrder> travelOrderCollection1;

    public City() {
    }

    public City(Integer iDCity) {
        this.iDCity = iDCity;
    }

    public Integer getIDCity() {
        return iDCity;
    }

    public void setIDCity(Integer iDCity) {
        this.iDCity = iDCity;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    @XmlTransient
    public Collection<TravelOrder> getTravelOrderCollection() {
        return travelOrderCollection;
    }

    public void setTravelOrderCollection(Collection<TravelOrder> travelOrderCollection) {
        this.travelOrderCollection = travelOrderCollection;
    }

    @XmlTransient
    public Collection<TravelOrder> getTravelOrderCollection1() {
        return travelOrderCollection1;
    }

    public void setTravelOrderCollection1(Collection<TravelOrder> travelOrderCollection1) {
        this.travelOrderCollection1 = travelOrderCollection1;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (iDCity != null ? iDCity.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof City)) {
            return false;
        }
        City other = (City) object;
        if ((this.iDCity == null && other.iDCity != null) || (this.iDCity != null && !this.iDCity.equals(other.iDCity))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "com.kvlahov.models.City[ iDCity=" + iDCity + " ]";
    }
    
}
