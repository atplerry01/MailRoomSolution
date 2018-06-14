import React from 'react';
import ReactDOM from 'react-dom';
import '../node_modules/bootstrap/dist/css/bootstrap.min.css';

import './index.css';
import App from './App';
import registerServiceWorker from './registerServiceWorker';

// import ApolloClient from "apollo-boost";
// import { ApolloProvider } from "react-apollo";
// import { InMemoryCache } from 'apollo-cache-inmemory';
// import { RestLink } from "apollo-link-rest";
// import gql from "graphql-tag";


ReactDOM.render(<App />, document.getElementById('root'));
registerServiceWorker();
