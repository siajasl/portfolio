import * as constants from '../../../utils/constants';

export const URL_POST_INITIATE='https://lon.netverify.com/api/v4/initiate';
export const URL_GET_STATUS='https://lon.netverify.com/api/netverify/v2/scans/';
export const URL_GET_SCANS='https://lon.netverify.com/api/netverify/v2/scans/';

export const HTTP_BASIC_AUTHENTICATION = {
    user: constants.JUMIO_API_TOKEN,
    pass: constants.JUMIO_API_SECRET,
    sendImmediately: true,
};

export const HTTP_HEADER_USER_AGENT = 'Trinkler Agora.Trade/1.0';

export const HTTP_HEADERS_JSON = {
    'Accept': 'application/json',
    'Content-Type': 'application/json',
    'User-Agent': HTTP_HEADER_USER_AGENT,
};

export const HTTP_HEADERS_IMG = {
    'Accept': 'image/jpeg',
    'Content-Type': 'image/jpeg',
    'User-Agent': HTTP_HEADER_USER_AGENT,
};

export const API_TOKEN_LIFETIME_IN_MINUTES = 4320;
