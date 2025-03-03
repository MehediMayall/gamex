import http  from "k6/http";
import { check, sleep } from "k6";

export let options = {
    insecureSkipTLSVerify: true,
    stages: [
        {duration: "600s", target: 50},
    ]
};
export default function() {

    let randomPageNumber = Math.floor(Math.random() * 60);
    const url = "https://gplay-api-gateway.happyhill-1497bde2.centralindia.azurecontainerapps.io/api/welcome";


    const params = {
        headers: {
        'Content-Type': 'application/json',
        },
    };

    let res = http.get(url, params);

    check(res, {
        "is status 200": (r) => r.status === 200
    });
    sleep(1);
};