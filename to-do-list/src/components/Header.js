import React from 'react';

function Header({ useXml, setUseXml }) {
    return (
        <div className="d-flex justify-content-center mb-3">
            <div className="btn-group" role="group">
                <button
                    className={`btn ${useXml ? "btn-primary" : "btn-outline-primary"}`}
                    onClick={() => setUseXml(true)}
                >
                    Показати завдання з XML
                </button>
                <button
                    className={`btn ${!useXml ? "btn-primary" : "btn-outline-primary"}`}
                    onClick={() => setUseXml(false)}
                >
                    Показати завдання з SQL
                </button>
            </div>
        </div>
    );
}

export default Header;
