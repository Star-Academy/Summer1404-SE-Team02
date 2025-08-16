import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    vus: 20,
    duration: '1m',
};

const BASE_URL = 'http://localhost:5178/api/search';

export default function () {
    const query = 'get';
    const res = http.get(`${BASE_URL}?query=${query}`);

    check(res, {
        'status is OK': (r) => r.status === 200,
    });

    sleep(0.3);
}
