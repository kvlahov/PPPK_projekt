/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.kvlahov.models;

import java.io.Serializable;
import java.util.Collection;
import javax.persistence.Basic;
import javax.persistence.CascadeType;
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
@Table(name = "Driver")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "Driver.findAll", query = "SELECT d FROM Driver d")
    , @NamedQuery(name = "Driver.findByIDDriver", query = "SELECT d FROM Driver d WHERE d.iDDriver = :iDDriver")
    , @NamedQuery(name = "Driver.findByFirstName", query = "SELECT d FROM Driver d WHERE d.firstName = :firstName")
    , @NamedQuery(name = "Driver.findByLastName", query = "SELECT d FROM Driver d WHERE d.lastName = :lastName")
    , @NamedQuery(name = "Driver.findByDriversLicence", query = "SELECT d FROM Driver d WHERE d.driversLicence = :driversLicence")})
public class Driver implements Serializable {

    private static final long serialVersionUID = 1L;
    @Id
    @Basic(optional = false)
    @Column(name = "IDDriver")
    private Integer iDDriver;
    @Basic(optional = false)
    @Column(name = "FirstName")
    private String firstName;
    @Basic(optional = false)
    @Column(name = "LastName")
    private String lastName;
    @Basic(optional = false)
    @Column(name = "DriversLicence")
    private String driversLicence;
    @OneToMany(cascade = CascadeType.ALL, mappedBy = "driverID")
    private Collection<TravelOrder> travelOrderCollection;

    public Driver() {
    }

    public Driver(Integer iDDriver) {
        this.iDDriver = iDDriver;
    }

    public Driver(Integer iDDriver, String firstName, String lastName, String driversLicence) {
        this.iDDriver = iDDriver;
        this.firstName = firstName;
        this.lastName = lastName;
        this.driversLicence = driversLicence;
    }

    public Integer getIDDriver() {
        return iDDriver;
    }

    public void setIDDriver(Integer iDDriver) {
        this.iDDriver = iDDriver;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public String getDriversLicence() {
        return driversLicence;
    }

    public void setDriversLicence(String driversLicence) {
        this.driversLicence = driversLicence;
    }

    @XmlTransient
    public Collection<TravelOrder> getTravelOrderCollection() {
        return travelOrderCollection;
    }

    public void setTravelOrderCollection(Collection<TravelOrder> travelOrderCollection) {
        this.travelOrderCollection = travelOrderCollection;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (iDDriver != null ? iDDriver.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof Driver)) {
            return false;
        }
        Driver other = (Driver) object;
        return this.driversLicence.equals(other.driversLicence);
    }

    @Override
    public String toString() {
        return "com.kvlahov.models.Driver[ iDDriver=" + iDDriver + " ]";
    }

}
