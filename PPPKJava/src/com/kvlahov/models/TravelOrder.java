/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.kvlahov.models;

import java.io.Serializable;
import java.math.BigDecimal;
import java.util.Collection;
import java.util.Date;
import javax.persistence.Basic;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.OneToMany;
import javax.persistence.Table;
import javax.persistence.Temporal;
import javax.persistence.TemporalType;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlTransient;

/**
 *
 * @author lordo
 */
@Entity
@Table(name = "TravelOrder")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "TravelOrder.findAll", query = "SELECT t FROM TravelOrder t")
    , @NamedQuery(name = "TravelOrder.findByIDTravelOrder", query = "SELECT t FROM TravelOrder t WHERE t.iDTravelOrder = :iDTravelOrder")
    , @NamedQuery(name = "TravelOrder.findByExpectedNumberOfDays", query = "SELECT t FROM TravelOrder t WHERE t.expectedNumberOfDays = :expectedNumberOfDays")
    , @NamedQuery(name = "TravelOrder.findByReasonForTravel", query = "SELECT t FROM TravelOrder t WHERE t.reasonForTravel = :reasonForTravel")
    , @NamedQuery(name = "TravelOrder.findByTotalCost", query = "SELECT t FROM TravelOrder t WHERE t.totalCost = :totalCost")
    , @NamedQuery(name = "TravelOrder.findByDocumentDate", query = "SELECT t FROM TravelOrder t WHERE t.documentDate = :documentDate")})
public class TravelOrder implements Serializable {

    private static final long serialVersionUID = 1L;
    @Id
    @Basic(optional = false)
    @Column(name = "IDTravelOrder")
    private Integer iDTravelOrder;
    @Basic(optional = false)
    @Column(name = "ExpectedNumberOfDays")
    private int expectedNumberOfDays;
    @Column(name = "ReasonForTravel")
    private String reasonForTravel;
    // @Max(value=?)  @Min(value=?)//if you know range of your decimal fields consider using these annotations to enforce field validation
    @Column(name = "TotalCost")
    private BigDecimal totalCost;
    @Column(name = "DocumentDate")
    @Temporal(TemporalType.TIMESTAMP)
    private Date documentDate;
    @JoinColumn(name = "CityStartId", referencedColumnName = "IDCity")
    @ManyToOne
    private City cityStartId;
    @JoinColumn(name = "CityEndId", referencedColumnName = "IDCity")
    @ManyToOne
    private City cityEndId;
    @JoinColumn(name = "DriverID", referencedColumnName = "IDDriver")
    @ManyToOne(optional = false)
    private Driver driverID;
    @JoinColumn(name = "TravelOrderTypeID", referencedColumnName = "IDTravelOrderType")
    @ManyToOne(optional = false)
    private TravelOrderType travelOrderTypeID;
    @JoinColumn(name = "VehicleID", referencedColumnName = "IDVehicle")
    @ManyToOne(optional = false)
    private Vehicle vehicleID;
    @OneToMany(mappedBy = "travelOrderID")
    private Collection<RouteInfo> routeInfoCollection;
    @OneToMany(mappedBy = "travelOrderID")
    private Collection<FuelInfo> fuelInfoCollection;

    public TravelOrder() {
    }

    public TravelOrder(Integer iDTravelOrder) {
        this.iDTravelOrder = iDTravelOrder;
    }

    public TravelOrder(Integer iDTravelOrder, int expectedNumberOfDays) {
        this.iDTravelOrder = iDTravelOrder;
        this.expectedNumberOfDays = expectedNumberOfDays;
    }

    public Integer getIDTravelOrder() {
        return iDTravelOrder;
    }

    public void setIDTravelOrder(Integer iDTravelOrder) {
        this.iDTravelOrder = iDTravelOrder;
    }

    public int getExpectedNumberOfDays() {
        return expectedNumberOfDays;
    }

    public void setExpectedNumberOfDays(int expectedNumberOfDays) {
        this.expectedNumberOfDays = expectedNumberOfDays;
    }

    public String getReasonForTravel() {
        return reasonForTravel;
    }

    public void setReasonForTravel(String reasonForTravel) {
        this.reasonForTravel = reasonForTravel;
    }

    public BigDecimal getTotalCost() {
        return totalCost;
    }

    public void setTotalCost(BigDecimal totalCost) {
        this.totalCost = totalCost;
    }

    public Date getDocumentDate() {
        return documentDate;
    }

    public void setDocumentDate(Date documentDate) {
        this.documentDate = documentDate;
    }

    public City getCityStartId() {
        return cityStartId;
    }

    public void setCityStartId(City cityStartId) {
        this.cityStartId = cityStartId;
    }

    public City getCityEndId() {
        return cityEndId;
    }

    public void setCityEndId(City cityEndId) {
        this.cityEndId = cityEndId;
    }

    public Driver getDriverID() {
        return driverID;
    }

    public void setDriverID(Driver driverID) {
        this.driverID = driverID;
    }

    public TravelOrderType getTravelOrderTypeID() {
        return travelOrderTypeID;
    }

    public void setTravelOrderTypeID(TravelOrderType travelOrderTypeID) {
        this.travelOrderTypeID = travelOrderTypeID;
    }

    public Vehicle getVehicleID() {
        return vehicleID;
    }

    public void setVehicleID(Vehicle vehicleID) {
        this.vehicleID = vehicleID;
    }

    @XmlTransient
    public Collection<RouteInfo> getRouteInfoCollection() {
        return routeInfoCollection;
    }

    public void setRouteInfoCollection(Collection<RouteInfo> routeInfoCollection) {
        this.routeInfoCollection = routeInfoCollection;
    }

    @XmlTransient
    public Collection<FuelInfo> getFuelInfoCollection() {
        return fuelInfoCollection;
    }

    public void setFuelInfoCollection(Collection<FuelInfo> fuelInfoCollection) {
        this.fuelInfoCollection = fuelInfoCollection;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (iDTravelOrder != null ? iDTravelOrder.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof TravelOrder)) {
            return false;
        }
        TravelOrder other = (TravelOrder) object;
        if ((this.iDTravelOrder == null && other.iDTravelOrder != null) || (this.iDTravelOrder != null && !this.iDTravelOrder.equals(other.iDTravelOrder))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "com.kvlahov.models.TravelOrder[ iDTravelOrder=" + iDTravelOrder + " ]";
    }
    
}
