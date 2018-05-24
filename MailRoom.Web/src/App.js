import React, { Component } from 'react';
import Dropzone from 'react-dropzone';
import axios from 'axios';
import logo from './logo.svg';
import logo2 from './note.png';

import './App.css';

class App extends Component {
  constructor(props) {
    super(props);

    this.state = {
      uploadedFile: null,
      uploadedFileCloudinaryUrl: ''
    };
  }

  onImageDrop(files) {
    this.setState({
      uploadedFile: files[0]
    });
  }

  onHandleClick = (e, file) => {
    e.preventDefault();

    console.log(file);

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
      console.log(response);
    }).catch(function (error) {
      console.log(error);
    });



    //this.props.handleDelete(value);
  };

  render() {
    return (
      <div className="App">
        <header className="App-header">
          <img src={logo2} className="App-logo" alt="logo" />
          <h1 className="App-title">SecureID eWay-Bill</h1>
        </header>
        <p className="App-intro">
          {/*To get started, edit <code>src/App.js</code> and save to reload.*/}
        </p>

        <div className="File-intro">
          <form>
            <div className="FileUpload">
              <Dropzone
                onDrop={this.onImageDrop.bind(this)}
                multiple={false}
                >
                <div>Drop an image or click to select a file to upload.</div>
              </Dropzone>
            </div>

            <div>
              {this.state.uploadedFileCloudinaryUrl === '' ? null :
                <div>
                  <p>{this.state.uploadedFile.name}</p>
                  <img src={this.state.uploadedFileCloudinaryUrl} />
                </div>}
            </div>

            <button onClick={(e) => this.onHandleClick(e, this.state.uploadedFile)}>Submit</button>
          </form>

        </div>
      </div>
    );
  }
}

export default App;
