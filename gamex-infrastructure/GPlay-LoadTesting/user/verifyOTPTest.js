import http  from "k6/http";
import { check, sleep } from "k6";

export let options = {
    insecureSkipTLSVerify: true,
    stages: [
        {duration: "60s", target: 200},
        // {duration: "60s", target: 700},
        {duration: "60s", target: 200},
        // {duration: "100s", target: 250},
        // {duration: "100s", target: 200},
        // {duration: "60s", target: 100},
        // {duration: "60s", target: 10},
        // {duration: "60s", target: 1},
        // {duration: "60s", target: 200},
        // {duration: "20s", target: 300},
        // {duration: "20s", target: 250},
        // {duration: "60s", target: 1},
    ]
};
export default function() {

    const host = "https://devgplayapi.gakktechnology.com/api/auth/";
    // const host = "http://localhost:5071/api/auth/";
    // const host = "http://localhost:5071/api/auth/";
    // const url = "http://localhost:3300/register";

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

    // Request 1: Request to Register
    let url = host + "register";
    let registrationResponse = http.post(url, payload, params);
    let data = registrationResponse.json("data");

    if (data == null || data.userId == null || data.userId == undefined) {
        console.log("Registration Failed");
        // console.log(registrationResponse);
        return;
    }

    // Request 2: Verify OTP
    payload = JSON.stringify({
            "userid": `${data.userId}`,
            "otp": "117908"
        });

    url = host + "verify/otp";
    let verifyOTP = http.post(url, payload, params);


    check(verifyOTP, {
        "is status 200": (r) => r.status === 200
    });
    sleep(1);

}