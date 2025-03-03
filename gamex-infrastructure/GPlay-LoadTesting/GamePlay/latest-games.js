import http  from "k6/http";
import { check, sleep } from "k6";

export let options = {
    insecureSkipTLSVerify: true,
    stages: [
        {duration: "10s", target: 500},
        {duration: "600s", target: 500},
    ]
};
export default function() {

    let randomPageNumber = Math.floor(Math.random() * 60);
    const url = "https://devapi.onegames.online/api/play/games/by/section/latest-games/"+ randomPageNumber +"/10";


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