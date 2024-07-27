import React from "react";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  Link,
  Navigate,
  useLocation,
} from "react-router-dom";
import useSignalR from "../../app/useSignalR";

const RequireAuth = ({ children }) => {
    let location = useLocation();
    if (localStorage.getItem("token") == undefined) {
      return <Navigate to="/login" state={{ from: location }} replace />;
    }
    return children;
  }
export default RequireAuth;