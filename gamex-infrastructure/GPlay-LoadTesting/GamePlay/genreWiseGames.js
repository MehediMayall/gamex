import http  from "k6/http";
import { check, sleep } from "k6";

export let options = {
    insecureSkipTLSVerify: true,
    stages: [
        {duration: "10s", target: 200},
        {duration: "60s", target: 500},
    ]
};
export default function() {

    let randomPageNumber = Math.floor(Math.random() * 3);
    const url = "https://devapi.onegames.online/api/play/games/genre/byname/arcade/"+ randomPageNumber +"/10";


    const params = {
        headers: {
        'Content-Type': 'application/json',
        },
    };

    let res = http.get(url, params);

    if(res.json().data.games.length != 10) 
        console.log("Requested 10 games but got", res.json().data.games.length);

    check(res, {
        "is status 200": (r) => r.status === 200
    });
    sleep(1);
};