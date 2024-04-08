import { TodoList } from "../../Components/TodoList";
import { useAuth } from "react-oidc-context";
import React, {useEffect, useState} from "react";
import {darkTheme, GlobalStyles, lightTheme} from "../../Components/Themes";
import {ThemeProvider} from "styled-components";

export const Home: React.FC = () => {
    const auth = useAuth();

    useEffect(() => {
        if (!auth.isAuthenticated && !auth.isLoading) {
            void auth.signinRedirect();
        }
    });

    const [theme, setTheme] = useState("light");
    const isDarkTheme = theme === "dark";
    const toggleTheme = () => setTheme(isDarkTheme ? "light" : "dark");

    return (
        <ThemeProvider theme={isDarkTheme ? darkTheme : lightTheme}>
            <>
                <GlobalStyles />
                <div className={"h-full w-full flex items-center justify-center align-center"}>
                    {auth.isAuthenticated && !auth.isLoading ? (
                        <>
                            <div>
                                <button
                                    className={"flex-no-shrink p-2 ml-4 mr-2 border-2 rounded bg-gray-dark border-gray-dark hover:brightness-110 text-nice-white"}
                                    onClick={() => void auth.signoutRedirect()}
                                >
                                    Log out
                                </button>
                            </div>
                            <button onClick={toggleTheme} className={"flex-no-shrink p-2 ml-4 mr-2 border-2 rounded bg-gray-dark border-gray-dark hover:brightness-110"}>
                                {isDarkTheme ?
                                    <span aria-label="Light mode" role="img">🌞</span> :
                                    <span aria-label="Dark mode" role="img">🌜</span>}
                            </button>
                            <TodoList/>
                        </>
                    ) : (
                        <div>
                            <img src={"loader.gif"} alt={"loading spinner"}/>
                        </div>
                    )}
                </div>
            </>
        </ThemeProvider>
    );
};