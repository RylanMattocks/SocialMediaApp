import { useContext, useState } from "react"
import { UserContext } from "../context/UserContext"
import { getUserLogin } from "../functions/user";
import { useNavigate, NavLink } from "react-router-dom";

const LoginPage = () => {
    const { login } = useContext(UserContext);
    const [userName, setUserName] = useState("");
    const navigate = useNavigate();

    const handleLogin = async (e) => {
        e.preventDefault();
        const user = await getUserLogin(userName);
        login(user);
        navigate("/home")
    };

    return (
        <>
            <div className="container">


                <div className="login-page">
                    <h1>Login</h1>
                    <form onSubmit={handleLogin}>
                        <div className="loginCol">
                            <input
                                type="text"
                                placeholder="Username"
                                value={userName}
                                onChange={(e) => setUserName(e.target.value)}
                            />
                            <button className="login-button" type="submit">Login</button>
                        </div>
                    </form>

                    <h3>Don't have an account?</h3>
                    <div>
                        <button className="login-button">
                            <NavLink to="/register">Register</NavLink>
                        </button>
                    </div>
                </div>
            </div>
        </>
    )
}

export default LoginPage;