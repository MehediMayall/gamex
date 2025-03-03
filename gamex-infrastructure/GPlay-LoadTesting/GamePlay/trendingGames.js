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

    let randomPageNumber = 1 + Math.floor(Math.random() * 5);
    const url = "https://devapi.onegames.online/api/play/games/by/section/trending-games/"+ randomPageNumber +"/10";


    const params = {
        headers: {
        'Content-Type': 'application/json',
        },
    };

    let res = http.get(url, params);

     
    if(res.json().data != null && res.json().data.games.length != 10) 
    {
        console.log("Requested 10 games but got", res.json().data.games.length);
    }
    else if (res.json().data == null)
        console.log("Data is null", randomPageNumber);

    check(res, {
        "is status 200": (r) => r.status === 200
    });
    sleep(1);
};