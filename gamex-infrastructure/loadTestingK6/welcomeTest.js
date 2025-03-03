import { check, sleep } from "k6";
import http from "k6/http";

// export let options = {
//   insecureSkipTLSVerify: true,
//   vus: 10,
//   duration: "1m"
// };
export let options = {
  stages: [
    { duration: "20s", target: 20 },
    { duration: "20s", target: 100 },
    { duration: "20s", target: 300 },
    { duration: "40s", target: 500 },
    { duration: "20s", target: 50 },
    { duration: "20s", target: 1 },
  ],
  insecureSkipTLSVerify: true,
};


export default function() {

  let res = http.get("https://devgplayapi.gakktechnology.com/api/auth/welcome");
  check(res, {
    "is status 200": (r) => r.status === 200
  });

  sleep(1);
};