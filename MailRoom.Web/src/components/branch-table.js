import React, { Component } from 'react';
//import { NavLink } from 'react-router-dom';

class BranchTable extends Component {

    render() {

        const list = () => {
            return this.props.branchDetails.jobManifestLogs.map((branch, index) => {
                return (
                    <tr key={branch.id} >
                        <td><a href={branch.id} target="_blank">{index + 1}</a></td>
                        <td>{branch.sn}</td>
                        <td>{branch.name}</td>
                        <td>{branch.pan}</td>
                        <td>{branch.branchCode}</td>
                        <td>{branch.branchName}</td>
                    </tr>
                )
            })
        }

        const footer = () => {
            
            return (
                <tr class="table-primary">
                    <td>JobId: {this.props.branchDetails.jobId}</td>
                    <td>AWB Number: {this.props.branchDetails.wayBillNumber}</td>
                    <td>Tracking Number {this.props.branchDetails.trackingNumber}</td>
                    <td>BRANCH: {this.props.branchDetails.clientBranch.address}</td>
                    <td>Data Number: {this.props.branchDetails.dataQuantity}</td>
                    <td>Custodian: {this.props.branchDetails.clientBranch.contactName} {this.props.branchDetails.clientBranch.contactPhone}</td>
                    <td></td>
                </tr>
            )
        }

        return (
            <div>
                <table className="table table-striped table-sm">
                    <thead>
                        <tr>
                            <th>SN</th>
                            <th>SortNo</th>
                            <th>NAME</th>
                            <th>PAN</th>
                            <th>BRANCH CODE</th>
                            <th>BRANCH NAME</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {list()}
                    </tbody>
                </table>
                <table className="table table-striped table-sm">
                    {footer()}
                </table>
                <br /><br />
            </div>
        );
    }
}

export default BranchTable;
