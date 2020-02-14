/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.kvlahov.models;

import java.io.Serializable;
import java.util.Date;
import javax.persistence.Basic;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.Table;
import javax.persistence.Temporal;
import javax.persistence.TemporalType;
import javax.xml.bind.annotation.XmlRootElement;

/**
 *
 * @author lordo
 */
@Entity
@Table(name = "RouteInfo")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "RouteInfo.findAll", query = "SELECT r FROM RouteInfo r")
    , @NamedQuery(name = "RouteInfo.findByIDRouteInfo", query = "SELECT r FROM RouteInfo r WHERE r.iDRouteInfo = :iDRouteInfo")
    , @NamedQuery(name = "RouteInfo.findByDateTimeStart", query = "SELECT r FROM RouteInfo r WHERE r.dateTimeStart = :dateTimeStart")
    , @NamedQuery(name = "RouteInfo.findByDateTimeEnd", query = "SELECT r FROM RouteInfo r WHERE r.dateTimeEnd = :dateTimeEnd")
    , @NamedQuery(name = "RouteInfo.findByLatitudeStart", query = "SELECT r FROM RouteInfo r WHERE r.latitudeStart = :latitudeStart")
    , @NamedQuery(name = "RouteInfo.findByLongitudeStart", query = "SELECT r FROM RouteInfo r WHERE r.longitudeStart = :longitudeStart")
    , @NamedQuery(name = "RouteInfo.findByLatitudeEnd", query = "SELECT r FROM RouteInfo r WHERE r.latitudeEnd = :latitudeEnd")
    , @NamedQuery(name = "RouteInfo.findByLongitudeEnd", query = "SELECT r FROM RouteInfo r WHERE r.longitudeEnd = :longitudeEnd")
    , @NamedQuery(name = "RouteInfo.findByDistanceInKm", query = "SELECT r FROM RouteInfo r WHERE r.distanceInKm = :distanceInKm")
    , @NamedQuery(name = "RouteInfo.findByAverageSpeed", query = "SELECT r FROM RouteInfo r WHERE r.averageSpeed = :averageSpeed")
    , @NamedQuery(name = "RouteInfo.findByFuelExpense", query = "SELECT r FROM RouteInfo r WHERE r.fuelExpense = :fuelExpense")})
public class RouteInfo implements Serializable {

    private static final long serialVersionUID = 1L;
    @Id
    @Basic(optional = false)
    @Column(name = "IDRouteInfo")
    private Integer iDRouteInfo;
    @Column(name = "DateTimeStart")
    @Temporal(TemporalType.TIMESTAMP)
    private Date dateTimeStart;
    @Column(name = "DateTimeEnd")
    @Temporal(TemporalType.TIMESTAMP)
    private Date dateTimeEnd;
    // @Max(value=?)  @Min(value=?)//if you know range of your decimal fields consider using these annotations to enforce field validation
    @Column(name = "LatitudeStart")
    private Double latitudeStart;
    @Column(name = "LongitudeStart")
    private Double longitudeStart;
    @Column(name = "LatitudeEnd")
    private Double latitudeEnd;
    @Column(name = "LongitudeEnd")
    private Double longitudeEnd;
    @Column(name = "DistanceInKm")
    private Double distanceInKm;
    @Column(name = "AverageSpeed")
    private Double averageSpeed;
    @Column(name = "FuelExpense")
    private Double fuelExpense;
    @JoinColumn(name = "TravelOrderID", referencedColumnName = "IDTravelOrder")
    @ManyToOne
    private TravelOrder travelOrderID;

    public RouteInfo() {
    }

    public RouteInfo(Integer iDRouteInfo) {
        this.iDRouteInfo = iDRouteInfo;
    }

    public Integer getIDRouteInfo() {
        return iDRouteInfo;
    }

    public void setIDRouteInfo(Integer iDRouteInfo) {
        this.iDRouteInfo = iDRouteInfo;
    }

    public Date getDateTimeStart() {
        return dateTimeStart;
    }

    public void setDateTimeStart(Date dateTimeStart) {
        this.dateTimeStart = dateTimeStart;
    }

    public Date getDateTimeEnd() {
        return dateTimeEnd;
    }

    public void setDateTimeEnd(Date dateTimeEnd) {
        this.dateTimeEnd = dateTimeEnd;
    }

    public Double getLatitudeStart() {
        return latitudeStart;
    }

    public void setLatitudeStart(Double latitudeStart) {
        this.latitudeStart = latitudeStart;
    }

    public Double getLongitudeStart() {
        return longitudeStart;
    }

    public void setLongitudeStart(Double longitudeStart) {
        this.longitudeStart = longitudeStart;
    }

    public Double getLatitudeEnd() {
        return latitudeEnd;
    }

    public void setLatitudeEnd(Double latitudeEnd) {
        this.latitudeEnd = latitudeEnd;
    }

    public Double getLongitudeEnd() {
        return longitudeEnd;
    }

    public void setLongitudeEnd(Double longitudeEnd) {
        this.longitudeEnd = longitudeEnd;
    }

    public Double getDistanceInKm() {
        return distanceInKm;
    }

    public void setDistanceInKm(Double distanceInKm) {
        this.distanceInKm = distanceInKm;
    }

    public Double getAverageSpeed() {
        return averageSpeed;
    }

    public void setAverageSpeed(Double averageSpeed) {
        this.averageSpeed = averageSpeed;
    }

    public Double getFuelExpense() {
        return fuelExpense;
    }

    public void setFuelExpense(Double fuelExpense) {
        this.fuelExpense = fuelExpense;
    }

    public TravelOrder getTravelOrderID() {
        return travelOrderID;
    }

    public void setTravelOrderID(TravelOrder travelOrderID) {
        this.travelOrderID = travelOrderID;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (iDRouteInfo != null ? iDRouteInfo.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof RouteInfo)) {
            return false;
        }
        RouteInfo other = (RouteInfo) object;
        if ((this.iDRouteInfo == null && other.iDRouteInfo != null) || (this.iDRouteInfo != null && !this.iDRouteInfo.equals(other.iDRouteInfo))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "com.kvlahov.models.RouteInfo[ iDRouteInfo=" + iDRouteInfo + " ]";
    }
    
}
