import axios from "axios";

export default axios.create({
    baseURL: "http://localhost:5057/api", 
    headers: {
        "Content-type": "application/json"
    }
});
