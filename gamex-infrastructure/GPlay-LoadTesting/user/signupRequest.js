import http  from "k6/http";
import { check, sleep } from "k6";

export let options = {
    insecureSkipTLSVerify: true,
    stages: [
        {duration: "60s", target: 1000},
        {duration: "300s", target: 5000},
    ]
};
export default function() {

    const host = "https://gplay-auth-service.greensky-ab79edb4.southindia.azurecontainerapps.io/";
    // const host = "http://localhost:5071/api/auth/";
    // const host = "http://localhost:3100/api/auth/";
    // const host = "http://localhost:3300/";

    let payload = JSON.stringify({
        "firstname":"Mehedi",
        "lastname":"Hasan",
        "mobile":"5554443333",
        "dateofbirth":"1998-09-25",
        "email":"mehedi.sun@gmail.com",
        "password": "2342343"
    });

    const params = {
        headers: {
        'Content-Type': 'application/json',
        },
    };

    // Request to Register
    let url = host + "signup/request";
    let signupResponse = http.post(url, payload, params);

    if(signupResponse.json().errors != null) 
        console.log(signupResponse.json().errors[0].message);
 
    check(signupResponse, {
        "is status 200": (r) => r.status === 200
    });
    sleep(1);

}