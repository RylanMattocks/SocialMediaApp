import { useContext, useState } from "react";
import { UserContext } from "../context/UserContext";
import { createUser, getUserLogin } from "../functions/user";
import { useNavigate, NavLink } from "react-router-dom";

const RegisterPage = () => {
    const { login } = useContext(UserContext);
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        username: "",
        email: "",
        profileImageUrl: ""
    });

    const handleChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const createdUser = await createUser(formData.username, formData.email, formData.profileImageUrl);
            if (createdUser) {
                const user = await getUserLogin(formData.username);
                login(user);
                navigate("/home");
            }
        } catch (err) {
            console.error(err);
        }
    };

    return (
        <div className="container">
            <div className="register-page">
            
                <h2>Register</h2>
                <form onSubmit={handleSubmit}>

                   
                    <div className="form-group">
                        <label htmlFor="username">Username</label>
                        <input
                            id="username"
                            name="username"
                            type="text"
                            value={formData.username}
                            onChange={handleChange}
                            placeholder="Username"
                            required
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="email">Email</label>
                        <input
                            id="email"
                            name="email"
                            type="email"
                            value={formData.email}
                            onChange={handleChange}
                            placeholder="Email"
                            required
                        />
                    </div>
                    <div className="form-group last">
                        <label htmlFor="profileImageUrl">Image URL</label>
                        <input
                            id="profileImageUrl"
                            name="profileImageUrl"
                            type="text"
                            value={formData.profileImageUrl}
                            onChange={handleChange}
                            placeholder="Image URL"
                            required
                        />
                    </div>
                    <div className="submitOrLogin">
                        <button type="submit">Register</button>
                        <p>Ready to login?</p>
                        <button>
                            <NavLink to="/login">Login Here</NavLink>
                        </button>
                    </div>
                </form>


            </div>
        </div>
    );
};

export default RegisterPage;