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
@Table(name = "TravelOrderType")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "TravelOrderType.findAll", query = "SELECT t FROM TravelOrderType t")
    , @NamedQuery(name = "TravelOrderType.findByIDTravelOrderType", query = "SELECT t FROM TravelOrderType t WHERE t.iDTravelOrderType = :iDTravelOrderType")
    , @NamedQuery(name = "TravelOrderType.findByType", query = "SELECT t FROM TravelOrderType t WHERE t.type = :type")})
public class TravelOrderType implements Serializable {

    private static final long serialVersionUID = 1L;
    @Id
    @Basic(optional = false)
    @Column(name = "IDTravelOrderType")
    private Integer iDTravelOrderType;
    @Basic(optional = false)
    @Column(name = "Type")
    private String type;
    @OneToMany(cascade = CascadeType.ALL, mappedBy = "travelOrderTypeID")
    private Collection<TravelOrder> travelOrderCollection;

    public TravelOrderType() {
    }

    public TravelOrderType(Integer iDTravelOrderType) {
        this.iDTravelOrderType = iDTravelOrderType;
    }

    public TravelOrderType(Integer iDTravelOrderType, String type) {
        this.iDTravelOrderType = iDTravelOrderType;
        this.type = type;
    }

    public Integer getIDTravelOrderType() {
        return iDTravelOrderType;
    }

    public void setIDTravelOrderType(Integer iDTravelOrderType) {
        this.iDTravelOrderType = iDTravelOrderType;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
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
        hash += (iDTravelOrderType != null ? iDTravelOrderType.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof TravelOrderType)) {
            return false;
        }
        TravelOrderType other = (TravelOrderType) object;
        if ((this.iDTravelOrderType == null && other.iDTravelOrderType != null) || (this.iDTravelOrderType != null && !this.iDTravelOrderType.equals(other.iDTravelOrderType))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "com.kvlahov.models.TravelOrderType[ iDTravelOrderType=" + iDTravelOrderType + " ]";
    }
    
}
