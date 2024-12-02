import React, { useContext, useState } from "react";
import { NavLink } from "react-router-dom";
import SideBar from "./SideBar.jsx";
import AddPostPopup from "./AddPostPopup.jsx";
import { UserContext } from "../context/UserContext.jsx";

function Navbar() {
    const [isMenuOpen, setIsMenuOpen] = useState(false); // Ensure it starts closed
    const [ postMenuOpen, setPostMenuOpen ] = useState(false);
    const { logout, currentUser } = useContext(UserContext);

    const toggleMenu = () => {
        setIsMenuOpen(prevState => !prevState); // Toggle the menu state
    };

    const togglePostAdd = () => {
        setPostMenuOpen(prevState => !prevState);
    }

    return (
        <>
            <nav>
                <section id="hamburger" onClick={toggleMenu}>
                    <span></span>
                    <span></span>
                    <span></span>
                </section>
                <div id="logo">
                    <div className="bookIcon">
                        <div className="bookLabel" />
                    </div>
                    <NavLink id="dropTable" to="/home">DROP TABLE</NavLink>
                </div>
                <ul id="navList">
                    <li id="navButtonsItem">
                        <section id="navButtons">
                            <ul>
                                <li className="navButton">
                                    <NavLink to="/home">Home</NavLink>
                                </li>
                                <li className="navButton">
                                    <NavLink to="/feed">Your Feed</NavLink>
                                </li>
                                <li id="profileButton" className="navButton">
                                    <NavLink to="/profile">Profile</NavLink>
                                </li>
                            </ul>
                        </section>
                    </li>
                </ul>
                <section id="avatarSection">
                    <ul>
                        <li>
                            <button onClick={logout}>Logout</button>
                        </li>
                        <li>
                            <img onClick={togglePostAdd}
                                src={currentUser?.profileImageUrl} 
                                alt="User Avatar" 
                                id="avatar" 
                            />
                        </li>
                    </ul>
                </section>
            </nav>
            {/* Sidebar should be placed here */}
            <SideBar isOpen={isMenuOpen} />
            {postMenuOpen && (
                <AddPostPopup isOpen={postMenuOpen} onClose={() => setPostMenuOpen(false)} />
            )}
        </>
    );
}

export default Navbar;                {/* The hamburger menu is used to toggle the menu on and off */}
            {/* The sidebar is placed here, and is only visible when the menu is open */}

