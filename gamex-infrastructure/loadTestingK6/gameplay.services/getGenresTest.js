import http  from "k6/http";
import { check, sleep } from "k6";

export let options = {
    insecureSkipTLSVerify: true,
    stages: [
        {duration: "10s", target: 10},
        {duration: "60s", target: 20},
    ]
};
export default function() {

    const host = "https://devapi.onegames.online/api/play/";


 

    const params = {
        headers: {
        'Content-Type': 'application/json',
        },
    };

    // Request to Register
    let url = host + "genres";
    let registrationResponse = http.get(url, params);

 
    check(registrationResponse, {
        "is status 200": (r) => r.status === 200
    });
    sleep(1);

}