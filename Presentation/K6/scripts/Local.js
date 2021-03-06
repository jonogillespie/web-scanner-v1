import http from 'k6/http';
import {sleep, check, group} from 'k6';
import {Counter} from 'k6/metrics';

export const requests = new Counter('http_reqs')

export const options = {
    stages: [
        {target: 50, duration: '1m'}
    ],
    thresholds: {
        requests: ['count < 100']
    }
}

const BASE_URL = 'http://host.docker.internal:5000/api'

export default function () {
   group('get latest scan result', function() {
       const url = `${BASE_URL}/scan-cycles/latest`;
       const res = http.get(url);
       sleep(1);
       check(res, {
           'status is 200': resp => resp.status === 200
       })
       })
}