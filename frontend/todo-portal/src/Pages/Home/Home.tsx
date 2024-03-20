import { TodoList } from "../../Components/TodoList";
import { useAuth } from "react-oidc-context";
import React, { useEffect } from "react";

export const Home: React.FC = () => {
    const auth = useAuth();

    useEffect(() => {
        if (!auth.isAuthenticated && !auth.isLoading) {
            void auth.signinRedirect();
        }
    });

    return (
        <>
            <div className={"h-full w-full flex items-center justify-center align-center"}>
                {auth.isAuthenticated && !auth.isLoading ? (
                    <>
                        <div>
                            <button
                                className={"font-bold"}
                                onClick={() => void auth.signoutRedirect()}
                            >
                                Log out
                            </button>
                        </div>
                        <TodoList />
                    </>
                ) : (
                    <div>
                        <img src={"loader.gif"} alt={"loading spinner"} />
                    </div>
                )}
            </div>
        </>
    );
};