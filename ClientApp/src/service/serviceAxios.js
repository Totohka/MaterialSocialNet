import axios from "axios";

export default axios.create({
  baseURL: "http://localhost:5148",
  headers: {
    "Content-type": "application/json"
  }
});