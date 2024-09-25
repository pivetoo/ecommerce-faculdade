import http from "../http-common";

class UserDataService {
    create(data) {
        return http.post("/user/addCliente", data);
    }

    logar(data) {
        return http.post("/user/login", data);
    }
}

export default new UserDataService();

