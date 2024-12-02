import { useContext } from "react"
import { UserContext } from "./context/UserContext"
import { Navigate } from "react-router-dom";

const LoginRoute = ({ children }) => {
    const { currentUser } = useContext(UserContext);

    if (!currentUser) {
        return <Navigate to="/login" />;
    }
    return children;
}

export default LoginRoute;