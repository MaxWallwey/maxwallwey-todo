import React, { useState } from "react";

export const ApiResponseContext = React.createContext({
    apiResponseMessage: "",
    apiResponseCode: 0,
    setApiResponse: (apiResponseMessage: string, apiResponseCode: number) => {},
});

export const ApiContextProvider = (props: any) => {
    const setApiResponse = (
        apiResponseMessage: string,
        apiResponseCode: number
    ) => {
        setState({
            ...state,
            apiResponseMessage: apiResponseMessage,
            apiResponseCode: apiResponseCode,
        });
    };

    const initState = {
        apiResponseMessage: "",
        apiResponseCode: 0,
        setApiResponse: setApiResponse,
    };

    const [state, setState] = useState(initState);

    return (
        <ApiResponseContext.Provider value={state}>
            {props.children}
        </ApiResponseContext.Provider>
    );
};