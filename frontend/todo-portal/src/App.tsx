import {Routes, Route, BrowserRouter} from "react-router-dom";
import { Home } from "./Pages";
import { AuthProvider } from "react-oidc-context";
import { oidcClient } from "./oidc";

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