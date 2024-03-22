import {Routes, Route, BrowserRouter} from "react-router-dom";
import { Home } from "./Pages";
import { AuthProvider } from "react-oidc-context";
import { oidcClient } from "./oidc";
import React, {useState} from "react";
import {ThemeProvider} from "styled-components";
import {darkTheme, GlobalStyles, lightTheme} from "./Components/Themes";

function App() {
    return (
        <>
            <AuthProvider {...oidcClient}>
                <Routes>
                    <Route path={"/"} element={<Home />} />
                </Routes>
            </AuthProvider>
        </>
    );
}

export default App;