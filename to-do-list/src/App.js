import React from 'react';
import { Provider } from 'react-redux';
import store from './store/store';
import Header from './components/Header';
import Main from './components/Main';
import 'bootstrap/dist/css/bootstrap.min.css';

const App = () => (
    <Provider store={store}>
        <div className="container">
            <Header />
            <Main />
        </div>
    </Provider>
);

export default App;
