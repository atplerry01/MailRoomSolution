import React, { Component } from 'react';
import Dropzone from 'react-dropzone';
import { Button, Grid, Row, Col } from 'react-bootstrap';

import axios from 'axios';
import logo2 from './note.png';
import BranchTable from './components/branch-table';

import './App.css';

class App extends Component {
  constructor(props) {
    super(props);

    this.state = {
      uploadedFile: null,
      uploadedFileCloudinaryUrl: '',
      manifest: '',
      trackingNumber: '',
      masterAWB: '',
    };
  }

  onImageDrop(files) {
    this.setState({
      uploadedFile: files[0]
    });
  }

  onHandleClick = (e, file) => {
    e.preventDefault();

    const url = 'http://localhost:5050/api/jobupload';
    const formData = new FormData();
    formData.append('file', file)
    const config = {
      headers: {
        'content-type': 'multipart/form-data'
      },
      acceptedFiles: 'text/csv',
    }

    return axios.post(url, formData, config)
      .then(response => {
        console.log(response.data);
        this.setState({
          manifest: response.data
        });

        console.log(this.state);

      }).catch(function (error) {
        console.log(error);
      });
  };

  onHandleAWBInfo = (e, id) => {
    e.preventDefault();

    const url = 'http://localhost:5050/api/airwaybill/' + id;
    console.log(url);

    
    return axios.post(url)
      .then(response => {
        console.log(response);
        //masterTrackerNumber
        this.setState({
          masterAWB: response.data
        });

        console.log(this.state.masterAWB);

      }).catch(function (error) {
        console.log(error);
      });


  }

  getManifestLogs(manifestLogs) {
    return this.state.manifest.jobManifestLogs.map((manifestLog, index) => {
      return (<div>Logs</div>)
    })
  }

  render() {
    const manifestTop = () => {
      if (this.state.masterAWB.trackingNumber) {
        return (<div>
          <table className="manifestDetail">
            <tr>
              <td><h6><strong>JobId:</strong> {this.state.manifest.jobId}</h6></td>
             
            </tr>
            <tr> 
            <td><h6><strong>Master AirWayBillNumber:</strong> {this.state.masterAWB.wayBillNumber}</h6></td>
           
            </tr>
            <tr> <td><h6><strong>Tracking Number:</strong> {this.state.masterAWB.trackingNumber}</h6></td></tr>
          </table>     

          <br /> <br />
        </div>);
      } else {
        return (<Button onClick={(e) => this.onHandleAWBInfo(e, this.state.manifest.id)} bsStyle="primary">Generate AWB</Button>); 
      }
    };

    const list = () => {
      console.log(this.state.masterAWB);

      if (this.state.masterAWB) {
        return this.state.masterAWB.jobManifestBranchs.map((branch, index) => {
          return (<BranchTable key = {index + 1} index = {index} branchDetails = {branch} />);
        })
      }
    }

    return (
      <div className="App">
        <header className="App-header">
          <img src={logo2} className="App-logo" alt="logo" />
          <h1 className="App-title">SecureID eWay-Bill</h1>
        </header>
        <p className="App-intro">
          {/*To get started, edit <code>src/App.js</code> and save to reload.*/}
        </p>

        <div>
          <Grid>
            <Row className="show-grid">
              <Col xs={12} md={3}>
                <div>
                  <form>
                    <div className="FileUpload">
                      <Dropzone
                        onDrop={this.onImageDrop.bind(this)}
                        multiple={false}
                      >
                        <div>Drop job file or click to select a file to upload.</div>
                      </Dropzone>
                    </div>

                    <div>
                      {this.state.uploadedFileCloudinaryUrl === '' ? null :
                        <div>
                          <p>{this.state.uploadedFile.name}</p>
                          <img src={this.state.uploadedFileCloudinaryUrl} alt="" />
                        </div>}
                    </div>
                    <br />
                    <Button onClick={(e) => this.onHandleClick(e, this.state.uploadedFile)} bsStyle="success">Upload JobFile</Button>

                  </form>

                  

                </div>
              </Col>
              <Col xs={6} md={9}>

                <h2> Job Manifest Details </h2>
                
                {manifestTop()}
              
                {list()}

              </Col>
            </Row>
          </Grid>
        </div>

      </div>
    );
  }
}

export default App;
