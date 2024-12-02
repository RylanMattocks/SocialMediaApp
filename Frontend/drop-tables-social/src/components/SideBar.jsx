import React from "react";
import { NavLink } from "react-router-dom";
import '../styles/SideBar.css';
// A
export default function SideBar({ isOpen }) {
    return (
        <section className={`sidebar ${isOpen ? 'open' : ''}`}>
            <label htmlFor="searchBar">Search: </label>
            <input type="search" name="search" id="searchBar" />

            <ul>
                <li>
                    <NavLink to="/home">Home</NavLink>
                </li>
                <li>
                    <NavLink to="/feed">Your Feed</NavLink>
                </li>
                <li>
                    <NavLink to="/profile">Profile</NavLink>
                </li>
            </ul>
        </section>
    );
}