import React, { Component } from 'react';
import Dropzone from 'react-dropzone';
import { Table, Button, Bootstrap, Grid, Row, Col } from 'react-bootstrap';

import axios from 'axios';
import logo from './logo.svg';
import logo2 from './note.png';
import BranchTable from './components/branch-table';

import './App.css';

class App extends Component {
  constructor(props) {
    super(props);

    this.state = {
      uploadedFile: null,
      uploadedFileCloudinaryUrl: '',
      manifest: ''
    };
  }

  componentWillUpdate() {
    console.log(this.state.manifest);
  }

  onImageDrop(files) {
    this.setState({
      uploadedFile: files[0]
    });
  }

  onHandleClick = (e, file) => {
    e.preventDefault();

    const url = 'http://localhost:5000/api/jobupload';
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
        const data = response.data;
        const fileURL = data.secure_url // You should store this URL for future references in your app
        console.log(response.data);
        this.setState({
          manifest: response.data
        });

        console.log(this.state);

      }).catch(function (error) {
        console.log(error);
      });



    //this.props.handleDelete(value);
  };

  getBranchTableList(branchLists) {
    console.log(branchLists);
    // return this.state.manifest.jobManifestBranchs.map((branch, index) => {
    //   return (<div>Branch</div>)
    // })
  }

  getManifestLogs(manifestLogs) {
    return this.state.manifest.jobManifestLogs.map((manifestLog, index) => {
      return (<div>Logs</div>)
    })
  }

  render() {

    console.log(this.state);
    console.log(this.state.manifest.jobManifestBranchs);
    console.log(this.state.manifest.jobManifestLogs);

    const list = () => {
      if (this.state.manifest) {
        return this.state.manifest.jobManifestBranchs.map((branch, index) => {
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
                          <img src={this.state.uploadedFileCloudinaryUrl} />
                        </div>}
                    </div>
                    <br />
                    <Button onClick={(e) => this.onHandleClick(e, this.state.uploadedFile)} bsStyle="success">Upload JobFile</Button>
                  </form>

                </div>
              </Col>
              <Col xs={6} md={9}>

                <h2> Job Manifest Details </h2>

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
