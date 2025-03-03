import http  from "k6/http";
import { check, sleep } from "k6";

export let options = {
    insecureSkipTLSVerify: true,
    stages: [
        {duration: "10s", target: 50},
    ]
};
export default function() {
    const url = "https://gplay-auth-service.greensky-ab79edb4.southindia.azurecontainerapps.io/token";
    // const url = "http://localhost:3100/api/auth/token";
    // const url = "http://localhost:3300/token";

    const payload = JSON.stringify({
        "email":"mayall@gakktechnology.com",
        "password": "123456"
    });

    const params = {
        headers: {
        'Content-Type': 'application/json',
        },
    };

    let res = http.post(url, payload, params);

    check(res, {
        "is status 200": (r) => r.status === 200
    });
    sleep(1);
};