import React, { Component } from 'react';
import { NavLink } from 'react-router-dom';

class BranchTable extends Component {

    componentDidUpdate() {
        console.log(this.props);
    }

    componentWillMount() {
        console.log(this.props);
    }

    render() {

        const list = () => {
            console.log(this.props.branchDetails.jobManifestLogs);
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
            console.log(this.props.branchDetails);
            return (
                <tr class="table-primary">
                    <td>JobId: 1234</td>
                    <td>AWB Number: WEATFDGHJJ</td>
                    <td>BRANCH: IKEJA</td>
                    <td>Data Number: 100</td>
                    <td>Custodian</td>
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
