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
@Table(name = "Vehicle")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "Vehicle.findAll", query = "SELECT v FROM Vehicle v")
    , @NamedQuery(name = "Vehicle.findByIDVehicle", query = "SELECT v FROM Vehicle v WHERE v.iDVehicle = :iDVehicle")
    , @NamedQuery(name = "Vehicle.findByRegistration", query = "SELECT v FROM Vehicle v WHERE v.registration = :registration")
    , @NamedQuery(name = "Vehicle.findByType", query = "SELECT v FROM Vehicle v WHERE v.type = :type")
    , @NamedQuery(name = "Vehicle.findByModel", query = "SELECT v FROM Vehicle v WHERE v.model = :model")
    , @NamedQuery(name = "Vehicle.findByYearManufactured", query = "SELECT v FROM Vehicle v WHERE v.yearManufactured = :yearManufactured")
    , @NamedQuery(name = "Vehicle.findByInitialKilometres", query = "SELECT v FROM Vehicle v WHERE v.initialKilometres = :initialKilometres")
    , @NamedQuery(name = "Vehicle.findByIsAvailable", query = "SELECT v FROM Vehicle v WHERE v.isAvailable = :isAvailable")})
public class Vehicle implements Serializable {

    private static final long serialVersionUID = 1L;
    @Id
    @Basic(optional = false)
    @Column(name = "IDVehicle")
    private Integer iDVehicle;
    @Basic(optional = false)
    @Column(name = "Registration")
    private String registration;
    @Basic(optional = false)
    @Column(name = "Type")
    private String type;
    @Basic(optional = false)
    @Column(name = "Model")
    private String model;
    @Basic(optional = false)
    @Column(name = "YearManufactured")
    private short yearManufactured;
    @Basic(optional = false)
    @Column(name = "InitialKilometres")
    private double initialKilometres;
    @Basic(optional = false)
    @Column(name = "IsAvailable")
    private boolean isAvailable;
    @OneToMany(cascade = CascadeType.ALL, mappedBy = "vehicleID")
    private Collection<ServiceInfo> serviceInfoCollection;
    @OneToMany(cascade = CascadeType.ALL, mappedBy = "vehicleID")
    private Collection<TravelOrder> travelOrderCollection;

    public Vehicle() {
    }

    public Vehicle(Integer iDVehicle) {
        this.iDVehicle = iDVehicle;
    }

    public Vehicle(Integer iDVehicle, String registration, String type, String model, short yearManufactured, double initialKilometres, boolean isAvailable) {
        this.iDVehicle = iDVehicle;
        this.registration = registration;
        this.type = type;
        this.model = model;
        this.yearManufactured = yearManufactured;
        this.initialKilometres = initialKilometres;
        this.isAvailable = isAvailable;
    }

    public Integer getIDVehicle() {
        return iDVehicle;
    }

    public void setIDVehicle(Integer iDVehicle) {
        this.iDVehicle = iDVehicle;
    }

    public String getRegistration() {
        return registration;
    }

    public void setRegistration(String registration) {
        this.registration = registration;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public String getModel() {
        return model;
    }

    public void setModel(String model) {
        this.model = model;
    }

    public short getYearManufactured() {
        return yearManufactured;
    }

    public void setYearManufactured(short yearManufactured) {
        this.yearManufactured = yearManufactured;
    }

    public double getInitialKilometres() {
        return initialKilometres;
    }

    public void setInitialKilometres(double initialKilometres) {
        this.initialKilometres = initialKilometres;
    }

    public boolean getIsAvailable() {
        return isAvailable;
    }

    public void setIsAvailable(boolean isAvailable) {
        this.isAvailable = isAvailable;
    }

    @XmlTransient
    public Collection<ServiceInfo> getServiceInfoCollection() {
        return serviceInfoCollection;
    }

    public void setServiceInfoCollection(Collection<ServiceInfo> serviceInfoCollection) {
        this.serviceInfoCollection = serviceInfoCollection;
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
        hash += (iDVehicle != null ? iDVehicle.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof Vehicle)) {
            return false;
        }
        Vehicle other = (Vehicle) object;
        return this.registration.equals(other.registration);
    }

    @Override
    public String toString() {
        return "com.kvlahov.models.Vehicle[ iDVehicle=" + iDVehicle + " ]";
    }
    
}
