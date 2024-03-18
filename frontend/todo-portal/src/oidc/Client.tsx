import { User } from "oidc-client-ts";

export const oidcClient = {
    authority: `${process.env.REACT_APP_IDENTITY_BASE_ADDRESS}`,
    redirect_uri: `${process.env.REACT_APP_BASE_ADDRESS}`,
    post_logout_redirect_uri: `${process.env.REACT_APP_BASE_ADDRESS}`,
    client_id: `${process.env.REACT_APP_CLIENT_ID}`,

    onSigninCallback: (_user: User | void): void => {
        window.history.replaceState({}, document.title, window.location.pathname);
    },
};