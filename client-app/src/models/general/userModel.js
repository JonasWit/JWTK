export class UserModel {
    constructor(email, role, claims) {
        this.email = email;
        this.role = role;
        this.claims = claims;
    }
}

export class ClaimModel {
    constructor(claimType, claimValue) {
        this.claimType = claimType;
        this.claimValue = claimValue;
    }
}

export class AccessKeyModel {
    constructor(claimType, email) {
        this.claimType = claimType;
        this.email = email;
    }
}
