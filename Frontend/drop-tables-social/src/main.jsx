import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import {createBrowserRouter,RouterProvider,} from "react-router-dom";

import './index.css'
import './Styles/Profile.css'
import './Styles/ProfilePage.css'
import './Styles/ProfileTab.css'
import './Styles/AddPost.css'
import "./styles/Navbar.css";
import "./Styles/LoginPage.css";
import "./Styles/RegisterPage.css";
import ErrorPage from "./error-page";
import Layout from './pages/Layout.jsx';
import Home from './pages/Home.jsx';
import ProfilePage from './pages/ProfilePage.jsx';
import Feed from './pages/Feed.jsx';
import { UserProvider } from './context/UserContext.jsx';
import LoginPage from './pages/LoginPage.jsx';
import LoginRoute from './LoginRoute.jsx';
import RegisterPage from './pages/RegisterPage.jsx';

const router = createBrowserRouter([
  {
    path: "/",
    element: <Layout/>,
    errorElement: <ErrorPage />,
    children: [
      {
        path: "home/",
        element: (
          <LoginRoute>
            <Home/>
          </LoginRoute>
        )
      },
      {
        path: "profile/",
        element: (
          <LoginRoute>
            <ProfilePage/>
          </LoginRoute>
        )
      },
      {
        path: "feed/",
        element: (
          <LoginRoute>
            <Feed/>
          </LoginRoute>
        )
      },
      {
        path: "login",
        element: <LoginPage/>
      },
      {
        path: "register",
        element: <RegisterPage />
      }
    ]
  }
])

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <UserProvider>
      <RouterProvider router={router} />
    </UserProvider>
  </StrictMode>,
)
