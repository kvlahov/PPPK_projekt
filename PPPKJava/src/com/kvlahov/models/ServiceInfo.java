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
@Table(name = "ServiceInfo")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "ServiceInfo.findAll", query = "SELECT s FROM ServiceInfo s")
    , @NamedQuery(name = "ServiceInfo.findByIDService", query = "SELECT s FROM ServiceInfo s WHERE s.iDService = :iDService")
    , @NamedQuery(name = "ServiceInfo.findByDatetimeService", query = "SELECT s FROM ServiceInfo s WHERE s.datetimeService = :datetimeService")
    , @NamedQuery(name = "ServiceInfo.findByCost", query = "SELECT s FROM ServiceInfo s WHERE s.cost = :cost")
    , @NamedQuery(name = "ServiceInfo.findByDescription", query = "SELECT s FROM ServiceInfo s WHERE s.description = :description")})
public class ServiceInfo implements Serializable {

    private static final long serialVersionUID = 1L;
    @Id
    @Basic(optional = false)
    @Column(name = "IDService")
    private Integer iDService;
    @Column(name = "DatetimeService")
    @Temporal(TemporalType.TIMESTAMP)
    private Date datetimeService;
    // @Max(value=?)  @Min(value=?)//if you know range of your decimal fields consider using these annotations to enforce field validation
    @Column(name = "Cost")
    private BigDecimal cost;
    @Column(name = "Description")
    private String description;
    @JoinColumn(name = "VehicleID", referencedColumnName = "IDVehicle")
    @ManyToOne(optional = false)
    private Vehicle vehicleID;

    public ServiceInfo() {
    }

    public ServiceInfo(Integer iDService) {
        this.iDService = iDService;
    }

    public Integer getIDService() {
        return iDService;
    }

    public void setIDService(Integer iDService) {
        this.iDService = iDService;
    }

    public Date getDatetimeService() {
        return datetimeService;
    }

    public void setDatetimeService(Date datetimeService) {
        this.datetimeService = datetimeService;
    }

    public BigDecimal getCost() {
        return cost;
    }

    public void setCost(BigDecimal cost) {
        this.cost = cost;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public Vehicle getVehicleID() {
        return vehicleID;
    }

    public void setVehicleID(Vehicle vehicleID) {
        this.vehicleID = vehicleID;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (iDService != null ? iDService.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof ServiceInfo)) {
            return false;
        }
        ServiceInfo other = (ServiceInfo) object;
        if ((this.iDService == null && other.iDService != null) || (this.iDService != null && !this.iDService.equals(other.iDService))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "com.kvlahov.models.ServiceInfo[ iDService=" + iDService + " ]";
    }
    
}
