class UserModel {
    constructor(id, email, role, claims, accessKeys) {
        this.id = id;
        this.email = email;
        this.role = role;
        this.claims = claims;
        this.accessKeys = accessKeys;
    }
}

class ClaimModel {
    constructor(claimType, claimValue) {
        this.claimType = claimType;
        this.claimValue = claimValue;
    }
}

class AccessKeyModel {
    constructor(claimType, email) {
        this.claimType = claimType;
        this.email = email;
    }
}