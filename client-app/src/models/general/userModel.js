export class UserModel {
    constructor(id, email, role, expire) {
        this.email = email;
        this.role = role;
        this.id = id;
        this.expire = expire;
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
