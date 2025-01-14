export class User {
    constructor(
        public username: string,
        private _accessToken: string,
        private _refreshToken: string,
        private _expiresIn: Date
    ) { }
    get token() {
        if (!this._expiresIn || this._expiresIn < new Date()) {
            return null;
        }
        return this._accessToken;
    }
}