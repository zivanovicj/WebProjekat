import React, {useState} from "react";

const AuthContext = React.createContext({
    token: '',
    isLoggedIn: false,
    userType: '',
    login: (token, userType) => {},
    logout: () => {}
});

export const AuthContextProvider = (props) => {
    const localToken=localStorage.getItem('token');
    const localUserType = localStorage.getItem('userType');
    const [token, setToken] = useState(localToken);
    const [userType, setUserType] = useState(localUserType);

    const userIsLoggedIn = !!token;
    
    const logoutHandler= () => {
        setToken(null);
        setUserType('');
        localStorage.removeItem('token');
        localStorage.removeItem('userType');
    };

    const loginHandler = (token, userType) => {
        setToken(token);
        setUserType(userType);
        localStorage.setItem('token', token);
    };

    const contextValue = {
        token:token,
        isloggedIn:userIsLoggedIn,
        userType:userType,
        login: loginHandler,
        logout: logoutHandler
    };

    return <AuthContext.Provider value={contextValue}>{props.children}</AuthContext.Provider>
};

export default AuthContext;