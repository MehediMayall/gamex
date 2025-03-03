import http  from "k6/http";
import { check, sleep } from "k6";

export let options = {
    insecureSkipTLSVerify: true,
    stages: [
        {duration: "10s", target: 100},
        {duration: "60s", target: 200},
    ]
};
export default function() {

    const host = "https://devapi.onegames.online/api/auth/";


    let payload = JSON.stringify({
        "msisdn":"01770069013",
        "clientid": "K931D6T7LN",
        "clientsecret": "31FBH64L8EHG201EKLXI25VWVNLP29R7"
    });

    const params = {
        headers: {
        'Content-Type': 'application/json',
        },
    };

    // Request to Register
    let url = host + "gp/sso/login";
    let registrationResponse = http.post(url, payload, params);

 
    check(registrationResponse, {
        "is status 200": (r) => r.status === 200
    });
    sleep(1);

}