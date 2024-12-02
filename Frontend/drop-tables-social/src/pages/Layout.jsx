import {Outlet} from "react-router-dom";
import Navbar from '../components/Navbar';

function Layout() {
    return(
        <>
            <Navbar/>
            <div id="page">
                <div id="main">
                    <Outlet/>
                </div>
            </div>
        </>
    );
};

export default Layout;