/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.kvlahov.models;

import java.io.Serializable;
import java.math.BigDecimal;
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
@Table(name = "FuelInfo")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "FuelInfo.findAll", query = "SELECT f FROM FuelInfo f")
    , @NamedQuery(name = "FuelInfo.findByIDFuelInfo", query = "SELECT f FROM FuelInfo f WHERE f.iDFuelInfo = :iDFuelInfo")
    , @NamedQuery(name = "FuelInfo.findByLocation", query = "SELECT f FROM FuelInfo f WHERE f.location = :location")
    , @NamedQuery(name = "FuelInfo.findByAmount", query = "SELECT f FROM FuelInfo f WHERE f.amount = :amount")
    , @NamedQuery(name = "FuelInfo.findByPrice", query = "SELECT f FROM FuelInfo f WHERE f.price = :price")
    , @NamedQuery(name = "FuelInfo.findByDatePurchased", query = "SELECT f FROM FuelInfo f WHERE f.datePurchased = :datePurchased")})
public class FuelInfo implements Serializable {

    private static final long serialVersionUID = 1L;
    @Id
    @Basic(optional = false)
    @Column(name = "IDFuelInfo")
    private Integer iDFuelInfo;
    @Column(name = "Location")
    private String location;
    // @Max(value=?)  @Min(value=?)//if you know range of your decimal fields consider using these annotations to enforce field validation
    @Column(name = "Amount")
    private Double amount;
    @Column(name = "Price")
    private BigDecimal price;
    @Column(name = "DatePurchased")
    @Temporal(TemporalType.DATE)
    private Date datePurchased;
    @JoinColumn(name = "TravelOrderID", referencedColumnName = "IDTravelOrder")
    @ManyToOne
    private TravelOrder travelOrderID;

    public FuelInfo() {
    }

    public FuelInfo(Integer iDFuelInfo) {
        this.iDFuelInfo = iDFuelInfo;
    }

    public Integer getIDFuelInfo() {
        return iDFuelInfo;
    }

    public void setIDFuelInfo(Integer iDFuelInfo) {
        this.iDFuelInfo = iDFuelInfo;
    }

    public String getLocation() {
        return location;
    }

    public void setLocation(String location) {
        this.location = location;
    }

    public Double getAmount() {
        return amount;
    }

    public void setAmount(Double amount) {
        this.amount = amount;
    }

    public BigDecimal getPrice() {
        return price;
    }

    public void setPrice(BigDecimal price) {
        this.price = price;
    }

    public Date getDatePurchased() {
        return datePurchased;
    }

    public void setDatePurchased(Date datePurchased) {
        this.datePurchased = datePurchased;
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
        hash += (iDFuelInfo != null ? iDFuelInfo.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof FuelInfo)) {
            return false;
        }
        FuelInfo other = (FuelInfo) object;
        if ((this.iDFuelInfo == null && other.iDFuelInfo != null) || (this.iDFuelInfo != null && !this.iDFuelInfo.equals(other.iDFuelInfo))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "com.kvlahov.models.FuelInfo[ iDFuelInfo=" + iDFuelInfo + " ]";
    }
    
}
