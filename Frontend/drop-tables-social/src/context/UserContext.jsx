import { createContext, useState } from "react";
import { getUserLogin } from "../functions/user";

export const UserContext = createContext();

export const UserProvider = ({ children }) => {
    const [currentUser, setCurrentUser] = useState(() => {
        const savedUser = localStorage.getItem("currentUser");
        return savedUser ? JSON.parse(savedUser) : null;
    });

    const login = (user) => {
        setCurrentUser(user);
        localStorage.setItem("currentUser", JSON.stringify(user));
    }

    const logout = () => {
        setCurrentUser(null);
        localStorage.removeItem("currentUser");
    }

    const handleChange = async () => {
        if (currentUser) {
            login(await getUserLogin(currentUser.username));
            console.log(await getUserLogin(currentUser.username))
        }
    }

    return (
        <UserContext.Provider value={{ currentUser, login, logout, handleChange }}>
            {children}
        </UserContext.Provider>
    )
}