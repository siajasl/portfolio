/**
 * @fileOverview Constants used across the library.
 */

// ------------------------------------------------------------
// GQL SETTINGS
// ------------------------------------------------------------

// GQL server port.
export const GQL_PORT = process.env.GQL_PORT;

// ------------------------------------------------------------
// JUMIO AUTHENTICATION
// ------------------------------------------------------------

// Jumio header api token
export const JUMIO_API_TOKEN = process.env.JUMIO_API_TOKEN;

// Jumio header api secret
export const JUMIO_API_SECRET = process.env.JUMIO_API_SECRET;

// ------------------------------------------------------------
// EMAIL SERVER SETTINGS
// ------------------------------------------------------------

// Mailgun api key.
export const MAILGUN_API_KEY = process.env.MAILGUN_API_KEY;

// Mailgun domain name.
export const MAILGUN_DOMAIN = process.env.MAILGUN_DOMAIN;

// ------------------------------------------------------------
// MONGO DB SERVER SETTINGS
// ------------------------------------------------------------

// dB: EXC: name.
export const MONGODB_EXC_NAME = process.env.MONGODB_EXC_NAME;

// dB: EXC: connection url for queries.
export const MONGODB_EXC_URL_READONLY = process.env.MONGODB_EXC_URL_READONLY;

// dB: EXC: onnection url for mutations.
export const MONGODB_EXC_URL_READWRITE = process.env.MONGODB_EXC_URL_READWRITE;

// dB: KYC: name.
export const MONGODB_KYC_NAME = process.env.MONGODB_KYC_NAME;

// dB: KYC: connection url for queries.
export const MONGODB_KYC_URL_READONLY = process.env.MONGODB_KYC_URL_READONLY;

// dB: KYC: connection url for mutations.
export const MONGODB_KYC_URL_READWRITE = process.env.MONGODB_KYC_URL_READWRITE;

// ------------------------------------------------------------
// OneSignal Web Push Notifications.
// ------------------------------------------------------------

// ONESIGNAL: userAuthKey
export const ONESIGNAL_USERAUTHKEY = process.env.ONESIGNAL_USERAUTHKEY || '';

// ONESIGNAL: appAuthKey
export const ONESIGNAL_APPAUTHKEY = process.env.ONESIGNAL_APPAUTHKEY || '';

// ONESIGNAL: appId
export const ONESIGNAL_APPID = process.env.ONESIGNAL_APPID || '';

// ------------------------------------------------------------
// SERVER SSL SETTINGS
// ------------------------------------------------------------

// Host certificate file path.
export const SSL_CERT_FPATH = process.env.SSL_CERT_FPATH;

// Host certificate private key file path.
export const SSL_KEY_FPATH = process.env.SSL_KEY_FPATH;

// ------------------------------------------------------------
// TRADE ENGINE
// ------------------------------------------------------------

// Interval after which asset price will be update.
export const ASSET_PRICE_UPDATE_INTERVAL = process.env.TRADE_ENGINE_ASSET_PRICE_UPDATE_INTERVAL;

// Transaction fee to be applied to market makers.
export const MAKER_FEE = process.env.TRADE_ENGINE_MAKER_FEE;

// Order threshold in USD.
export const ORDER_THRESHOLD_USD = process.env.TRADE_ENGINE_ORDER_THRESHOLD_USD;

// Transaction fee to be applied to market takers.
export const TAKER_FEE = process.env.TRADE_ENGINE_TAKER_FEE;

// Interval after which a trade is considered to have timed out.
export const TRADE_TIMEOUT = process.env.TRADE_ENGINE_TRADE_TIMEOUT;

// ------------------------------------------------------------
// DLT
// ------------------------------------------------------------

// Type of DLT networks to listen to.
export const DLT_NETWORK_TYPE = process.env.TRADE_ENGINE_DLT_NETWORK_TYPE || 'Prod';
